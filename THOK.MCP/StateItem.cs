using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public class StateItem
    {
        string name;

        string itemName;

        object state;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }
        }

        public object State
        {
            get
            {
                return state;
            }
        }

        public StateItem(string name, string itemName, object state)
        {
            this.name = name;
            this.itemName = itemName;
            this.state = state;
        }
    }
}
