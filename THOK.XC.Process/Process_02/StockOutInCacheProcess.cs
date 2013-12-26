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

            object obj = ObjectUtil.GetObject(stateItem.State);

            if (obj == null || obj.ToString() == "0")
                return;

            string WriteItem = "";
            string ChannelNo = "";
            object objChannel;
            string strReadItem = "";
            try
            {
                switch (stateItem.ItemName)
                {

                    case "02_1_378_1":
                        WriteItem = "02_2_378";
                        ChannelNo = "415";
                        strReadItem = "02_1_378_2";
                        break;
                    case "02_1_381_1":
                        WriteItem = "02_2_381";
                        ChannelNo = "431";
                        strReadItem = "02_1_381_2";
                        break;
                    case "02_1_383_1":
                        WriteItem = "02_2_383";
                        ChannelNo = "438";
                        strReadItem = "02_1_383_2";
                        break;
                    case "02_1_385_1":
                        WriteItem = "02_2_385";
                        ChannelNo = "461";
                        strReadItem = "02_1_385_2";
                        break;
                    case "02_1_387_1":
                        WriteItem = "02_2_387";
                        ChannelNo = "465";
                        strReadItem = "02_1_387_2";
                        break;
                    case "02_1_389_1":
                        WriteItem = "02_2_389";
                        ChannelNo = "471";
                        strReadItem = "02_1_389_2";
                        break;
                }
                string TaskNo = obj.ToString().PadLeft(4, '0');
                objChannel = ObjectUtil.GetObject(WriteToService("StockPLC_02", strReadItem));

                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);

                if (!string.IsNullOrEmpty(strValue[0]))
                {
                     ChannelDal Cdal = new ChannelDal();

                     ChannelNo = Cdal.GetChannelFromTask(TaskNo, strValue[1]);

                     dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=5", strValue[0]), "2"); //更新
                     if (objChannel.ToString() == ChannelNo) //返回正确的缓存道。
                     {
                         int value = Cdal.UpdateInChannelTime(strValue[0], strValue[1], ChannelNo);
                         WriteToService("StockPLC_02", WriteItem + "_1", value);
                         WriteToService("StockPLC_02", WriteItem + "_2", 1);
                     }
                     else
                     {
                         int value = Cdal.UpdateInChannelAndTime(strValue[0], strValue[1], objChannel.ToString());
                         WriteToService("StockPLC_02", WriteItem + "_1", value);
                         WriteToService("StockPLC_02", WriteItem + "_2", 2);
                     }
                }
                
               
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.StockOutInCacheProcess，原因：" + e.Message);
            }
        }
    }
}
