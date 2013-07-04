namespace THOK.TCP
{
    using System;
    using System.IO;
    using System.Net.Sockets;

    internal class Writer
    {
        private NetworkStream stream;

        public Writer(Socket socket)
        {
            this.stream = new NetworkStream(socket);
        }

        internal void Close()
        {
            this.stream.Close();
        }

        public void Write(string msg)
        {
            StreamWriter writer = new StreamWriter(this.stream);
            writer.WriteLine(msg);
            writer.Flush();
        }
    }
}

