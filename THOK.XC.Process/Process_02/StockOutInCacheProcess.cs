using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class StockOutInCacheProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二层小车进入缓存站台
            */

            object[] obj = ObjectUtil.GetObjects(stateItem.State);

            if (obj[0] == null || obj[0].ToString() == "0")
                return;

            string WriteItem = "";
            string ChannelNo = "";
            try
            {
                switch (stateItem.ItemName)
                {

                    case "02_1_378":
                        WriteItem = "02_2_378";
                        ChannelNo = "415";
                        break;
                    case "02_1_381":
                        WriteItem = "02_2_381";
                        ChannelNo = "431";
                        break;
                    case "02_1_383":
                        WriteItem = "02_2_383";
                        ChannelNo = "438";
                        break;
                    case "02_1_385":
                        WriteItem = "02_2_385";
                        ChannelNo = "461";
                        break;
                    case "02_1_387":
                        WriteItem = "02_2_387";
                        ChannelNo = "465";
                        break;
                    case "02_1_389":
                        WriteItem = "02_2_389";
                        ChannelNo = "471";
                        break;
                }
                string TaskNo = obj[0].ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);
                if (!string.IsNullOrEmpty(strValue[0]))
                {

                    ChannelDal Cdal = new ChannelDal();
                    int value = Cdal.UpdateInChannelTime(strValue[0], strValue[1], ChannelNo);

                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=5", strValue[0]), "2"); //更新
                    WriteToService("StockPLC_02", WriteItem + "_1", value);
                    WriteToService("StockPLC_02", WriteItem + "_2", 1);
                }
                
               
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.StockOutInCacheProcess，原因：" + e.Message);
            }
        }
    }
}
