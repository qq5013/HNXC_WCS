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
        public string InsertChannel(string TaskID,string Bill_No)
        {
            string strChannel_No = "";
            TaskDao dao = new TaskDao();
            DataTable dt = dao.TaskInfo(string.Format("TASK_ID='{0}'", TaskID));
            string Line_No = dt.Rows[0]["TARGET_CODE"].ToString();
            string BillNo = dt.Rows[0]["BILL_NO"].ToString();

            ChannelDao Cdao = new ChannelDao();
            dt = Cdao.ChannelInfo(Line_No);
            switch (Line_No)
            {
                case "01":
                     DataRow dr011=dt.Rows[0];
                    DataRow dr012=dt.Rows[1];
                    DataRow dr013 = dt.Rows[2];
                    if (dr011["QTY"].ToString() == "0")
                    {
                        if (dr012["QTY"].ToString() == "0")
                        {
                            if (dr013["QTY"].ToString() == "0")
                            {
                                strChannel_No = dr011["CHANNEL_NO"].ToString();
                            }
                            else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                            {
                                DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                if (dt013.Rows.Count > 0)
                                {
                                    if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                    else
                                    {
                                        strChannel_No = dr011["CHANNEL_NO"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                strChannel_No = dr011["CHANNEL_NO"].ToString();
                            }
                        }
                        else if (int.Parse(dr012["CACHE_QTY"].ToString()) - int.Parse(dr012["QTY"].ToString()) > 0)
                        {
                            DataTable dt012 = Cdao.ChannelProductInfo(dr012["CHANNEL_NO"].ToString());
                            if (dt012.Rows.Count > 0)
                            {
                                if (dt012.Rows[0]["BILL_NO"].ToString() == BillNo)
                                {
                                    strChannel_No = dr012["CHANNEL_NO"].ToString();
                                }
                                else
                                {
                                    if (dr013["QTY"].ToString() == "0")
                                    {
                                        strChannel_No = dr011["CHANNEL_NO"].ToString();
                                    }
                                    else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                                    {
                                        DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                        if (dt013.Rows.Count > 0)
                                        {
                                            if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                            {
                                                strChannel_No = dr013["CHANNEL_NO"].ToString();
                                            }
                                            else
                                            {
                                                strChannel_No = dr011["CHANNEL_NO"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strChannel_No = dr011["CHANNEL_NO"].ToString();
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (dr013["QTY"].ToString() == "0")
                            {
                                strChannel_No = dr011["CHANNEL_NO"].ToString();
                            }
                            else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                            {
                                DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                if (dt013.Rows.Count > 0)
                                {
                                    if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                    else
                                    {
                                        strChannel_No = dr011["CHANNEL_NO"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                strChannel_No = dr011["CHANNEL_NO"].ToString();
                            }
                        }

                        
                    }
                    else if (int.Parse(dr011["CACHE_QTY"].ToString()) - int.Parse(dr011["QTY"].ToString()) > 0)
                    {
                        DataTable dt011 = Cdao.ChannelProductInfo(dr011["CHANNEL_NO"].ToString());
                        if (dt011.Rows.Count > 0)
                        {
                            if (dt011.Rows[0]["BILL_NO"].ToString() == BillNo)
                            {
                                strChannel_No = dr011["CHANNEL_NO"].ToString();
                            }
                            else
                            {
                                if (dr012["QTY"].ToString() == "0")
                                {
                                    if (dr013["QTY"].ToString() == "0")
                                    {
                                        strChannel_No = dr012["CHANNEL_NO"].ToString();
                                    }
                                    else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                                    {
                                        DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                        if (dt013.Rows.Count > 0)
                                        {
                                            if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                            {
                                                strChannel_No = dr013["CHANNEL_NO"].ToString();
                                            }
                                            else
                                            {
                                                strChannel_No = dr012["CHANNEL_NO"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strChannel_No = dr012["CHANNEL_NO"].ToString();
                                    }
                                }
                                else if (int.Parse(dr012["CACHE_QTY"].ToString()) - int.Parse(dr012["QTY"].ToString()) > 0)
                                {
                                    DataTable dt012 = Cdao.ChannelProductInfo(dr012["CHANNEL_NO"].ToString());
                                    if (dt012.Rows.Count > 0)
                                    {
                                        if (dt012.Rows[0]["BILL_NO"].ToString() == BillNo)
                                        {
                                            strChannel_No = dr012["CHANNEL_NO"].ToString();
                                        }
                                        else
                                        {
                                            if (dr013["QTY"].ToString() == "0")
                                            {
                                                strChannel_No = dr013["CHANNEL_NO"].ToString();
                                            }
                                            else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                                            {
                                                DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                                if (dt013.Rows.Count > 0)
                                                {
                                                    if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                                    {
                                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                                    }
                                                }
                                            }

                                        }
                                    }

                                }
                                else
                                {
                                    if (dr013["QTY"].ToString() == "0")
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                    else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                                    {
                                        DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                        if (dt013.Rows.Count > 0)
                                        {
                                            if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                            {
                                                strChannel_No = dr013["CHANNEL_NO"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dr012["QTY"].ToString() == "0")
                        {
                            if (dr013["QTY"].ToString() == "0")
                            {
                                strChannel_No = dr012["CHANNEL_NO"].ToString();
                            }
                            else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                            {
                                DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                if (dt013.Rows.Count > 0)
                                {
                                    if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                    else
                                    {
                                        strChannel_No = dr012["CHANNEL_NO"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                strChannel_No = dr012["CHANNEL_NO"].ToString();
                            }
                        }
                        else if (int.Parse(dr012["CACHE_QTY"].ToString()) - int.Parse(dr012["QTY"].ToString()) > 0)
                        {
                            DataTable dt012 = Cdao.ChannelProductInfo(dr012["CHANNEL_NO"].ToString());
                            if (dt012.Rows.Count > 0)
                            {
                                if (dt012.Rows[0]["BILL_NO"].ToString() == BillNo)
                                {
                                    strChannel_No = dr012["CHANNEL_NO"].ToString();
                                }
                                else
                                {
                                    if (dr013["QTY"].ToString() == "0")
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                    else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                                    {
                                        DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                        if (dt013.Rows.Count > 0)
                                        {
                                            if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                            {
                                                strChannel_No = dr013["CHANNEL_NO"].ToString();
                                            }
                                        }
                                    }

                                }
                            }

                        }
                        else if (dr012["QTY"].ToString() == "0")
                        {
                            if (dr013["QTY"].ToString() == "0")
                            {
                                strChannel_No = dr012["CHANNEL_NO"].ToString();
                            }
                            else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                            {
                                DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                if (dt013.Rows.Count > 0)
                                {
                                    if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                    else
                                    {
                                        strChannel_No = dr012["CHANNEL_NO"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                strChannel_No = dr012["CHANNEL_NO"].ToString();
                            }
                        }
                        else if (int.Parse(dr012["CACHE_QTY"].ToString()) - int.Parse(dr012["QTY"].ToString()) > 0)
                        {
                            DataTable dt012 = Cdao.ChannelProductInfo(dr012["CHANNEL_NO"].ToString());
                            if (dt012.Rows.Count > 0)
                            {
                                if (dt012.Rows[0]["BILL_NO"].ToString() == BillNo)
                                {
                                    strChannel_No = dr012["CHANNEL_NO"].ToString();
                                }
                                else
                                {
                                    if (dr013["QTY"].ToString() == "0")
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                    else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                                    {
                                        DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                        if (dt013.Rows.Count > 0)
                                        {
                                            if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                            {
                                                strChannel_No = dr013["CHANNEL_NO"].ToString();
                                            }
                                        }
                                    }

                                }
                            }

                        }
                        else
                        {
                            if (dr013["QTY"].ToString() == "0")
                            {
                                strChannel_No = dr013["CHANNEL_NO"].ToString();
                            }
                            else if (int.Parse(dr013["CACHE_QTY"].ToString()) - int.Parse(dr013["QTY"].ToString()) > 0)
                            {
                                DataTable dt013 = Cdao.ChannelProductInfo(dr013["CHANNEL_NO"].ToString());
                                if (dt013.Rows.Count > 0)
                                {
                                    if (dt013.Rows[0]["BILL_NO"].ToString() == BillNo)
                                    {
                                        strChannel_No = dr013["CHANNEL_NO"].ToString();
                                    }
                                }
                            }
                        }
                    }

                    break;
                case "02":
                    DataRow dr021=dt.Rows[0];
                    DataRow dr022=dt.Rows[1];
                    if (dr021["QTY"].ToString() == "0")
                    {
                        if (dr022["QTY"].ToString() == "0")
                        {
                            strChannel_No = dr021["CHANNEL_NO"].ToString();
                        }
                        else if (int.Parse(dr022["CACHE_QTY"].ToString()) - int.Parse(dr022["QTY"].ToString()) > 0)
                        {
                            DataTable dt022 = Cdao.ChannelProductInfo(dr022["CHANNEL_NO"].ToString());
                            if (dt022.Rows.Count > 0)
                            {
                                if (dt022.Rows[0]["BILL_NO"].ToString() == BillNo)
                                {
                                    strChannel_No = dr022["CHANNEL_NO"].ToString();
                                }
                                else
                                {
                                    strChannel_No = dr021["CHANNEL_NO"].ToString();
                                }
                            }
                        }

                        else
                        {
                            strChannel_No = dr021["CHANNEL_NO"].ToString();
                        }

                    }
                    else if (int.Parse(dr021["CACHE_QTY"].ToString()) - int.Parse(dr021["QTY"].ToString()) > 0)
                    {
                        DataTable dt021 = Cdao.ChannelProductInfo(dr021["CHANNEL_NO"].ToString());
                        DataTable dt022 = Cdao.ChannelProductInfo(dr022["CHANNEL_NO"].ToString());

                        if (dt021.Rows.Count > 0)
                        {
                            if (dt021.Rows[0]["BILL_NO"].ToString() == BillNo)
                            {
                                strChannel_No = dr021["CHANNEL_NO"].ToString();
                            }
                            else
                            {
                                if (int.Parse(dr022["QTY"].ToString()) == 0)
                                {
                                    strChannel_No = dr022["CHANNEL_NO"].ToString();
                                }
                                else
                                {
                                    if (int.Parse(dr022["CACHE_QTY"].ToString()) - int.Parse(dr022["QTY"].ToString()) > 0)
                                    {
                                        if (dt022.Rows[0]["BILL_NO"].ToString() == BillNo)
                                        {
                                            strChannel_No = dr022["CHANNEL_NO"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        DataTable dt022 = Cdao.ChannelProductInfo(dr022["CHANNEL_NO"].ToString());
                        if (int.Parse(dr022["QTY"].ToString()) == 0)
                        {
                            strChannel_No = dr022["CHANNEL_NO"].ToString();
                        }
                        else
                        {
                            if (int.Parse(dr022["CACHE_QTY"].ToString()) - int.Parse(dr022["QTY"].ToString()) > 0)
                            {
                                if (dt022.Rows[0]["BILL_NO"].ToString() == BillNo)
                                {
                                    strChannel_No = dr022["CHANNEL_NO"].ToString();
                                }
                            }
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
                Cdao.InsertChannel(TaskID,Bill_No, strChannel_No);
            }

            return strChannel_No;
        }

        /// <summary>
        /// 更新进入缓存道时间，及ORDER_NO     
        /// </summary>
        /// <returns></returns>
        public int  UpdateInChannelTime(string TaskID, string Bill_No, string ChannelNo)
        {
            int strValue = 0;
            ChannelDao dao = new ChannelDao();
            int count = dao.ProductCount(Bill_No);
            TaskDao tdao=new TaskDao();

            int taskCount = tdao.TaskCount(Bill_No);
            if (count == 0)
                strValue = 1;
            if (count == taskCount - 1)
                strValue = 2;
            dao.UpdateInChannelTime(TaskID, Bill_No, ChannelNo);
            return strValue;
        }
       
    }
}
