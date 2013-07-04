using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.MCP;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.StockOutProcess
{
    public class DataRequestProcess: AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    StockOutBatchDao stockOutBatchDao = new StockOutBatchDao();
                    StockOutDao stockOutDao = new StockOutDao();
                    StockInDao stockInDao = new StockInDao();
                    stockOutBatchDao.SetPersistentManager(pm);
                    stockOutDao.SetPersistentManager(pm);
                    stockInDao.SetPersistentManager(pm);

                    try
                    {
                        DataTable outTable = stockOutDao.FindSupply();
                        DataTable stockInTable = stockInDao.FindStockInForIsInAndNotOut();                        

                        if (outTable.Rows.Count > 0)
                        {
                            pm.BeginTransaction();

                            for (int i = 0; i < outTable.Rows.Count; i++)
                            {
                                DataRow[] stockInRows = stockInTable.Select(string.Format("CIGARETTECODE='{0}' AND STATE ='1' AND ( STOCKOUTID IS NULL OR STOCKOUTID = 0 )",
                                    outTable.Rows[i]["CIGARETTECODE"].ToString()), "STOCKINID");

                                if (stockInRows.Length <= Convert.ToInt32(Context.Attributes["StockInRequestRemainQuantity"]) + 1)
                                {
                                    WriteToProcess("StockInRequestProcess", "StockInRequest", outTable.Rows[i]["CIGARETTECODE"].ToString());
                                }
                                else if (stockInRows.Length > 0 && stockInRows.Length + Convert.ToInt32(stockInRows[0]["STOCKINQUANTITY"]) <= Convert.ToInt32(Context.Attributes["StockInCapacityQuantity"]) + 1)
                                {
                                    WriteToProcess("StockInRequestProcess", "StockInRequest", outTable.Rows[i]["CIGARETTECODE"].ToString());
                                }

                                if (stockInRows.Length > 0)
                                {
                                    stockInRows[0]["STOCKOUTID"] = outTable.Rows[i]["STOCKOUTID"].ToString();
                                    outTable.Rows[i]["STATE"] = 1;
                                }
                                else
                                {
                                    Logger.Error(string.Format("[{0}] [{1}] 库存不足！", outTable.Rows[i]["CIGARETTECODE"].ToString(), outTable.Rows[i]["CIGARETTENAME"].ToString()));
                                    WriteToProcess("LEDProcess", "StockInRequestShow", outTable.Rows[0]["CIGARETTENAME"]);
                                    break;
                                }
                            }

                            stockOutDao.UpdateStatus(outTable);
                            stockInDao.UpdateStockOutIdToStockIn(stockInTable);

                            pm.Commit();
                            Logger.Info("处理出库数据成功。");
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error("处理出库数据失败，原因：" + e.Message);
                        pm.Rollback();
                    }
                }                
            }
            catch (Exception e)
            {
                Logger.Error("处理出库数据失败，原因：" + e.Message);
            }
        }
    }
}
