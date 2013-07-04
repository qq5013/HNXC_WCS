using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.XC.Process.Dao;
using System.Data;
using System.Windows.Forms;
using THOK.Util;

namespace THOK.XC.Process.StockInProcess
{
    class StockInRequestProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 
             *  stateItem.ItemName ：
             *      Init - 初始化。
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
                        AddFirstBatch();
                        break;
                    case "StockInRequest":                        
                        cigaretteCode = Convert.ToString(stateItem.State);
                        StockInRequest(cigaretteCode);
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

        private void AddFirstBatch()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao supplyDao = new SupplyDao();              
                ChannelDao channelDao = new ChannelDao();

                DataTable cigaretteTable = supplyDao.FindCigarette();
                if (cigaretteTable.Rows.Count !=0)
                {
                    foreach (DataRow row in cigaretteTable.Rows)
                    {
                        DataTable channelTable = channelDao.FindChannelForCigaretteCode(row["CigaretteCode"].ToString());
                        int stockRemainQuantity = Convert.ToInt32(channelTable.Rows[0]["REMAINQUANTITY"]);

                        if (Convert.ToInt32(row["Quantity"]) + stockRemainQuantity >= Convert.ToInt32(Context.Attributes["StockInCapacityQuantity"]))
                        {
                            StockInRequest(row["CigaretteCode"].ToString(), Convert.ToInt32(Context.Attributes["StockInCapacityQuantity"]), stockRemainQuantity);
                        }
                        else if (Convert.ToInt32(row["Quantity"]) + stockRemainQuantity > 0)
                        {
                            StockInRequest(row["CigaretteCode"].ToString(), Convert.ToInt32(row["Quantity"]) + stockRemainQuantity, stockRemainQuantity);
                        }
                    }
                    Logger.Info("生产第一批次入库任务成功");
                    WriteToProcess("LEDProcess", "Refresh", null);
                }
            }
        }

        private void StockInRequest(string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao stockInBatchDao = new StockInBatchDao();
                SupplyDao supplyDao = new SupplyDao();
                ChannelDao channelDao = new ChannelDao();

                DataTable stockInBatchTable = stockInBatchDao.FindStockInBatch(cigaretteCode);

                DataTable channelTable = channelDao.FindChannelForCigaretteCode(cigaretteCode);
                int stockRemainQuantity = Convert.ToInt32(channelTable.Rows[0]["REMAINQUANTITY"]);

                DataTable cigaretteTable = supplyDao.FindCigarette(cigaretteCode, stockRemainQuantity.ToString());

                if (stockInBatchTable.Rows.Count == 0 && cigaretteTable.Rows.Count != 0 )
                {
                    DataRow row = cigaretteTable.Rows[0];

                    if (Convert.ToInt32(row["Quantity"]) >= Convert.ToInt32(Context.Attributes["StockInCapacityQuantity"]))
                    {
                        StockInRequest(row["CigaretteCode"].ToString(), Convert.ToInt32(Context.Attributes["StockInCapacityQuantity"]), 0);
                        Logger.Info(row["CigaretteName"].ToString() + "生成入库任务成功");
                    }
                    else if (Convert.ToInt32(row["Quantity"]) > 0)
                    {
                        StockInRequest(row["CigaretteCode"].ToString(), Convert.ToInt32(row["Quantity"]),0);
                        Logger.Info(row["CigaretteName"].ToString() + "生成入库任务成功");
                    }
                    
                    WriteToProcess("LEDProcess", "Refresh", null);
                }
            }
        }

        private void StockInRequest(string cigaretteCode, int quantity, int stockRemainQuantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao stockInBatchDao = new StockInBatchDao();
                StockInDao stockInDao = new StockInDao();
                ChannelDao channelDao = new ChannelDao();
                
                stockInBatchDao.SetPersistentManager(pm);
                stockInDao.SetPersistentManager(pm);
                channelDao.SetPersistentManager(pm);
                
                pm.BeginTransaction();
                try
                {
                    DataTable cigaretteTable = channelDao.FindChannelForCigaretteCode(cigaretteCode);

                    if (cigaretteTable.Rows.Count != 0)
                    {
                        DataRow row = cigaretteTable.Rows[0];
                        bool isStockIn = row["ISSTOCKIN"].ToString() == "1" ? true : false;

                        int batchNo = stockInBatchDao.FindMaxBatchNo() + 1;
                        stockInBatchDao.InsertBatch(batchNo, row["CHANNELCODE"].ToString(), cigaretteCode, row["CIGARETTENAME"].ToString(), quantity, isStockIn ? stockRemainQuantity : 0);
                        
                        int stockInID = stockInDao.FindMaxInID();
                        for (int i = 1; i <= quantity; i++)
                        {
                            stockInID = stockInID + 1;
                            stockInDao.Insert(stockInID, batchNo, row["CHANNELCODE"].ToString(), cigaretteCode, row["CIGARETTENAME"].ToString(), row["BARCODE"].ToString(), (isStockIn && stockRemainQuantity-- > 0) ? "1" : "0");
                        }

                        pm.Commit();
                        
                        try
                        {
                            using (PersistentManager pmWES = new PersistentManager("WESConnection"))
                            {
                                StockInBatchDao stockInBatchDaoWES = new StockInBatchDao();
                                stockInBatchDaoWES.SetPersistentManager(pmWES);
                                stockInBatchDaoWES.InsertBatch(batchNo, row["CHANNELCODE"].ToString(), cigaretteCode, row["CIGARETTENAME"].ToString(), quantity, isStockIn ? stockRemainQuantity : 0);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error("上传入库计划失败，详情："+ e.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    pm.Rollback();
                    Logger.Error("生成入库计划失败，详情：" + ex.Message);
                }
            }
        }
    }
}
