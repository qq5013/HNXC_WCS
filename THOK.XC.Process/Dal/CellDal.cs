using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.XC.Process.Dao;
using System.Data;
namespace THOK.XC.Process.Dal
{
    public class CellDal : BaseDal
    {
        /// <summary>
        /// 出库-- 货位解锁
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellOutFinishUnLock(string strCell)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CellDao dao = new CellDao();
                dao.UpdateCellOutFinishUnLock(strCell);
            }
        }

        /// <summary>
        /// 入库---解除货位锁定，更新货位储存信息。
        /// </summary>
        public void UpdateCellInFinishUnLock(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CellDao dao = new CellDao();
                dao.UpdateCellInFinishUnLock(TaskID);
            }
        }

        /// <summary>
        /// 货位锁定
        /// </summary>
        public void UpdateCellLock(string strCell)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CellDao dao = new CellDao();
                dao.UpdateCellLock(strCell);
            }
        }

        /// <summary>
        /// 更新货位新的RFID,及出库错误标志。
        /// </summary>
        /// <param name="NewPalletCode"></param>
        public void UpdateCellNewPalletCode(string CellCode, string NewPalletCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CellDao dao = new CellDao();
                dao.UpdateCellNewPalletCode(CellCode, NewPalletCode);
            }
        }

        /// <summary>
        /// 更新货位错误标志，错误内容
        /// </summary>
        /// <param name="NewPalletCode"></param>
        public void UpdateCellErrFlag(string CellCode, string ErrMsg)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CellDao dao = new CellDao();
                dao.UpdateCellErrFlag(CellCode, ErrMsg);
            }
        }

        public DataTable GetCellInfo(string CellCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CellDao dao = new CellDao();
                return dao.GetCellInfo(CellCode);
            }
        }

        public DataTable GetCell()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CellDao cellDao = new CellDao();
                return cellDao.Find();
            }
        }

    }
}
