namespace THOK.OPC
{
    using System;

    public class OPCItem
    {
        private int clientHandler;
        private bool isActive;
        private string itemName;
        private string opcItemName;
        private OPCGroup parent;
        private int serverHandler;

        internal OPCItem(OPCGroup opcGroup, string itemName, string opcItemName, int clientHandler, bool isActive)
        {
            this.parent = opcGroup;
            this.ItemName = itemName;
            this.opcItemName = opcItemName;
            this.clientHandler = clientHandler;
            this.isActive = isActive;
        }

        public object Read()
        {
            return this.parent.Read(this.serverHandler);
        }

        public object[] ReadArray()
        {
            Array array = this.parent.Read(this.serverHandler);
            object[] objArray = new object[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                objArray[i] = Convert.ToInt32(array.GetValue(i));
            }
            return objArray;
        }

        public bool Write(object[] states)
        {
            return this.parent.Write(this.serverHandler, states);
        }

        public bool Write(object state)
        {
            return this.parent.Write(this.serverHandler, state);
        }

        public int ClientHandler
        {
            get
            {
                return this.clientHandler;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
            }
        }

        public string OPCItemName
        {
            get
            {
                return this.opcItemName;
            }
        }

        public int ServerHandler
        {
            get
            {
                return this.serverHandler;
            }
            set
            {
                this.serverHandler = value;
            }
        }
    }
}

