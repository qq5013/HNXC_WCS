using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class StockOutDao: BaseDao
    {
        public void Delete()
        {
            string sql = "TRUNCATE TABLE AS_STOCK_OUT";
            ExecuteNonQuery(sql);
            sql = @"UPDATE AS_STATEMANAGER_ORDER SET ROW_INDEX = 0";
            ExecuteNonQuery(sql);
        }

        public int FindOutQuantity()
        {
            string sql = "SELECT COUNT(*) FROM AS_STOCK_OUT WHERE STATE='1' ";
            return Convert.ToInt32(ExecuteScalar(sql));
        }

        public void UpdateCigarette(string barcode, string CIGARETTECODE)
        {
            string sql = "UPDATE AS_SC_SUPPLY SET BARCODE = '{0}' WHERE CIGARETTECODE = '{1}'";
            ExecuteNonQuery(string.Format(sql, barcode, CIGARETTECODE));
            sql = "UPDATE AS_STOCK_IN SET BARCODE = '{0}' WHERE CIGARETTECODE = '{1}'";
            ExecuteNonQuery(string.Format(sql, barcode, CIGARETTECODE));
            sql = "UPDATE AS_STOCK_OUT SET BARCODE = '{0}' WHERE CIGARETTECODE = '{1}'";
            ExecuteNonQuery(string.Format(sql, barcode, CIGARETTECODE));
        }

        public DataTable FindAll()
        {
            string sql = " SELECT A.STOCKOUTID,A.LINECODE,A.CIGARETTECODE,A.CIGARETTENAME,D.CHANNELNAME AS SORT_CHANNELNAME," +
                            " CASE WHEN A.STATE=1 THEN 'ÒÑ·¢ËÍ' ELSE 'Î´·¢ËÍ' END STATE," +
                            " CASE WHEN SCAN_STATE_02 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN02, " +
                            " CASE WHEN SCAN_STATE_03 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN03, " +
                            " CASE WHEN SCAN_STATE_04 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN04, " +
                            " CASE WHEN SCAN_STATE_05 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN05, " +
                            " B.SUPPLYCARCODE,C.CHANNELNAME AS STOCK_CHANNELNAME" +
                            " FROM AS_STOCK_OUT A" +
                            " LEFT JOIN AS_BI_SUPPLYCAR B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE" +
                            " LEFT JOIN V_STOCKCHANNEL C ON A.CIGARETTECODE = C.CIGARETTECODE " + //!!!@
                            " LEFT JOIN AS_SC_CHANNELUSED D ON A.LINECODE = D.LINECODE AND A.CHANNELCODE = D.CHANNELCODE"+
                            " WHERE  (C.CHANNELTYPE =1 OR C.CHANNELTYPE = 2) AND D.CHANNELTYPE = 3" +
                            " UNION " +
                         " SELECT A.STOCKOUTID,A.LINECODE,A.CIGARETTECODE,A.CIGARETTENAME,D.CHANNELNAME AS SORT_CHANNELNAME," +
                            " CASE WHEN A.STATE=1 THEN 'ÒÑ·¢ËÍ' ELSE 'Î´·¢ËÍ' END STATE," +
                            " CASE WHEN SCAN_STATE_02 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN02, " +
                            " CASE WHEN SCAN_STATE_03 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN03, " +
                            " CASE WHEN SCAN_STATE_04 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN04, " +
                            " CASE WHEN SCAN_STATE_05 ='1' THEN 'ÒÑÉ¨Ãè' ELSE 'Î´É¨Ãè' END ISSCAN05, " +
                            " B.SUPPLYCARCODE,C.CHANNELNAME AS STOCK_CHANNELNAME" +
                            " FROM AS_STOCK_OUT A" +
                            " LEFT JOIN AS_BI_SUPPLYCAR B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE" +
                            " LEFT JOIN V_STOCKCHANNEL C ON A.CIGARETTECODE = C.CIGARETTECODE " + //!!!@
                            " LEFT JOIN AS_SC_CHANNELUSED D ON A.LINECODE = D.LINECODE AND A.CHANNELCODE = D.CHANNELCODE" +
                            " WHERE C.CHANNELTYPE = 3 AND D.CHANNELTYPE = 2";
            return ExecuteQuery(sql).Tables[0];
        }



        public string FindMinStockOutID(string channelCode)
        {
            string sql = "SELECT MIN(STOCKOUTID) FROM AS_STOCK_OUT A " +
                            " LEFT JOIN V_STOCKCHANNEL B ON A.CIGARETTECODE = B.CIGARETTECODE " + //!!!
                            " LEFT JOIN AS_SC_CHANNELUSED C ON A.LINECODE = C.LINECODE AND A.CHANNELCODE = C.CHANNELCODE "+
                            " WHERE B.CHANNELCODE='{0}' AND LED_STATE='0' AND C.CHANNELTYPE = 2";
            return ExecuteScalar(string.Format(sql, channelCode)).ToString();
        }

        public DataTable FindCigaretteForScanner(string scannerCode)
        {
            string sql = "SELECT  TOP 1 A.*,C.SUPPLYADDRESS FROM AS_STOCK_OUT A " +
                            " LEFT JOIN AS_BI_SCANNER B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE " +
                            " LEFT JOIN AS_SC_CHANNELUSED C ON A.LINECODE = C.LINECODE AND A.CHANNELCODE = C.CHANNELCODE " +
                            " WHERE SCAN_STATE_{0}='0'  AND B.SCANNERCODE = '{0}' " +
                            " ORDER BY BATCHNO,STOCKOUTID";
            return ExecuteQuery(string.Format(sql, scannerCode)).Tables[0];
        }

        public DataTable FindCigaretteForSupplyCar(string supplyCarCode)
        {
            string sql = "SELECT TOP 1 A.* FROM AS_STOCK_OUT A " +
                            " LEFT JOIN AS_BI_SUPPLYCAR B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE " +
                            " WHERE  A.STATE='1' AND A.ISSCAN='1' AND A.SUPPLYCARSTATE='0' AND B.SUPPLYCARCODE = '{0}' " +
                            " ORDER BY A.BATCHNO,A.STOCKOUTID";
            return ExecuteQuery(string.Format(sql, supplyCarCode)).Tables[0];
        }





        public DataTable FindLEDData(string channelCode)
        {            
            string sql = "SELECT TOP 50 B.CHANNELCODE SCHANNELCODE,A.* FROM AS_STOCK_OUT A " +
                            " LEFT JOIN V_STOCKCHANNEL B ON A.CIGARETTECODE = B.CIGARETTECODE  " + //!!!
                            " LEFT JOIN AS_SC_CHANNELUSED C ON A.LINECODE = C.LINECODE AND A.CHANNELCODE = C.CHANNELCODE " +
                            " WHERE B.CHANNELCODE='{0}' AND LED_STATE='0' AND C.CHANNELTYPE = 2" +
                            " ORDER BY STOCKOUTID";
            return ExecuteQuery(string.Format(sql, channelCode)).Tables[0];
        }

        public void UpdateLEDStatus(string stockOutID)
        {
            string sql = "UPDATE AS_STOCK_OUT SET LED_STATE = '1' WHERE STOCKOUTID='{0}'";
            ExecuteNonQuery(string.Format(sql, stockOutID));
        }

        public void UpdateScanStatus(string outID, string scannerCode)
        {
            string sql = "UPDATE AS_STOCK_OUT SET SCAN_STATE_{0} = '1' WHERE STOCKOUTID={1}";
            ExecuteNonQuery(string.Format(sql,scannerCode, outID));
        }

        public void UpdateSupplyCarStatus(string outID)
        {
            string sql = "UPDATE AS_STOCK_OUT SET SUPPLYCARSTATE = '1' WHERE STOCKOUTID={0}";
            ExecuteNonQuery(string.Format(sql, outID));
        }

        public void ClearNoScanData()
        {
            string sql = string.Format("UPDATE AS_STOCK_OUT SET STATE = '0' WHERE STATE = 1 AND SCAN_STATE_02 = '{0}'", "0");
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE AS_STOCK_IN SET STOCKOUTID='0' WHERE STOCKOUTID >  (SELECT ISNULL(MAX(STOCKOUTID),0) FROM AS_STOCK_OUT WHERE STATE = 1)");
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE AS_STOCK_OUT_BATCH SET OUTQUANTITY = 0");
            ExecuteNonQuery(sql);
        }

        //zys_2011-10-06
        internal int FindMaxOutID()
        {
            string sql = "SELECT ISNULL(MAX(STOCKOUTID),0) FROM AS_STOCK_OUT";
            return Convert.ToInt32(ExecuteScalar(sql));
        }

        //zys_2011-10-05
        internal void Insert(int outID, DataTable supplyTable)
        {
            foreach (DataRow row in supplyTable.Rows)
            {
                SqlCreate sqlCreate = new SqlCreate("AS_STOCK_OUT", SqlType.INSERT);
                sqlCreate.Append("STOCKOUTID", ++outID);
                sqlCreate.AppendQuote("ORDERDATE", row["ORDERDATE"]);
                sqlCreate.Append("BATCHNO", row["BATCHNO"]);
                sqlCreate.AppendQuote("LINECODE", row["LINECODE"]);
                sqlCreate.Append("SORTNO", row["SORTNO"]);
                sqlCreate.Append("SERIALNO", row["SERIALNO"]);
                sqlCreate.AppendQuote("CIGARETTECODE", row["CIGARETTECODE"]);
                sqlCreate.AppendQuote("CIGARETTENAME", row["CIGARETTENAME"]);
                sqlCreate.AppendQuote("BARCODE", row["BARCODE"]);
                sqlCreate.AppendQuote("CHANNELCODE", row["CHANNELCODE"]);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        //zys_2011-10-06
        public void UpdateStatus(DataTable table)
        {
            DataRow[] stockOutRows = table.Select(string.Format("STATE = '1'"), "STOCKOUTID");
            foreach (DataRow row in stockOutRows)
            {
                SqlCreate sqlCreate = new SqlCreate("AS_STOCK_OUT", SqlType.UPDATE);
                sqlCreate.AppendQuote("STATE", "1");
                sqlCreate.AppendWhere("STOCKOUTID", row["STOCKOUTID"]);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        //zys_2011-10-06
        public DataTable FindSupply()
        {
            string sql = "SELECT B.CHANNELCODE SCHANNELCODE,A.* FROM AS_STOCK_OUT A " +
                            " LEFT JOIN V_STOCKCHANNEL B ON A.CIGARETTECODE = B.CIGARETTECODE " +
                            " WHERE STATE='0'  AND B.CHANNELTYPE = '2' ORDER BY STOCKOUTID";
            return ExecuteQuery(sql).Tables[0];
        }
    }
}
