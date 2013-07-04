using System;
using System.Xml;
using System.IO.Ports;

namespace THOK.MCP.Service.Sick.Config
{
	/// <summary>
	/// OPC配置文件处理类
	/// </summary>
	public class Configuration
	{
        private string configFile = "";
		private XmlDocument doc;

        private string portName = "COM1";

        private int baudRate = 9600;

        private Parity parity = Parity.None;

        private int dataBits = 8;

        private StopBits stopBits = StopBits.One;

        private bool isHex = false;

        private string type = null;

        private string separator = "}";

        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        public Parity Parity
        {
            get { return parity; }
            set { parity = value; }
        }

        public int DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }

        public StopBits StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }

        public bool IsHex
        {
            get { return isHex; }
        }

        public string Type
        {
            get
            {
                return type;
            }
        }

        public string Separator
        {
            get { return separator; }
            set { separator = value; }
        }

		public Configuration(string configFile)
		{
            this.configFile = configFile;
			doc = new XmlDocument();
			doc.Load(configFile);
			Initialize();
		}

		private void Initialize()
		{
			XmlNodeList xmlNodeList = doc.GetElementsByTagName("SerialPort");
            if (xmlNodeList.Count != 0)
			{
                XmlNode xmlNode = xmlNodeList[0];
                portName = xmlNode.Attributes["PortName"].Value.ToString();
                baudRate = Convert.ToInt32(xmlNode.Attributes["BaudRate"].Value.ToString());
                parity = (System.IO.Ports.Parity)Enum.Parse(typeof(Parity), xmlNode.Attributes["Parity"].Value.ToString());
                dataBits = Convert.ToInt32(xmlNode.Attributes["DataBits"].Value.ToString()) > 8 || Convert.ToInt32(xmlNode.Attributes["DataBits"].Value.ToString()) < 5 ? 8 : Convert.ToInt32(xmlNode.Attributes["DataBits"].Value.ToString());
                stopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(StopBits), xmlNode.Attributes["StopBits"].Value.ToString());
                isHex = Convert.ToBoolean(xmlNode.Attributes["IsHex"].Value);
			}
			else
			{
                throw new Exception("在配置文件中找不到关于SerialPort的信息");
			}

            xmlNodeList = doc.GetElementsByTagName("Parse");
            if (xmlNodeList.Count != 0)
            {
                type = xmlNodeList[0].Attributes["Type"].Value.ToString();
                Separator = xmlNodeList[0].Attributes["Separator"].Value.ToString();
            }
		}

        public void Save()
        {
            XmlNodeList nodeList = doc.GetElementsByTagName("SerialPort");
            if (nodeList.Count != 0)
            {
                XmlNode xmlNode = nodeList[0];
                xmlNode.Attributes["PortName"].Value = portName;
                xmlNode.Attributes["BaudRate"].Value = baudRate.ToString();
                xmlNode.Attributes["Parity"].Value = parity.ToString();
                xmlNode.Attributes["DataBits"].Value = dataBits.ToString();
                xmlNode.Attributes["StopBits"].Value = stopBits.ToString();
                doc.Save(configFile);
            }
            else
            {
                throw new Exception("在配置文件中找不到关于UDPServer的信息");
            }
        }
	}
}
