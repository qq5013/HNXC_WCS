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
            object obj = ObjectUtil.GetObject(stateItem.State);

            if (obj == null || obj.ToString() == "0")
                return;

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
                        WriteItem = "02_2_340";
                        break;
                    case "02_1_360":
                        FromStation = "360";
                        ToStation = "392";
                        WriteItem = "02_2_360";
                        break;
                        
                }

                string TaskNo = obj.ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=3", strValue[0]), "2");

                    //
                    DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strValue[0]));
                    if (dt.Rows.Count > 0)
                    {
                        int[] WriteValue = new int[3];
                        WriteValue[0] = int.Parse(FromStation);
                        WriteValue[1] = int.Parse(ToStation);
                        WriteValue[2] = 1;
                        WriteToService("StockPLC_02", WriteItem + "_1", WriteValue);

                        string barcode = dt.Rows[0]["PRODUCT_BARCODE"].ToString();
                        string palletcode = dt.Rows[0]["PALLET_CODE"].ToString();

                        sbyte[] b = new sbyte[190];
                        Common.ConvertStringChar.stringToBytes(barcode, 80).CopyTo(b, 0);
                        Common.ConvertStringChar.stringToBytes(palletcode, 110).CopyTo(b, 80);

                        WriteToService("StockPLC_02", WriteItem + "_2", b);
                        WriteToService("StockPLC_02", WriteItem + "_3", 1);
                    }
                    dal.UpdateTaskDetailStation(FromStation, ToStation, "1", string.Format("TASK_ID='{0}' AND ITEM_NO=4", strValue[0]));
                }
            }

            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.StockOutCarFinishProcess，原因：" + e.Message);
            }
        }
    }
}
