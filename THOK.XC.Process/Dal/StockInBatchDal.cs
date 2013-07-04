using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.Dal
{
    public class StockInBatchDal : BaseDal
    {
        public void Delete()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                batchDao.Delete();
            }
        }

        public int FindMaxBatchNo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                return batchDao.FindMaxBatchNo();
            }
        }

        public void InsertBatch(int batchNo, string channelCode, string cigaretteCode, string cigaretteName, int quantity, int StockRemainQuantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                batchDao.InsertBatch(batchNo, channelCode, cigaretteCode, cigaretteName, quantity, StockRemainQuantity);
            }
        }

        public DataTable FindAll()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                return batchDao.FindAll();
            }
        }

        public DataTable FindStockInTopAnyBatch()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                return batchDao.FindStockInTopAnyBatch();
            }
        }

        public DataTable FindStockInBatch(string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                return batchDao.FindStockInBatch(cigaretteCode);
            }
        }

        public void UpdateQuantityForBatch(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                batchDao.UpdateQuantityForBatch(batchNo);
            }
        }

        public void UpdateState(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao batchDao = new StockInBatchDao();
                batchDao.UpdateState(batchNo);
            }
        }
    }
}
