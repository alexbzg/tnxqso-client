//#define FAKE_GPS
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
        private double _lat;
        private double _lng;
        public double lat { get { return _lat; } }
        public double lng { get { return _lng; } }
        internal void setLat( double value ) {
            _lat = value;
        }
        internal void setLng(double value) { _lng = value; }
        public override string ToString()
        {
            return _lat.ToString() + " " + _lng.ToString();
        }
        public string toJSON()
        {
            return "[" + _lat.ToString( CultureInfo.InvariantCulture) + "," + _lng.ToString(CultureInfo.InvariantCulture) + "]";
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

        public GPSReader(string portName)
        {
            sport = new SerialPort(portName, 4800, Parity.None, 8, StopBits.One);
            sport.DataReceived += sportDataReceived;
            try
            {
                sport.Open();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
#if DEBUG
#if FAKE_GPS
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = SystemIcons.Hand;
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            foreach ( DebugCoords dc in new DebugCoords[] {
                new DebugCoords
                {
                    loc = "LP04IO",
                    lat = "6460.6076",
                    latD = "N",
                    lng = "4071.2153",
                    lngD = "E"
                }
            })
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(dc.loc);
                mi.Click += delegate (object sender, EventArgs e)
                {
                    parseLat(dc.lat, dc.latD);
                    parseLong(dc.lng, dc.lngD);
                    locationChanged?.Invoke(this, new EventArgs());
                };
                notifyIcon.ContextMenuStrip.Items.Add(mi);

            }
#endif
#endif
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

        private double[] splitDouble( double v )
        {
            double[] r = new double[2];
            r[0] = Math.Truncate(v);
            r[1] = v - r[0];
            return r;
        }

        private bool parseLat( string n, string d)
        {
            double newLat = double.Parse(n, NumberStyles.Any, CultureInfo.InvariantCulture) / 100;
            double signLat = newLat * (d == "N" ? 1 : -1);
            if (signLat != _coords.lat)
            {
                _coords.setLat(signLat);
                return true;
            }
            return false;
        }

        private bool parseLong(string n, string d)
        {
            double newLong = double.Parse(n, NumberStyles.Any, CultureInfo.InvariantCulture) / 100;
            double signLong = newLong * (d == "E" ? 1 : -1);
            if (signLong != _coords.lng)
            {
                _coords.setLng( signLong );
                return true;
            }
            return false;
        }



        private void parse(string line)
        {
            string[] lineArr = line.Split(',');
            if (lineArr.Count() > 5 && ( lineArr[3] == "N" || lineArr[3] == "S" ) && (lineArr[5] == "W" || lineArr[5] == "E") )
            {
                try
                {
                    bool flLat = parseLat(lineArr[2], lineArr[3]);
                    bool flLng = parseLong(lineArr[4],lineArr[5]);

                    if (flLat || flLng)
                    {
                        System.Diagnostics.Debug.WriteLine(_coords.ToString());
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
