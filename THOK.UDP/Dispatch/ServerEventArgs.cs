namespace THOK.UDP.Dispatch
{
    using System;

    public class ServerEventArgs
    {
        private string clientIP;
        private string clientName;
        private int clientPort;

        public ServerEventArgs(string clientName, string clientIP, int clientPort)
        {
            this.clientName = clientName;
            this.clientIP = clientIP;
            this.clientPort = clientPort;
        }

        public string ClientIP
        {
            get
            {
                return this.clientIP;
            }
        }

        public string ClientName
        {
            get
            {
                return this.clientName;
            }
        }

        public int ClientPort
        {
            get
            {
                return this.clientPort;
            }
        }
    }
}

