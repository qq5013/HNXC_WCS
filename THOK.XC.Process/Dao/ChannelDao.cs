using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class ChannelDao: BaseDao
    {
        public void Delete()
        {
            ExecuteNonQuery("TRUNCATE TABLE AS_BI_STOCKCHANNEL");

            ExecuteNonQuery("TRUNCATE TABLE AS_SC_STOCKMIXCHANNEL");

            ExecuteQuery("TRUNCATE TABLE AS_SC_CHANNELUSED");
        }

        public void InsertChannel(DataTable channelTable)
        {
            BatchInsert(channelTable, "AS_BI_STOCKCHANNEL");            
        }

        public void InsertChannelUSED(DataTable channelTable)
        {            
            BatchInsert(channelTable, "AS_SC_CHANNELUSED");
        }

        public void InsertMixChannel(DataTable mixTable)
        {
            BatchInsert(mixTable, "AS_SC_STOCKMIXCHANNEL");
        }

        public DataTable FindAll()
        {
            string sql = "SELECT CHANNELCODE,CHANNELNAME,"+
                            " CASE WHEN CHANNELTYPE='3' THEN '混合烟道' ELSE '单一烟道' END CHANNELTYPE,"+
                            " CIGARETTECODE, CIGARETTENAME "+
                            " FROM AS_BI_STOCKCHANNEL";
            return ExecuteQuery(sql).Tables[0];
        }

        public string FindLed(string channelCode)
        {
            string sql = "SELECT ISNULL(MIN(LEDNO),0) FROM AS_BI_STOCKCHANNEL WHERE CHANNELCODE = '{0}'";
            return ExecuteScalar(string.Format(sql, channelCode)).ToString();
        }

        public DataTable FindChannelForCigaretteCode(string cigaretteCode)
        {
            string sql = "SELECT A.CHANNELCODE,A.CIGARETTECODE,A.CIGARETTENAME,A.ISSTOCKIN,A.REMAINQUANTITY,B.BARCODE "+
                            " FROM AS_BI_STOCKCHANNEL A " +
                            " LEFT JOIN AS_SC_SUPPLY B ON A.CIGARETTECODE = B.CIGARETTECODE "+         
                            " WHERE A.CIGARETTECODE = '{0}' "+
                            " GROUP BY A.CHANNELCODE,A.CIGARETTECODE,A.CIGARETTENAME,A.IsStockIn,A.REMAINQUANTITY,B.BARCODE";
            return ExecuteQuery(string.Format(sql, cigaretteCode)).Tables[0];
        }

        #region 交换分拣烟道
       
        public DataTable FindChannelUSED(string lineCode, string channelCode)
        {
            string sql = "SELECT * FROM AS_SC_CHANNELUSED WHERE CHANNELCODE='{0}' AND LINECODE = '{1}' ";
            return ExecuteQuery(string.Format(sql, channelCode, lineCode)).Tables[0];
        }

        public DataTable FindEmptyChannel(string lineCode, string channelCode, object channelGroup, object channelType)
        {
            string sql = "SELECT CHANNELCODE, " +
                            " LINECODE + ' 线  ' + CHANNELNAME + ' ' + CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机'  ELSE '通道机' END CHANNELNAME " +
                            " FROM AS_SC_CHANNELUSED " +
                            " WHERE CHANNELTYPE IN ('{0}') AND CHANNELTYPE != '5' AND CHANNELGROUP = {1} AND CHANNELCODE != '{2}' AND LINECODE = '{3}' " +
                            " ORDER BY CHANNELNAME";
            return ExecuteQuery(string.Format(sql, channelType, channelGroup, channelCode, lineCode)).Tables[0];
        }

        public void UpdateChannelUSED(string lineCode, string channelCode, string cigaretteCode, string cigaretteName, int quantity, string sortNo)
        {
            string sql = "UPDATE AS_SC_CHANNELUSED SET CIGARETTECODE='{0}', CIGARETTENAME='{1}', QUANTITY={2}, SORTNO={3} "+
                            " WHERE CHANNELCODE='{4}' AND LINECODE = '{5}'";
            ExecuteNonQuery(string.Format(sql,cigaretteCode, cigaretteName, quantity, sortNo, channelCode, lineCode));
        }

        public DataTable FindChannelUSED()
        {
            string sql = "SELECT CHANNELCODE, CHANNELNAME, "+
                            " CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '混合烟道' ELSE '通道机' END CHANNELTYPE, " +
                            " LINECODE, CIGARETTECODE, CIGARETTENAME, QUANTITY "+
                            " FROM AS_SC_CHANNELUSED ORDER BY LINECODE, CHANNELNAME";
            return ExecuteQuery(sql).Tables[0];
        }

        #endregion
    }
}
