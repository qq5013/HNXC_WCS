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
        /// 生成退库单，并生成任务，任务明细。
        /// </summary>
        public void CreateReturnBillTaskDetail()
        {
            string strBillNo = GetBillNo("TK");
            
            string strSQL = string.Format("INSERT INTO WMS_BILL_MASTER (BILL_NO,BILL_DATE,BTYPE_CODE,SCHEDULE_NO,WAREHOUSE_CODE,TARGET_CODE,STATUS,STATE,SOURCE_BILLNO,OPERATER,OPERATE_DATE,CHECKER,TASKER,TASK_DATE,BILL_METHOD,SCHEDULE_ITEMNO)" +
                                          "SELECT {0},SYSDATE,'008',SCHEDULE_NO,WAREHOUSE_CODE,'','1','3',BILL_NO,'000001',SYSDATE,'000001',SYSDATE,SYSDATE,BILL_METHOD,SCHEDULE_ITEMNO FROM WMS_BILL_MASTER WHERE BILL_NO='{1}'", strBillNo, "");
            ExecuteNonQuery(strSQL);

            strSQL = "INSERT INTO WMS_BILL_DETAIL(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX,FPRODUCT_CODE) VALUES"+
                    "()";

            strSQL = "INSERT INTO WCS_PRODUCT_STATE(BILL_NO,ITEM_NO,SCHEDULE_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,OUT_BILLNO,CELL_CODE,NEWCELL_CODE,PRODUCT_BARCODE,PALLET_CODE,IS_MIX)";
            
            strSQL = "INSERT INTO WCS_TASK(TASK_ID,ORITASKNO,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,CELL_CODE,TARGET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,NEWCELL_CODE,IS_MIX)";

          
            
 
        }

        /// <summary>
        /// 空托盘组组盘入库，申请货位时，生成入库单。
        /// </summary>
        public void CreateReturnBillTaskDetail()
        {
            string strBillNo = GetBillNo("TK");

            string strSQL = string.Format("INSERT INTO WMS_BILL_MASTER (BILL_NO,BILL_DATE,BTYPE_CODE,WAREHOUSE_CODE,STATUS,STATE,OPERATER,OPERATE_DATE,CHECKER,TASKER,TASK_DATE,BILL_METHOD,SCHEDULE_ITEMNO)" +
                                          "values ('{0}',SYSDATE,'010','001','1','3','000001',SYSDATE,'000001','000001',SYSDATE,'0',0)",strBillNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("INSERT INTO WMS_BILL_DETAIL(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,NC_COUNT,IS_MIX)" +
                    " VALUES('{0}',1,'0000',0,0,0,0,0)", strBillNo);
            ExecuteNonQuery(strSQL);
            strSQL = string.Format("INSERT INTO WMS_PRODUCT_STATE(BILL_NO,ITEM_NO,PRODUCT_CODE,WEIGHT,REAL_WEIGHT,PACKAGE_COUNT,IS_MIX)" +
                     "VALUES('{0}',1,'0000',0,0,1,0)", strBillNo);
            ExecuteNonQuery(strSQL);
            strSQL = "INSERT INTO WCS_TASK(TASK_ID,ORITASKNO,TASK_TYPE,TASK_LEVEL,BILL_NO,PRODUCT_CODE,REAL_WEIGHT,PRODUCT_BARCODE,PALLET_CODE,CELL_CODE,TARGET_CODE,STATE,TASK_DATE,TASKER,PRODUCT_TYPE,NEWCELL_CODE,IS_MIX)";
            ExecuteNonQuery(strSQL);



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
            if (dt.Rows.Count>0)
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

    }
}