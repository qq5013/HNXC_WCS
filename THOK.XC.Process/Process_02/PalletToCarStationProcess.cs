using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class PalletToCarStationProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 
             *  stateItem.ItemName ：
             * 
             *  stateItem.State ：参数 - 任务号。        
            */
            object obj = ObjectUtil.GetObject(stateItem.State);

            if (obj == null || obj.ToString() == "0")
                return;

            string TaskNo = obj.ToString().PadLeft(4, '0');
            string FromStation = "";
            string WriteItem = "";
            string ToStation = "";
            try
            {
                switch (stateItem.ItemName)
                {
                    case "02_1_301":
                        FromStation = "301";
                        ToStation = "302";
                        WriteItem = "02_2_301";
                        break;
                    case "02_1_305":
                        FromStation = "305";
                        ToStation = "306";
                        WriteItem = "02_2_305";
                        break;
                    case "02_1_309":
                        FromStation = "309";
                        ToStation = "310";
                        WriteItem = "02_2_309";
                        break;
                    case "02_1_313":
                        FromStation = "313";
                        ToStation = "314";
                        WriteItem = "02_2_313";
                        break;
                    case "02_1_317":
                        FromStation = "317";
                        ToStation = "318";
                        WriteItem = "02_2_317";
                        break;
                    case "02_1_323":
                        FromStation = "323";
                        ToStation = "324";
                        WriteItem = "02_2_323";
                        break;

                }
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);
                DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strValue[0]));
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    string strWhere = string.Format("TASK_ID='{0}' AND ITEM_NO=2", strValue[0]);
                    dal.UpdateTaskDetailState(strWhere, "2"); //更新小车任务完成
                    strWhere = string.Format("TASK_ID='{0}' AND ITEM_NO=3", strValue[0]);
                    dal.UpdateTaskDetailStation(FromStation, ToStation, "1", strWhere);//更新货物到达入库站台明细
                    int[] writestate = new int[3];
                    writestate[0] = int.Parse(TaskNo);
                    writestate[1] = int.Parse(ToStation);
                    writestate[2] = 2;
                    WriteToService("StockPLC_02", WriteItem + "_1", writestate);
                    string barcode = "";
                    string palletcode = "";
                    if (dt.Rows[0]["PRODUCT_CODE"].ToString() != "0000") //
                    {
                        barcode = dt.Rows[0]["PRODUCT_BARCODE"].ToString();
                        palletcode = dt.Rows[0]["PALLET_CODE"].ToString();
                    }
                    sbyte [] b=new sbyte[90];
                    Common.ConvertStringChar.stringToBytes(barcode, 40).CopyTo(b, 0);
                    Common.ConvertStringChar.stringToBytes(palletcode, 50).CopyTo(b, 40);
                    WriteToService("StockPLC_02", WriteItem + "_2", b);

                    WriteToService("StockPLC_02", WriteItem + "_3", 1);
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.PalletToCarStationProcess，原因：" + e.Message);
            }
        }
    }
}
