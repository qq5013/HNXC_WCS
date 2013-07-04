using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class StockInDao:BaseDao 
    {
        public void Delete()
        {
            string sql = "TRUNCATE TABLE AS_STOCK_IN";
            ExecuteNonQuery(sql);
        }

        public int FindMaxInID()
        {
            string sql = "SELECT ISNULL(MAX(STOCKINID),0) FROM AS_STOCK_IN";
            return Convert.ToInt32(ExecuteScalar(sql));
        }

        public void Insert(int stockInID, int batchNo, string channelCode, string cigaretteCode, string cigaretteName, string barode, string state)
        {
            SqlCreate sqlCreate = new SqlCreate("AS_STOCK_IN", SqlType.INSERT);
            sqlCreate.Append("STOCKINID", stockInID);
            sqlCreate.Append("BATCHNO", batchNo);
            sqlCreate.AppendQuote("CHANNELCODE", channelCode);
            sqlCreate.AppendQuote("CIGARETTECODE", cigaretteCode);
            sqlCreate.AppendQuote("CIGARETTENAME", cigaretteName);
            sqlCreate.AppendQuote("BARCODE", barode);
            sqlCreate.AppendQuote("STATE", state);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }

        public DataTable FindAll()
        {
            string sql = "SELECT A.*,"+
                            " B.CHANNELNAME ,CASE WHEN STATE='0' THEN 'Î´Èë¿â' ELSE 'ÒÑÈë¿â' END STRSTATE " +
                            " FROM AS_STOCK_IN A"+
                            " LEFT JOIN AS_BI_STOCKCHANNEL B ON A.CHANNELCODE = B.CHANNELCODE" +
                            " ORDER BY STOCKINID";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindCigarette()
        {
            string sql = "SELECT TOP 1 A.* ,B.QUANTITY - B.INQUANTITY QUANTITY FROM AS_STOCK_IN A " +
                            " LEFT JOIN AS_STOCK_IN_BATCH B ON A.BATCHNO = B.BATCHNO " + 
                            " WHERE A.STATE = '0' AND B.STATE = '0' " +
                            " ORDER BY BATCHNO,STOCKINID";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindCigarette(string barcode)
        {
            string sql = "SELECT TOP 1 A.* ,B.QUANTITY - B.INQUANTITY QUANTITY FROM AS_STOCK_IN A " +
                            " LEFT JOIN AS_STOCK_IN_BATCH B ON A.BATCHNO = B.BATCHNO " +
                            " WHERE A.STATE = '0' AND B.STATE = '0' AND A.BARCODE = '{0}'" +
                            " ORDER BY BATCHNO,STOCKINID";
            return ExecuteQuery(string.Format(sql, barcode)).Tables[0];
        }



        public void UpdateScanStatus(string stockInID)
        {
            string sql = "UPDATE AS_STOCK_IN SET STATE = '1' WHERE STOCKINID={0}";
            ExecuteNonQuery(string.Format(sql, stockInID));
        }

        //zys_2011-10-06
        public void UpdateStockOutIdToStockIn(DataTable table)
        {
            DataRow[] stockInRows = table.Select(string.Format("STOCKINID IS NOT NULL AND STOCKOUTID <> 0 "), "STOCKINID");
            foreach (DataRow row in stockInRows)
            {
                SqlCreate sqlCreate = new SqlCreate("AS_STOCK_IN", SqlType.UPDATE);
                sqlCreate.AppendQuote("STOCKOUTID", row["STOCKOUTID"]);
                sqlCreate.AppendWhere("STOCKINID", row["STOCKINID"]);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        //zys_2011-10-06
        public DataTable FindStockInForIsInAndNotOut()
        {
            string sql = "SELECT A.*,B.QUANTITY + D.REMAINQUANTITY - C.INQUANTITY STOCKINQUANTITY " +
                            " FROM AS_STOCK_IN A " +
                            " LEFT JOIN (SELECT A.CIGARETTECODE,A.CIGARETTENAME,COUNT(*) QUANTITY " +
                            " 			  FROM AS_SC_SUPPLY A " +
                            "             LEFT JOIN V_STOCKCHANNEL B ON A.CIGARETTECODE = B.CIGARETTECODE " +
                            "             WHERE B.CHANNELTYPE = '2' " +
                            "             GROUP BY A.CIGARETTECODE,A.CIGARETTENAME,B.CHANNELCODE " +
                            "            ) B ON A.CIGARETTECODE = B.CIGARETTECODE " +
                            " LEFT JOIN (SELECT CIGARETTECODE,ISNULL(SUM(INQUANTITY),0) INQUANTITY " +
                            " 			FROM AS_STOCK_IN_BATCH " +
                            " 			GROUP BY CIGARETTECODE " +
                            " 		    ) C ON A.CIGARETTECODE = C.CIGARETTECODE " +
                            " LEFT JOIN AS_BI_STOCKCHANNEL D ON A.CIGARETTECODE = D.CIGARETTECODE " +
                            " WHERE A.STATE = '1' AND ( A.STOCKOUTID IS NULL OR A.STOCKOUTID = 0) ";
            return ExecuteQuery(sql).Tables[0];
        }
    }
}
