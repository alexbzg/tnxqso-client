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

namespace dxpClient
{
    public partial class FSettings : Form
    {
        public string rda {  get { return tbRDA.Text; } }
        public string loc { get { return tbLoc.Text; } }
        public string wwf { get { return tbWWF.Text; } }
        public string gpsReaderDeviceID {  get { return serialPorts[cbGPSPort.SelectedIndex].deviceID; } }

        List<SerialDeviceInfo> serialPorts = GPSReader.listSerialDevices();

        public FSettings( DXpConfig data )
        {
            InitializeComponent();
            tbRDA.Text = data.rda;
            tbLoc.Text = data.loc;
            tbWWF.Text = data.wwf;

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
            
        }
    }
}
