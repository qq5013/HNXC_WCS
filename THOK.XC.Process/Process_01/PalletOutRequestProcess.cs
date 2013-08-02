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
            string TARGET_CODE = "";
            try
            {
                switch (stateItem.ItemName)
                {
                    case "01_1_158":
                        TARGET_CODE = "158";
                        break;
                    case "01_1_200":
                        TARGET_CODE = "200";
                        break;
                    default:
                        break;
                }




















                object sta = stateItem.State;
                if (sta == null) //申请
                { //空托盘组出库单，生成任务，及明细。

                    BillDal dal = new BillDal();
                    string Taskid = dal.CreatePalletOutBillTask(TARGET_CODE);
                    TaskDal task = new TaskDal();
                    DataTable dt = task.CraneTask(string.Format("TASK_ID='{0}'", Taskid));
                    WriteToProcess("CraneProcess", "PalletOutRequest", dt);
                }
                else // 空托盘组到达。
                {
                    TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
                    string[] strInfo = dal.GetTaskInfo(sta.ToString());
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                    dal.UpdateTaskState(strInfo[0], "2");

                    
                    //更新Bill_Master
                    BillDal billdal = new BillDal();
                    billdal.UpdateBillMasterFinished(strInfo[1]);
                  
                  
 
                }
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
