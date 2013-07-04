using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public class Message
    {
        private bool parsed = false;
        private string msg = null ;
        private string command = null;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public bool Parsed
        {
            get { return parsed; }
        }

        public string Msg
        {
            get { return msg; }
        }

        public string Command
        {
            get { return command; }
        }

        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
        }

        public Message(bool parsed, string msg, string command, Dictionary<string, string> parameters)
        {
            this.parsed = parsed;
            this.msg = msg;
            this.command = command;
            this.parameters = parameters;
        }

        public Message(bool parsed, string command, Dictionary<string, string> parameters)
        {
            this.parsed = parsed;
            this.command = command;
            this.parameters = parameters;
        }

        public Message(string msg)
        {
            parsed = false;
            this.msg = msg;
        }
    }
}
