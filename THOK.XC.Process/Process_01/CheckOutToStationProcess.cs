using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class CheckOutToStationProcess : AbstractProcess
    {
           /*  处理事项：
            *  抽签烟包到达，195
            */
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {

        }
    }
}

