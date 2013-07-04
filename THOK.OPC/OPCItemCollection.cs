namespace THOK.OPC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class OPCItemCollection
    {
        private Dictionary<int, OPCItem> dictItemIndex = new Dictionary<int, OPCItem>();
        private Dictionary<string, OPCItem> dictItemName = new Dictionary<string, OPCItem>();

        internal OPCItemCollection()
        {
        }

        internal void Add(OPCItem item)
        {
            this.AddItem(item.ItemName, item);
            this.dictItemIndex.Add(item.ClientHandler, item);
        }

        protected void AddItem(string itemName, OPCItem item)
        {
            this.dictItemName.Add(itemName, item);
        }

        public void Release()
        {
            this.dictItemName.Clear();
            this.dictItemIndex.Clear();
        }

        public ICollection AllItem
        {
            get
            {
                return this.dictItemIndex.Values;
            }
        }

        public OPCItem this[int clientHandler]
        {
            get
            {
                OPCItem item = null;
                if (this.dictItemIndex.ContainsKey(clientHandler))
                {
                    item = this.dictItemIndex[clientHandler];
                }
                return item;
            }
        }

        public OPCItem this[string itemName]
        {
            get
            {
                OPCItem item = null;
                if (this.dictItemName.ContainsKey(itemName))
                {
                    item = this.dictItemName[itemName];
                }
                return item;
            }
        }
    }
}

