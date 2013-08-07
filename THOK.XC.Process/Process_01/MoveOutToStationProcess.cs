using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class MoveOutToStationProcess : AbstractProcess
    {
        /*  处理事项：
         *  倒库烟包 122
         */
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
        }
    }
}
           
