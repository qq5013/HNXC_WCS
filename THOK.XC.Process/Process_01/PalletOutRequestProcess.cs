using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class PalletOutRequestProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 空托盘组出库申请，
             * 空托盘组到达指定出库位置。
             *  
            */


            try
            {
                object obj = ObjectUtil.GetObject(stateItem.State);
                if (obj == null || obj.ToString() == "0")
                    return;
               
                string TARGET_CODE = "";
                switch (stateItem.ItemName)
                {
                    case "01_1_158_1":
                        TARGET_CODE = "158";
                        break;
                    case "01_1_200_1":
                        TARGET_CODE = "200";
                        break;
                    default:
                        break;
                }
                PalletBillDal dal = new PalletBillDal();
                string Taskid = dal.CreatePalletOutBillTask(TARGET_CODE);
                TaskDal task = new TaskDal();
                DataTable dt = task.CraneOutTask(string.Format("TASK_ID='{0}'", Taskid));
                if (dt.Rows.Count > 0)
                    WriteToProcess("CraneProcess", "CraneInRequest", dt);

            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.PalletOutRequestProcess：" + e.Message);
            }
        }
    }
}
