using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.XC.Process.Dao;
using System.Data;
namespace THOK.XC.Process.Dal
{
    public class BillDal : BaseDal
    {

        /// <summary>
        /// 空托盘组组盘入库申请
        /// </summary>
        /// <returns></returns>
        public string CreatePalletInBillTaskDetail()
        {
            BillDao dao = new BillDao();
            return dao.CreatePalletInBillTaskDetail();
        }

         /// <summary>
        /// 空托盘组出库申请
        /// </summary>
        public string CreatePalletOutBillTask(string TARGET_CODE)
        {
            BillDao dao = new BillDao();
            return dao.CreatePalletOutBillTask(TARGET_CODE);
        }

        public void UpdateBillMasterFinished(string BillNo)
        {
            BillDao dao = new BillDao();
            dao.UpdateBillMasterFinished(BillNo);
        }
    }
}