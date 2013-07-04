namespace THOK.OPC
{
    using System;

    public class DataChangedEventArgs
    {
        private string groupName;
        private string itemName;
        private string serverName;
        private object[] states;

        public DataChangedEventArgs(string serverName, string groupName, string itemName, object[] states)
        {
            this.serverName = serverName;
            this.groupName = groupName;
            this.itemName = itemName;
            this.states = states;
        }

        public string Groupname
        {
            get
            {
                return this.groupName;
            }
        }

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
        }

        public string ServerName
        {
            get
            {
                return this.serverName;
            }
        }

        public object State
        {
            get
            {
                if (this.states.Length != 0)
                {
                    return this.states[0];
                }
                return -1;
            }
        }

        public object[] States
        {
            get
            {
                return this.states;
            }
        }
    }
}

