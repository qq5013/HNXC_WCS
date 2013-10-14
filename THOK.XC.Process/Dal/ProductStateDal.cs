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

        /// <summary>
        /// 更新出库单号
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public void UpdateOutBillNo(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ProductStateDao dao = new ProductStateDao();
                dao.UpdateOutBillNo(TaskID);
            }
        }
        
        /// <summary>
        /// 判断错误RfID，是否已经有替代产品出库
        /// </summary>
        /// <param name="OLD_PALLET_CODE"></param>
        /// <returns></returns>

        public bool ExistsPalletCode(string OLD_PALLET_CODE)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ProductStateDao dao = new ProductStateDao();
               return  dao.ExistsPalletCode(OLD_PALLET_CODE);
            }
        }
    }
}
