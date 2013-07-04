namespace THOK.TCP
{
    using System;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class ClientThread
    {
        private bool isRun;
        private Server server;
        private Socket socket;
        private Thread thread;
        private Writer writer;

        public event SocketEventHandler OnDisconnect;

        public event ReceiveEventHandler OnReceive;

        public ClientThread(Server server, Socket client)
        {
            this.server = server;
            this.socket = client;
            this.writer = new Writer(this.socket);
        }

        public void Start()
        {
            this.thread = new Thread(new ThreadStart(this.StartThread));
            this.thread.Name = "客户线程";
            this.isRun = true;
            this.thread.Start();
        }

        private void StartThread()
        {
            try
            {
                while (this.isRun)
                {
                    try
                    {
                        byte[] buffer = new byte[0x400];
                        if (this.socket.Receive(buffer, SocketFlags.Peek) != 0)
                        {
                            if (this.OnReceive != null)
                            {
                                ReceiveEventArgs e = new ReceiveEventArgs(this.socket, this.server.Clients);
                                this.OnReceive(this, e);
                            }
                            continue;
                        }
                        if (this.OnDisconnect != null)
                        {
                            this.OnDisconnect(this, new SocketEventArgs(this.socket));
                        }
                        this.server.RemoveClient(this);
                        return;
                    }
                    catch (Exception)
                    {
                        this.isRun = false;
                        if (this.OnDisconnect != null)
                        {
                            this.OnDisconnect(this, new SocketEventArgs(this.socket));
                        }
                        this.server.RemoveClient(this);
                        continue;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
            }
        }

        public void Stop()
        {
            this.isRun = false;
            this.thread.Abort();
        }

        internal void Write(string msg)
        {
            this.writer.Write(msg);
        }

        public string RemoteAddress
        {
            get
            {
                return this.socket.RemoteEndPoint.ToString();
            }
        }
    }
}

