using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.XC.Process.Dao;
using THOK.Util;

namespace THOK.XC.Process.Dal
{
    public class ServerDal : BaseDal
    {
        public DataTable FindBatch()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                return dao.FindBatch();
            }
        }

        public DataTable FindStockChannel(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                return dao.FindStockChannel(orderDate, batchNo);
            }
        }

        public DataTable FindMixChannel(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                return dao.FindMixChannel(orderDate, batchNo);
            }
        }

        public DataTable FindSupply(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                return dao.FindSupply(orderDate, batchNo);
            }
        }

        public DataTable FindChannelUSED(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                return dao.FindChannelUSED(orderDate, batchNo);
            }
        }

        /// <summary>
        /// 打印烟道补货报表查询语句,留烟问题未处理。
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="linecCode"></param>
        /// <returns></returns>
        public DataTable FindChannelUSED(string orderDate, string batchNo, string linecCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                return dao.FindChannelUSED(orderDate, batchNo, linecCode);
            }
        }

        public DataTable FindPrintBatchTable()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                return dao.FindPrintBatchTable();
            }
        }

        public void UpdateBatchStatus(string batchID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                dao.UpdateBatchStatus(batchID);
            }
        }

        public void UpdateCigaretteToServer(string barcode, string CIGARETTECODE)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ServerDao dao = new ServerDao();
                dao.UpdateCigaretteToServer(barcode, CIGARETTECODE);
            }
        }
    }
}
