using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class ChannelDao: BaseDao
    {
        /// <summary>
        /// 根据制丝线，获取缓存道信息。
        /// </summary>
        /// <param name="Line_No"></param>
        /// <returns></returns>
        public DataTable ChannelInfo(string Line_No)
        {
            string strSQL = "SELECT LINE_NO, ORDERNO, CMD_CACHE_CHANNEL.CHANNEL_NO,CACHE_QTY,DECODE(TMPCACHE.QTY,NULL, 0,TMPCACHE.QTY) AS QTY  FROM CMD_CACHE_CHANNEL " +
                            "LEFT JOIN (SELECT  CHANNEL_NO, COUNT(*) AS QTY FROM WCS_PRODUCT_CACHE WHERE STATE=0 GROUP BY CHANNEL_NO) TMPCACHE " +
                            "ON CMD_CACHE_CHANNEL.CHANNEL_NO=TMPCACHE.CHANNEL_NO WHERE LINE_NO='01' ORDER BY ORDERNO";
            return null;
        }
        /// <summary>
        /// 获取缓存道最后入库的产品信息。
        /// </summary>
        /// <param name="Channel_No"></param>
        /// <returns></returns>
        public DataTable ChannelProductInfo(string Channel_No)
        {
            return null;
        }

        public void InsertChannel(string Task_ID, string Bill_No, string Channel_No)
        {
            string strSQL = string.Format("INSERT INTO WCS_PRODUCT_CACHE(TASK_ID,TASK_NO,ORDER_NO,PRODUCT_CODE,PRODUCT_BARCODE,CHANNEL_NO,BILL_NO, SCHEDULE_NO,SCHEDULE_ITEMNO,STATE)" +
                          "SELECT TASK.TASK_ID,DETAIL.TASK_NO,(SELECT COUNT(*) FROM WCS_PRODUCT_CACHE WHERE BILL_NO='{0}')+1 AS ORDER_NO,PRODUCT_CODE,PRODUCT_BARCODE,'{1}' AS CHANNEL_NO,TASK.BILL_NO,BILL.SCHEDULE_NO,SCHEDULE_ITEMNO, 0 " +
                          "FROM WCS_TASK TASK " +
                          "INNER JOIN WCS_TASK_DETAIL DETAIL ON TASK.TASK_ID=DETAIL.TASK_ID AND DETAIL.ITEM_NO=5 " +
                          "INNER JOIN WMS_BILL_MASTER BILL ON BILL.BILL_NO=TASK.BILL_NO " +
                          "WHERE TASK.TASK_ID='{2}'", Bill_No, Channel_No, Task_ID);

            ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 更新进入
        /// </summary>
        public void UpdateInChannelTime(string TaskID, string Bill_No, string ChannelNo)
        {


            string strSQL = string.Format("UPDATE WCS_PRODUCT_CACHE SET ORDER_NO=(SELECT COUNT(*) FROM WCS_PRODUCT_CACHE WHERE BILL_NO='{0}')+1,IN_DATE =SYSDATE " +
                                          "WHERE TASK_ID='{0}' AND BILL_NO='{1}' AND CHANNEL_NO='{2}'", TaskID, Bill_No, ChannelNo);
            ExecuteNonQuery(strSQL);
        }

        public int ProductCount(string Bill_NO)
        {
            string strSQL = string.Format("SELECT COUNT(*) FROM WCS_PRODUCT_CACHE WHERE BILL_NO='{0}' AND IN_DATE IS NOT NULL ", Bill_NO);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            int Count = int.Parse(dt.Rows[0][0].ToString());
            return Count;
 
        }
    }
}
