using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Collection
{
    public class AttributeCollection
    {
        private Dictionary<string, object> attributes = new Dictionary<string, object>();

        ~AttributeCollection()
        {
            attributes.Clear();
        }

        public void Add(string name, object value)
        {
            if (attributes.ContainsKey(name))
                attributes[name] = value;
            else
                attributes.Add(name, value);
        }

        public object this[string attributeName]
        {
            get 
            {
                if (attributes.ContainsKey(attributeName))
                    return attributes[attributeName];
                else
                    return null;
            }
            set
            {
                if (attributes.ContainsKey(attributeName))
                    attributes[attributeName] = value;
                else
                    attributes.Add(attributeName, value);
            }
        }
    }
}
