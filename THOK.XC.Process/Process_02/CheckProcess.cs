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
        private string TaskID = "";
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二楼出库条码校验
             * 生成入库单，入库作业。
            */
            try
            {
                object obj = ObjectUtil.GetObject(stateItem.State);
                if (obj == null || obj.ToString() == "0")
                    return;
                PalletBillDal Billdal = new PalletBillDal();
                TaskID = Billdal.CreatePalletInBillTask(false);

                //判断是否还有出库任务
                TaskDal dal = new TaskDal();
                DataTable dtTask = dal.TaskInfo("TASK_TYPE='22' AND STATE IN (0,1)");
                if (dtTask.Rows.Count > 0)
                {
                    PalletTime.Enabled = true;
                    PalletTime.Interval = 100000;
                    PalletTime.AutoReset = true;
                    PalletTime.Elapsed += new System.Timers.ElapsedEventHandler(PalletTime_Elapsed);
                    PalletTime.Start();
                }
                else
                {
                    string strWhere = string.Format("TASK_ID='{0}'", TaskID);
                    dal.AssignCellTwo(strWhere);//货位申请

                    strWhere = string.Format("WCS_TASK.TASK_ID='{0}' AND ITEM_NO=3", TaskID);
                    DataTable dt = dal.TaskCarDetail(strWhere);
                    WriteToProcess("CarProcess", "CarInRequest", dt);
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.CheckProcess，原因：" + e.Message);
            }
        }

        private void PalletTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int str = (int)WriteToService("", "");















            if (str >= 5)
            {
                string strWhere = string.Format("TASK_ID='{0}'", TaskID);
                TaskDal dal = new TaskDal();
                dal.AssignCellTwo(strWhere);//货位申请

                strWhere = string.Format("TASK_ID='{0}' AND TASK_TYPE='{1}'", TaskID, "21");
                DataTable dt = dal.TaskCarDetail(strWhere);
                WriteToProcess("CarProcess", "CarInRequest", dt);
                PalletTime.Enabled = false;
                PalletTime.Stop();

            }
        }
    }
}
