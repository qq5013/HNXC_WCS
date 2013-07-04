namespace THOK.UDP
{
    using System;

    public class ReceiveEventArgs : EventArgs
    {
        private string message;
        private string remoteAddress;

        public ReceiveEventArgs(string remoteAddress, string message)
        {
            this.remoteAddress = remoteAddress;
            this.message = message;
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public string RemoteAddress
        {
            get
            {
                return this.remoteAddress;
            }
        }
    }
}

