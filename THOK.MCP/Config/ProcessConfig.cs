using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Config
{
    public class ProcessConfig
    {
        private string name;
        private string assembly;
        private string className;
        private List<ProcessItemConfig> items = new List<ProcessItemConfig>();
        private bool suspend = false;

        public bool Suspend
        {
            get { return suspend; }
        }

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

        public List<ProcessItemConfig> Items
        {
            get { return items; }
        }

        public ProcessConfig(string name, string assembly, string className, bool suspend)
        {
            this.name = name;
            this.assembly = assembly;
            this.className = className;
            this.suspend = suspend;
        }
    }
}
