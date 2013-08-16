using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class ChannelDao: BaseDao
    {
        public DataTable ChannelInfo(string Line_No)
        {
            string strSQL = "SELECT LINE_NO, ORDERNO, CMD_CACHE_CHANNEL.CHANNEL_NO,CACHE_QTY,DECODE(TMPCACHE.QTY,NULL, 0,TMPCACHE.QTY) AS QTY  FROM CMD_CACHE_CHANNEL " +
                            "LEFT JOIN (SELECT  CHANNEL_NO, COUNT(*) AS QTY FROM WCS_PRODUCT_CACHE WHERE STATE=0 GROUP BY CHANNEL_NO) TMPCACHE " +
                            "ON CMD_CACHE_CHANNEL.CHANNEL_NO=TMPCACHE.CHANNEL_NO WHERE LINE_NO='01' ORDER BY ORDERNO";
            return null;
        }
        public DataTable ChannelProductInfo(string Channel_No)
        {
            return null;
        }
    }
}
