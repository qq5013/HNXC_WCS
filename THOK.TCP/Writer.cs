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
            try
            {
                this.stream = new NetworkStream(socket);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        internal void Close()
        {
            this.stream.Close();
        }

        public void Write(string msg)
        {
            //StreamWriter writer = new StreamWriter(this.stream);
            Byte[] sendBytes = System.Text.Encoding.Default.GetBytes(msg);
            this.stream.Write(sendBytes, 0, sendBytes.Length);
            //writer.Flush();
        }
    }
}

