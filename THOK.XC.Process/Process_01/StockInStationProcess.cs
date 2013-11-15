using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class StockInStationProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  
             * 一楼入库到达入库站台。
            */
            try
            {
                object obj =  ObjectUtil.GetObject(stateItem.State);
                if (obj == null || obj.ToString() == "0")
                    return;

                switch (stateItem.ItemName)
                {
                    case "01_1_136":
                        break;
                    case "01_1_148":
                        break;
                    case "01_1_152":
                        break;
                    case "01_1_170":
                        break;
                    case "01_1_178":
                        break;
                    case "01_1_186":
                        break;
                }
                string TaskNo = obj.ToString().PadLeft(4, '0'); //读取PLC任务号。
                TaskDal taskDal = new TaskDal();
                string[] TaskInfo = taskDal.GetTaskInfo(TaskNo);
                DataTable dt = taskDal.TaskInfo(string.Format("TASK_ID='{0}'", TaskInfo[0]));
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string taskType = dt.Rows[0]["TASK_TYPE"].ToString();
                    string ItemNo = "0";
                    string NextItemNo="1";
                    string CellCode = "";
                    switch(taskType)
                    {
                        case "11":
                              ItemNo = "2";
                            NextItemNo = "3";
                            CellCode = dr["CELL_CODE"].ToString();
                            break;
                        case "14":
                            ItemNo = "2";
                            NextItemNo = "3";
                            CellCode = dr["NEWCELL_CODE"].ToString();
                            break;
                        case "13":
                            ItemNo = "3";
                            NextItemNo = "4";
                            CellCode = dr["CELL_CODE"].ToString();
                            break;

                    }

                    taskDal.UpdateTaskDetailState(string.Format("TASK_NO='{0}' AND ITEM_NO='{1}'", TaskNo, ItemNo), "2");

                    SysStationDal sysdal = new SysStationDal();
                    DataTable dtstation = sysdal.GetSationInfo(CellCode, "11");
                    taskDal.UpdateTaskDetailCrane(dtstation.Rows[0]["STATION_NO"].ToString(), CellCode, "0", dtstation.Rows[0]["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO={1}", TaskInfo[0],NextItemNo));//更新调度堆垛机的其实位置及目标地址。

                    string strWhere = string.Format("TASK_NO='{0}'AND DETAIL.STATE='0' and ITEM_NO='{1}'", TaskNo, NextItemNo);
                    DataTable dtInCrane = taskDal.TaskCraneDetail(strWhere);
                    if (dtInCrane.Rows.Count > 0)
                        WriteToProcess("CraneProcess", "CraneInRequest", dtInCrane);
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.StockInStationProcess：" + e.Message);
            }
        }
    }
}
