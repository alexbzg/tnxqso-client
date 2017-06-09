#define FAKE_GPS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Globalization;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using SerialPortTester;

namespace GPSReaderNS
{
    public class SerialDeviceInfo
    {
        public string portName;
        public string caption;
        public string deviceID;
    }

    public class Coords
    {
        public bool valid
        {
            get
            {
                return flLng && flLat;
            }
        }
        public void invalidate()
        {
            flLng = false;
            flLat = false;
        }
        private bool flLat = false;
        private bool flLng = false;
        private double _lat;
        private double _lng;
        public double lat { get { return _lat; } }
        public double lng { get { return _lng; } }
        internal void setLat( double value ) {
            flLat = true;
            _lat = value;
        }
        internal void setLng(double value) {
            flLng = true;
            _lng = value;
        }
        public override string ToString()
        {
            return valid ? _lat.ToString() + " " + _lng.ToString() : null;
        }
        public string toJSON()
        {
            return valid ? "[" + _lat.ToString( CultureInfo.InvariantCulture) + "," + _lng.ToString(CultureInfo.InvariantCulture) + "]" : null;
        }

    }


    class DebugCoords
    {
        internal string loc;
        internal string lat;
        internal string latD;
        internal string lng;
        internal string lngD;
    }

    class GPSReader
    {
        private static Regex portRE = new Regex(@"(?<=\()COM\d+(?=\))");
        public static List<SerialDeviceInfo> listSerialDevices()
        {
            List<SerialDeviceInfo> r = new List<SerialDeviceInfo>();
            using (var searcher = new ManagementObjectSearcher
                ("root\\CIMV2",
                    "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\""))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                foreach (var p in ports)
                {
                    string caption = p["Caption"].ToString();
                    if (caption.Contains("COM"))
                    {
                        Match m = portRE.Match(caption);
                        if (m.Success)
                            r.Add(new SerialDeviceInfo {
                                caption = caption,
                                deviceID = p["DeviceID"].ToString(),
                                portName = m.Value
                            });
                    }
                }
            }
            return r;
        }

        SerialPort sport;
        private Coords _coords = new Coords();

        public Coords coords { get { return _coords; } }
        volatile StringBuilder sb = new StringBuilder();
        public EventHandler<EventArgs> locationChanged;

        public GPSReader()
        {
#if DEBUG
#if FAKE_GPS
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = SystemIcons.Hand;
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            foreach ( string l in new string[] {
                "$GPGGA,123519,4807.038,N,01131.000,E,1,08,0.9,545.4,M,46.9,M,,*47",
                "$GPGGA,123519,4507.490,N,03900.4874,E,1,08,0.9,545.4,M,46.9,M,,*47"
            })
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(l);
                mi.Click += delegate (object sender, EventArgs e)
                {
                    parse(l);
                };
                notifyIcon.ContextMenuStrip.Items.Add(mi);

            }
#endif
#endif
        }

        public void listenPort(string portName)
        {
            if (sport?.PortName != portName)
            {
                sport?.Close();
                sport = new SerialPort(portName, 4800, Parity.None, 8, StopBits.One);
                sport.DataReceived += sportDataReceived;
                try
                {
                    SerialPortFixer.Execute(portName);
                    sport.Open();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error opening port " + portName + " " + e.ToString());
                }
            }
        }


            private void sportDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
#if DEBUG
#if FAKE_GPS
            return;
#endif
#endif
            byte[] buf = new byte[sport.BytesToRead];
            sport.Read(buf, 0, buf.Length);
            for (int c = 0; c < buf.Length; c++)
            {
                string ch = Encoding.ASCII.GetString(buf, c++, 1);
                sb.Append(ch);
                if ( ch.Equals("$"))
                {
                    string line = sb.ToString();
                    if (line.StartsWith("GPGGA"))
                        parse(line);
                    sb.Clear();
                }
            }
        }

        private double parseCoord( string n, string d)
        {
            int intL = d == "N" || d == "S" ? 2 : 3;
            double coord = double.Parse( n.Substring(0, intL) ) +
                double.Parse(n.Substring(intL), NumberStyles.Any, CultureInfo.InvariantCulture) / 60;
            if (d == "S" || d == "W")
                coord *= -1;
            return coord;
        }


        private void parse(string line)
        {
            string[] lineArr = line.Split(',');
            if (lineArr.Count() > 5 && ( lineArr[3] == "N" || lineArr[3] == "S" ) && (lineArr[5] == "W" || lineArr[5] == "E") )
            {
                try
                {
                    double nLat = parseCoord(lineArr[2], lineArr[3]);
                    double nLng = parseCoord(lineArr[4],lineArr[5]);

                    if (nLat != coords.lat || nLng != coords.lng)
                    {
                        _coords.setLat(nLat);
                        _coords.setLng(nLng);
                        System.Diagnostics.Debug.WriteLine("New location: " + _coords.ToString());
                        locationChanged?.Invoke(this, new EventArgs());
                    }
                }
                catch (Exception e)
                {
                    
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
        }
    }
}
