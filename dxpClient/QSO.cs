﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;
using ProtoBuf;
using SerializationNS;

namespace dxpClient
{
    [DataContract, ProtoContract]
    public class QSO
    {
        internal string _ts;
        internal string _myCS;
        internal string _band;
        internal string _freq;
        internal string _mode;
        internal string _cs;
        internal string _snt;
        internal string _rcv;
        internal string _rda;
        internal string _wwf;
        internal int _no;

        [DataMember, ProtoMember(1)]
        public string ts { get { return _ts; } set { _ts = value; } }
        [DataMember, ProtoMember(2)]
        public string myCS { get { return _myCS; } set { _myCS = value; } }
        [DataMember, ProtoMember(3)]
        public string band { get { return _band; } set { _band = value; } }
        [DataMember, ProtoMember(4)]
        public string freq { get { return _freq; } set { _freq = value; } }
        [DataMember, ProtoMember(5)]
        public string mode { get { return _mode; } set { _mode = value; } }
        [DataMember, ProtoMember(6)]
        public string cs { get { return _cs; } set { _cs = value; } }
        [DataMember, ProtoMember(7)]
        public string snt { get { return _snt; } set { _snt = value; } }
        [DataMember, ProtoMember(8)]
        public string rcv { get { return _rcv; } set { _rcv = value; } }
        [DataMember, ProtoMember(9)]
        public string rda { get { return _rda; } set { _rda = value; } }
        [DataMember, ProtoMember(10)]
        public string wwf { get { return _wwf; } set { _wwf = value; } }
        [DataMember, ProtoMember(11)]
        public int no { get { return _no; } set { _no = value; } }

        public string toJSON()
        {
            return JSONSerializer.Serialize<QSO>(this);
        }
    }

    public class QSOFactory
    {
        private DXpConfig settings;
        public int no = 1;



        public QSOFactory( DXpConfig _settings )
        {
            settings = _settings;
            settings.rdaChanged += rdaChanged; 
        }

        private void rdaChanged(object sender, EventArgs e)
        {
            no = 1;
        }

        public QSO create( string xml )
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;

            if (root.Name != "contactinfo")
                return null;

            return new QSO {
                _ts = root.SelectSingleNode("timestamp").InnerText,
                _myCS = root.SelectSingleNode("mycall").InnerText,
                _band = root.SelectSingleNode("band").InnerText,
                _freq = root.SelectSingleNode("txfreq").InnerText,
                _mode = root.SelectSingleNode("mode").InnerText,
                _cs = root.SelectSingleNode("call").InnerText,
                _snt = root.SelectSingleNode("snt").InnerText,
                _rcv = root.SelectSingleNode("rcv").InnerText,
                _no = no++,
                _rda = settings.rda,
                _wwf = settings.wwf
            };
        }
    }
}
