using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPSReaderNS;

namespace tnxqsoClient
{
    public partial class FSettings : Form
    {
        public string rda {  get { return tbRDA.Text; } }
        public string wwf { get { return tbWWF.Text; } }
        public string gpsReaderDeviceID {  get { return cbGPSPort.SelectedIndex == -1 ? null : serialPorts[cbGPSPort.SelectedIndex].deviceID; } }
        public bool gpsReaderWirelessGW { get { return rbGPSWirelessGW.Checked; } }

        List<SerialDeviceInfo> serialPorts = GPSReader.listSerialDevices();

        public FSettings( DXpConfig data )
        {
            InitializeComponent();
            tbRDA.Text = data.rda;
            tbWWF.Text = data.wff;

            foreach (SerialDeviceInfo sp in serialPorts)
            {
                cbGPSPort.Items.Add(sp.caption);
                int w = TextRenderer.MeasureText(sp.caption, cbGPSPort.Font).Width;
                if (cbGPSPort.DropDownWidth < w)
                    cbGPSPort.DropDownWidth = w;
            }
            int portIdx = serialPorts.FindIndex(x => x.deviceID == data.gpsReaderDeviceID);
            if (portIdx != -1)
                cbGPSPort.SelectedIndex = portIdx;
            rbGPSSerial.Checked = !data.gpsReaderWirelessGW;
            rbGPSWirelessGW.Checked = data.gpsReaderWirelessGW;
            cbGPSPort.Enabled = !data.gpsReaderWirelessGW;
            
        }

        private void rbGPSSource_Click(object sender, EventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            foreach (RadioButton rb in new RadioButton[] { rbGPSSerial, rbGPSWirelessGW })
                rb.Checked = rb == s;
            cbGPSPort.Enabled = rbGPSSerial.Checked;
        }
    }
}
