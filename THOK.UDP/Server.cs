namespace THOK.UDP
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class Server
    {
        private string address;
        private Thread listenThread;
        private int port;
        private Socket server;

        public event ReceiveEventHandler OnReceive;

        public Server()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            if (hostEntry.AddressList.Length != 0)
            {
                this.address = hostEntry.AddressList[0].ToString();
            }
            else
            {
                this.address = "127.0.0.1";
            }
            this.port = 0x3e8;
        }

        private void Listen()
        {
            byte[] buffer;
        Label_0000:
            buffer = new byte[0x800];
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                int count = this.server.ReceiveFrom(buffer, ref remoteEP);
                string message = Encoding.UTF8.GetString(buffer, 0, count);
                if (this.OnReceive != null)
                {
                    this.OnReceive(this, new ReceiveEventArgs(remoteEP.ToString(), message));
                }
                goto Label_0000;
            }
            catch (Exception)
            {
                goto Label_0000;
            }
        }

        public void StartListen()
        {
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.server.Bind(new IPEndPoint(IPAddress.Parse(this.address), this.port));
            this.listenThread = new Thread(new ThreadStart(this.Listen));
            this.listenThread.IsBackground = true;
            this.listenThread.Name = "监听线程";
            this.listenThread.Start();
        }

        public void StartListen(string address, int port)
        {
            this.address = address;
            this.port = port;
            this.StartListen();
        }

        public void StopListen()
        {
            if (this.listenThread != null)
            {
                this.listenThread.Abort();
            }
            if (this.server != null)
            {
                this.server.Close();
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }
    }
}

