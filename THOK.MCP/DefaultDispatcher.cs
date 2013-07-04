using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public sealed class DefaultDispatcher: IServiceDispatcher, IProcessDispatcher
    {
        public bool WriteToService(string serverName, string itemName, object state)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[Ð´Service] [{0} {1} {2}]", serverName, itemName, state));
            return true;
        }

        public object WriteToService(string serverName, string itemName)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[¶ÁService] [{0} {1}]", serverName, itemName));
            return -1;
        }

        public void WriteToProcess(string processName, string itemName, object state)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[Ð´Process] [{0} {1} {2}]", processName, itemName, state));
        }

        public void DispatchState(StateItem item)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[·ÖÅÉ×´Ì¬] [{0} {1} {2}]", item.Name, item.ItemName, item.State));
        }
    }
}
