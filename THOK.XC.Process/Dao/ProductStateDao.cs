using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class ProductStateDao : BaseDao
    {
        /// <summary>
        /// 更新wms_product_state 货位
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="state"></param>
        public void UpdateProductCellCode(string TaskID, string strCell)
        {
            string BillNo = TaskID.Substring(0, TaskID.Length - 2);
            string ItemNo = int.Parse(TaskID.Substring(TaskID.Length - 2)).ToString();

            string strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET CELL_CODE='{0}' WHERE BILL_NO='{1}' AND ITEM_NO='{2}'", strCell, BillNo, ItemNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 根据条码返回烟包信息。
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public DataTable GetProductInfo(string BarCode)
        {
            string strSQL = string.Format("SELECT  T.PRODUCT_CODE,T.PRODUCT_BARCODE,T.BILL_NO,P.*,CIGARETTE_NAME,G.GRADE_NAME,O.ORIGINAL_NAME,S.STYLE_NAME " +
                            "FROM  WMS_PRODUCT_STATE T LEFT JOIN CMD_PRODUCT P ON T.PRODUCT_CODE=P.PRODUCT_CODE " +
                            "LEFT JOIN WMS_BILL_MASTER M ON T.BILL_NO=M.BILL_NO " +
                            "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=M.CIGARETTE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_ORIGINAL O ON O.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                            "LEFT JOIN CMD_PRODUCT_GRADE G ON G.GRADE_CODE=P.GRADE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE S ON S.STYLE_NO=P.STYLE_NO WHERE PRODUCT_BARCODE='{0}'", BarCode);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 更新出库单号
        /// </summary>
        /// <param name="TaskID"></param>
        public void UpdateOutBillNo(string TaskID)
        {
            string BillNO = TaskID.Substring(0, TaskID.Length - 2);
            string ItemNo = int.Parse(TaskID.Substring(TaskID.Length - 2, 2)).ToString();
            string strSQL = string.Format("UPDATE WMS_PRODUCT_STATEH SET OUT_BILLNO=(SELECT BILL_NO FROM WMS_PRODUCT_STATE STATE WHERE WMS_PRODUCT_STATEH.CELL_CODE=STATE.CELL_CODE AND WMS_PRODUCT_STATEH.PRODUCT_BARCODE=STATE.PRODUCT_BARCODE AND STATE.BILL_NO='{0}' AND STATE.ITEM_NO={1} ) " +
                            "WHERE PRODUCT_BARCODE IN (SELECT PRODUCT_BARCODE FROM WMS_PRODUCT_STATE WHERE BILL_NO='{0}' AND ITEM_NO={1}) AND OUT_BILLNO IS  NULL AND BILL_NO<>'{0}' ", BillNO, ItemNo);
            ExecuteNonQuery(strSQL);

        }
        public bool ExistsPalletCode(string OLD_PALLET_CODE)
        {
            bool blnValue = false;
            string strSQL = string.Format("SELECT * FROM WMS_PRODUCT_STATE WHERE OLD_PALLET_CODE='{0}'", OLD_PALLET_CODE);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                blnValue = true;
            return blnValue;
        }
    }
}
