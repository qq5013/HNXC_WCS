using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class ServerDao: BaseDao
    {
        public DataTable FindBatch()
        {
            string sql = "SELECT  TOP 1 BATCHID,ORDERDATE,BATCHNO  FROM AS_BI_BATCH "+
                            " WHERE ISUPTONOONEPRO='1' AND ISDOWNLOAD = '0' "+
                            " ORDER BY ORDERDATE,BATCHNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindStockChannel(string orderDate,string batchNo)
        {
            string sql = "SELECT CHANNELCODE, CHANNELNAME, CHANNELTYPE, CIGARETTECODE, CIGARETTENAME, QUANTITY, REMAINQUANTITY, "+
                            " ORDERNO, LEDNO, STATUS, ISSTOCKIN FROM AS_SC_STOCKCHANNELUSED WHERE ORDERDATE = '{0}' AND BATCHNO={1}";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo)).Tables[0];
        }

        public DataTable FindMixChannel(string orderDate, string batchNo)
        {
            string sql = "SELECT * FROM AS_SC_STOCKMIXCHANNEL "+
                            " WHERE ORDERDATE = '{0}' AND BATCHNO={1}";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo)).Tables[0];
        }

        public DataTable FindSupply(string orderDate, string batchNo)
        {
            string sql = "SELECT A.*,ltrim(rtrim(B.BARCODE)) BARCODE FROM AS_SC_SUPPLY A "+
                            " LEFT JOIN AS_BI_CIGARETTE B ON A.CIGARETTECODE=B.CIGARETTECODE " +
                            " WHERE ORDERDATE='{0}' AND BATCHNO = {1} "+
                            " ORDER BY LINECODE,SERIALNO";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo)).Tables[0];
        }

        public DataTable FindChannelUSED(string orderDate, string batchNo)
        {
            string sql = "SELECT A.* FROM AS_SC_CHANNELUSED A "+
                            " LEFT JOIN AS_BI_CHANNEL B ON A.CHANNELID = B.CHANNELID "+
                            " WHERE ORDERDATE='{0}' AND BATCHNO={1} ";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo)).Tables[0];
        }

        /// <summary>
        /// 打印烟道补货报表查询语句,留烟问题未处理。
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="linecCode"></param>
        /// <returns></returns>
        public DataTable FindChannelUSED(string orderDate, string batchNo,string linecCode)
        {
            string sql = "SELECT A.*,"+
                            " CASE A.CHANNELTYPE "+
                            " 	WHEN '3' THEN '通道机' "+
                            " 	WHEN '5' THEN '混合烟道'  "+
                            " 	WHEN '2' THEN '立式机' "+
                            " END CHANNELTYPENAME,"+
                            " A.QUANTITY / 50 BOXQUANTITY,"+
                            " A.QUANTITY % 50 ITEMQUANTITY,"+
                            " ISNULL(B.QUANTITY,0) BALANCEQUANTITY"+
                            " FROM AS_SC_CHANNELUSED A"+
                            " LEFT JOIN AS_SC_BALANCE_HISTORY B" +
                            " ON A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO "+
                            " AND A.LINECODE = B.LINECODE  AND A.CHANNELCODE = B.CHANNELCODE"+
                            " WHERE  A.ORDERDATE='{0}' AND A.BATCHNO='{1}' AND A.LINECODE='{2}'";
            return ExecuteQuery(string.Format(sql,orderDate,batchNo,linecCode)).Tables[0];
        }

        public DataTable FindPrintBatchTable()
        {
            string sql = "SELECT DISTINCT "+
                            " CONVERT(varchar(10),ORDERDATE,120) + '|' + ltrim(STR(BATCHNO)) + '|' + LINECODE AS BATCHINFO," +
                            " CONVERT(varchar(10),ORDERDATE,120) + ' 第 ' + ltrim(STR(BATCHNO)) + ' 批次 ' + LINECODE + ' 号分拣线'  AS BATCHNAME"+
                            " FROM AS_SC_CHANNELUSED ";
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateBatchStatus(string batchID)
        {
            string sql = "UPDATE AS_BI_BATCH SET ISDOWNLOAD='1' WHERE BATCHID={0}";
            ExecuteNonQuery(string.Format(sql, batchID));
        }

        public void UpdateCigaretteToServer(string barcode, string CIGARETTECODE)
        {
            string sql = "UPDATE AS_BI_CIGARETTE SET BARCODE = '{0}' WHERE CIGARETTECODE = '{1}'";
            ExecuteNonQuery(string.Format(sql, barcode, CIGARETTECODE));
        }
    }
}
