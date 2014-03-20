using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;


namespace THOK.XC.Process.Process_02
{
    public class PalletToStationProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 
             *  stateItem.ItemName ：
             *  空托盘组，从小车站台到达入库站台。
             *  stateItem.State ：参数 - 任务号。        
            */
            object obj = ObjectUtil.GetObject(stateItem.State);
            if (obj == null || obj.ToString() == "0")
                return;

            string TaskNo = obj.ToString().PadLeft(4, '0');
            try
            {
                switch (stateItem.ItemName)
                {
                    case "02_1_302":
                        break;
                    case "02_1_306":
                        break;
                    case "02_1_310":
                        break;
                    case "02_1_314":
                        break;
                    case "02_1_318":
                        break;
                    case "02_1_324":
                        break;
                    default:
                        break;
                }
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);

                if (!string.IsNullOrEmpty(strValue[1]))
                {
                    DataTable dtTask = dal.TaskInfo(string.Format("TASK_ID='{0}'", strValue[0]));
                    string CellCode = dtTask.Rows[0]["CELL_CODE"].ToString();
                    //更新小车站台到达入库站台任务完成。
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO='3'", strValue[0]), "2");
                    SysStationDal sysdal = new SysStationDal();
                    DataTable dtstation = sysdal.GetSationInfo(CellCode, "21","4");
                    //更新调度堆垛机的起始位置及目标地址。
                    dal.UpdateTaskDetailCrane(dtstation.Rows[0]["STATION_NO"].ToString(), CellCode, "0", dtstation.Rows[0]["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO=4", strValue[0]));

                    DataTable dt = dal.CraneTaskIn(string.Format("TASK.TASK_ID='{0}' and ITEM_NO='4'", strValue[0]));
                    if (dt.Rows.Count > 0)
                    {
                        WriteToProcess("CraneProcess", "CraneInRequest", dt);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.PalletToStationProcess, 原因：" + e.Message);
            }
        }
    }
}
