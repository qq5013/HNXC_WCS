using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class CellDao : BaseDao
    {
        /// <summary>
        /// 出库-- 货位解锁
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellOutUnLock(string strCell)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='0',PRODUCT_CODE='',SCHEDULE_NO='',REAL_WEIGHT=0,PALLET_CODE='',BILL_NO='',IN_DATE=NULL WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);

        }

        /// <summary>
        /// 入库-- 更新货位存储信息
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellInLock(string strCell)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='0',PRODUCT_CODE='',SCHEDULE_NO='',REAL_WEIGHT=0,PALLET_CODE='',BILL_NO='',IN_DATE=NULL WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);

        }

        /// <summary>
        /// 货位锁定
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellLock(string strCell)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1' WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);
        }


        public void UpdateCellNewPalletCode(string CellCode, string NewPalletCode)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET NEW_PALLET_CODE ='{0}',ERROR_FLAG='1' WHERE CELL_CODE='{1}' ", NewPalletCode, CellCode);
            ExecuteNonQuery(strSQL);
        }
    }
}
