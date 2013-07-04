using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.Dal
{
    public class StockOutDal : BaseDal
    {
        
        public void Delete()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.Delete();
            }
        }

        public int FindOutQuantity()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindOutQuantity();
            }
        }

        public void UpdateCigarette(string barcode, string CIGARETTECODE)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.UpdateCigarette(barcode,CIGARETTECODE);
            }
        }

        public DataTable FindAll()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindAll();
            }
        }



        public string FindMinStockOutID(string channelCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindMinStockOutID(channelCode);
            }
        }

        public DataTable FindCigaretteForScanner(string scannerCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindCigaretteForScanner(scannerCode);
            }
        }

        public DataTable FindCigaretteForSupplyCar(string supplyCarCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindCigaretteForSupplyCar(supplyCarCode);
            }
        }


        public DataTable FindLEDData(string channelCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindLEDData(channelCode);
            }
        }

        public void UpdateLEDStatus(string stockOutID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.UpdateLEDStatus(stockOutID);
            }
        }

        public void UpdateScanStatus(string outID, string scannerCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.UpdateScanStatus(outID, scannerCode);
            }
        }

        public void UpdateSupplyCarStatus(string outID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.UpdateSupplyCarStatus(outID);
            }
        }

        public void ClearNoScanData()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.ClearNoScanData();
            }
        }

        //zys_2011-10-06
        internal int FindMaxOutID()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindMaxOutID();
            }
        }

        //zys_2011-10-05
        internal void Insert(int outID, DataTable supplyTable)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.Insert(outID, supplyTable);
            }
        }

        //zys_2011-10-06
        public void UpdateStatus(DataTable table)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                outDao.UpdateStatus(table);
            }
        }

        //zys_2011-10-06
        public DataTable FindSupply()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                return outDao.FindSupply();
            }
        }
    }
}
