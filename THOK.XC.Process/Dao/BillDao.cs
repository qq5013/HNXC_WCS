using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class BillDao : BaseDao
    {
      
        /// <summary>
        /// 二楼出库托盘校验出错，生成退库单,返回TaskID
        /// </summary>
        /// <param name="blnOne">true,一楼入库</param>
        /// <returns>TaskID</returns>
        public string CreateCancelBillInTask(string TaskID,string Bill_No)
        {
            string strBillNo = GetBillNo("IS");

            

            string strSQL = string.Format("INSERT INTO WMS_BILL_MASTER (BILL_NO,BILL_DATE,BTYPE_CODE,WAREHOUSE_CODE,STATUS,STATE,OPERATER,OPERATE_DATE,CHECKER,CHECK_DATE, TASKER,TASK_DATE,BILL_METHOD,SOURCE_BILLNO,SCHEDULE_ITEMNO)" +
                "values ('{0}',SYSDATE,'007','001','1','3','000001',SYSDATE,'000001',SYSDATE,'000001',SYSDATE,'0','{1}',0)", strBillNo, Bill_No);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("INSERT INTO WMS_BILL_DETAIL(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE) " +
                    "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE FROM  WMS_BILL_DETAIL WHERE BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'", strBillNo, TaskID);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("INSERT INTO WMS_PRODUCT_STATE(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,CELL_CODE,PRODUCT_BARCODE,PALLET_CODE,IS_MIX) " +
                     "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,CELL_CODE,PRODUCT_BARCODE,PALLET_CODE,IS_MIX FROM WMS_PRODUCT_STATE WHERE  BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'", strBillNo, TaskID);
            ExecuteNonQuery(strSQL);

            string Source_BillNO="";
            strSQL = string.Format("SELECT CMD_CELL.BILL_NO FROM CMD_CELL INNER JOIN WCS_TASK ON WCS_TASK.CELL_CODE=CMD_CELL.CELL_CODE " +
                     "WHERE TASK_ID='{0}'", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                Source_BillNO = dt.Rows[0][0].ToString();

            strSQL = string.Format("INSERT INTO WCS_TASK(TASK_ID,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,IS_MIX,CELL_CODE,SOURCE_BILLNO)" +
                 "SELECT STATE.BILL_NO||LPAD(ITEM_NO, 2, '0'),BTYPE.TASK_TYPE ,BTYPE.TASK_LEVEL,STATE.BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,'0',TASK_DATE,TASKER,0,IS_MIX,CELL_CODE,'{1}' FROM  WMS_PRODUCT_STATE STATE " +
                  "LEFT JOIN WMS_BILL_MASTER BILL ON STATE.BILL_NO=BILL.BILL_NO " +
                  "LEFT JOIN CMD_BILL_TYPE BTYPE ON BILL.BTYPE_CODE=BTYPE.BTYPE_CODE WHERE  STATE.BILL_NO='{0}'", strBillNo, Source_BillNO);
            ExecuteNonQuery(strSQL);

            return strBillNo + "01";

        }
        ///// <summary>
        ///// 二楼出库托盘校验出错，补充生成 出库单。
        ///// </summary>
        ///// <returns>TaskID</returns>
        //public string CreateCancelBillOutTask(string TaskID, string Bill_No)
        //{
        //    string strBillNo = GetBillNo("OS");

        //    string strSQL = string.Format("INSERT INTO WMS_BILL_MASTER (BILL_NO,BILL_DATE,BTYPE_CODE,WAREHOUSE_CODE,STATUS,STATE,OPERATER,OPERATE_DATE,CHECKER,CHECK_DATE, TASKER,TASK_DATE,BILL_METHOD,SOURCE_BILLNO)" +
        //        "values ('{0}',SYSDATE,'002','001','1','3','000001',SYSDATE,'000001',SYSDATE,'000001',SYSDATE,'0','{1}')", strBillNo, Bill_No);
        //    ExecuteNonQuery(strSQL);
        //    strSQL = string.Format("INSERT INTO WMS_BILL_DETAIL(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE) " +
        //         "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE FROM  WMS_BILL_DETAIL WHERE BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'", TaskID, Bill_No);
        //    ExecuteNonQuery(strSQL);

        //    strSQL = string.Format("INSERT INTO WMS_PRODUCT_STATE(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX,FORDERBILLNO,FORDER) " +
        //          "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX,'{2}',FORDER FROM WMS_PRODUCT_STATE WHERE  BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'",strBillNo, TaskID, Bill_No);
        //    ExecuteNonQuery(strSQL);


        //    //分配货位
        //    strSQL = string.Format("SELECT DISTINCT CELL.CELL_CODE,CELL.PRODUCT_CODE,CELL.PRODUCT_BARCODE,CELL.PALLET_CODE,CELL.IN_DATE FROM CMD_CELL CELL INNER JOIN WMS_BILL_MASTER WBM ON CELL.BILL_NO=WBM.BILL_NO " +
        //           "INNER JOIN WMS_PRODUCT_STATE PS ON PS.PRODUCT_CODE=CELL.PRODUCT_CODE AND PS.PRODUCT_BARCODE=CELL.PRODUCT_BARCODE " +
        //           "INNER JOIN (SELECT PRODUCT_CODE,REAL_WEIGHT,BILL_METHOD,CIGARETTE_CODE,FORMULA_CODE,IS_MIX FROM WMS_PRODUCT_STATE PS " +
        //           "INNER JOIN WMS_BILL_MASTER BM ON PS.BILL_NO=BM.BILL_NO WHERE  PS.BILL_NO||LPAD(ITEM_NO, 2, '0')='{0}') TMP " +
        //           "ON TMP.BILL_METHOD=WBM.BILL_METHOD AND TMP.CIGARETTE_CODE=WBM.CIGARETTE_CODE AND TMP.FORMULA_CODE=WBM.FORMULA_CODE " +
        //           "AND TMP.PRODUCT_CODE=CELL.PRODUCT_CODE AND TMP.REAL_WEIGHT=CELL.REAL_WEIGHT  AND TMP.IS_MIX=PS.IS_MIX " +
        //           "WHERE  CELL.IS_LOCK='0' AND CELL.IS_ACTIVE='1' AND CELL.ERROR_FLAG='0'  ORDER BY CELL.IN_DATE", TaskID);


        //    DataTable dt = ExecuteQuery(strSQL).Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];
        //        strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET CELL_CODE='{0}',PRODUCT_BARCODE='{1}',PALLET_CODE='{2}' WHERE BILL_NO='{3}'", new string[] { dr["CELL_CODE"].ToString(), dr["PRODUCT_BARCODE"].ToString(), dr["PALLET_CODE"].ToString(), strBillNo });
        //        ExecuteNonQuery(strSQL);


        //        strSQL = string.Format("INSERT INTO WCS_TASK(TASK_ID,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,IS_MIX,CELL_CODE,FORDERBILLNO,FORDER)" +
        //             "SELECT STATE.BILL_NO||LPAD(ITEM_NO, 2, '0'),BTYPE.TASK_TYPE ,BTYPE.TASK_LEVEL,STATE.BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,'0',TASK_DATE,TASKER,0,IS_MIX,CELL_CODE,FORDERBILLNO,FORDER FROM  WMS_PRODUCT_STATE STATE " +
        //              "LEFT JOIN WMS_BILL_MASTER BILL ON STATE.BILL_NO=BILL.BILL_NO " +
        //              "LEFT JOIN CMD_BILL_TYPE BTYPE ON BILL.BTYPE_CODE=BTYPE.BTYPE_CODE WHERE  STATE.BILL_NO='{0}'", strBillNo);
        //        ExecuteNonQuery(strSQL);
        //    }
          

        //    return strBillNo + "01";

          
        //}

        /// <summary>
        /// 二楼出库托盘校验出错，由用户选定出库的入库单号OutBillNO， 补充生成 出库单。
        /// </summary>
        /// <returns>TaskID</returns>
        public string CreateCancelBillOutTask(string TaskID, string Bill_No, string OutBillNO)
        {
            string strBillNo = GetBillNo("OS");

            string strSQL = string.Format("INSERT INTO WMS_BILL_MASTER (BILL_NO,BILL_DATE,BTYPE_CODE,WAREHOUSE_CODE,STATUS,STATE,OPERATER,OPERATE_DATE,CHECKER,CHECK_DATE, TASKER,TASK_DATE,BILL_METHOD,SOURCE_BILLNO)" +
                "values ('{0}',SYSDATE,'002','001','1','3','000001',SYSDATE,'000001',SYSDATE,'000001',SYSDATE,'0','{1}')", strBillNo, Bill_No);
            ExecuteNonQuery(strSQL);
            strSQL = string.Format("INSERT INTO WMS_BILL_DETAIL(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE) " +
                 "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE FROM  WMS_BILL_DETAIL WHERE BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'", TaskID, Bill_No);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("INSERT INTO WMS_PRODUCT_STATE(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX) " +
                  "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX FROM WMS_PRODUCT_STATE WHERE  BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'", strBillNo, TaskID);
            ExecuteNonQuery(strSQL);


            //分配货位
            strSQL = string.Format("SELECT DISTINCT CELL.CELL_CODE,CELL.PRODUCT_CODE,CELL.PRODUCT_BARCODE,CELL.PALLET_CODE,CELL.IN_DATE FROM CMD_CELL CELL INNER JOIN WMS_BILL_MASTER WBM ON CELL.BILL_NO=WBM.BILL_NO " +
                   "INNER JOIN WMS_PRODUCT_STATE PS ON PS.PRODUCT_CODE=CELL.PRODUCT_CODE AND PS.PRODUCT_BARCODE=CELL.PRODUCT_BARCODE " +
                   "INNER JOIN (SELECT PRODUCT_CODE,REAL_WEIGHT,BILL_METHOD,CIGARETTE_CODE,FORMULA_CODE,IS_MIX FROM WMS_PRODUCT_STATE PS " +
                   "INNER JOIN WMS_BILL_MASTER BM ON PS.BILL_NO=BM.BILL_NO WHERE  PS.BILL_NO||LPAD(ITEM_NO, 2, '0')='{0}') TMP " +
                   "ON TMP.BILL_METHOD=WBM.BILL_METHOD AND TMP.CIGARETTE_CODE=WBM.CIGARETTE_CODE AND TMP.FORMULA_CODE=WBM.FORMULA_CODE " +
                   "AND TMP.PRODUCT_CODE=CELL.PRODUCT_CODE AND TMP.REAL_WEIGHT=CELL.REAL_WEIGHT  AND TMP.IS_MIX=PS.IS_MIX " +
                   "WHERE  CELL.IS_LOCK='0' AND CELL.IS_ACTIVE='1' AND CELL.ERROR_FLAG='0' AND WBM.BILL_NO='{1}'  ORDER BY CELL.IN_DATE", TaskID, OutBillNO);


            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET CELL_CODE='{0}',PRODUCT_BARCODE='{1}',PALLET_CODE='{2}' WHERE BILL_NO='{3}'", new string[] { dr["CELL_CODE"].ToString(), dr["PRODUCT_BARCODE"].ToString(), dr["PALLET_CODE"].ToString(), strBillNo });
                ExecuteNonQuery(strSQL);


                strSQL = string.Format("INSERT INTO WCS_TASK(TASK_ID,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,IS_MIX,CELL_CODE)" +
                     "SELECT STATE.BILL_NO||LPAD(ITEM_NO, 2, '0'),BTYPE.TASK_TYPE ,BTYPE.TASK_LEVEL,STATE.BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,'0',TASK_DATE,TASKER,0,IS_MIX,CELL_CODE FROM  WMS_PRODUCT_STATE STATE " +
                      "LEFT JOIN WMS_BILL_MASTER BILL ON STATE.BILL_NO=BILL.BILL_NO " +
                      "LEFT JOIN CMD_BILL_TYPE BTYPE ON BILL.BTYPE_CODE=BTYPE.BTYPE_CODE WHERE  STATE.BILL_NO='{0}'", strBillNo);
                ExecuteNonQuery(strSQL);
            }


            return strBillNo + "01";


        }

        /// <summary>
        /// 二楼出库堆垛机出现问题，由用户选定出库的入库单号OutBillNO（其它堆垛机的入库单）， 补充生成新出库单。
        /// </summary>
        /// <returns>TaskID</returns>
        public string CreateCancelBillOutTask(string TaskID, string Bill_No, string OutBillNO, string CraneNo)
        {
            string strBillNo = GetBillNo("OS");

            string strSQL = string.Format("INSERT INTO WMS_BILL_MASTER (BILL_NO,BILL_DATE,BTYPE_CODE,WAREHOUSE_CODE,STATUS,STATE,OPERATER,OPERATE_DATE,CHECKER,CHECK_DATE, TASKER,TASK_DATE,BILL_METHOD,SOURCE_BILLNO)" +
                "values ('{0}',SYSDATE,'002','001','1','3','000001',SYSDATE,'000001',SYSDATE,'000001',SYSDATE,'0','{1}')", strBillNo, Bill_No);
            ExecuteNonQuery(strSQL);
            strSQL = string.Format("INSERT INTO WMS_BILL_DETAIL(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE) " +
                 "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE FROM  WMS_BILL_DETAIL WHERE BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'", TaskID, Bill_No);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("INSERT INTO WMS_PRODUCT_STATE(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX) " +
                  "SELECT '{0}',1,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX FROM WMS_PRODUCT_STATE WHERE  BILL_NO||LPAD(ITEM_NO, 2, '0')='{1}'", strBillNo, TaskID);
            ExecuteNonQuery(strSQL);


            //分配货位
            strSQL = string.Format("SELECT DISTINCT CELL.CELL_CODE,CELL.PRODUCT_CODE,CELL.PRODUCT_BARCODE,CELL.PALLET_CODE,CELL.IN_DATE FROM CMD_CELL CELL INNER JOIN WMS_BILL_MASTER WBM ON CELL.BILL_NO=WBM.BILL_NO " +
                   "INNER JOIN WMS_PRODUCT_STATE PS ON PS.PRODUCT_CODE=CELL.PRODUCT_CODE AND PS.PRODUCT_BARCODE=CELL.PRODUCT_BARCODE " +
                   "INNER JOIN (SELECT PRODUCT_CODE,REAL_WEIGHT,BILL_METHOD,CIGARETTE_CODE,FORMULA_CODE,IS_MIX FROM WMS_PRODUCT_STATE PS " +
                   "INNER JOIN WMS_BILL_MASTER BM ON PS.BILL_NO=BM.BILL_NO WHERE  PS.BILL_NO||LPAD(ITEM_NO, 2, '0')='{0}') TMP " +
                   "ON TMP.BILL_METHOD=WBM.BILL_METHOD AND TMP.CIGARETTE_CODE=WBM.CIGARETTE_CODE AND TMP.FORMULA_CODE=WBM.FORMULA_CODE " +
                   "AND TMP.PRODUCT_CODE=CELL.PRODUCT_CODE AND TMP.REAL_WEIGHT=CELL.REAL_WEIGHT  AND TMP.IS_MIX=PS.IS_MIX " +
                   " INNER JOIN CMD_SHELF ON CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                   "WHERE  CELL.IS_LOCK='0' AND CELL.IS_ACTIVE='1' AND CELL.ERROR_FLAG='0' AND WBM.BILL_NO='{1}' AND CMD_SHELF.CRANE_NO<>'{2}'  ORDER BY CELL.IN_DATE", TaskID, OutBillNO, CraneNo);


            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET CELL_CODE='{0}',PRODUCT_BARCODE='{1}',PALLET_CODE='{2}' WHERE BILL_NO='{3}'", new string[] { dr["CELL_CODE"].ToString(), dr["PRODUCT_BARCODE"].ToString(), dr["PALLET_CODE"].ToString(), strBillNo });
                ExecuteNonQuery(strSQL);


                strSQL = string.Format("INSERT INTO WCS_TASK(TASK_ID,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,IS_MIX,CELL_CODE)" +
                     "SELECT STATE.BILL_NO||LPAD(ITEM_NO, 2, '0'),BTYPE.TASK_TYPE ,BTYPE.TASK_LEVEL,STATE.BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,'0',TASK_DATE,TASKER,0,IS_MIX,CELL_CODE FROM  WMS_PRODUCT_STATE STATE " +
                      "LEFT JOIN WMS_BILL_MASTER BILL ON STATE.BILL_NO=BILL.BILL_NO " +
                      "LEFT JOIN CMD_BILL_TYPE BTYPE ON BILL.BTYPE_CODE=BTYPE.BTYPE_CODE WHERE  STATE.BILL_NO='{0}'", strBillNo);
                ExecuteNonQuery(strSQL);
            }


            return strBillNo + "01";


        }
        /// <summary>
        /// 根据 错误烟包 查找相同入库单据信息，供用户选择入库单号。
        /// </summary>
        /// <returns></returns>
        public DataTable GetCancelBillNo(string TaskID)
        {
            string strSQL = string.Format("SELECT DISTINCT WBM.BILL_NO,BILL_DATE,WBM.OPERATER,WBM.OPERATE_DATE,WBM.BILL_METHOD,WBM.CIGARETTE_CODE,CIGARETTE_NAME, WBM.FORMULA_CODE,WFM.FORMULA_NAME, BATCH_WEIGHT,CELL.IN_DATE " +
                            "FROM CMD_CELL CELL INNER JOIN WMS_BILL_MASTER WBM ON CELL.BILL_NO=WBM.BILL_NO " +
                            "INNER JOIN WMS_PRODUCT_STATE PS ON PS.PRODUCT_CODE=CELL.PRODUCT_CODE AND PS.PRODUCT_BARCODE=CELL.PRODUCT_BARCODE " +
                            "INNER JOIN (SELECT PRODUCT_CODE,REAL_WEIGHT,BILL_METHOD,CIGARETTE_CODE,FORMULA_CODE,IS_MIX FROM WMS_PRODUCT_STATE PS " +
                            "INNER JOIN WMS_BILL_MASTER BM ON PS.BILL_NO=BM.BILL_NO WHERE PS.BILL_NO||LPAD(ITEM_NO, 2, '0')='{0}') TMP  " +
                            "ON TMP.BILL_METHOD=WBM.BILL_METHOD AND TMP.CIGARETTE_CODE=WBM.CIGARETTE_CODE AND TMP.FORMULA_CODE=WBM.FORMULA_CODE " +
                            "AND TMP.PRODUCT_CODE=CELL.PRODUCT_CODE AND TMP.REAL_WEIGHT=CELL.REAL_WEIGHT  AND TMP.IS_MIX=PS.IS_MIX " +
                            "INNER JOIN CMD_CIGARETTE ON CMD_CIGARETTE.CIGARETTE_CODE=WBM.CIGARETTE_CODE " +
                            "INNER JOIN WMS_FORMULA_MASTER WFM ON WFM.FORMULA_CODE=WBM.FORMULA_CODE " +
                            "WHERE CELL.IS_LOCK='0' AND CELL.IS_ACTIVE='1' AND CELL.ERROR_FLAG='0' " +
                            "ORDER BY DECODE(CELL.IN_DATE,NULL,'0000', IN_DATE) ", TaskID);

            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 根据 错误烟包 查找相同入库单据信息，供用户选择入库单号。
        /// </summary>
        /// <returns></returns>
        public DataTable GetCancelBillNo(string TaskID,string CraneNO)
        {
            string strSQL = string.Format("SELECT DISTINCT WBM.BILL_NO,BILL_DATE,WBM.OPERATER,WBM.OPERATE_DATE,WBM.BILL_METHOD,WBM.CIGARETTE_CODE,CIGARETTE_NAME, WBM.FORMULA_CODE,WFM.FORMULA_NAME, BATCH_WEIGHT,CELL.IN_DATE " +
                            "FROM CMD_CELL CELL INNER JOIN WMS_BILL_MASTER WBM ON CELL.BILL_NO=WBM.BILL_NO " +
                            "INNER JOIN WMS_PRODUCT_STATE PS ON PS.PRODUCT_CODE=CELL.PRODUCT_CODE AND PS.PRODUCT_BARCODE=CELL.PRODUCT_BARCODE " +
                            "INNER JOIN (SELECT PRODUCT_CODE,REAL_WEIGHT,BILL_METHOD,CIGARETTE_CODE,FORMULA_CODE,IS_MIX FROM WMS_PRODUCT_STATE PS " +
                            "INNER JOIN WMS_BILL_MASTER BM ON PS.BILL_NO=BM.BILL_NO WHERE PS.BILL_NO||LPAD(ITEM_NO, 2, '0')='{0}') TMP  " +
                            "ON TMP.BILL_METHOD=WBM.BILL_METHOD AND TMP.CIGARETTE_CODE=WBM.CIGARETTE_CODE AND TMP.FORMULA_CODE=WBM.FORMULA_CODE " +
                            "AND TMP.PRODUCT_CODE=CELL.PRODUCT_CODE AND TMP.REAL_WEIGHT=CELL.REAL_WEIGHT  AND TMP.IS_MIX=PS.IS_MIX " +
                            "INNER JOIN CMD_CIGARETTE ON CMD_CIGARETTE.CIGARETTE_CODE=WBM.CIGARETTE_CODE " +
                            "INNER JOIN WMS_FORMULA_MASTER WFM ON WFM.FORMULA_CODE=WBM.FORMULA_CODE " +
                            "INNER JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                            "WHERE CELL.IS_LOCK='0' AND CELL.IS_ACTIVE='1' AND CELL.ERROR_FLAG='0' AND CMD_SHELF.CRANE_NO<>'{1}' " +
                            "ORDER BY DECODE(CELL.IN_DATE,NULL,'0000', IN_DATE) ", TaskID,CraneNO);

            return ExecuteQuery(strSQL).Tables[0];
        }


        private string GetBillNo(string PREFIX_CODE)
        {
            string strSQL = string.Format("SELECT * FROM SYS_TABLE_CODE WHERE PREFIX_CODE='{0}'", PREFIX_CODE);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            DataRow dr = dt.Rows[0];
            string strNew = "";
            string PreCode = PREFIX_CODE + DateTime.Now.ToString(dr["DATE_FORMAT"].ToString());
            string SuqueceNo = "";
            for (int i = 0; i < int.Parse(dt.Rows[0]["SERIAL_LENGTH"].ToString()); i++)
            {
                SuqueceNo += "[0-9]";
            }
            strSQL = string.Format("SELECT MAX({1}) as AA FROM {0} WHERE REGEXP_LIKE({1},'^{2}$')", dr["TABLE_NAME"].ToString(), dr["FIELD_NAME"].ToString(), PreCode + SuqueceNo);
            dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string value = dt.Rows[0][0].ToString();
                strNew = PreCode + (int.Parse(value.Substring(PreCode.Length, int.Parse(dr["SERIAL_LENGTH"].ToString()))) + 1).ToString().PadLeft(int.Parse(dr["SERIAL_LENGTH"].ToString()), '0');
            }
            else
            {
                strNew = PreCode + "1".PadLeft(int.Parse(dr["SERIAL_LENGTH"].ToString()), '0');
            }
            return strNew;
        }
        /// <summary>
        /// 空托盘组出库单
        /// </summary>
        /// <param name="TARGET_CODE"></param>
        /// <returns></returns>
        public string CreatePalletOutBillTask(string TARGET_CODE)
        {
            string strSQL = string.Format("SELECT * FROM WCS_TASK WHERE PRODUCT_CODE='0000' AND STATE IN (0,1) AND TASK_TYPE='12' AND TARGET_CODE='{0}'", TARGET_CODE);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count == 0)
            {

                StoredProcParameter parameters = new StoredProcParameter();
                parameters.AddParameter("VCELL", "", DbType.String, ParameterDirection.Output);
                ExecuteNonQuery("APPLYPALLETOUTCELL", parameters);
                string VCell = parameters["VCELL"].ToString();

                if (VCell != "-1")
                {
                    string strBillNo = GetBillNo("PK");

                    strSQL = string.Format("INSERT INTO WMS_BILL_MASTER (BILL_NO,BILL_DATE,BTYPE_CODE,WAREHOUSE_CODE,STATUS,STATE,OPERATER,OPERATE_DATE,CHECKER,TASKER,TASK_DATE,BILL_METHOD,SCHEDULE_ITEMNO,TARGET_CODE)" +
                                                  "values ('{0}',SYSDATE,'011','001','1','3','000001',SYSDATE,'000001','000001',SYSDATE,'0',0,'{1}')", strBillNo, TARGET_CODE);
                    ExecuteNonQuery(strSQL);
                    strSQL = string.Format("INSERT INTO WMS_BILL_DETAIL(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX)" +
                           " VALUES('{0}',1,'0000',0,0,0,0,0)", strBillNo);
                    ExecuteNonQuery(strSQL);
                    strSQL = string.Format("INSERT INTO WMS_PRODUCT_STATE(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX,CELL_CODE)" + "VALUES('{0}',1,'0000',0,0,1,0,{1})", strBillNo, VCell);
                    ExecuteNonQuery(strSQL);
                    strSQL = string.Format("INSERT INTO WCS_TASK(TASK_ID,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,IS_MIX,CELL_CODE,TARGET_CODE)" +
                            "SELECT STATE.BILL_NO||LPAD(ITEM_NO, 2, '0'),BTYPE.TASK_TYPE ,BTYPE.TASK_LEVEL,STATE.BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,'0',TASK_DATE,TASKER,0,IS_MIX,CELL_CODE,'{1}' FROM  WMS_PRODUCT_STATE STATE " +
                           "LEFT JOIN WMS_BILL_MASTER BILL ON STATE.BILL_NO=BILL.BILL_NO " +
                            "LEFT JOIN CMD_BILL_TYPE BTYPE ON BILL.BTYPE_CODE=BTYPE.BTYPE_CODE WHERE  STATE.BILL_NO='{0}'", strBillNo, TARGET_CODE);
                    ExecuteNonQuery(strSQL);
                    return strBillNo + "01";
                }
                else
                    throw new Exception("没有找到可以出库的托盘货位。");
            }
            else
                return dt.Rows[0]["TASK_ID"].ToString();
            

        }
       /// <summary>
       /// 更新单号完成标志。
        /// </summary>
        /// <param name="BillNo"></param>
         public void UpdateInBillMasterFinished(string BillNo,string isBill)
         {
             StoredProcParameter parameters = new StoredProcParameter();
             parameters.AddParameter("VBILLNO", BillNo);
             parameters.AddParameter("VISBILL", isBill);
             ExecuteNonQuery("CONFIRMBILLFINSHED", parameters);

         }
         /// <summary>
         /// 更新单号完成标志。
         /// </summary>
         /// <param name="BillNo"></param>
         public void UpdateOutBillMasterFinished(string BillNo)
         {
             StoredProcParameter parameters = new StoredProcParameter();
             parameters.AddParameter("VBILLNO", BillNo);
             ExecuteNonQuery("CONFIRMOUTBILLFINSHED", parameters);

         }
         /// <summary>
         /// 更新单号开始标志。
         /// </summary>
         /// <param name="BillNo"></param>
         public void UpdateBillMasterStart(string BillNo, bool IsBill)
         {
             string strSQL = "";
             if (IsBill)
             {
                 strSQL = string.Format("UPDATE WMS_BILL_MASTER SET STATE='4' WHERE BILL_NO='{0}' AND STATE='3'", BillNo);
             }
             else
             {
                 strSQL = string.Format("UPDATE WMS_PALLET_MASTER SET STATE='4' WHERE BILL_NO='{0}' AND STATE='3'", BillNo);
             }
             ExecuteNonQuery(strSQL);
         }

         public DataTable GetBillByType(string BillType)
         {
             string strSQL = string.Format("SELECT BTYPE_CODE,BTYPE_NAME FROM CMD_BILL_TYPE WHERE BILL_TYPE in ({0}) ORDER BY BTYPE_CODE", BillType);
             return ExecuteQuery(strSQL).Tables[0];
         }

         public DataTable GetCigarette()
         {
             string strSQL = "SELECT * FROM CMD_CIGARETTE";
             return ExecuteQuery(strSQL).Tables[0];
         }

         public DataTable GetFormula()
         {
             string strSQL = "SELECT A.*,B.CIGARETTE_NAME FROM WMS_FORMULA_MASTER A LEFT JOIN CMD_CIGARETTE B ON A.CIGARETTE_CODE=B.CIGARETTE_CODE ";
             return ExecuteQuery(strSQL).Tables[0];
         }
         public DataTable GetBillInTask(string filter)
         {
             string strSQL = "SELECT ROWNUM AS ROWIDSTRING, A.BILL_NO,I.BTYPE_NAME,J.USER_NAME AS TASKNAME,A.TASK_DATE,K.STATE_DESC AS BILL_METHOD, A.CIGARETTE_CODE,C.CIGARETTE_NAME, A.FORMULA_CODE,D.FORMULA_NAME,A.BATCH_WEIGHT, " +
                            " B.PRODUCT_CODE,B.PRODUCT_BARCODE,B.CELL_CODE,B.FINISH_DATE,B.IS_MIX,P.PRODUCT_NAME,E.GRADE_NAME,F.ORIGINAL_NAME,P.YEARS,G.STYLE_NAME,L.CATEGORY_NAME, H.STATE_DESC AS TASKSTATE,B.TASK_ID " +
                            "FROM VIEW_BILL_MAST A " +
                            "LEFT JOIN VIEW_WCS_TASK B ON A.BILL_NO=B.BILL_NO " +
                            "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=A.CIGARETTE_CODE " +
                            "LEFT JOIN WMS_FORMULA_MASTER D ON D.FORMULA_CODE=A.FORMULA_CODE " +
                            "LEFT JOIN CMD_PRODUCT P ON P.PRODUCT_CODE=B.PRODUCT_CODE " +
                            "LEFT JOIN CMD_PRODUCT_GRADE E ON E.GRADE_CODE=P.GRADE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_CATEGORY L ON L.CATEGORY_CODE=P.CATEGORY_CODE " +
                            "LEFT JOIN CMD_PRODUCT_ORIGINAL F ON F.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE G ON G.STYLE_NO=P.STYLE_NO " +
                            "LEFT JOIN SYS_TABLE_STATE H ON H.Table_Name='WCS_TASK' AND Field_Name='STATE' AND B.STATE=H.STATE " +
                            "LEFT JOIN CMD_BILL_TYPE I ON I.BTYPE_CODE=A.BTYPE_CODE " +
                            "LEFT JOIN AUTH_USER J ON J.USER_ID=A.TASKER " +
                            "LEFT JOIN SYS_TABLE_STATE K  ON K.Table_Name='WMS_BILL_MASTER' and K.Field_Name='BILL_METHOD' AND A.BILL_METHOD=K.STATE WHERE "+
                             filter + " ORDER BY B.TASK_ID ";
             return ExecuteQuery(strSQL).Tables[0];
         }

         public DataTable GetBillOutTask(string filter)
         {
             string strSQL = "SELECT ROWNUM AS ROWIDSTRING, A.BILL_NO,I.BTYPE_NAME,J.USER_NAME AS TASKNAME,A.TASK_DATE,K.STATE_DESC AS BILL_METHOD, A.CIGARETTE_CODE,C.CIGARETTE_NAME, A.FORMULA_CODE,D.FORMULA_NAME,A.BATCH_WEIGHT, " +
                            " B.PRODUCT_CODE,B.PRODUCT_BARCODE,B.CELL_CODE,B.FINISH_DATE,B.IS_MIX,P.PRODUCT_NAME,E.GRADE_NAME,F.ORIGINAL_NAME,P.YEARS,G.STYLE_NAME,L.CATEGORY_NAME, H.STATE_DESC AS TASKSTATE,B.TASK_ID " +
                            "FROM VIEW_BILL_MAST A " +
                            "LEFT JOIN VIEW_WCS_TASK B ON A.BILL_NO=B.BILL_NO " +
                            "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=A.CIGARETTE_CODE " +
                            "LEFT JOIN WMS_FORMULA_MASTER D ON D.FORMULA_CODE=A.FORMULA_CODE " +
                            "LEFT JOIN CMD_PRODUCT P ON P.PRODUCT_CODE=B.PRODUCT_CODE " +
                            "LEFT JOIN CMD_PRODUCT_GRADE E ON E.GRADE_CODE=P.GRADE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_CATEGORY L ON L.CATEGORY_CODE=P.CATEGORY_CODE " +
                            "LEFT JOIN CMD_PRODUCT_ORIGINAL F ON F.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE G ON G.STYLE_NO=P.STYLE_NO " +
                            "LEFT JOIN SYS_TABLE_STATE H ON H.Table_Name='WCS_TASK' AND Field_Name='STATE' AND B.STATE=H.STATE " +
                            "LEFT JOIN CMD_BILL_TYPE I ON I.BTYPE_CODE=A.BTYPE_CODE " +
                            "LEFT JOIN AUTH_USER J ON J.USER_ID=A.TASKER " +
                            "LEFT JOIN SYS_TABLE_STATE K  ON K.Table_Name='WMS_BILL_MASTER' and K.Field_Name='BILL_METHOD' AND A.BILL_METHOD=K.STATE WHERE " +
                             filter + " ORDER BY B.TASK_ID ";
             return ExecuteQuery(strSQL).Tables[0];
         }
         public DataTable GetBillTaskDetail(string TaskID)
         {
             string strSQL = string.Format("SELECT ITEM_NO,DESCRIPTION,DETAIL.CRANE_NO,CMD_CRANE.CRANE_NAME, DETAIL.CAR_NO,CMD_CAR.CAR_NAME, FROM_STATION,TO_STATION,STATE.STATE_DESC " +
                            "FROM VIEW_WCS_TASK_DETAIL DETAIL " +
                            "LEFT JOIN CMD_CRANE ON DETAIL.CRANE_NO=CMD_CRANE.CRANE_NO " +
                            "LEFT JOIN CMD_CAR ON CMD_CAR.CAR_NO=DETAIL.CAR_NO " +
                            "LEFT JOIN SYS_TABLE_STATE STATE ON STATE.TABLE_NAME='WCS_TASK' AND STATE.FIELD_NAME='STATE' AND STATE.STATE=DETAIL.STATE " +
                            "WHERE TASK_ID='{0}' ORDER BY ITEM_NO ", TaskID);
             return ExecuteQuery(strSQL).Tables[0];
         }


         public DataTable GetCranTaskByCraneNo(string CraneNo)
         {
             string strSQL = string.Format("SELECT TASK.TASK_ID,TASK_NO,DETAIL.ITEM_NO,ASSIGNMENT_ID,DETAIL.CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION  ,'0' AS STATE,TASK.BILL_NO," +
                            "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1, " +
                            "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE," +
                            "CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE, DECODE(TASK.NEWCELL_CODE,NULL,'',  '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION," +
                            "SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,P.PRODUCT_NAME,E.GRADE_NAME, F.ORIGINAL_NAME,P.YEARS,G.STYLE_NAME,L.CATEGORY_NAME, " +
                            "C.CIGARETTE_NAME,D.FORMULA_NAME,BMASTER.BATCH_WEIGHT,I.BTYPE_NAME,BMASTER.CIGARETTE_CODE,BMASTER.FORMULA_CODE,ERR_CODE,ERRCODE.DESCRIPTION,DETAIL.FROM_STATION,DETAIL.TO_STATION " +
                            "FROM WCS_TASK_DETAIL DETAIL " +
                            "LEFT JOIN WCS_TASK TASK  ON DETAIL.TASK_ID=TASK.TASK_ID " +
                            "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO " +
                            "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=BMASTER.CIGARETTE_CODE " +
                            "LEFT JOIN WMS_FORMULA_MASTER D ON D.FORMULA_CODE=BMASTER.FORMULA_CODE " +
                            "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE " +
                            "LEFT JOIN SYS_STATION on SYS_STATION.STATION_TYPE=TASK.TASK_TYPE  and SYS_STATION.CRANE_NO=DETAIL.CRANE_NO and SYS_STATION.ITEM=DETAIL.ITEM_NO " +
                            "LEFT JOIN CMD_CELL CMD_CELL_NEW on CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE  " +
                            "LEFT JOIN CMD_SHELF CMD_SHELF_NEW on CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
                            "LEFT JOIN SYS_STATION SYS_STATION_NEW ON   SYS_STATION_NEW.STATION_TYPE='14' and SYS_STATION_NEW.ITEM=3  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
                            "LEFT JOIN CMD_PRODUCT P ON P.PRODUCT_CODE=TASK.PRODUCT_CODE  " +
                            "LEFT JOIN CMD_PRODUCT_GRADE E ON E.GRADE_CODE=P.GRADE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_CATEGORY L ON L.CATEGORY_CODE=P.CATEGORY_CODE  " +
                            "LEFT JOIN CMD_PRODUCT_ORIGINAL F ON F.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE G ON G.STYLE_NO=P.STYLE_NO " +
                            "LEFT JOIN CMD_BILL_TYPE I ON I.BTYPE_CODE=BMASTER.BTYPE_CODE " +
                            "LEFT JOIN SYS_ERROR_CODE ERRCODE ON ERRCODE.CODE=DETAIL.ERR_CODE " +
                            "WHERE DETAIL.CRANE_NO='{0}' AND  DETAIL.STATE  IN (0,1) ", CraneNo);
             return ExecuteQuery(strSQL).Tables[0];
         }

    }
}