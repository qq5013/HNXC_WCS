using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;
namespace THOK.XC.Process.Process_02
{
    public class StockOutToUnpackLineProcess : AbstractProcess
    {

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
            * 二层出库到开包线
            *         
           */

            object obj = ObjectUtil.GetObject(stateItem.State);

            if (obj == null || obj.ToString() == "0")
                return;

            string[] StationState = new string[2];
            try
            {
               
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.StockOutToUnpackLineProcess，原因：" + e.Message);
            }
        }
    }
}
