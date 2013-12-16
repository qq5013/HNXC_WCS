using System;
using System.Xml;

namespace THOK.MCP.Service.Siemens.Config
{
	/// <summary>
	/// OPC配置文件处理类
	/// </summary>
	public class Configuration
	{
		private XmlDocument doc;

		private string connectionString;

		private string groupName;

		private string groupString;

		private int updateRate;

		private ItemInfo[] items;

        private string progid;
        private string servername;

		public string ConnectionString
		{
			get
			{
				return connectionString;
			}
		}
        public string ProgID
        {
            get
            {
                return progid;
            }
            set
            {
                this.progid = value;
            }
        }
        public string ServerName
        {
            get
            {
                return servername;
            }
            set
            {
                this.servername = value;
            }
        }

		public string GroupName
		{
			get
			{
				return groupName;
			}
		}

		public string GroupString
		{
			get
			{
				return groupString;
			}
            set
            {
                this.groupString = value;
            }
		}

		public int UpdateRate
		{
			get
			{
				return updateRate;
			}
            set
            {
                this.updateRate = value;
            }
		}
		
		public ItemInfo[] Items
		{
			get
			{
				return items;
			}
		}
        private string ConfigFile = "";
		public Configuration(string configFile)
		{
            this.ConfigFile = configFile;
			doc = new XmlDocument();
			doc.Load(configFile);
			Initialize();
		}
        public void Save()
        {
            XmlNodeList nodeList = doc.GetElementsByTagName("OPCServer");
            if (nodeList.Count != 0)
            {
                XmlNode xmlNode = nodeList[0];
                xmlNode.Attributes["ConnectionString"].Value = servername + (string.IsNullOrEmpty(progid) ? "" : ";" + progid);
                doc.Save(ConfigFile);
            }
            else
            {
                throw new Exception("在配置文件中找不到关于OPCServer的信息");
            }

            nodeList = doc.GetElementsByTagName("OPCGroup");

            if (nodeList.Count != 0)
            {
                XmlNode xmlNode = nodeList[0];
                xmlNode.Attributes["GroupName"].Value = groupName;
                xmlNode.Attributes["GroupString"].Value = groupString;
                xmlNode.Attributes["UpdateRate"].Value = updateRate.ToString();   
                doc.Save(ConfigFile);
            }
            else
            {
                throw new Exception("在配置文件中找不到关于OPCGroup的信息");
            }

        }

		private void Initialize()
		{
			XmlNodeList nodeList = doc.GetElementsByTagName("OPCServer");
			if (nodeList.Count != 0)
			{
				connectionString = nodeList[0].Attributes["ConnectionString"].Value;
                string[] str = connectionString.Split(';');
                if (str.Length == 1)
                {
                    progid = null;
                    servername = str[0];
                }
                else
                {
                    progid = str[0];
                    servername = str[1];
                }
			}
			else
			{
				throw new Exception("在配置文件中找不到关于OPCServer的信息");
			}

			nodeList = doc.GetElementsByTagName("OPCGroup");

			if (nodeList.Count != 0)
			{
				groupName = nodeList[0].Attributes["GroupName"].Value;
				groupString = nodeList[0].Attributes["GroupString"].Value;
				updateRate = Convert.ToInt32(nodeList[0].Attributes["UpdateRate"].Value);
			}
			else
			{
				throw new Exception("在配置文件中找不到关于OPCGroup的信息");
			}

			nodeList = doc.GetElementsByTagName("OPCItem");

			items = new ItemInfo[nodeList.Count];
			for (int i = 0; i < nodeList.Count; i++)
			{
				XmlNode node = nodeList[i];
				items[i] = new ItemInfo(node.Attributes["ItemName"].Value,
					groupString + node.Attributes["OPCItemName"].Value,
					Convert.ToInt32(node.Attributes["ClientHandler"].Value),
					node.Attributes["ItemType"].Value);
                if (node.Attributes["IsActive"] != null )
                {
                    items[i].IsActive = Convert.ToBoolean(node.Attributes["IsActive"].Value);
                }
			}
		}
	}
}
