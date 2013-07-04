using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class StockInBatchDao:BaseDao 
    {
        public void Delete()
        {
            string sql = "TRUNCATE TABLE AS_STOCK_IN_BATCH";
            ExecuteNonQuery(sql);
        }

        public int FindMaxBatchNo()
        {
            string sql = "SELECT ISNULL(MAX(BATCHNO),0) FROM AS_STOCK_IN_BATCH ";
            return Convert.ToInt32(ExecuteScalar(sql));
        }

        public void InsertBatch(int batchNo, string channelCode, string cigaretteCode, string cigaretteName, int quantity, int StockRemainQuantity)
        {
            SqlCreate sqlCreate = new SqlCreate("AS_STOCK_IN_BATCH", SqlType.INSERT);
            sqlCreate.Append("BATCHNO", batchNo);
            sqlCreate.AppendQuote("CHANNELCODE", channelCode);
            sqlCreate.AppendQuote("CIGARETTECODE", cigaretteCode);
            sqlCreate.AppendQuote("CIGARETTENAME", cigaretteName);
            sqlCreate.Append("QUANTITY", quantity);
            sqlCreate.Append("INQUANTITY", StockRemainQuantity);
            sqlCreate.AppendQuote("T1", DateTime.Now.ToString());
            sqlCreate.AppendQuote("STATE", StockRemainQuantity == quantity ? "1" : "0");
            string sql = sqlCreate.GetSQL();
            ExecuteNonQuery(sql);
        }

        public DataTable FindAll()
        {
            string sql = "SELECT A.*,B.CHANNELNAME ,CASE WHEN STATE='0' THEN '未完成' ELSE '已完成' END STRSTATE ," +
                            " ISNULL((SELECT TOP 1 BARCODE FROM AS_SC_SUPPLY WHERE CIGARETTECODE = A.CIGARETTECODE),'') AS BARCODE "+
                            " FROM AS_STOCK_IN_BATCH A "+
                            " LEFT JOIN AS_BI_STOCKCHANNEL B ON A.CHANNELCODE = B.CHANNELCODE" +
                            " ORDER BY BATCHNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindStockInTopAnyBatch()
        {
            string sql = "SELECT TOP 10 CIGARETTENAME,QUANTITY - INQUANTITY AS QUANTITY FROM AS_STOCK_IN_BATCH "+
                            " WHERE STATE = 0 ORDER BY BATCHNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindStockInBatch(string cigaretteCode)
        {
            string sql = "SELECT * FROM AS_STOCK_IN_BATCH "+
                            " WHERE STATE = 0 AND CIGARETTECODE = '{0}' "+
                            " ORDER BY BATCHNO";
            return ExecuteQuery(string.Format(sql,cigaretteCode)).Tables[0];
        }

        public void UpdateQuantityForBatch(string batchNo)
        {
            string sql = "UPDATE AS_STOCK_IN_BATCH SET INQUANTITY = INQUANTITY + 1 WHERE BATCHNO = {0}";
            ExecuteNonQuery(string.Format(sql,batchNo));
            sql = "UPDATE AS_STOCK_IN_BATCH SET T2 = GETDATE() WHERE BATCHNO = {0} AND T2 IS NULL ";
            ExecuteNonQuery(string.Format(sql, batchNo));
        }

        public void UpdateState(string batchNo)
        {
            string sql = "UPDATE AS_STOCK_IN_BATCH SET STATE = '1',T3 = GETDATE() WHERE BATCHNO = {0}";
            ExecuteNonQuery(string.Format(sql, batchNo));
        }
    }
}
