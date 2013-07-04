namespace THOK.UDP.Util
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class MessageGenerator
    {
        private string command;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private List<string> receivers = new List<string>();
        private string sender;

        public MessageGenerator(string command, string sender)
        {
            this.command = command;
            this.sender = sender;
        }

        public void AddParameter(string paramName, string paramValue)
        {
            this.parameters.Add(paramName, paramValue);
        }

        public void AddReceiver(string receiver)
        {
            if (!this.receivers.Contains(receiver))
            {
                this.receivers.Add(receiver);
            }
        }

        public void Clear()
        {
            this.command = null;
            this.sender = null;
            this.receivers.Clear();
            this.parameters.Clear();
        }

        private XmlElement GetCommand(XmlDocument doc)
        {
            XmlElement element = null;
            if (this.command == null)
            {
                throw new Exception("未设置命令，不能生成消息。");
            }
            element = doc.CreateElement("Command");
            element.InnerText = this.command;
            return element;
        }

        public string GetMessage()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement newChild = doc.CreateElement("Message");
            newChild.AppendChild(this.GetSender(doc));
            newChild.AppendChild(this.GetReceiver(doc));
            newChild.AppendChild(this.GetCommand(doc));
            newChild.AppendChild(this.GetParameter(doc));
            doc.AppendChild(newChild);
            return doc.OuterXml;
        }

        private XmlElement GetParameter(XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("Parameters");
            foreach (string str in this.parameters.Keys)
            {
                string str2 = this.parameters[str];
                XmlElement newChild = doc.CreateElement(str);
                newChild.InnerText = str2;
                element.AppendChild(newChild);
            }
            return element;
        }

        private XmlElement GetReceiver(XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("Receivers");
            for (int i = 0; i < this.receivers.Count; i++)
            {
                XmlElement newChild = doc.CreateElement("Receiver");
                newChild.InnerText = this.receivers[i].ToString();
                element.AppendChild(newChild);
            }
            return element;
        }

        private XmlElement GetSender(XmlDocument doc)
        {
            XmlElement element = null;
            if (this.sender == null)
            {
                throw new Exception("未设置消息发送者，不能生成消息。");
            }
            element = doc.CreateElement("Sender");
            element.InnerText = this.sender;
            return element;
        }

        public void SetCommand(string command)
        {
            this.command = command;
        }

        public void SetSender(string sender)
        {
            this.sender = sender;
        }
    }
}

