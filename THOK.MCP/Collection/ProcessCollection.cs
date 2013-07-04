using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Collection
{
    public class ProcessCollection
    {
        private Dictionary<string, IProcess> processes = new Dictionary<string, IProcess>();

        ~ProcessCollection()
        {
            foreach (IProcess process in processes.Values)
            {
                process.Stop();
                process.Release();
            }
            processes.Clear();
        }

        internal void Add(IProcess process)
        {
            if (process.Name == null)
                process.Name = string.Format("Process{0}", Environment.TickCount);

            string key = process.Name.ToUpper();

            if (!processes.ContainsKey(key))
                processes.Add(key, process);
        }

        
        public IProcess this[string processName]
        {
            get
            {
                string key = processName.ToUpper();

                if (processes.ContainsKey(key))
                    return processes[processName.ToUpper()];
                else
                    return null;
            }
        }

        
    }
}
