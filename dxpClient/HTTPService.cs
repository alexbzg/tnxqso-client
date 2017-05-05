using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlConfigNS;
using System.Windows.Forms;

namespace dxpClient
{
    class HTTPService
    {
        HttpClient client = new HttpClient();
        string srvURI;
        System.Threading.Timer pingTimer;
        ConcurrentQueue<QSO> qsoQueue = new ConcurrentQueue<QSO>();
        XmlConfig<List<QSO>> qsoBackup = new XmlConfig<List<QSO>>(Application.StartupPath + "\\unsent.xml");
        private volatile bool _connected;
        public bool connected {  get { return _connected; } }
        public EventHandler<EventArgs> connectionStateChanged;


        public HTTPService( string _srvURI)
        {
            srvURI = _srvURI;
            pingTimer = new System.Threading.Timer( obj => ping(), null, 1, Timeout.Infinite);
            if ( qsoBackup.data != null)
                Task.Run( () =>
               {
                   foreach (QSO qso in qsoBackup.data)
                       postQso(qso);
               });
        }

        private async Task<HttpResponseMessage> post(string sContent)
        {
            pingTimer.Change(Timeout.Infinite, Timeout.Infinite);
            HttpContent content = new StringContent(sContent);
            HttpResponseMessage response = await client.PostAsync(srvURI, content);
            if (_connected != response.IsSuccessStatusCode)
            {
                _connected = !_connected;
                if (_connected)
                    await processQueue();
                connectionStateChanged?.Invoke(this, new EventArgs());
            }
            pingTimer.Change(10000, Timeout.Infinite);
            return response;
        }

        public async Task postQso( QSO qso)
        {
            if (qsoQueue.IsEmpty)
            {
                HttpResponseMessage response = await post(qso.toJSON());
                if (!response.IsSuccessStatusCode)
                    addToQueue(qso);
            }
            else
                addToQueue(qso);
        }

        private void addToQueue( QSO qso)
        {
            qsoQueue.Enqueue(qso);
            saveUnsent();
        }

        private void saveUnsent()
        {
            qsoBackup.data = qsoQueue.ToList();
            qsoBackup.write();
        }

        private async Task processQueue()
        {
            while (!qsoQueue.IsEmpty)
            {
                qsoQueue.TryPeek(out QSO qso);
                HttpResponseMessage r = await post(qso.toJSON());
                if (r.IsSuccessStatusCode)
                {
                    qsoQueue.TryDequeue(out qso);
                    saveUnsent();
                }
                else
                    break;
            }
        }

        public async Task ping()
        {
            await post( "{\"ping\": 1 }");
        }
    }

}
