using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDPListenerNS;

namespace dxpClient
{
    public partial class FMain : Form
    {
        private UDPListener udpListener = new UDPListener();
        private QSOFactory qsoFactory = new QSOFactory();

        public FMain()
        {
            InitializeComponent();
            udpListener.DataReceived += UDPDataReceived;
            udpListener.StartListener(12060);
        }

        private void UDPDataReceived(object sender, DataReceivedArgs e)
        {
            string data = Encoding.UTF8.GetString(e.data);
            System.Diagnostics.Debug.WriteLine(data);
            System.Diagnostics.Debug.WriteLine(qsoFactory.create(data).toJSON());
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpListener.DataReceived -= UDPDataReceived;
            udpListener.StopListener();
            Application.DoEvents();
        }
    }
}
