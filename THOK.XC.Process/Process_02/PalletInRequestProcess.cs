using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;

namespace THOK.XC.Process.Process_02
{
    public class PalletInRequestProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二楼空托盘组入库
             * 生成入库单，入库作业。
            */
            try
            {

                
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
