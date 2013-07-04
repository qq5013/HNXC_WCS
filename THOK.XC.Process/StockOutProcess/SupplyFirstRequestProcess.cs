using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.MCP;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.StockOutProcess
{
    public class SupplyFirstRequestProcess: AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                bool needNotify = false;
                switch (stateItem.ItemName)
                {
                    case "FirstBatch":
                        needNotify = AddFirstSupply();                     
                        break;
                }
                if (needNotify)
                {
                    WriteToProcess("LedStateProcess", "Refresh", null);
                    WriteToProcess("ScannerStateProcess", "Refresh", null);
                    dispatcher.WriteToProcess("DataRequestProcess", "SupplyRequest", 1);
                }
            }
            catch (Exception e)
            {
                Logger.Error("补货批次生成处理失败，原因：" + e.Message);
            }
        }

        private bool AddFirstSupply()
        {
            bool result = false;
            try
            {                
                using (PersistentManager pm = new PersistentManager())
                {
                    StockOutBatchDao batchDao = new StockOutBatchDao();
                    SupplyDao supplyDao = new SupplyDao();
                    StockOutDao outDao = new StockOutDao();
                    batchDao.SetPersistentManager(pm);
                    supplyDao.SetPersistentManager(pm);
                    outDao.SetPersistentManager(pm);

                    DataTable supplyTable = supplyDao.FindFirstSupply();

                    if (supplyTable.Rows.Count != 0)
                    {
                        try
                        {
                            pm.BeginTransaction();

                            int batchNo = batchDao.FindMaxBatchNo() + 1;         
                            batchDao.InsertBatch(batchNo, "00", "0", "0", 0, supplyTable.Rows.Count);

                            int outID = outDao.FindMaxOutID();
                            outDao.Insert(outID, supplyTable);

                            pm.Commit();
                            result = true;                            

                            Logger.Info("生产第一批次出库任务成功");
                        }
                        catch (Exception e)
                        {
                            Logger.Error("生成第一批次出库任务失败，原因：" + e.Message);
                            pm.Rollback();
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error("生成第一批次出库任务失败，原因：" + ee.Message);
            }

            return result;
        }        
    }
}
