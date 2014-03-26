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

                    case "02_1_475":
                        WriteItem = "02_2_475";
                        break;
                    case "02_1_440":
                        WriteItem = "02_2_440";
                        break;
                    case "02_1_412":
                        WriteItem = "02_2_412";
                        break;
                }
                string TaskNo = obj.ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=5", strValue[0]), "2"); //更新
                    ChannelDal cDal = new ChannelDal();
                    cDal.UpdateOutChannelTime(strValue[0]);

                    WriteToService("StockPLC_02", WriteItem, 1);
                }

                //读取码盘机是否处于，申请位；
                string SeparateItem = "";
                object objSeparate = ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_372_1"));
                if (objSeparate.ToString() != "0")
                    SeparateItem = "02_1_372_1";
                else
                {

                    objSeparate = ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_392_1"));
                    if (objSeparate.ToString() != "0")
                        SeparateItem = "02_1_392_1";
                }
                if (SeparateItem != "")
                {
                    WriteToProcess("StockOutSeparateProcess", SeparateItem, 1);
                }
                    
               
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.StockOutCacheProcess，原因：" + e.Message);
            }
        }
    }
}
