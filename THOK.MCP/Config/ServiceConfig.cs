using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Config
{
    public class ServiceConfig
    {
        private string name;
        private string assembly;
        private string className;
        private string fileName;

        public string Name
        {
            get { return name; }
        }

        public string Assembly
        {
            get { return assembly; }
        }

        public string ClassName
        {
            get { return className; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public ServiceConfig(string name, string assembly, string className, string fileName)
        {
            this.name = name;
            this.assembly = assembly;
            this.className = className;
            this.fileName = fileName;
        }
    }
}
