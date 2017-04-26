using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dxpClient
{
    class HTTPService
    {
        HttpClient client = new HttpClient();
        string srvURI;
        Timer pingTimer;
        ConcurrentQueue<QSO> qsoQueue = new ConcurrentQueue<QSO>();
        private volatile bool _connected;
        public bool connected {  get { return _connected; } }

        public HTTPService( string _srvURI)
        {
            srvURI = _srvURI;
            pingTimer = new Timer(obj => ping(), null, 1, Timeout.Infinite);
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
                    qsoQueue.Enqueue(qso);
            }
            else
                qsoQueue.Enqueue(qso);
        }

        private async Task processQueue()
        {
            while (!qsoQueue.IsEmpty)
            {
                qsoQueue.TryPeek(out QSO qso);
                HttpResponseMessage r = await post(qso.toJSON());
                if (r.IsSuccessStatusCode)
                    qsoQueue.TryDequeue(out qso);
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
