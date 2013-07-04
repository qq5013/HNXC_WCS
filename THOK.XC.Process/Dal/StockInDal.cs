using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.Dal
{
    public class StockInDal : BaseDal
    {
        public void Delete()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                dao.Delete();
            }
        }

        public int FindMaxInID()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                return dao.FindMaxInID();
            }
        }

        public void Insert(int stockInID, int batchNo, string channelCode, string cigaretteCode, string cigaretteName, string barode, string state)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                dao.Insert(stockInID, batchNo, channelCode, cigaretteCode, cigaretteName, barode, state);
            }
        }

        public DataTable FindAll()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                return dao.FindAll();
            }
        }

        public DataTable FindCigarette()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                return dao.FindCigarette();
            }
        }

        public DataTable FindCigarette(string barcode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                return dao.FindCigarette(barcode);
            }
        }



        public void UpdateScanStatus(string stockInID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                dao.UpdateScanStatus(stockInID);
            }
        }

        //zys_2011-10-06
        public void UpdateStockOutIdToStockIn(DataTable table)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                dao.UpdateStockOutIdToStockIn(table);
            }
        }

        //zys_2011-10-06
        public DataTable FindStockInForIsInAndNotOut()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao dao = new StockInDao();
                return dao.FindStockInForIsInAndNotOut();
            }
        }
    }
}
