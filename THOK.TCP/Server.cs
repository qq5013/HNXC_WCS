namespace THOK.TCP
{
    using System;
    using System.Collections;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class Server
    {
        private string address;
        private ArrayList clients = new ArrayList();
        private bool isRun;
        private Thread listenThread;
        private int port;
        private Socket server;

        public event SocketEventHandler OnConnect;

        public event SocketEventHandler OnDisconnect;

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
            this.server.Listen(10);
            while (this.isRun)
            {
                Socket client = this.server.Accept();
                client.Blocking = true;
                ClientThread thread = new ClientThread(this, client);
                thread.OnReceive += this.OnReceive;
                thread.OnDisconnect += this.OnDisconnect;
                this.clients.Add(thread);
                if (this.OnConnect != null)
                {
                    this.OnConnect(this, new SocketEventArgs(client));
                }
                thread.Start();
            }
        }

        internal void RemoveClient(ClientThread client)
        {
            this.clients.Remove(client);
        }

        public void StartListen()
        {
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.server.Bind(new IPEndPoint(IPAddress.Parse(this.address), this.port));
            this.isRun = true;
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
            for (int i = 0; i < this.clients.Count; i++)
            {
                ((ClientThread) this.clients[i]).Stop();
            }
            this.clients.Clear();
            this.isRun = false;
            if (this.listenThread != null)
            {
                this.listenThread.Abort();
            }
            if (this.server != null)
            {
                this.server.Close();
            }
        }

        public void Write(string remoteAddress, string msg)
        {
            for (int i = 0; i < this.clients.Count; i++)
            {
                ClientThread thread = (ClientThread) this.clients[i];
                //if (thread.RemoteAddress.Equals(remoteAddress))
                //{
                    thread.Write(msg);
                    return;
            //}
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

        internal ArrayList Clients
        {
            get
            {
                return this.clients;
            }
        }

        public int OnlineCount
        {
            get
            {
                return this.clients.Count;
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

