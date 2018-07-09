using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDPListenerNS;
using StorableFormState;
using System.Xml.Serialization;
using SerializationNS;
using System.Runtime.Serialization;
using GPSReaderNS;
using System.IO;
using System.Diagnostics;
using AutoUpdaterDotNET;
using System.Reflection;

namespace tnxqsoClient
{
    public partial class FMain : FormWStorableState<DXpConfig>
    {
        static readonly string AutoUpdaterURI = "http://tnxqso.com/static/files/qsoclient.xml";
        UDPListener udpListener = new UDPListener();
        QSOFactory qsoFactory;
        HTTPService http;
        GPSReader gpsReader = new GPSReader();
        BindingList<QSO> blQSO = new BindingList<QSO>();
        Dictionary<string, BindingList<QSO>> qsoIndex = new Dictionary<string, BindingList<QSO>>();
        BindingSource bsQSO;
        string qsoFilePath = Application.StartupPath + "\\qso.dat";

        public FMain()
        {
#if DEBUG
            TextWriterTraceListener[] listeners = new TextWriterTraceListener[] {
            new TextWriterTraceListener("debug.log"),
            new TextWriterTraceListener(Console.Out)};
            Debug.Listeners.AddRange(listeners);
            Trace.Listeners.AddRange(listeners);
            Trace.AutoFlush = true;
            Debug.AutoFlush = true;
#endif
            config = new XmlConfigNS.XmlConfig<DXpConfig>();
            config.data.initialize();
            qsoFactory = new QSOFactory( config.data );
            InitializeComponent();
            udpListener.DataReceived += UDPDataReceived;
            udpListener.StartListener(12060);

            bsQSO = new BindingSource(blQSO, null);
            dgvQSO.AutoGenerateColumns = false;
            dgvQSO.DataSource = bsQSO;
            for (int c = 0; c < DXpConfig.UserColumnsCount; c++)
            {
                UserColumnSettings cs = config.data.userColumns != null && config.data.userColumns.Count > c ? config.data.userColumns[c] : null;
                DataGridViewColumn col = new DataGridViewTextBoxColumn();
                col.Name = "userFieldColumn" + c.ToString();
                col.DataPropertyName = "userField" + c.ToString();
                col.Visible = cs != null ? cs.show : false;
                col.HeaderText = cs != null ? cs.name : "";
                dgvQSO.Columns.Add(col);
            }
            if (config.data.dgvQSOColumnsWidth != null)
                for (int co = 0; co < dgvQSO.ColumnCount && co < config.data.dgvQSOColumnsWidth.Count(); co++)
                    dgvQSO.Columns[co].Width = config.data.dgvQSOColumnsWidth[co];
            else
                config.data.dgvQSOColumnsWidth = new List<int>();
            if (config.data.dgvQSOColumnsWidth.Count < dgvQSO.ColumnCount)
            {
                for (int co = config.data.dgvQSOColumnsWidth.Count; co < dgvQSO.ColumnCount; co++)
                    config.data.dgvQSOColumnsWidth.Add(dgvQSO.Columns[co].Width);
                writeConfig();
            }

            bool qsoEr;
            List<QSO> storedQSOs = ProtoBufSerialization.ReadListItems<QSO>(qsoFilePath, out qsoEr);
            if (storedQSOs.Count > 0)
            {
                foreach (QSO qso in storedQSOs)
                {
                    if (qso.freq.IndexOf('.') == -1)
                    {
                        qso.freq = QSO.formatFreq(qso.freq);
                        qsoEr = true;
                    }
                    blQSO.Insert(0, qso);
                    updateQsoIndex(qso);
                }
                QSO lastQSO = storedQSOs.Last();
                if (lastQSO.rda == config.data.getOptionalColumnValue("RDA"))
                    qsoFactory.no = lastQSO.no + 1;
            }
            if (qsoEr)
                ProtoBufSerialization.WriteList(qsoFilePath, storedQSOs, false);
            config.data.optionalColumnValueChanged += optionalColumnValueChanged;
            gpsReader.locationChanged += locationChanged;
            config.data.logIO += onLoggedIO;
            onLoggedIO(null, null);
            startGPSReader();
            http = new HTTPService( gpsReader, config.data);
            http.connectionStateChanged += onHTTPConnection;

        }

        public void updateUserColumn( int c)
        {
            UserColumnSettings cs = config.data.userColumns != null && config.data.userColumns.Count > c ? config.data.userColumns[c] : null;
            DataGridViewColumn col = dgvQSO.Columns["userFieldColumn" + c.ToString()];
            col.Visible = cs != null ? cs.show : false;
            col.HeaderText = cs != null ? cs.name : "";
        }

        private void onLoggedIO(object sender, EventArgs e)
        {
            DoInvoke(() => {
                if (config.data.token == null)
                {
                    miLogin.Text = "Login";
                    slLoggedIn.Text = "Not logged in";
                    slLoggedIn.ForeColor = Color.Red;
                } else
                {
                    miLogin.Text = "Logout";
                    slLoggedIn.Text = "Logged in as " + config.data.callsign;
                    slLoggedIn.ForeColor = Color.Green;
                }
            });
            config.write();
        }            

        private void updateQsoIndex( QSO qso )
        {
            if (!qsoIndex.ContainsKey(qso.cs))
                qsoIndex[qso.cs] = new BindingList<QSO>();
            qsoIndex[qso.cs].Insert(0, qso);
        }

        private void onHTTPConnection(object sender, EventArgs e)
        {
            DoInvoke(() =>
           {
               if (http.connected)
               {
                   slConnection.Text = "Connected";
                   slConnection.ForeColor = Color.Green;
               } 
               else
               {
                   slConnection.Text = "No connection";
                   slConnection.ForeColor = Color.Red;
               }
           });

        }

        private void startGPSReader()
        {
            if (config.data.gpsReaderWirelessGW)
                gpsReader.listenWirelessGW();
            else
            {
                List<SerialDeviceInfo> ports = GPSReader.listSerialDevices();
                SerialDeviceInfo port = ports.FirstOrDefault(x => x.deviceID == config.data.gpsReaderDeviceID);
                if (port != null)
                {
                    string portName = port.portName;
                    gpsReader.listenPort(portName);
                }
            }
            //gpsReader.debugCoords(new double[] { 47.2738544, 39.715804483333336 });
        }

        private void locationChanged( object sender, EventArgs e )
        {
            config.data.setOptionalColumnValue( "Locator", DXpConfig.qth(gpsReader.coords) );
            config.write();
            slCoords.Text = gpsReader.coords.ToString();
            slLoc.Text = config.data.optionalColumns["Locator"].value;
            if (config.data.optionalColumns["RAFA"].value != null)
                slLoc.Text += " RAFA " + config.data.optionalColumns["RAFA"].value;
        }

        private void optionalColumnValueChanged( object sender, OptionalColumnValueChangedEventArgs e)
        {
            if (e.column == "RAFA")
            {
                showBalloon("New RAFA " + config.data.optionalColumns["RAFA"].value);
                config.write();
            }
        }

        private void showBalloon( string text )
        {
            DoInvoke(() =>
           {
               NotifyIcon notifyIcon = new NotifyIcon();
               notifyIcon.Visible = true;
               notifyIcon.Icon = SystemIcons.Information;
               notifyIcon.BalloonTipTitle = "DXpedition";
               notifyIcon.BalloonTipText = text;
               notifyIcon.ShowBalloonTip(0);
               notifyIcon.BalloonTipClosed += delegate (object sender, EventArgs e)
               {
                   notifyIcon.Dispose();
               };
           });
        }

        private async void UDPDataReceived(object sender, DataReceivedArgs e)
        {
            string data = Encoding.UTF8.GetString(e.data);
            System.Diagnostics.Debug.WriteLine(data);
            QSO qso = qsoFactory.create(data);
            if (qso == null)
                return;
            //System.Diagnostics.Debug.WriteLine(qso.toJSON());
            dgvQSOInsert(qso);
            ProtoBufSerialization.Write<QSO>(qsoFilePath, qso, true);
            await http.postQso(qso);
        }

        private void dgvQSOInsert( QSO qso )
        {
            DoInvoke(() => {
                blQSO.Insert(0, qso);
                updateQsoIndex(qso);
                DataGridViewRow r = dgvQSO.Rows[0];
                setRowColors(r, Color.White, Color.SteelBlue);
                Task.Run( async () => 
                {
                    await Task.Delay(5000);
                    DoInvoke(() => 
                    {
                        setRowColors(r, dgvQSO.DefaultCellStyle.ForeColor, dgvQSO.DefaultCellStyle.BackColor);
                        dgvQSO.Refresh();
                    });
                });
                dgvQSO.FirstDisplayedScrollingRowIndex = 0;
                dgvQSO.Refresh();
            });
        }

        private void setRowColors( DataGridViewRow r, Color f, Color b)
        {
            for (int co = 0; co < dgvQSO.ColumnCount; co++)
            {
                r.Cells[co].Style.BackColor = b;
                r.Cells[co].Style.ForeColor = f;
            }
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpListener.DataReceived -= UDPDataReceived;
            udpListener.StopListener();
            Application.DoEvents();
        }

        private void DgvQSO_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            config.data.dgvQSOColumnsWidth[e.Column.Index] = e.Column.Width;
            config.write();
        }

        private async void miSettings_Click(object sender, EventArgs e)
        {
            FSettings fs = new FSettings( config.data);
            if ( fs.ShowDialog(this) == DialogResult.OK )
            {
                fs.blColumns.Where( x => !x.isUser).ToList().ForEach(c =>
               {
                   config.data.setOptionalColumnValue(c.name, c.value);
                   config.data.optionalColumns[c.name].show = c.show;
                   dgvQSO.Columns[c.name].Visible = c.show;
               });
                config.data.userColumns.Clear();
                config.data.userColumns.AddRange(
                    fs.blColumns
                        .Where(x => x.isUser)
                        .Select( x => new UserColumnSettings {
                            _name = x.name,
                            _show = x.show,
                            _value = x.value
                        })
                );
                for (int c = 0; c < DXpConfig.UserColumnsCount; c++)
                    updateUserColumn(c);
                if (config.data.gpsReaderDeviceID != fs.gpsReaderDeviceID || config.data.gpsReaderWirelessGW != fs.gpsReaderWirelessGW)
                {
                    config.data.gpsReaderWirelessGW = fs.gpsReaderWirelessGW;
                    config.data.gpsReaderDeviceID = fs.gpsReaderDeviceID;
                    startGPSReader();
                }
                config.write();
                await http.postUserColumns();
            }
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start(AutoUpdaterURI);
            if (blQSO.Count > 0)
            {
                dgvQSO.FirstDisplayedScrollingRowIndex = 0;
                dgvQSO.Refresh();
            }
            foreach (KeyValuePair<string, OptionalColumnSettings> kv in config.data.optionalColumns)
                dgvQSO.Columns[kv.Key].Visible = kv.Value.show;
            if (config.data.token == null)
                showLoginForm();
            Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void tbCSFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbCSFilter.Text != "")
                tbCSFilter.Text = tbCSFilter.Text.ToUpper();
            if (miFilter.Checked && tbCSFilter.Text != "")
            {
                if (qsoIndex.ContainsKey(tbCSFilter.Text))
                    bsQSO.DataSource = qsoIndex[tbCSFilter.Text];
                else
                    MessageBox.Show("Callsign not found!");
            }
            else if (!miFilter.Checked)
                bsQSO.DataSource = blQSO;
            else
                miFilter.Checked = false;
            miFilter.BackColor = miFilter.Checked ? SystemColors.MenuHighlight : DefaultBackColor;
        }

        private void miLogin_Click(object sender, EventArgs e)
        {
            if (config.data.token == null)
            {
                showLoginForm();
            } else
            {
                config.data.token = null;
            }
        }

        private void showLoginForm()
        {
            new FLogin(config.data, http).ShowDialog();
        }

        private void miStatsRDA_Click(object sender, EventArgs e)
        {
            new FStats(blQSO.ToList(), "RDA").Show();
        }

        private void miStatsRAFA_Click(object sender, EventArgs e)
        {
            new FStats(blQSO.ToList(), "RAFA").Show();
        }


        private void miExportRDA_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                blQSO
                    .GroupBy(x => x.rda)
                    .ToList()
                    .ForEach(l =>
                        writeADIF(Path.Combine(folderBrowserDialog.SelectedPath, l.First().rda + ".adi"), l.ToList(), new Dictionary<string, string>()));
            }
        }

        private void miExportRAFA_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, List<QSO>> data = new Dictionary<string, List<QSO>>();
                blQSO
                    .Where(qso => qso.rafa != null).ToList()
                    .ForEach(qso =>
                    {
                        string[] rafas = qso.rafa.Split(new string[] { ", " }, StringSplitOptions.None);
                        foreach (string rafa in rafas)
                        {
                            if (!data.ContainsKey(rafa))
                                data[rafa] = new List<QSO>();
                            data[rafa].Add(qso);
                        }
                    });
                data.Keys.ToList().ForEach(val =>
                {
                    writeADIF(Path.Combine(folderBrowserDialog.SelectedPath, val + ".adi"), data[val], new Dictionary<string, string>() { { "RAFA", val } });
                });
            }
        }

        private void writeADIF(string fileName, List<QSO> data, Dictionary<string, string> adifParams)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    /*
                    sw.WriteLine("Nr;My Call;Ur Call;Ur RST;My RST;Freq;Mode;RDA;RAFA;Loc;WFF");
                    foreach (QSO qso in blQSO)
                        sw.WriteLine(qso.no.ToString() + ";" + qso.myCS + ";" + qso.cs + ";" + qso.rcv + ";" + qso.snt + ";" + qso.freq + ";" + qso.mode + ";" + qso.rda + ";" + qso.rafa + ";" +
                            qso.loc + ";" + qso.wff);
                            */
                    DateTime ts = DateTime.UtcNow;
                    sw.WriteLine("ADIF Export from DxpClient");
                    sw.WriteLine("Logs generated @ {0:yyyy-MM-dd HH:mm:ssZ}", ts);
                    sw.WriteLine("<EOH>");
                    foreach (QSO qso in data)
                        sw.WriteLine(qso.adif(adifParams));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show("Can not export to text file: " + ex.ToString(), "DXpedition", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }

    public class OptionalColumnSettings
    {
        internal bool _show;
        public bool show { get { return _show; } set { _show = value; } }
        internal string _value;
        public string value { get { return _value; } set { _value = value; } }
    }

    public class UserColumnSettings : OptionalColumnSettings
    {
        internal string _name;
        public string name { get { return _name; } set { _name = value; } }
    }

    public class OptionalColumnValueChangedEventArgs : EventArgs
    {
        public string column;
        public string value;
    }


    [DataContract]
    public class DXpConfig : StorableFormConfig
    {
        static readonly string[] OptionalColumnsList = new string[] { "RDA", "RAFA", "WFF", "Locator" };
        public static readonly int UserColumnsCount = 2;

        [XmlIgnore]
        string _gpsReaderDeviceID;
        public string gpsReaderDeviceID {
            get { return _gpsReaderDeviceID; }
            set { _gpsReaderDeviceID = value; }
        }
        public bool gpsReaderWirelessGW;

        [DataMember]
        public SerializableDictionary<string, OptionalColumnSettings> optionalColumns = new SerializableDictionary<string, OptionalColumnSettings>();
        [DataMember]
        public List<UserColumnSettings> userColumns = new List<UserColumnSettings>();

        [XmlIgnore]
        Dictionary<string, string> rafaData = new Dictionary<string, string>();
        [XmlIgnore]
        public EventHandler<OptionalColumnValueChangedEventArgs> optionalColumnValueChanged;
        [XmlIgnore]
        public EventHandler<EventArgs> logIO;

        public void setOptionalColumnValue( string column, string value )
        {
            if (optionalColumns[column].value != value)
            {
                optionalColumns[column].value = value;
                if ( column == "Locator")
                {
                    string newRafa = rafaData.ContainsKey(value) ? rafaData[value] : null;
                    setOptionalColumnValue("RAFA", newRafa);
                }
                optionalColumnValueChanged?.Invoke(this, new OptionalColumnValueChangedEventArgs
                {
                    column = column,
                    value = value
                });
            }

        }

        public string getOptionalColumnValue( string column )
        {
            if (optionalColumns != null && optionalColumns.ContainsKey(column) && optionalColumns[column] != null)
                return optionalColumns[column].value;
            else
                return null;
        }

        public List<int> dgvQSOColumnsWidth;

        [DataMember]
        public string callsign;
        [DataMember]
        public string password;
        [XmlIgnore]
        private string _token;
        [DataMember]
        public string token { get { return _token; }
            set {
                if (_token != value)
                {
                    _token = value;
                    logIO?.Invoke(this, new EventArgs());
                }
            } }


        public DXpConfig() : base() {
            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\rafa.csv"))
                {
                    do
                    {
                        string line = sr.ReadLine();
                        string[] lineData = line.Split(';');
                        if ( lineData[0] ==  "" ) 
                        {
                            string[] keys = lineData[3].Split(',');
                            foreach (string key in keys)
                            {
                                string entry = lineData[1];
                                if (rafaData.ContainsKey(key))
                                    rafaData[key] += ", " + entry;
                                else
                                    rafaData[key] = entry;
                            }
                        }
                    } while (sr.Peek() >= 0);
                    System.Diagnostics.Debug.WriteLine(rafaData["KN97TF"]);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                MessageBox.Show("Rafa data could not be loaded: " + e.ToString(), "DXpedition", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void initialize()
        {
            foreach (string c in OptionalColumnsList)
                if (!optionalColumns.ContainsKey(c))
                    optionalColumns[c] = new OptionalColumnSettings
                    {
                        _show = true,
                        _value = null
                    };

        }

        public string toJSON()
        {
            return JSONSerializer.Serialize<DXpConfig>(this);
        }

        

        public static string qth(Coords c)
        {
            double lat = c.lat;
            double lng = c.lng;
            string qth = "";
            lat += 90;
            lng += 180;
            lat = lat / 10 + 0.0000001;
            lng = lng / 20 + 0.0000001;
            qth += (char)(65 + lng);
            qth += (char)(65 + lat);
            lat = 10 * (lat - Math.Truncate(lat));
            lng = 10 * (lng - Math.Truncate(lng));
            qth += (char)(48 + lng);
            qth += (char)(48 + lat);
            lat = 24 * (lat - Math.Truncate(lat));
            lng = 24 * (lng - Math.Truncate(lng));
            qth += (char)(65 + lng);
            qth += (char)(65 + lat);
            lat = 10 * (lat - Math.Truncate(lat));
            lng = 10 * (lng - Math.Truncate(lng));
            /*            qth += (char)(48 + lng) + (char)(48 + lat);
                        lat = 24 * (lat - Math.Truncate(lat));
                        lng = 24 * (lng - Math.Truncate(lng));
                        qth += (char)(65 + lng) + (char)(65 + lat);*/
            System.Diagnostics.Debug.WriteLine(qth);
            return qth;
        } // returnQth()


    }
}
