namespace THOK.TCP
{
    using System;
    using System.Net.Sockets;

    public class SocketEventArgs : EventArgs
    {
        private string address;
        private Socket socket;

        public SocketEventArgs(Socket socket)
        {
            this.socket = socket;
            this.address = socket.RemoteEndPoint.ToString();
        }

        public SocketEventArgs(string ip, int port)
        {
            this.address = string.Format("{0}:{1}", ip, port);
        }

        public string RemoteAddress
        {
            get
            {
                return this.address;
            }
        }
    }
}

