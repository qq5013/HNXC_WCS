namespace THOK.UDP.Util
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using THOK.UDP;

    public class MessageParser
    {
        private string GetCommand(XmlDocument doc)
        {
            XmlNodeList elementsByTagName = doc.GetElementsByTagName("Command");
            if (elementsByTagName.Count == 0)
            {
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            }
            return elementsByTagName[0].InnerText;
        }

        private Dictionary<string, string> GetParameters(XmlDocument doc)
        {
            XmlNodeList elementsByTagName = doc.GetElementsByTagName("Parameters");
            if (elementsByTagName.Count == 0)
            {
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (XmlNode node in elementsByTagName[0].ChildNodes)
            {
                dictionary.Add(node.Name, node.InnerText);
            }
            return dictionary;
        }

        private List<string> GetReceivers(XmlDocument doc)
        {
            XmlNodeList elementsByTagName = doc.GetElementsByTagName("Receivers");
            if (elementsByTagName.Count == 0)
            {
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            }
            List<string> list2 = new List<string>();
            foreach (XmlNode node in elementsByTagName[0].ChildNodes)
            {
                if (!node.Name.Equals("Receiver"))
                {
                    throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
                }
                list2.Add(node.InnerText);
            }
            return list2;
        }

        private string GetSender(XmlDocument doc)
        {
            XmlNodeList elementsByTagName = doc.GetElementsByTagName("Sender");
            if (elementsByTagName.Count == 0)
            {
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            }
            return elementsByTagName[0].InnerText;
        }

        public Message Parse(string msg)
        {
            Message message = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(msg);
                string sender = this.GetSender(doc);
                string command = this.GetCommand(doc);
                List<string> receivers = this.GetReceivers(doc);
                Dictionary<string, string> parameters = this.GetParameters(doc);
                message = new Message(msg, sender, command, receivers, parameters);
            }
            catch
            {
            }
            return message;
        }
    }
}

