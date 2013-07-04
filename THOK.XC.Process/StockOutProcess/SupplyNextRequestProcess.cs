using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.XC.Process.Dao;
using THOK.MCP;
using THOK.Util;

namespace THOK.XC.Process.StockOutProcess
{
    class SupplyNextRequestProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                bool needNotify = false;

                string lineCode = stateItem.ItemName.Split("_"[0])[0];
                string channelGroup = stateItem.ItemName.Split("_"[0])[1];
                string channelType = stateItem.ItemName.Split("_"[0])[2];

                object obj = ObjectUtil.GetObject(stateItem.State);
                int sortNo = obj != null ? Convert.ToInt32(obj) : 0;

                if (sortNo==0)
                {
                    return;
                }

                sortNo = sortNo + Convert.ToInt32(Context.Attributes["SupplyAheadCount-" + lineCode + "-" + channelGroup + "-" + channelType]);

                needNotify = AddNextSupply(lineCode, channelGroup, channelType, sortNo);

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

        private bool AddNextSupply(string lineCode, string channelGroup, string channelType, int sortNo)
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

                    DataTable supplyTable = supplyDao.FindNextSupply(lineCode, channelGroup, channelType, sortNo);

                    if (supplyTable.Rows.Count != 0)
                    {
                        Logger.Info(string.Format("收到补货请求，分拣线 '{0}'，烟道组 '{1}'，烟道类型 '{2}'，流水号 '{3}'", lineCode, channelGroup, channelType, sortNo));
                        try
                        {
                            pm.BeginTransaction();

                            int batchNo = batchDao.FindMaxBatchNo() + 1;
                            batchDao.InsertBatch(batchNo, lineCode, channelGroup, channelType, sortNo, supplyTable.Rows.Count);

                            int outID = outDao.FindMaxOutID();
                            outDao.Insert(outID, supplyTable);

                            pm.Commit();
                            result = true;

                            Logger.Info("生成出库任务成功");
                        }
                        catch (Exception e)
                        {
                            Logger.Error("生成出库任务失败，原因：" + e.Message);
                            pm.Rollback();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("生成出库任务失败，原因：" + e.Message);
            }

            return result;
        }
    }
}
