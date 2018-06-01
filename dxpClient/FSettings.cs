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

        

        public string gpsReaderDeviceID {  get { return cbGPSPort.SelectedIndex == -1 ? null : serialPorts[cbGPSPort.SelectedIndex].deviceID; } }
        public bool gpsReaderWirelessGW { get { return rbGPSWirelessGW.Checked; } }

        List<SerialDeviceInfo> serialPorts = GPSReader.listSerialDevices();

        public BindingList<UserColumnSettings> blOptionalColumns = new BindingList<UserColumnSettings>();
        BindingSource bsOptionalColumns;

        public BindingList<UserColumnSettings> blUserColumns = new BindingList<UserColumnSettings>();
        BindingSource bsUserColumns;

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
            cbGPSPort.Enabled = !data.gpsReaderWirelessGW;

            bsOptionalColumns = new BindingSource(blOptionalColumns, null);
            dgvOptionalColumns.AutoGenerateColumns = false;
            dgvOptionalColumns.DataSource = bsOptionalColumns;

            foreach (KeyValuePair<string, OptionalColumnSettings> kv in data.optionalColumns)
                blOptionalColumns.Add(new UserColumnSettings
                {
                    _name = kv.Key,
                    _show = kv.Value.show,
                    _value = kv.Value.value
                });

            dgvOptionalColumns.Refresh();

            bsUserColumns = new BindingSource(blUserColumns, null);
            dgvUserColumns.AutoGenerateColumns = false;
            dgvUserColumns.DataSource = bsUserColumns;

            foreach (UserColumnSettings c in data.userColumns)
                blUserColumns.Add(new UserColumnSettings
                {
                    _name = c.name,
                    _show = c.show,
                    _value = c.value
                });
            updateAllowAddUserColumns();

            dgvUserColumns.Refresh();

        }

        private void updateAllowAddUserColumns()
        {
            dgvUserColumns.AllowUserToAddRows = blUserColumns.Count < DXpConfig.UserColumnsCount;
        }

        private void rbGPSSource_Click(object sender, EventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            foreach (RadioButton rb in new RadioButton[] { rbGPSSerial, rbGPSWirelessGW })
                rb.Checked = rb == s;
            cbGPSPort.Enabled = rbGPSSerial.Checked;
        }

        private void dgvColumns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvUserColumns_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dgvUserColumns_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            updateAllowAddUserColumns();
        }

        private void dgvUserColumns_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            updateAllowAddUserColumns();
        }
    }
}
