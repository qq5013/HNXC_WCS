using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class SupplyDao : BaseDao
    {
        public void Delete()
        {
            string sql = "TRUNCATE TABLE AS_SC_SUPPLY";
            ExecuteNonQuery(sql);
        }

        public void Insert(DataTable supplyTable)
        {
            BatchInsert(supplyTable, "AS_SC_SUPPLY");
        }

        public int FindCount()
        {
            string sql = "SELECT COUNT(*) FROM AS_SC_SUPPLY";
            return Convert.ToInt32(ExecuteScalar(sql));
        }



        public DataTable FindSupplyBatch(string lineCode, string sortNo, string channelGroup)
        {
            string sql = "SELECT DISTINCT SORTNO FROM AS_SC_SUPPLY " +
                         " WHERE LINECODE='{0}' AND SORTNO > 0 AND SORTNO <= {1} " +
                         " AND CHANNELGROUP = {2} " +
                         " AND SORTNO NOT IN (SELECT SORTNO FROM AS_STOCK_OUT_BATCH WHERE LINECODE='{0}'AND CHANNELGROUP = {2}) " +
                         " ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql, lineCode, sortNo, channelGroup)).Tables[0];
        }

        public DataTable FindSupply(string lineCode, string sortNo, string channelGroup)
        {
            string sql = "SELECT * FROM AS_SC_SUPPLY A " +
                         //--" LEFT JOIN V_STOCKCHANNEL B ON A.CIGARETTECODE = B.CIGARETTECODE " + //???
                         " WHERE LINECODE='{0}' AND SORTNO={1} AND CHANNELGROUP = {2} " +
                         " ORDER BY SERIALNO";
            return ExecuteQuery(string.Format(sql, lineCode, sortNo, channelGroup)).Tables[0];
        }

        public void UpdateChannelUSED(string lineCode, string sourceChannel, string targetChannel,
                                      string targetChannelGroupNo)
        {
            string sql =
                "UPDATE AS_SC_SUPPLY SET CHANNELCODE='{0}',GROUPNO = '{1}' WHERE CHANNELCODE='{2}' AND LINECODE = '{3}' ";
            ExecuteNonQuery(string.Format(sql, targetChannel, targetChannelGroupNo, sourceChannel, lineCode));
            sql = "UPDATE AS_STOCK_OUT SET CHANNELCODE='{0}' WHERE CHANNELCODE='{2}' AND LINECODE = '{3}' ";
            ExecuteNonQuery(string.Format(sql, targetChannel, targetChannelGroupNo, sourceChannel, lineCode));
        }

        public DataTable FindCigarette()
        {
            string sql = "SELECT A.CIGARETTECODE,A.CIGARETTENAME,COUNT(*) QUANTITY" +
                         " FROM AS_SC_SUPPLY A " +
                         " LEFT JOIN V_STOCKCHANNEL B ON A.CIGARETTECODE = B.CIGARETTECODE " +
                         " WHERE B.CHANNELTYPE = '2' " +
                         " GROUP BY A.CIGARETTECODE,A.CIGARETTENAME,B.CHANNELCODE" +
                         " ORDER BY B.CHANNELCODE";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindCigarette(string cigaretteCode, string quantity)
        {
            string sql = "SELECT A.CIGARETTECODE,A.CIGARETTENAME," +
                         " COUNT(*) + {0} - (SELECT ISNULL(SUM(INQUANTITY),0) FROM AS_STOCK_IN_BATCH WHERE CIGARETTECODE = '{1}') QUANTITY " +
                         " FROM AS_SC_SUPPLY A " +
                         " LEFT JOIN V_STOCKCHANNEL B ON A.CIGARETTECODE = B.CIGARETTECODE " +
                         " WHERE B.CHANNELTYPE = '2' AND A.CIGARETTECODE = '{1}'" +
                         " GROUP BY A.CIGARETTECODE,A.CIGARETTENAME";
            return ExecuteQuery(string.Format(sql, quantity, cigaretteCode)).Tables[0];
        }

        internal DataTable FindCigaretteAll()
        {
            string sql = "SELECT CIGARETTECODE,CIGARETTENAME FROM AS_SC_SUPPLY " +
                         " GROUP BY CIGARETTECODE, CIGARETTENAME";
            return ExecuteQuery(sql).Tables[0];
        }

        internal DataTable FindCigaretteAll(string cigaretteCode)
        {
            string sql = "";
            if (cigaretteCode == string.Empty)
            {
                return FindCigaretteAll();
            }
            else if (cigaretteCode != string.Empty)
            {
                sql = "SELECT CIGARETTECODE,CIGARETTENAME,BARCODE FROM AS_SC_SUPPLY " +
                      " WHERE CIGARETTECODE = '{0}'" +
                      " GROUP BY CIGARETTECODE, CIGARETTENAME,BARCODE";
            }
            return ExecuteQuery(string.Format(sql, cigaretteCode)).Tables[0];
        }

        internal bool Exist(string barcode)
        {
            string sql = "SELECT CIGARETTECODE,CIGARETTENAME FROM AS_SC_SUPPLY " +
                         " WHERE BARCODE = '{0}'" +
                         " GROUP BY CIGARETTECODE, CIGARETTENAME";
            return ExecuteQuery(string.Format(sql, barcode)).Tables[0].Rows.Count > 0;
        }

        internal int FindSortNoForSupply(string lineCode, string sortNo, string channelGroup, int supplyAheadCount)
        {
            string sql = "SELECT TOP {0} A.SORTNO FROM AS_SC_SUPPLY A " +
                         " LEFT JOIN AS_SC_CHANNELUSED B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE " +
                         " WHERE A.LINECODE='{1}' AND A.SORTNO > 0 AND A.ORIGINALSORTNO >= {2} " +
                         " AND A.CHANNELGROUP = {3} AND B.CHANNELTYPE = '3' " +
                         " ORDER BY A.SORTNO";
            object obj =
                ExecuteQuery(string.Format(sql,supplyAheadCount,lineCode, sortNo, channelGroup)).Tables[0].Compute("MAX(SORTNO)", "");
            return obj is DBNull ? 0 : Convert.ToInt32(obj);
        }

        //zys_2011-10-06
        internal DataTable FindFirstSupply()
        {
            string sql = @"SELECT * FROM AS_SC_SUPPLY A 
                            WHERE SORTNO = 0 AND SERIALNO NOT IN (SELECT SERIALNO FROM AS_STOCK_OUT B
	                            WHERE A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO AND A.LINECODE = B.LINECODE )
                            ORDER BY A.SERIALNO,A.LINECODE DESC,A.CHANNELGROUP DESC";
            return ExecuteQuery(sql).Tables[0];
        }

        //zys_2011-10-05
        internal DataTable FindNextSupply(string lineCode, string channelGroup, string channelType, int sortNo)
        {
            string sql = @"SELECT TOP 3 * FROM
                            (
                                SELECT ROW_NUMBER() OVER(ORDER BY A.ORDERDATE,A.BATCHNO,A.LINECODE,A.SERIALNO) ROW_INDEX,
                                A.* FROM AS_SC_SUPPLY A
                                LEFT JOIN AS_SC_CHANNELUSED B ON A.ORDERDATE = B.ORDERDATE 
                                AND A.BATCHNO = B.BATCHNO AND A.LINECODE = B.LINECODE 
                                AND A.CHANNELCODE = B.CHANNELCODE
                                WHERE A.LINECODE ='{0}' AND A.CHANNELGROUP = '{1}' AND B.CHANNELTYPE = '{2}'
                            ) C
                            WHERE ROW_INDEX <= {3} AND SERIALNO NOT IN (SELECT SERIALNO FROM AS_STOCK_OUT D 
                                WHERE C.ORDERDATE = D.ORDERDATE AND C.BATCHNO = D.BATCHNO AND C.LINECODE = D.LINECODE )
                            ORDER BY ROW_INDEX";
            return ExecuteQuery(string.Format(sql, lineCode, channelGroup, channelType,sortNo)).Tables[0];
        }
    }
}