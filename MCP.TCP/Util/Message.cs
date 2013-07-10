using System;
using System.Collections.Generic;
using System.Text;

namespace MCP.TCP.Util
{
    public class Message
    {
        private string command;
        private string msg;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private string receiver;
        private string sender;

        public Message(string msg, string sender, string command, string receiver, Dictionary<string, string> parameters)
        {
            this.msg = msg;
            this.sender = sender;
            this.receiver = receiver;
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

        public string Receivers
        {
            get
            {
                return this.receiver;
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
