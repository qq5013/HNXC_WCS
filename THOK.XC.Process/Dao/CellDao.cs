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
        public void UpdateCellOutFinishUnLock(string strCell)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='0',PRODUCT_CODE='',SCHEDULE_NO='',REAL_WEIGHT=0,PALLET_CODE='',BILL_NO='',IN_DATE=NULL WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);

        }

        /// <summary>
        /// 入库-- 更新货位存储信息
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellInFinishUnLock(string TaskID)
        {
            string strSQL = string.Format("UPDATE CMD_CELL CELL " +
                            "SET (PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,IN_DATE,IS_LOCK)= " +
                            "(SELECT PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,SYSDATE AS IN_DATE,'0' FROM WCS_TASK TASK WHERE CELL.CELL_CODE=TASK.CELL_CODE AND TASK.TASK_ID='{0}' ) " +
                            "WHERE EXISTS(SELECT 1 FROM WCS_TASK T WHERE CELL.CELL_CODE=T.CELL_CODE AND T.TASK_ID='{0}' ) ", TaskID);
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
            string strSQL = string.Format("UPDATE CMD_CELL SET MEMO='货位RFID信息不匹配', NEW_PALLET_CODE ='{0}',ERROR_FLAG='1' WHERE CELL_CODE='{1}' ", NewPalletCode, CellCode);
            ExecuteNonQuery(strSQL);
        }

        public void UpdateCellErrFlag(string CellCode, string ErrMsg)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET MEMO='{0}',ERROR_FLAG='1' WHERE CELL_CODE='{1}' ", ErrMsg, CellCode);
            ExecuteNonQuery(strSQL);
        }
        public DataTable GetCellInfo(string CellCode)
        {
            string strSQL = string.Format("SELECT * FROM CMD_CELL WHERE CELL_CODE='{1}' ", CellCode);
            return ExecuteQuery(strSQL).Tables[0];
        }

        public DataTable Find()
        {
            string sql = "select * from cmd_cell A "+
                          "LEFT JOIN cmd_AREA B on a. area_code=b.area_code "+
                            "LEFT JOIN cmd_SHELF C on A.Shelf_Code=C.Shelf_Code "+
                        "LEFT JOIN cmd_product D ON A.Product_Code=D.Product_Code " +
                "ORDER BY AREA_CODE,A.SHELF_CODE,CELL_CODE";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable GetShelf()
        {
            string sql = "SELECT * FROM CMD_SHELF ORDER BY SHELF_CODE";
            return ExecuteQuery(sql).Tables[0];
        }

    }
}
