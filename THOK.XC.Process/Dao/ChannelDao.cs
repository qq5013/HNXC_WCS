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
            string strSQL = string.Format("SELECT LINE_NO, ORDERNO, CMD_CACHE_CHANNEL.CHANNEL_NO,CACHE_QTY,DECODE(TMPCACHE.QTY,NULL, 0,TMPCACHE.QTY) AS QTY " +
                                          "FROM CMD_CACHE_CHANNEL " +
                                          "LEFT JOIN (SELECT CHANNEL_NO, COUNT(*) AS QTY FROM WCS_PRODUCT_CACHE WHERE STATE=0 GROUP BY CHANNEL_NO) TMPCACHE " +
                                          "ON CMD_CACHE_CHANNEL.CHANNEL_NO=TMPCACHE.CHANNEL_NO WHERE LINE_NO='{0}' ORDER BY ORDERNO", Line_No);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 获取缓存道最后入库的产品信息。
        /// </summary>
        /// <param name="Channel_No"></param>
        /// <returns></returns>
        public DataTable ChannelProductInfo(string Channel_No)
        {
            string strSQL = string.Format("SELECT * FROM WCS_PRODUCT_CACHE WHERE STATE='0' AND  CHANNEL_NO='{0}' ORDER BY ORDER_NO DESC", Channel_No);
            return ExecuteQuery(strSQL).Tables[0];
        }

        public void InsertChannel(string Task_ID, string Bill_No, string Channel_No)
        {
            string strSQL = string.Format("INSERT INTO WCS_PRODUCT_CACHE(TASK_ID,TASK_NO,ORDER_NO,PRODUCT_CODE,PRODUCT_BARCODE,CHANNEL_NO,BILL_NO, SCHEDULE_NO,SCHEDULE_ITEMNO,STATE,CHECK_RESULT)" +
                          "SELECT TASK.TASK_ID,DETAIL.TASK_NO,(SELECT COUNT(*) FROM WCS_PRODUCT_CACHE WHERE BILL_NO='{0}')+1 AS ORDER_NO,PRODUCT_CODE,PRODUCT_BARCODE,'{1}' AS CHANNEL_NO,TASK.BILL_NO,BILL.SCHEDULE_NO,SCHEDULE_ITEMNO, 0,0 " +
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

          /// <summary>
        /// 判断是否已经在缓存道中，true 存在
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool HasTaskInChannel(string TaskID)
        {
            bool blnValue = false;
            string strSQL = string.Format("SELECT * FROM WCS_PRODUCT_CACHE WHERE TASK_ID='{0}' ", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                blnValue = true;
            return blnValue;
 
        }

        /// <summary>
        /// 更新出库
        /// </summary>
        public void UpdateOutChannelTime(string TaskID)
        {
            string strSQL = string.Format("UPDATE WCS_PRODUCT_CACHE SET STATE=1,OUT_DATE =SYSDATE WHERE TASK_ID='{0}'", TaskID);
            ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="TaskNO"></param>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetChannelFromTask(string TaskNO, string BillNo)
        {
            string strValue = "";
            string strSQL = string.Format("SELECT CHANNEL_NO FROM WCS_PRODUCT_CACHE WHERE TASK_NO='{0}' AND BILL_NO='{1}'", TaskNO, BillNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                strValue = dt.Rows[0][0].ToString();
            return strValue;
        }

        /// <summary>
        /// 更新进入
        /// </summary>
        public void UpdateInChannelAndTime(string TaskID, string Bill_No, string ChannelNo)
        {
            string strSQL = string.Format("UPDATE WCS_PRODUCT_CACHE SET ORDER_NO=(SELECT COUNT(*) FROM WCS_PRODUCT_CACHE WHERE BILL_NO='{0}')+1,IN_DATE =SYSDATE,CHANNEL_NO='{2}' " +
                                          "WHERE TASK_ID='{0}' AND BILL_NO='{1}' ", TaskID, Bill_No, ChannelNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 根据单号，获取缓存表中该单号的最近入库。
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetChannelNoByBillNo(string BillNo)
        {
            string strValue = "";
            string strSQL = string.Format("SELECT  ROWNUM,t.* FROM WCS_PRODUCT_CACHE T WHERE BILL_NO='{0}' AND ROWNUM=1 ORDER BY ORDER_NO DESC", BillNo);
            DataTable dt= ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strValue = dt.Rows[0]["CHANNEL_NO"].ToString();
            }
            return strValue;
        }
    }
}
