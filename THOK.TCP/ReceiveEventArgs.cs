namespace THOK.TCP
{
    using System;
    using System.Collections;
    using System.Net.Sockets;

    public class ReceiveEventArgs : EventArgs
    {
        private ArrayList clients;
        private Reader reader;
        private Socket socket;
        private Writer writer;

        public ReceiveEventArgs(Socket socket, ArrayList sockets)
        {
            this.socket = socket;
            this.clients = sockets;
            this.reader = new Reader(this.socket);
            this.writer = new Writer(this.socket);
        }

        public string Read()
        {
            return this.reader.Read();
        }

        public void Write(string msg)
        {
            this.writer.Write(msg);
        }

        public void WriteToAll(string msg)
        {
            if (this.clients != null)
            {
                foreach (ClientThread thread in this.clients)
                {
                    thread.Write(msg);
                }
            }
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

