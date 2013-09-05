using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.XC.Process.Dao;
using System.Data;
namespace THOK.XC.Process.Dal
{
    public class ProductStateDal : BaseDal
    {
          /// <summary>
        /// 更新wms_product_state 货位
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="state"></param>
        public void UpdateProductCellCode(string TaskID, string strCell)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ProductStateDao dao = new ProductStateDao();
                dao.UpdateProductCellCode(TaskID, strCell);
            }
        }
        /// <summary>
        /// 根据条码返回条码信息。
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public DataTable  GetProductInfoByBarCode(string BarCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ProductStateDao dao = new ProductStateDao();
                return dao.GetProductInfo(BarCode);
            }
        }
    }
}
