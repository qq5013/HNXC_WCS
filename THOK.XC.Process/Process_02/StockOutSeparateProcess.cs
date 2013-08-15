using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class StockOutSeparateProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二层出库拆盘完成
             *  stateItem.State ：参数 - 请求的卷烟编码。        
            */
           
            try
            {
                switch (stateItem.ItemName)
                {
                    case "02_1_372":
                        break;
                    case "02_1_392":
                        break;
                }
                string TaskNo = ((int)stateItem.State).ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskInfo(TaskNo);
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEMNO=4", strValue[0]), "2");
                    //分配缓存到






                    //
                    WriteToService("", "", "");






                
                }
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
