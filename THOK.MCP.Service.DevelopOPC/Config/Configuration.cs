using System;
using System.Xml;

namespace THOK.MCP.Service.DevelopOPC.Config
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

		public string ConnectionString
		{
			get
			{
				return connectionString;
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
		}

		public int UpdateRate
		{
			get
			{
				return updateRate;
			}
		}
		
		public ItemInfo[] Items
		{
			get
			{
				return items;
			}
		}

		public Configuration(string configFile)
		{
			doc = new XmlDocument();
			doc.Load(configFile);
			Initialize();
		}

		private void Initialize()
		{
			XmlNodeList nodeList = doc.GetElementsByTagName("OPCServer");
			if (nodeList.Count != 0)
			{
				connectionString = nodeList[0].Attributes["ConnectionString"].Value;
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
					node.Attributes["OPCItemName"].Value,
					Convert.ToInt32(node.Attributes["ClientHandler"].Value),
					node.Attributes["ItemType"].Value);
			}
		}
	}
}
