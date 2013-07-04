using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace THOK.MCP.Config
{
    public class Configuration
    {
        private string logLevel = "DEBUG";
        private string viewProcess = "ViewProcess";
        private List<ServiceConfig> services = new List<ServiceConfig>();
        private List<ProcessConfig> processes = new List<ProcessConfig>();
        private Dictionary<string, string> attributes = new Dictionary<string, string>();

        public string LogLevel
        {
            get { return logLevel; }
        }

        public string ViewProcess
        {
            get { return viewProcess; }
        }

        public List<ServiceConfig> Services
        {
            get { return services; }
        }

        public List<ProcessConfig> Processes
        {
            get { return processes; }
        }

        public Dictionary<string, string> Attributes
        {
            get { return attributes; }
        }

        public void Load(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            ReadLogLevel(doc);
            ReadViewProcess(doc);
            ReadAttributes(doc);
            ReadProcesses(doc);
            ReadServices(doc);
            doc = null;
        }

        public void Release()
        {
            services.Clear();
            processes.Clear();
        }

        private void ReadLogLevel(XmlDocument doc)
        {
            XmlNodeList logLevel = doc.GetElementsByTagName("LogLevel");
            if (logLevel.Count != 0)
                this.logLevel = logLevel[0].InnerText;
        }

        private void ReadViewProcess(XmlDocument doc)
        {
            XmlNodeList viewProcess = doc.GetElementsByTagName("ViewProcess");
            if (viewProcess.Count != 0)
                this.viewProcess = viewProcess[0].InnerText;
        }

        private void ReadAttributes(XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("Attribute");
            foreach (XmlNode node in nodes)
            {
                try
                {
                    string name = node.Attributes["Name"].Value;
                    string value = node.Attributes["Value"].Value;
                    attributes.Add(name, value);
                }
                catch (Exception e)
                {
                    throw new MCPException("初始化Attributes节出错，请检查配置文件。\n原因：" + e.Message);
                }
            }
        }

        private void ReadServices(XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("Service");
            foreach (XmlNode node in nodes)
            {
                try
                {
                    string name = node.Attributes["Name"].Value;
                    string[] type = node.Attributes["Type"].Value.Split(',');
                    string fileName = node.Attributes["ConfigFile"].Value;
                    services.Add(new ServiceConfig(name, type[0].Trim(), type[1].Trim(), fileName));
                }
                catch (Exception e)
                {
                    throw new MCPException("初始化Services节出错，请检查配置文件。\n原因：" + e.Message);
                }
            }
        }

        private void ReadProcesses(XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("Process");
            foreach (XmlNode node in nodes)
            {
                try
                {
                    string name = node.Attributes["Name"].Value;
                    string[] type = node.Attributes["Type"].Value.Split(',');
                    bool suspend = false;
                    if (node.Attributes["Suspend"] != null)
                        suspend = Convert.ToBoolean(node.Attributes["Suspend"].Value);

                    ProcessConfig processConfig = new ProcessConfig(name, type[0].Trim(), type[1].Trim(), suspend);

                    foreach (XmlNode itemNode in node.ChildNodes)
                    {
                        string serviceName = itemNode.Attributes["ServiceName"].Value;
                        string itemName = itemNode.Attributes["ItemName"].Value;
                        processConfig.Items.Add(new ProcessItemConfig(serviceName, itemName));
                    }

                    processes.Add(processConfig);
                }
                catch (Exception e)
                {
                    throw new MCPException("初始化Processes节出错，请检查配置文件。\n原因：" + e.Message);
                }
            }
        }
    }
}
