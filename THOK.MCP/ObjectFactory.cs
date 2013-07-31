using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public class ObjectFactory
    {
        public static object CreateInstance(string typeName)
        {
            object result = null;
            
            if (typeName != null && typeName.Trim().Length != 0)
            {
                int p = typeName.IndexOf(',');
                if (p == -1)
                {
                    result = System.Reflection.Assembly.GetCallingAssembly().CreateInstance(typeName);
                }
                else
                {
                    string dll = typeName.Substring(0, p).Trim();
                    string type = typeName.Substring(p + 1).Trim();

                     System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dll);
                    result = assembly.CreateInstance(type);
                }
            }
            return result;
        }
    }
}
