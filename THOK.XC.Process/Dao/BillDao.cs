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
            strSQL = string.Format("INSERT INTO WCS_TASK(TASK_ID,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,IS_MIX,CELL_CODE)" +
                 "SELECT STATE.BILL_NO||LPAD(ITEM_NO, 2, '0'),BTYPE.TASK_TYPE ,BTYPE.TASK_LEVEL,STATE.BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,'0',TASK_DATE,TASKER,0,IS_MIX,CELL_CODE FROM  WMS_PRODUCT_STATE STATE " +
                  "LEFT JOIN WMS_BILL_MASTER BILL ON STATE.BILL_NO=BILL.BILL_NO " +
                  "LEFT JOIN CMD_BILL_TYPE BTYPE ON BILL.BTYPE_CODE=BTYPE.BTYPE_CODE WHERE  STATE.BILL_NO='{0}'", strBillNo);
            ExecuteNonQuery(strSQL);

            return strBillNo + "01";

        }
        /// <summary>
        /// 二楼出库托盘校验出错，补充生成 出库单。
        /// </summary>
        /// <returns>TaskID</returns>
        public string CreateCancelBillOutTask(string TaskID, string Bill_No)
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
                   "WHERE  CELL.IS_LOCK='0' AND CELL.IS_ACTIVE='1' AND CELL.ERROR_FLAG='0'  ORDER BY CELL.IN_DATE", TaskID);


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
        /// 二楼出库托盘校验出错，由用户选定出库的入库单号OutBillNO， 补充生成 出库单。
        /// </summary>
        /// <returns>TaskID</returns>
        public string CreateCancelBillOutTask(string TaskID, string Bill_No, string OutBillNO, string OLD_PALLET_CODE)
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
         public void UpdateBillMasterFinished(string BillNo,string isBill)
         {
             StoredProcParameter parameters = new StoredProcParameter();
             parameters.AddParameter("VBILLNO", BillNo);
             parameters.AddParameter("VISBILL", isBill);
             ExecuteNonQuery("CONFIRMBILLFINSHED", parameters);

         }

    }
}