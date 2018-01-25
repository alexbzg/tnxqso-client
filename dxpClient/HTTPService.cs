//#define DISABLE_HTTP
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlConfigNS;
using System.Windows.Forms;
using SerializationNS;
using GPSReaderNS;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace tnxqsoClient
{
    public class HTTPService
    {
        private static int pingInterval = 60 * 1000;
        HttpClient client = new HttpClient();
#if DEBUG
        private static readonly string srvURI = "http://test.tnxqso.com/aiohttp/";
#else
        private static readonly string srvURI = "http://tnxqso.com/aiohttp/";
#endif
        System.Threading.Timer pingTimer;
        ConcurrentQueue<QSO> qsoQueue = new ConcurrentQueue<QSO>();
        private string unsentFilePath = Application.StartupPath + "\\unsent.dat";
        private volatile bool _connected;
        public bool connected {  get { return _connected; } }
        public EventHandler<EventArgs> connectionStateChanged;
        private GPSReader gpsReader;
        private DXpConfig config;
        volatile LocationData locationData;


        public HTTPService( GPSReader _gpsReader, DXpConfig _config )
        {
            gpsReader = _gpsReader;
            config = _config;
            locationData = new LocationData(config, gpsReader);
            pingTimer = new System.Threading.Timer( obj => ping(), null, 5000, pingInterval);
            List<QSO> unsentQSOs = ProtoBufSerialization.Read<List<QSO>>(unsentFilePath);
            if (unsentQSOs != null && unsentQSOs.Count > 0)
                Task.Run( () =>
                {
                    foreach (QSO qso in unsentQSOs)
                        postQso(qso);
                    saveUnsent();
                });
        }

        private async Task<HttpContent> post(string _URI, JSONSerializable data)
        {
            return await post(_URI, data, true);
        }

        private async Task<HttpContent> post(string _URI, JSONSerializable data, bool warnings)
        {
            string sContent = data.serialize();
            System.Diagnostics.Debug.WriteLine(sContent);
            string URI = srvURI + _URI;
#if DEBUG && DISABLE_HTTP
            return true;
#endif
            HttpContent content = new StringContent(sContent);
            HttpResponseMessage response = null;
            bool result = false;
            try
            {
                response = await client.PostAsync(URI, content);
                result = response.IsSuccessStatusCode;
                System.Diagnostics.Debug.WriteLine(response.ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            if (_connected != result )
            {
                _connected = result;
                if (_connected)
                    await processQueue();
                connectionStateChanged?.Invoke(this, new EventArgs());
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string srvMsg = await response.Content.ReadAsStringAsync();
                if (srvMsg == "Login expired")
                {
                    if (config.callsign.Length > 3 && config.password.Length > 5 && await login(config.callsign, config.password))
                        return await post(_URI, data, warnings);
                }
                if (warnings)
                    MessageBox.Show(await response.Content.ReadAsStringAsync(), "Bad request to server");
                else
                    response.EnsureSuccessStatusCode();
            }
            return result ? response.Content : null;
        }


        public async Task postQso( QSO qso)
        {
            if (qsoQueue.IsEmpty && config.token != null)
            {
                HttpContent response = await post("log", qsoToken( qso));
                if (response != null)
                    addToQueue(qso);
            }
            else
                addToQueue(qso);
        }

        private void addToQueue( QSO qso)
        {
            qsoQueue.Enqueue(qso);
            saveUnsent();
        }

        private void saveUnsent()
        {
            ProtoBufSerialization.Write<List<QSO>>(unsentFilePath, qsoQueue.ToList());
        }

        private async Task processQueue()
        {
            while (!qsoQueue.IsEmpty && config.token != null)
            {
                qsoQueue.TryPeek(out QSO qso);
                HttpContent r = await post("log", qsoToken( qso ));
                if (r != null)
                {
                    qsoQueue.TryDequeue(out qso);
                    saveUnsent();
                }
                else
                    break;
            }
        }

        private static string stringJSONfield(string val)
        {
            return val == null || val == "" ? "null" : "\"" + val + "\"";
        }

        private static string JSONfield( string val )
        {
            return val == null || val == "" ? "null" : val;
        }

        public async Task ping()
        {
            if (config.token == null)
                return;
            HttpContent response  = await post("location", locationData );
            pingTimer.Change( response != null ? pingInterval : 1000, pingInterval);
        }

        public async Task<Boolean> login(string login, string password)
        {
            HttpContent response = await post("login", new LoginRequest() { login = login, password = password });
            if (response != null )
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LoginResponse));
                LoginResponse userData = (LoginResponse)serializer.ReadObject(await response.ReadAsStreamAsync());
                config.callsign = userData.callsign;
                config.password = password;
                config.token = userData.token;
                return true;
            }
            return false;
        }

        private QSOtoken qsoToken( QSO qso )
        {
            return new QSOtoken(config, qso);
        }
    }

    [DataContract]
    public class JSONSerializable
    {
        public string serialize()
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
                string output = string.Empty;

                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, this);
                    output = Encoding.UTF8.GetString(ms.ToArray());
                }
                return output;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return string.Empty;
        }

    }

    [DataContract]
    public class JSONToken : JSONSerializable
    {
        [IgnoreDataMember]
        internal DXpConfig config;
        [DataMember]
        internal string token { get { return config.token; }  set { } }

        public JSONToken( DXpConfig _config)
        {
            config = _config;
        }
    }

    [DataContract]
    public class LoginRequest : JSONSerializable
    {
        [DataMember]
        public string login;
        [DataMember]
        public string password;
    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public string token;
        [DataMember]
        public string callsign;
    }

    [DataContract]
    class QSOtoken : JSONToken
    {
        [DataMember]
        internal QSO qso;

        internal QSOtoken( DXpConfig _config, QSO _qso) : base(_config)
        {
            qso = _qso;
        }
    }

    [DataContract]
    class LocationData : JSONToken
    {
        [IgnoreDataMember]
        GPSReader gps;
        [DataMember]
        public string location { get { return gps?.coords?.toJSON(); } set { } }
        [DataMember]
        public string loc { get { return config.loc; } set { } }
        [DataMember]
        public string rafa { get { return config.rafa; } set { } }
        [DataMember]
        public string rda { get { return config.rda; } set { } }

        internal LocationData(DXpConfig _config, GPSReader _gps) : base(_config)
        {
            gps = _gps;
        }

    }

}
