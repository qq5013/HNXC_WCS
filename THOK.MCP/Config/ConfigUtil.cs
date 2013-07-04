using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace THOK.MCP.Config
{
    public class ConfigUtil
    {

        public Dictionary<string, string> GetAttribute()
        {
            Dictionary<string, string> property = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            XmlNodeList attributeList = doc.GetElementsByTagName("Attribute");
            foreach (XmlNode node in attributeList)
            {
                property.Add(node.Attributes["Name"].InnerText, node.Attributes["Value"].InnerText);
            }
            return property;
        }

        public void Save(Dictionary<string, string> property)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            XmlNodeList attributeList = doc.GetElementsByTagName("Attribute");
            foreach (XmlNode node in attributeList)
            {
                if (property.ContainsKey(node.Attributes["Name"].InnerText))
                    node.Attributes["Value"].InnerText = property[node.Attributes["Name"].InnerText];
            }
            doc.Save("Config.xml");
        }
    }
}
