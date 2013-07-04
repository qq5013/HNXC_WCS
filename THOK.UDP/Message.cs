namespace THOK.UDP
{
    using System;
    using System.Collections.Generic;

    public class Message
    {
        private string command;
        private string msg;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private List<string> receivers = new List<string>();
        private string sender;

        public Message(string msg, string sender, string command, List<string> receivers, Dictionary<string, string> parameters)
        {
            this.msg = msg;
            this.sender = sender;
            this.receivers = receivers;
            this.command = command;
            this.parameters = parameters;
        }

        public string Command
        {
            get
            {
                return this.command;
            }
        }

        public string Msg
        {
            get
            {
                return this.msg;
            }
        }

        public Dictionary<string, string> Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public List<string> Receivers
        {
            get
            {
                return this.receivers;
            }
        }

        public string Sender
        {
            get
            {
                return this.sender;
            }
        }
    }
}

