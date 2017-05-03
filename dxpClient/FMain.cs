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

namespace dxpClient
{
    public partial class FMain : FormWStorableState<DXpConfig>
    {
        UDPListener udpListener = new UDPListener();
        QSOFactory qsoFactory = new QSOFactory();
        HTTPService http = new HTTPService("http://73.ru/dxped/uwsgi/qso");
        private BindingList<QSO> blQSO = new BindingList<QSO>();
        private BindingSource bsQSO;


        public FMain()
        {
            InitializeComponent();
            udpListener.DataReceived += UDPDataReceived;
            udpListener.StartListener(12060);
            bsQSO = new BindingSource(blQSO, null);
            dgvQSO.AutoGenerateColumns = false;
            dgvQSO.DataSource = bsQSO;

        }

        private async void UDPDataReceived(object sender, DataReceivedArgs e)
        {
            string data = Encoding.UTF8.GetString(e.data);
            System.Diagnostics.Debug.WriteLine(data);
            QSO qso = qsoFactory.create(data);
            System.Diagnostics.Debug.WriteLine(qso.toJSON());
            await http.postQso(qso);
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpListener.DataReceived -= UDPDataReceived;
            udpListener.StopListener();
            Application.DoEvents();
        }
    }

    public class DXpConfig : StorableFormConfig
    {

    }
}
