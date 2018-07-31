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

        public class ColumnSettingsEntry : UserColumnSettings
        {
            public Boolean isUser = false;
        }

        public string gpsReaderDeviceID {  get { return cbGPSPort.SelectedIndex == -1 ? null : serialPorts[cbGPSPort.SelectedIndex].deviceID; } }
        public bool gpsReaderWirelessGW { get { return rbGPSWirelessGW.Checked; } }
        public bool gpsServerLoad { get { return rbGPSServerLoad.Checked; } }

        List<SerialDeviceInfo> serialPorts = GPSReader.listSerialDevices();

        public BindingList<ColumnSettingsEntry> blColumns = new BindingList<ColumnSettingsEntry>();
        BindingSource bsColumns;

        public FSettings( DXpConfig data )
        {
            InitializeComponent();

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
            rbGPSServerLoad.Checked = data.gpsServerLoad;
            cbGPSPort.Enabled = !data.gpsReaderWirelessGW;

            bsColumns = new BindingSource(blColumns, null);
            dgvColumns.AutoGenerateColumns = false;
            dgvColumns.DataSource = bsColumns;

            foreach (KeyValuePair<string, OptionalColumnSettings> kv in data.optionalColumns)
                blColumns.Add(new ColumnSettingsEntry
                {
                    _name = kv.Key,
                    _show = kv.Value.show,
                    _value = kv.Value.value
                });

            foreach (UserColumnSettings c in data.userColumns)
                blColumns.Add(new ColumnSettingsEntry
                {
                    _name = c.name,
                    _show = c.show,
                    _value = c.value,
                    isUser = true
                });

            for ( int c = data.userColumns.Count; c < DXpConfig.UserColumnsCount; c++)
                blColumns.Add(new ColumnSettingsEntry
                {
                    _name = "User field #" + (c + 1).ToString(),
                    _show = false,
                    _value = null,
                    isUser = true
                });

            dgvColumns.Height = dgvColumns.ColumnHeadersHeight + dgvColumns.Rows.Cast<DataGridViewRow>().Sum(x => x.Height) + 8;

            dgvColumns.Refresh();

        }

        private void rbGPSSource_Click(object sender, EventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            foreach (RadioButton rb in new RadioButton[] { rbGPSSerial, rbGPSWirelessGW })
                rb.Checked = rb == s;
            cbGPSPort.Enabled = rbGPSSerial.Checked;
        }

        private void dgvColumns_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int c = 0; c < blColumns.Count; c++)
                if (!blColumns[c].isUser)
                    {
                        DataGridViewCell cell = dgvColumns.Rows[c].Cells["name"];
                        cell.ReadOnly = true;
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = SystemColors.Control;
                        style.ForeColor = SystemColors.ControlText;
                        cell.Style = style;
                    }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
