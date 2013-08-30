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
            string BillNo = TaskID.Substring(0,TaskID.Length-2);
            string ItemNo = int.Parse(TaskID.Substring(TaskID.Length - 2)).ToString();

            string strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET CELL_CODE='{0}' WHERE BILLNO='{1}' AND ITEM_NO='{2}'", strCell, BillNo, ItemNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 根据条码返回烟包信息。
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public DataTable  GetProductInfo(string BarCode)
        {
            return null;
        }
    }
}
