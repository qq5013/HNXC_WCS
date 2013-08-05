using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class StockOutToStationProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 货物到达指定位置
            */

            try
            {

                switch (stateItem.ItemName)
                {
                    case "01_1_158_2":

                        break;
                    case "01_1_200_2":

                        break;
                    case "01_1_195":

                        break;
                    case "01_1_122":

                        break;
                    default:
                        break;
                }
                object sta = "";

                TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
                string[] strInfo = dal.GetTaskInfo(sta.ToString());
                dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                dal.UpdateTaskState(strInfo[0], "2");

                BillDal billdal = new BillDal();
                billdal.UpdateBillMasterFinished(strInfo[1]);


                //更新Bill_Master
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
