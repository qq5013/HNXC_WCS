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
        public void UpdateCellOutUnLock(string strCell)
        {
            CellDao dao = new CellDao();
            dao.UpdateCellOutUnLock(strCell);
        }

        /// <summary>
        /// 入库---更新货位储存信息。
        /// </summary>
        public void UpdateCellInLock()
        {
            CellDao dao = new CellDao();
            dao.UpdateCellInLock("");
        }

        /// <summary>
        /// 货位锁定
        /// </summary>
        public void UpdateCellLock(string strCell)
        {
            CellDao dao = new CellDao();
            dao.UpdateCellLock(strCell);
        }

        /// <summary>
        /// 更新货位新的RFID,及出库错误标志。
        /// </summary>
        /// <param name="NewPalletCode"></param>
        public void UpdateCellNewPalletCode(string CellCode, string NewPalletCode)
        {
            CellDao dao = new CellDao();
            dao.UpdateCellNewPalletCode(CellCode, NewPalletCode);
        }

    }
}
