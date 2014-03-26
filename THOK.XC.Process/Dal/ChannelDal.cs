using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.XC.Process.Dao;
using THOK.Util;

namespace THOK.XC.Process.Dal
{
    public class ChannelDal : BaseDal
    {

        /// <summary>
        /// 分配缓存道，插入缓存到表。并返回缓存道ID。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public string InsertChannel(string TaskID, string Bill_No)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                string strChannel_No = "";
                TaskDao dao = new TaskDao();
                DataTable dt = dao.TaskInfo(string.Format("TASK_ID='{0}'", TaskID));
                string Line_No = dt.Rows[0]["TARGET_CODE"].ToString().Trim();
                string BillNo = dt.Rows[0]["BILL_NO"].ToString();

                ChannelDao Cdao = new ChannelDao();
                dt = Cdao.ChannelInfo(Line_No);

                if (!Cdao.HasTaskInChannel(TaskID))
                {
                    switch (Line_No)
                    {
                        case "01":
                        case "02":
                            DataRow[] drs = dt.Select("QTY>0 AND QTY<CACHE_QTY", "ORDERNO");
                            if (drs.Length == 0)
                            {
                                drs = dt.Select("QTY=0 AND QTY<CACHE_QTY", "ORDERNO");
                                if (drs.Length > 0)
                                    strChannel_No = drs[0]["CHANNEL_NO"].ToString();

                            }
                            else
                            {
                                if (drs.Length > 1)
                                {
                                    strChannel_No = Cdao.GetChannelNoByBillNo(BillNo);
                                }
                                else
                                {
                                    DataTable dtHaveProduct = Cdao.ChannelProductInfo(drs[0]["CHANNEL_NO"].ToString());
                                    if (dtHaveProduct.Rows.Count > 0)
                                    {
                                        if (dtHaveProduct.Rows[0]["BILL_NO"].ToString() == BillNo)
                                            strChannel_No = drs[0]["CHANNEL_NO"].ToString();
                                    }
                                }
                                if (strChannel_No == "")
                                {
                                    drs = dt.Select("QTY=0 AND QTY<CACHE_QTY", "ORDERNO");
                                    if (drs.Length > 0)
                                        strChannel_No = drs[0]["CHANNEL_NO"].ToString();
                                }
                            }
                            break;
                        case "03":
                            if (int.Parse(dt.Rows[0]["CACHE_QTY"].ToString()) - int.Parse(dt.Rows[0]["QTY"].ToString()) > 15)
                            {
                                strChannel_No = dt.Rows[0]["CHANNEL_NO"].ToString();
                            }
                            break;
                    }

                    if (strChannel_No != "")
                    {
                        Cdao.InsertChannel(TaskID, Bill_No, strChannel_No);
                    }
                }

                return strChannel_No;
            }
        }

        /// <summary>
        /// 更新进入缓存道时间，及ORDER_NO     
        /// </summary>
        /// <returns></returns>
        public int UpdateInChannelTime(string TaskID, string Bill_No, string ChannelNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                int strValue = 0;
                ChannelDao dao = new ChannelDao();
                int count = dao.ProductCount(Bill_No);
                TaskDao tdao = new TaskDao();

                int taskCount = tdao.TaskCount(Bill_No);
                if (count == 0)
                    strValue = 1;
                if (count == taskCount - 1)
                    strValue = 2;
                dao.UpdateInChannelTime(TaskID, Bill_No, ChannelNo);
                return strValue;
            }
        }
        /// <summary>
        /// 判断是否已经在缓存道中，true 存在
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool HasTaskInChannel(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                bool blnValue = false;
                 ChannelDao dao = new ChannelDao();
                 blnValue= dao.HasTaskInChannel(TaskID);
                 return blnValue;
            }
 
        }
          /// <summary>
        /// 更新出库
        /// </summary>
        public void UpdateOutChannelTime(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao dao = new ChannelDao();
                dao.UpdateOutChannelTime(TaskID);
            }
        }
        /// <summary>
        /// 获取分配的缓存道
        /// </summary>
        /// <param name="TaskNO"></param>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetChannelFromTask(string TaskNO, string BillNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao dao = new ChannelDao();
                return dao.GetChannelFromTask(TaskNO, BillNo);
            }
        }

        /// <summary>
        /// 更新出库
        /// </summary>
        public int UpdateInChannelAndTime(string TaskID, string Bill_No, string ChannelNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                int strValue = 0;
                ChannelDao dao = new ChannelDao();
                int count = dao.ProductCount(Bill_No);
                TaskDao tdao = new TaskDao();

                int taskCount = tdao.TaskCount(Bill_No);
                if (count == 0)
                    strValue = 1;
                if (count == taskCount - 1)
                    strValue = 2;
                dao.UpdateInChannelAndTime(TaskID, Bill_No, ChannelNo);
                return strValue;
            }
        }

        /// <summary>
        /// 根据单号，获取最近入库的缓存道编号。
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetChannelNoByBillNo(string BillNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                string strChannelNo = "";
                ChannelDao dao = new ChannelDao();
                strChannelNo = dao.GetChannelNoByBillNo(BillNo);

                return strChannelNo;
            }

        }
       
    }
}
