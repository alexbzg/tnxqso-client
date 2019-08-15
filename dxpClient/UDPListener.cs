using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPListenerNS
{
    class DataReceivedArgs
    {
        public byte[] data;
    }

    class UDPListener
    {
        private int _listenPort = 12060;
        private volatile bool listening;
        Thread _listenThread;
        public event EventHandler<DataReceivedArgs> DataReceived;

        //constructor
        public UDPListener()
        {
            this.listening = false;
        }

        public void StartListener(int listenPort)
        {
            if (!this.listening)
            {
                _listenPort = listenPort;
                _listenThread = new Thread(listen);
                this.listening = true;
                _listenThread.IsBackground = true;
                _listenThread.Start();
            }
        }

        public void StopListener()
        {
            this.listening = false;
        }

        private void listen()
        {
            UdpClient listener = null;
            try
            {
                listener = new UdpClient(_listenPort);
            }
            catch (SocketException)
            {
                //do nothing
            }

            if (listener != null)
            {
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, _listenPort);

                try
                {
                    while (this.listening)
                    {
                        byte[] bytes = listener.Receive(ref groupEP);

                        //raise event                        
                        DataReceived(this, new DataReceivedArgs { data = bytes });
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.TraceInformation(e.ToString());
                }
                finally
                {
                    listener.Close();
                    System.Diagnostics.Trace.TraceInformation("Done listening for UDP broadcast");
                }
            }
        }
    }
}
