using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;

namespace dxpClient
{
    [DataContract]
    public class QSO
    {
        string _ts;
        [DataMember]
        public string myCS;
        [DataMember]
        public string band;
        [DataMember]
        public string freq;
        [DataMember]
        public string mode;
        [DataMember]
        public string cs;
        [DataMember]
        public string snt;
        [DataMember]
        public string rcv;

        [DataMember]
        public string ts;
        [DataMember]
        public string myCS;
        [DataMember]
        public string band;
        [DataMember]
        public string freq;
        [DataMember]
        public string mode;
        [DataMember]
        public string cs;
        [DataMember]
        public string snt;
        [DataMember]
        public string rcv;

        public string toJSON()
        {
            var ser = new DataContractJsonSerializer(typeof(QSO));
            var output = string.Empty;

            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, this);
                output = Encoding.UTF8.GetString(ms.ToArray());
            }
            return output;
        }
    }

    public class QSOFactory
    {
        public QSO create( string xml )
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;

            if (root.Name != "contactinfo")
                return null;

            return new QSO {
                ts = root.SelectSingleNode("timestamp").InnerText,
                myCS = root.SelectSingleNode("mycall").InnerText,
                band = root.SelectSingleNode("band").InnerText,
                freq = root.SelectSingleNode("txfreq").InnerText,
                mode = root.SelectSingleNode("mode").InnerText,
                cs = root.SelectSingleNode("call").InnerText,
                snt = root.SelectSingleNode("snt").InnerText,
                rcv = root.SelectSingleNode("rcv").InnerText
            };
        }
    }
}
