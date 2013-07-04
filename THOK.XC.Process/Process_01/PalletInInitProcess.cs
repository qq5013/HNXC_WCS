using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;

namespace THOK.XC.Process.Process_01
{
    public class StockInRequestProcess : AbstractProcess
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
            string cigaretteCode = "";
            try
            {
                switch (stateItem.ItemName)
                {
                    case "Init":
                        break;
                    case "FirstBatch":
                        //AddFirstBatch();
                        break;
                    case "StockInRequest":
                        cigaretteCode = Convert.ToString(stateItem.State);
                        //StockInRequest(cigaretteCode);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
