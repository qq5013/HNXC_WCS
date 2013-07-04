using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Collection
{
    public class RelationCollection
    {
        private Dictionary<string, List<string>> relation = new Dictionary<string, List<string>>();

        ~RelationCollection()
        {
            relation.Clear();
        }

        internal void Add(string serviceName, string itemName, string processName)
        {
            string key = GetKey(serviceName, itemName);

            processName = processName.ToUpper();

            List<string> processList = null;

            if (relation.ContainsKey(key))
                processList = relation[key];

            if (processList == null)
            {
                processList = new List<string>();
                processList.Add(processName);
                relation.Add(key, processList);
            }
            else
            {
                if (!processList.Contains(processName))
                    processList.Add(processName);
            }
        }

        public List<string> GetProcessName(string serviceName, string itemName)
        {
            string key = GetKey(serviceName, itemName);
            if (relation.ContainsKey(key))
                return relation[key];
            else
                return null;
        }

        private string GetKey(string serviceName, string itemName)
        {
            return serviceName.ToUpper() + "." + itemName.ToUpper();
        }
    }
}
