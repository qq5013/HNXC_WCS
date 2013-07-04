using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.MCP;
using THOK.Util;
using THOK.XC.Process.Dao;
using THOK.XC.Process.Util.LED2008;

namespace THOK.XC.Process.StockInProcess
{
    public class LEDProcess: AbstractProcess
    {
        private LEDUtil ledUtil = new LEDUtil();
        private Dictionary<int, string> isActiveLeds = new Dictionary<int, string>();

        public override void Release()
        {
            try
            {
                ledUtil.Release();
                base.Release();
            }
            catch (Exception e)
            {
                Logger.Error("LEDProcess 资源释放失败，原因：" + e.Message);
            }
        }

        public override void Initialize(Context context)
        {
            base.Initialize(context);

            Microsoft.VisualBasic.Devices.Network network = new Microsoft.VisualBasic.Devices.Network();
            string[] ledConfig = context.Attributes["IsActiveLeds"].ToString().Split(';');

            foreach (string led in ledConfig)
            {
                if (network.Ping(led.Split(',')[1]))
                {
                    isActiveLeds.Add(Convert.ToInt32(led.Split(',')[0]), led.Split(',')[1]);
                }
                else
                {
                    Logger.Error(Convert.ToInt32(led.Split(',')[0]) + "号LED屏故障，请检查！IP:[" + led.Split(',')[1] + "]");
                }
            }

            ledUtil.isActiveLeds = isActiveLeds;
        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             *  Init：初始化
             *  Refresh：刷新LED屏。
             *      ‘01’：一号屏 显示请求入库托盘信息
             *      ‘02’：二号屏 显示请求补货的混合烟道补货顺序信息
             */
            string cigaretteName = "";

            switch (stateItem.ItemName)
            {
                case "Refresh":
                    this.Refresh();
                    break;
                case "StockInRequestShow":
                    cigaretteName = Convert.ToString(stateItem.State);
                    this.StockInRequestShow(cigaretteName);
                    break;
                default:
                    if (stateItem.ItemName != string.Empty && stateItem.State is LedItem[])
                    {
                        Show(stateItem.ItemName,(LedItem[])stateItem.State);
                    }                    
                    break;
            }        
        }

        private void Refresh()
        {
            //刷新1号屏
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao stockInBatchDao = new StockInBatchDao();
                DataTable batchTable = stockInBatchDao.FindStockInTopAnyBatch();
                ledUtil.RefreshStockInLED(batchTable, "1");
            }
        }

        private void StockInRequestShow(string cigaretteName)
        {
            ledUtil.RefreshStockInLED("1",cigaretteName);
            Logger.Info("缺烟提醒：请入库" + cigaretteName);
        }

        internal void Show(string ledCode,LedItem[] ledItems)
        {
            ledUtil.Show(ledCode, ledItems);
        }
    }
}
 