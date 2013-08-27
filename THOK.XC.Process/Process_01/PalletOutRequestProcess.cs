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

            int intRequest = (int)stateItem.State;
          
            if (intRequest == 0) //申请位为0
                return;
            string TARGET_CODE = "";
            try
            {
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
                WriteToProcess("CraneProcess", "PalletOutRequest", dt);

            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
