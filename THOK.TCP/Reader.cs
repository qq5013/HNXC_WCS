namespace THOK.TCP
{
    using System;
    using System.IO;
    using System.Net.Sockets;

    internal class Reader
    {
        private bool hasRead;
        private NetworkStream stream;
        private string strMsg = "";

        public Reader(Socket socket)
        {
            this.stream = new NetworkStream(socket);
        }

        internal void Close()
        {
            this.stream.Close();
        }

        public string Read()
        {
            if (!this.hasRead)
            {
                this.strMsg = new StreamReader(this.stream).ReadLine();
                this.hasRead = true;
            }
            return this.strMsg;
        }
    }
}

