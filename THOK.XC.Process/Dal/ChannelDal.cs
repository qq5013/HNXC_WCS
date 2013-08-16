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
        public string InsertChannel(string TaskID)
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
                    if (dt.Rows[0][""].ToString() == "0")
                    {
                        strChannel_No = dt.Rows[0][""].ToString();
                    }
                    else
                    {
                        if (dt.Rows[1][""].ToString() == "0")
                        {
                            strChannel_No = dt.Rows[0][""].ToString();

                        }
                        else
                        {
                            if (dt.Rows[2][""].ToString() == "0")
                            {
                                strChannel_No = dt.Rows[1][""].ToString();
                            }
                            else
                            {
 
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
                        else
                        {
                            if (int.Parse(dr022["CACHE_QTY"].ToString()) - int.Parse(dr022["QTY"].ToString()) > 0)
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
                    }
                    else
                    {

                        if (int.Parse(dr021["CACHE_QTY"].ToString()) - int.Parse(dr021["QTY"].ToString()) > 0)
                        {
                            if (int.Parse(dr022["QTY"].ToString()) == 0)
                            {
                                strChannel_No = dr021["CHANNEL_NO"].ToString();
                            }
                            else
                            {
                                if (int.Parse(dr022["CACHE_QTY"].ToString()) - int.Parse(dr022["QTY"].ToString()) > 0)
                                {
                                    DataTable dt021 = Cdao.ChannelProductInfo(dr021["CHANNEL_NO"].ToString());
                                    DataTable dt022 = Cdao.ChannelProductInfo(dr022["CHANNEL_NO"].ToString());

                                    if (dt021.Rows[0]["BILL_NO"].ToString() == BillNo)
                                    {
                                        if (dt022.Rows[0]["BILL_NO"].ToString() == BillNo)
                                        {
                                            if (int.Parse(dr021[""].ToString()) > int.Parse(dr022[""].ToString()))
                                            {
                                                strChannel_No = dr021["CHANNEL_NO"].ToString();
                                            }
                                            else
                                            {
                                                strChannel_No = dr022["CHANNEL_NO"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            strChannel_No = dr021["CHANNEL_NO"].ToString();
                                        }
                                    }
                                    else
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

                        }
                        else
                        {
                            if (int.Parse(dr022["CACHE_QTY"].ToString()) - int.Parse(dr022["QTY"].ToString()) > 0)
                            {
                                strChannel_No = dr022["CHANNEL_NO"].ToString();
                            }
                        } 
                    }

                    break;
                case "03":
                    if (int.Parse(dt.Rows[0]["CACHE_QTY"].ToString()) - int.Parse(dt.Rows[0]["QTY"].ToString()) > 0)
                    {
                        strChannel_No = dt.Rows[0]["CHANNEL_NO"].ToString();
                    }
                    break;
            }


            return "";
        }
       
    }
}
