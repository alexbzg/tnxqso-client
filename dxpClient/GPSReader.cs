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

namespace GPSReaderNS
{
    public class SerialDeviceInfo
    {
        public string portName;
        public string caption;
        public string deviceID;
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

        string _lat;
        string _long;
        double _dlat;
        double _dlong;

        public string latitude { get { return _lat; } }
        public string longitude { get { return _long; } }
        public double dlat { get { return _dlat; } }
        public double dlong { get { return _dlong; } }
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
        }

        private void sportDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
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

        private void parse(string line)
        {
            string[] lineArr = line.Split(',');
            if (lineArr.Count() > 5 && ( lineArr[3] == "N" || lineArr[3] == "S" ) && (lineArr[5] == "W" || lineArr[5] == "E") )
            {
                bool fl = false;
                try
                {
                    //Latitude
                    double newLat = double.Parse(lineArr[2], NumberStyles.Any, CultureInfo.InvariantCulture) / 100;
                    double signLat = newLat * (lineArr[3] == "N" ? 1 : -1);
                    if (signLat != dlat)
                    {
                        _dlat = signLat;
                        fl = true;
                        double[] lat = splitDouble(newLat);
                        _lat = lineArr[3] + lat[0].ToString() + "°" + (lat[1] * 100).ToString("00.00", CultureInfo.InvariantCulture);
                    }

                    //Longitude
                    double newLong = double.Parse(lineArr[4], NumberStyles.Any, CultureInfo.InvariantCulture) / 100;
                    double signLong = newLong * (lineArr[5] == "E" ? 1 : -1);
                    if (signLong != dlong)
                    {
                        _dlong = signLong;
                        fl = true;
                        double[] lon = splitDouble(newLong);
                        _long = lineArr[5] + lon[0].ToString() + "°" + (lon[1] * 100).ToString("00.00", CultureInfo.InvariantCulture);
                    }

                    if (fl)
                    {
                        System.Diagnostics.Debug.WriteLine(_lat + " " + _long);
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
