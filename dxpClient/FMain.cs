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

namespace dxpClient
{
    public partial class FMain : Form
    {
        UDPListener udpListener = new UDPListener();
        QSOFactory qsoFactory = new QSOFactory();
        HTTPService http = new HTTPService("http://73.ru/dxped/uwsgi/qso");

        public FMain()
        {
            InitializeComponent();
            udpListener.DataReceived += UDPDataReceived;
            udpListener.StartListener(12060);
        }

        private async void UDPDataReceived(object sender, DataReceivedArgs e)
        {
            string data = Encoding.UTF8.GetString(e.data);
            System.Diagnostics.Debug.WriteLine(data);
            QSO qso = qsoFactory.create(data);
            System.Diagnostics.Debug.WriteLine(qso.toJSON());
            HttpResponseMessage response = await http.postQso(qso);
            System.Diagnostics.Debug.WriteLine(response.ToString());
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpListener.DataReceived -= UDPDataReceived;
            udpListener.StopListener();
            Application.DoEvents();
        }
    }
}
