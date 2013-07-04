namespace THOK.TCP
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class Client
    {
        private Socket client;
        private string hostAddress;
        private bool isConnected;
        private int port;
        private Reader reader;
        private Thread thread;
        private Writer writer;

        public event ReceiveEventHandler OnReceive;

        public event SocketEventHandler OnServerClosed;

        public Client()
        {
        }

        public Client(string hostAddress, int port)
        {
            this.hostAddress = hostAddress;
            this.port = port;
        }

        public void Close()
        {
            this.isConnected = false;
            if (this.writer != null)
            {
                this.writer.Close();
                this.writer = null;
            }
            if (this.reader != null)
            {
                this.reader.Close();
                this.reader = null;
            }
            if (this.thread != null)
            {
                this.thread.Abort();
            }
            if (this.client != null)
            {
                this.client.Shutdown(SocketShutdown.Both);
                this.client.Close();
                this.client = null;
            }
        }

        public void Connect()
        {
            this.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.client.Connect(new IPEndPoint(IPAddress.Parse(this.hostAddress), this.port));
            this.isConnected = true;
            this.reader = new Reader(this.client);
            this.writer = new Writer(this.client);
            if (this.OnReceive != null)
            {
                this.thread = new Thread(new ThreadStart(this.ReceiveData));
                this.thread.Name = "接收数据";
                this.thread.Start();
            }
        }

        public void Connect(string hostAddress, int port)
        {
            this.hostAddress = hostAddress;
            this.port = port;
            this.Connect();
        }

        public string Read()
        {
            string str = null;
            if (this.reader != null)
            {
                str = this.reader.Read();
            }
            return str;
        }

        private void ReceiveData()
        {
            while (this.isConnected)
            {
                try
                {
                    byte[] buffer = new byte[0x400];
                    if (this.client.Receive(buffer, SocketFlags.Peek) != 0)
                    {
                        ReceiveEventArgs e = new ReceiveEventArgs(this.client, null);
                        this.OnReceive(this, e);
                    }
                    else
                    {
                        this.isConnected = false;
                        if (this.OnServerClosed != null)
                        {
                            this.OnServerClosed(this, new SocketEventArgs(this.hostAddress, this.port));
                        }
                    }
                    continue;
                }
                catch (Exception)
                {
                    this.isConnected = false;
                    if (this.OnServerClosed != null)
                    {
                        this.OnServerClosed(this, new SocketEventArgs(this.hostAddress, this.port));
                    }
                    continue;
                }
            }
            this.Close();
        }

        public void Write(string msg)
        {
            if (this.writer != null)
            {
                this.writer.Write(msg);
            }
        }

        public string HostAddress
        {
            get
            {
                return this.hostAddress;
            }
            set
            {
                this.hostAddress = value;
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.isConnected;
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

