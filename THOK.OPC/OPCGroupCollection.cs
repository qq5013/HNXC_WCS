namespace THOK.OPC
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class OPCGroupCollection
    {
        private string defaultGroup;
        private Hashtable groupTable = new Hashtable();

        internal OPCGroupCollection()
        {
        }

        internal void Add(string groupName, OPCGroup group)
        {
            this.AddGroup(groupName, group);
        }

        protected void AddGroup(string groupName, OPCGroup group)
        {
            if (this.defaultGroup == null)
            {
                this.defaultGroup = groupName.ToUpper();
            }
            this.groupTable.Add(groupName.ToUpper(), group);
        }

        public virtual void Release()
        {
            this.groupTable.Clear();
        }

        public ICollection AllGroup
        {
            get
            {
                return this.groupTable.Values;
            }
        }

        public OPCGroup DefaultGroup
        {
            get
            {
                return (OPCGroup) this.groupTable[this.defaultGroup];
            }
        }

        public OPCGroup this[string groupName]
        {
            get
            {
                if (!this.groupTable.ContainsKey(groupName.ToUpper()))
                {
                    throw new Exception(string.Format("名称为'{0}'的组不存在，请检查配置文件", groupName));
                }
                return (OPCGroup) this.groupTable[groupName.ToUpper()];
            }
        }
    }
}

