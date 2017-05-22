﻿using System;
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

namespace dxpClient
{
    public partial class FMain : FormWStorableState<DXpConfig>
    {
        UDPListener udpListener = new UDPListener();
        QSOFactory qsoFactory;
        HTTPService http;
        BindingList<QSO> blQSO = new BindingList<QSO>();
        BindingSource bsQSO;
        string qsoFilePath = Application.StartupPath + "\\qso.dat";


        public FMain()
        {
            config = new XmlConfigNS.XmlConfig<DXpConfig>();
            qsoFactory = new QSOFactory( config.data );
            http = new HTTPService("http://73.ru/dxped/uwsgi/qso", config.data);
            InitializeComponent();
            udpListener.DataReceived += UDPDataReceived;
            udpListener.StartListener(12060);
            bsQSO = new BindingSource(blQSO, null);
            dgvQSO.AutoGenerateColumns = false;
            dgvQSO.DataSource = bsQSO;
            if (config.data.dgvQSOColumnsWidth != null && config.data.dgvQSOColumnsWidth.Count() == dgvQSO.ColumnCount)
                for (var co = 0; co < dgvQSO.ColumnCount; co++)
                    dgvQSO.Columns[co].Width = config.data.dgvQSOColumnsWidth[co];
            else
            {
                config.data.dgvQSOColumnsWidth = new int[dgvQSO.ColumnCount];
                for (var co = 0; co < dgvQSO.ColumnCount; co++)
                    config.data.dgvQSOColumnsWidth[co] = dgvQSO.Columns[co].Width;
                config.write();
            }
            List<QSO> storedQSOs = ProtoBufSerialization.ReadList<QSO>(qsoFilePath);
            if (storedQSOs.Count > 0)
            {
                foreach (QSO qso in storedQSOs)
                    dgvQSOInsert(qso);
                QSO lastQSO = storedQSOs.Last();
                if (lastQSO.rda == config.data.rda)
                    qsoFactory.no = lastQSO.no + 1;
            }
            config.data.rafaChanged += rafaChanged;
            config.data.startGPSReader();
        }

        private void rafaChanged( object sender, EventArgs e)
        {
            showBalloon("New RAFA " + config.data.rafa);
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
                dgvQSO.Refresh();
            });
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

        private void miSettings_Click(object sender, EventArgs e)
        {
            FSettings fs = new FSettings( config.data);
            if ( fs.ShowDialog(this) == DialogResult.OK )
            {
                config.data.rda = fs.rda;
                config.data.loc = fs.loc;
                config.data.wff = fs.wwf;
                config.data.gpsReaderDeviceID = fs.gpsReaderDeviceID;
                config.write();
            }
        }

        
    }

    [DataContract]
    public class DXpConfig : StorableFormConfig
    {
        private static string portByDeviceID(string deviceID)
        {
            List<SerialDeviceInfo> ports = GPSReader.listSerialDevices();
            SerialDeviceInfo port = ports.FirstOrDefault(x => x.deviceID == deviceID);
            return port?.portName;
        }

        [XmlIgnoreAttribute]
        private string _rda;
        [XmlIgnoreAttribute]
        private string _loc;
        [XmlIgnoreAttribute]
        public EventHandler<EventArgs> rdaChanged;
        [XmlIgnoreAttribute]
        GPSReader gpsReader;
        [XmlIgnore]
        string _gpsReaderDeviceID;
        public string gpsReaderDeviceID {
            get { return _gpsReaderDeviceID; }
            set
            {
                if (value != _gpsReaderDeviceID)
                {
                    _gpsReaderDeviceID = value;
                    startGPSReader();
                }
            }
        }

        [XmlIgnore]
        Dictionary<string, string> rafaData = new Dictionary<string, string>();
        [XmlIgnore]
        string _rafa;
        [DataMember]
        public string rafa { get { return _rafa; } set { _rafa = value; } }
        [XmlIgnore]
        public EventHandler<EventArgs> rafaChanged;

        

        public int[] dgvQSOColumnsWidth;
        [DataMember]
        public string rda {
            get { return _rda; }
            set
            {
                if ( value != _rda )
                {
                    _rda = value;
                    rdaChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        [DataMember]
        public string loc
        {
            get { return _loc; }
            set
            {
                if (value != _loc)
                {
                    _loc = value;
                    string newRafa = rafaData.ContainsKey(_loc) ? rafaData[_loc] : null;
                    if ( newRafa != rafa)
                    {
                        rafa = newRafa;
                        rafaChanged?.Invoke(this, new EventArgs());
                    }
                }
            }
        }
        [DataMember]
        public string wff;

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
                                rafaData[key] = lineData[1];
                        }
                    } while (sr.Peek() >= 0);

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                MessageBox.Show("Rafa data could not be loaded: " + e.ToString(), "DXpedition", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string toJSON()
        {
            return JSONSerializer.Serialize<DXpConfig>(this);
        }

        public void startGPSReader()
        {
            string portName = portByDeviceID(_gpsReaderDeviceID);
            if (portName != null && portName != "")
            {
                gpsReader = new GPSReader(portName);
                gpsReader.locationChanged += locationChanged;
            }
        }

        private void locationChanged( object sender, EventArgs e)
        {
            loc = qth(gpsReader.dlat, gpsReader.dlong);
        }

        private string qth(double _lat, double _lng)
        {
            double lat = _lat;
            double lng = _lng;
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
            return qth;
        } // returnQth()


    }
}
