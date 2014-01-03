using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.TCP.Util
{
    public class Message
    {
        private string command;
        private string msg;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private string receiver;
        private string sender;
        private string confirmFlag;
        private string seqno;

        public Message(string msg, string confirmFlag,string seqno,string sender, string command, string receiver, Dictionary<string, string> parameters)
        {
            this.msg = msg;
            this.confirmFlag = confirmFlag;
            this.seqno = seqno;
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
        public string ConfirmFlag
        {
            get
            {
                return this.confirmFlag;
            }
        }
        public string Seqno
        {
            get
            {
                return this.seqno;
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
