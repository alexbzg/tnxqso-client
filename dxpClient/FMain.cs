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
using GpsReaderNS;


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
        GPSReader gpsReader = new GPSReader("COM5");


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
                config.data.wwf = fs.wwf;
                config.write();
            }
        }
    }

    [DataContract]
    public class DXpConfig : StorableFormConfig
    {
        [XmlIgnoreAttribute]
        private string _rda;
        [XmlIgnoreAttribute]
        private string _loc;
        [XmlIgnoreAttribute]
        public EventHandler<EventArgs> rdaChanged;

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
                }
            }
        }
        [DataMember]
        public string wwf;

        public DXpConfig() : base() { }

        public string toJSON()
        {
            return JSONSerializer.Serialize<DXpConfig>(this);
        }
    }
}
