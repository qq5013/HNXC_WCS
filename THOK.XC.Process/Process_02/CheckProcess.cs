using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class CheckProcess : AbstractProcess
    {
        private System.Timers.Timer PalletTime = new System.Timers.Timer();
         
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二楼出库条码校验
             *  
            */
            try
            {
                object[] obj = ObjectUtil.GetObjects(stateItem.State);
                if (obj[0] == null || obj[0].ToString() == "0")
                    return;

                string TaskNo = obj[0].ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);

                string WriteItem = "";
                string ReadItem = "";
                switch (stateItem.ItemName)
                {
                    case "02_1_340_1":
                        WriteItem = "02_2_340";
                        ReadItem = "02_1_340_2";
                        break;
                    case "02_1_360_1":
                        WriteItem = "02_2_360";
                        ReadItem = "02_1_360_2";
                        break;

                }
                if (obj[1].ToString() == "1")
                {
                    string BarCode = Common.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(WriteToService("StockPLC_02", ReadItem)));
                    dal.UpdateTaskCheckBarCode(strValue[0], BarCode);
                }
                WriteToService("StockPLC_02", WriteItem + "_3", 1);


            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.CheckProcess，原因：" + e.Message);
            }
        }

    }
}
