using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.Dal
{
    public class StockOutBatchDal : BaseDal
    {
        public void Delete()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutBatchDao batchDao = new StockOutBatchDao();
                batchDao.Delete();
            }
        }

        public DataTable FindAll()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutBatchDao batchDao = new StockOutBatchDao();
                return batchDao.FindAll();
            }
        }

        public DataTable FindBatch()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutBatchDao batchDao = new StockOutBatchDao();
                return batchDao.FindBatch();
            }
        }

        public void UpdateBatch(string batchNo, int quantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutBatchDao batchDao = new StockOutBatchDao();
                batchDao.UpdateBatch(batchNo, quantity);
            }
        }

        //zys_2011-10-06
        public int FindMaxBatchNo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutBatchDao batchDao = new StockOutBatchDao();
                return batchDao.FindMaxBatchNo();
            }
        }

        //zys_2011-10-06
        internal void InsertBatch(int batchNo, string lineCode, string channelGroup, string channelType, int sortNo, int quantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutBatchDao batchDao = new StockOutBatchDao();
                batchDao.InsertBatch(batchNo, lineCode, channelGroup, channelType, sortNo, quantity);
            }
        }
    }
}
