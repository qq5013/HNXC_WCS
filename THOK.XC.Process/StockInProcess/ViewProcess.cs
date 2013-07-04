using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;

namespace THOK.XC.Process.StockInProcess
{
    public class ViewProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            THOK.MCP.View.ViewClickArgs e = (THOK.MCP.View.ViewClickArgs)stateItem.State;
            System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", e.DeviceClass, e.DeviceNo));
        }
    }
}
