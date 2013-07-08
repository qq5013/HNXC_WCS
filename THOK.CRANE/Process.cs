using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;

namespace THOK.CRANE
{
    public class Process : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             *  Init：初始化
             *  Refresh：刷新LED屏。
             *      ‘01’：一号屏 显示请求入库托盘信息
             *      ‘02’：二号屏 显示请求补货的混合烟道补货顺序信息
             */

            switch (stateItem.ItemName)
            {
                case "Refresh":
                    
                    break;
                case "StockInRequestShow":
                    
                    break;
                default:
                    
                    break;
            }
        }
    }
}
