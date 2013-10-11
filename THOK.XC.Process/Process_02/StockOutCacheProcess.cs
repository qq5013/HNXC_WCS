using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class StockOutCacheProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二层小车进入缓存站台
            */
            object obj = ObjectUtil.GetObject(stateItem.State);

            if (obj == null || obj.ToString() == "0")
                return;
            string WriteItem = ""; 
            try
            {
                switch (stateItem.ItemName)
                {

                    case "02_1_378":
                        WriteItem = "02_2_378";
                        break;
                    case "02_1_381":
                        WriteItem = "02_2_381";
                        break;
                    case "02_1_383":
                        WriteItem = "02_2_383";
                        break;
                    case "02_1_385":
                        WriteItem = "02_2_385";
                        break;
                    case "02_1_387":
                        WriteItem = "02_2_387";
                        break;
                    case "02_1_389":
                        WriteItem = "02_2_389";
                        break;
                }
                string TaskNo = obj.ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEMNO=5", strValue[0]), "2"); //更新
                    dal.UpdateTaskState(strValue[0], "2"); //更新任务
                    WriteToService("StockPLC_02", WriteItem, 1);
                }
                
               
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.StockOutCacheProcess，原因：" + e.Message);
            }
        }
    }
}
