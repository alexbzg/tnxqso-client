using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;

namespace dxpClient
{
    [DataContract]
    public class QSO
    {
        internal string _ts;
        internal string _myCS;
        internal string _band;
        internal string _freq;
        internal string _mode;
        internal string _cs;
        internal string _snt;
        internal string _rcv;
        internal string _rda;
        internal string _wwf;
        internal int _no;

        [DataMember]
        public string ts { get { return _ts; } set { _ts = value; } }
        [DataMember]
        public string myCS { get { return _myCS; } set { _myCS = value; } }
        [DataMember]
        public string band { get { return _band; } set { _band = value; } }
        [DataMember]
        public string freq { get { return _freq; } set { _freq = value; } }
        [DataMember]
        public string mode { get { return _mode; } set { _mode = value; } }
        [DataMember]
        public string cs { get { return _cs; } set { _cs = value; } }
        [DataMember]
        public string snt { get { return _snt; } set { _snt = value; } }
        [DataMember]
        public string rcv { get { return _rcv; } set { _rcv = value; } }
        [DataMember]
        public string rda { get { return _rda; } set { _rda = value; } }
        [DataMember]
        public string wwf { get { return _wwf; } set { _wwf = value; } }
        [DataMember]
        public int no { get { return _no; } set { _no = value; } }

        public string toJSON()
        {
            var ser = new DataContractJsonSerializer(typeof(QSO));
            var output = string.Empty;

            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, this);
                output = Encoding.UTF8.GetString(ms.ToArray());
            }
            return output;
        }
    }

    public class QSOFactory
    {
        private DXpConfig settings;
        private int no = 1;



        public QSOFactory( DXpConfig _settings )
        {
            settings = _settings;
            settings.rdaChanged += rdaChanged; 
        }

        private void rdaChanged(object sender, EventArgs e)
        {
            no = 1;
        }

        public QSO create( string xml )
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;

            if (root.Name != "contactinfo")
                return null;

            return new QSO {
                _ts = root.SelectSingleNode("timestamp").InnerText,
                _myCS = root.SelectSingleNode("mycall").InnerText,
                _band = root.SelectSingleNode("band").InnerText,
                _freq = root.SelectSingleNode("txfreq").InnerText,
                _mode = root.SelectSingleNode("mode").InnerText,
                _cs = root.SelectSingleNode("call").InnerText,
                _snt = root.SelectSingleNode("snt").InnerText,
                _rcv = root.SelectSingleNode("rcv").InnerText,
                _no = no++,
                _rda = settings.rda,
                _wwf = settings.wwf
            };
        }
    }
}
