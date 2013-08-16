using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class StockOutCarFinishProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 
             *  stateItem.ItemName ：
             *  Init - 初始化。
             *      FirstBatch - 生成第一批入库请求任务。
             *      StockInRequest - 根据请求，生成入库任务。
             * 
             *  stateItem.State ：参数 - 请求的卷烟编码。        
            */
            string FromStation="";
            string ToStation="";
            string WriteItem = "";
            try
            {
                switch (stateItem.ItemName)
                {
                    case "02_1_340":
                        FromStation = "340";
                        ToStation = "372";
                        WriteItem = "02_2_372";
                        break;
                    case "02_1_360":
                        FromStation = "360";
                        ToStation = "392";
                        WriteItem = "02_2_392";
                        break;
                        
                }

                string TaskNo = ((int)stateItem.State).ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEMNO=3", strValue[0]), "2");

                    //
                    DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strValue[0]));
                    if (dt.Rows.Count > 0)
                    {
                        int[] WriteValue = new int[3];
                        WriteValue[0] = (int)stateItem.State;
                        WriteValue[1] = int.Parse(ToStation);
                        WriteValue[2] = 1;
                        WriteToService("StockPLC_02",  WriteItem  + "_1", WriteValue);
                        string BarCode = "";//PRODUCT_BARCODE






                        WriteToService("StockPLC_02",  WriteItem  + "_2", BarCode);
                        WriteToService("StockPLC_02",  WriteItem  + "_3", 1);  
                    }
                    dal.UpdateTaskDetailStation(FromStation, ToStation, "1", string.Format("TASK_ID='{0}' AND ITEMNO=4", strValue[0]));
                }
            }

            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
