using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class PalletOutToStationProcess : AbstractProcess
    {
            /*  处理事项：
             *  空托盘组出库到达158，200
            */
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string writeItem = "";
                switch (stateItem.ItemName)
                {
                    case "01_1_158_2":
                        writeItem = "01_1_158_3";
                        break;
                    case "01_1_200_2":
                        writeItem = "01_1_200_3";
                        break;
                }
                string TaskNo = ((int)stateItem.State).ToString().PadLeft(4, '0');
                
                TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
                string[] strInfo = dal.GetTaskInfo(TaskNo);
                if (!string.IsNullOrEmpty(strInfo[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                    dal.UpdateTaskState(strInfo[0], "2");

                    PalletBillDal billdal = new PalletBillDal();
                    billdal.UpdateBillMasterFinished(strInfo[1]);
                    WriteToService("StockPLC_01", writeItem, 1); //通知电控，空托盘组到达158,200       
                }
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }

}
