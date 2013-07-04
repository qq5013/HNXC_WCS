using System;

namespace THOK.MCP.Service.Siemens.Config
{
	/// <summary>
	/// Item 的摘要说明。
	/// </summary>
	public class ItemInfo
	{
		private string itemName;

		private string opcItemName;

		private int clientHandler;

		private bool isActive;

		public string ItemName
		{
			get
			{
				return itemName;
			}
		}

		public string OpcItemName
		{
			get
			{
				return opcItemName;
			}
		}

		public int ClientHandler
		{
			get
			{
				return clientHandler;
			}
		}

		public bool IsActive
		{
			get
			{
				return isActive;
			}
            set
            {
                isActive = value;
            }
		}

		public ItemInfo(string itemName, string opcItemName, int clientHandler, string itemType)
		{
			this.itemName = itemName;
			this.opcItemName = opcItemName;
			this.clientHandler = clientHandler;
			this.isActive = itemType.ToUpper() == "READ";
		}
	}
}
