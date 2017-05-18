using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Globalization;

namespace GpsReaderNS
{
    class GPSReader
    {
        SerialPort sport = new SerialPort();
        Timer readTimer;

        string _lat;
        string _long;

        public string latitude { get { return _lat; } }
        public string longitude { get { return _long; } }

        public GPSReader(string portName)
        {
            sport.PortName = portName;
            sport.BaudRate = 4800;
            sport.DataBits = 8;
            sport.StopBits = StopBits.One;
            sport.Parity = Parity.None;
            try
            {
                sport.Open();
                readTimer = new Timer(obj => read(), null, 1000, 1000);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        private double[] splitDouble( double v )
        {
            double[] r = new double[2];
            r[0] = Math.Truncate(v);
            r[1] = v - r[0];
            return r;
        }

        public void read()
        {
            if (sport.IsOpen)
            {
                string data = sport.ReadExisting();
                string[] strArr = data.Split('$');
                for (int i = 0; i < strArr.Length; i++)
                {
                    string strTemp = strArr[i];
                    string[] lineArr = strTemp.Split(',');
                    if (lineArr[0] == "GPGGA" && lineArr.Count() > 5)
                    {
                        try
                        {
                            //Latitude
                            Double dLat = double.Parse(lineArr[2], NumberStyles.Any, CultureInfo.InvariantCulture );
                            double[] lat = splitDouble( dLat / 100 );
                            _lat = lineArr[3].ToString() + lat[0].ToString() + "°" + (lat[1] * 100).ToString("00.00", CultureInfo.InvariantCulture);

                            //Longitude
                            Double dLon = double.Parse(lineArr[4], NumberStyles.Any, CultureInfo.InvariantCulture);
                            double[] lon = splitDouble( dLon / 100 );
                            _long = lineArr[5].ToString() + lon[0].ToString() + "°" + (lon[1] * 100).ToString("00.00", CultureInfo.InvariantCulture);

                            System.Diagnostics.Debug.WriteLine(_lat + " " + _long);
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e.ToString());
                        }
                    }
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Port is closed");
            }
        }
    }
}
