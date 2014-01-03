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
            try
            {
                if (!this.hasRead)
                {
                    //this.strMsg = new StreamReader(this.stream).ReadLine();

                    //StreamReader sr = new StreamReader(this.stream);
                    //string ss = sr.ReadLine();
                    byte[] buff = new byte[1024];
                    int i = this.stream.Read(buff,0, buff.Length);
                    strMsg = System.Text.Encoding.Default.GetString(buff, 0, i);
                    this.hasRead = true;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return this.strMsg;
        }
    }
}

