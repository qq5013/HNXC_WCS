using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using System.Data.OracleClient;

namespace THOK.XC.Process.Dao
{
    public  class TaskDao : BaseDao
    {
        /// <summary>
        /// 根据出库任务生成Task_Detail，并返回。
        /// </summary>
        /// <returns></returns>
        public DataTable TaskOutToDetail()
        {
            //处理一楼出库(抽检倒库12、盘点13、移库14)，生成Task_Detail。
            DataTable dtCraneTask = CraneTaskOut("TASK.TASK_TYPE IN('12','13'，'14') AND TASK.STATE='0'");

            string strBillNo = "";
            //找出2楼状态为1正在执行出库任务的出库单
            string strSQL = "SELECT * FROM WCS_TASK_DETAIL " + 
                            "LEFT JOIN WCS_TASK ON WCS_TASK_DETAIL.TASK_ID=WCS_TASK.TASK_ID " +
                            "WHERE TASK_TYPE='22' AND CRANE_NO IS NOT NULL AND WCS_TASK_DETAIL.STATE=1";
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            //如果没有正在出库的任务，找二楼还没有产生TASK_DTAIL的出库单
            if (dt.Rows.Count == 0)
            {
                strSQL = "SELECT  DISTINCT WMS_BILL_MASTER.SCHEDULE_NO,WMS_BILL_MASTER.SCHEDULE_ITEMNO,WCS_TASK.TASK_LEVEL, WCS_TASK.TASK_DATE,WCS_TASK.BILL_NO " +
                         "FROM WCS_TASK " +
                         "INNER JOIN WMS_BILL_MASTER  ON WCS_TASK.BILL_NO=WMS_BILL_MASTER.BILL_NO " +
                         "WHERE WMS_BILL_MASTER.STATE=3  AND WCS_TASK.STATE=0 AND WCS_TASK.TASK_TYPE='22' " +
                         "ORDER BY WMS_BILL_MASTER.SCHEDULE_NO,WMS_BILL_MASTER.SCHEDULE_ITEMNO,WCS_TASK.TASK_LEVEL, WCS_TASK.TASK_DATE,WCS_TASK.BILL_NO ";

                dt = ExecuteQuery(strSQL).Tables[0];
            }
            if (dt.Rows.Count > 0)
            {
                //正在出或者准备要出的出库单号
                strBillNo = dt.Rows[0]["BILL_NO"].ToString();
                string strWhere = string.Format(" TASK.TASK_TYPE='22' and TASK.BILL_NO ='{0}' AND TASK.STATE='0'", strBillNo);
                dt = CraneTaskOut(strWhere);
                //与1楼出库任务进行合并
                dtCraneTask.Merge(dt);
            }
            return dtCraneTask;
        }
        /// <summary>
        /// 出入库，插入明细。如果明细已经存在则不进行重新插入， 返回TaskNo
        /// </summary>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public string InsertTaskDetail(string TaskID)
        {
            string strTaskDetailNo = "";

            string strSQL = string.Format("SELECT TASK_NO FROM  WCS_TASK_DETAIL WHERE TASK_ID='{0}' ", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strTaskDetailNo = dt.Rows[0][0].ToString();
            }
            else
            {
                strTaskDetailNo = GetTaskDetailNo(TaskID);

                strSQL = string.Format("INSERT INTO WCS_TASK_DETAIL(TASK_ID,ITEM_NO,TASK_NO,ASSIGNMENT_ID,STATE,DESCRIPTION,BILL_NO) " +
                         "SELECT  WCS_TASK.TASK_ID,SYS_TASK_ROUTE.ITEM_NO,'{1}','{2}','0'," +
                         "SYS_TASK_ROUTE.DESCRIPTION,WCS_TASK.BILL_NO " +
                         "FROM WCS_TASK  " +
                         "LEFT JOIN SYS_TASK_ROUTE ON WCS_TASK.TASK_TYPE=SYS_TASK_ROUTE.TASK_TYPE " +
                         "WHERE TASK_ID='{0}' " +
                         "ORDER BY SYS_TASK_ROUTE.ITEM_NO ", TaskID, strTaskDetailNo, "0000" + strTaskDetailNo);
                ExecuteNonQuery(strSQL);

                dt = TaskInfo(string.Format("TASK_ID='{0}'", TaskID));
                if (dt.Rows.Count > 0)
                {
                    string TaskType = dt.Rows[0]["TASK_TYPE"].ToString();
                    if (TaskType == "13")
                    {
                        strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET ASSIGNMENT_ID='{0}' WHERE TASK_ID='{1}' AND ITEM_NO=4 ", strTaskDetailNo.PadLeft(8, '1'), TaskID);
                        ExecuteNonQuery(strSQL);
                    }
                }

            }
            return strTaskDetailNo;
        }

     

        /// <summary>
        /// 获取已经插入Task_Detail 中，堆垛机调度程序。
        /// </summary>
        /// <returns></returns>
        public DataTable CraneTaskIn(string strWhere)
        {
            string where = strWhere;
            if (strWhere.Trim() == "")
                where = "1=1";
            string strSQL = "SELECT TASK.TASK_ID,TASK_NO,DETAIL.ITEM_NO,ASSIGNMENT_ID,DETAIL.CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION  ,'0' AS STATE,TASK.BILL_NO," +
                            "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1," +
                            "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE,CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE,  " +
                            "DECODE(TASK.NEWCELL_CODE,NULL,'',  '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION,SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,DETAIL.ERR_CODE " +
                            "FROM WCS_TASK_DETAIL DETAIL " +
                            "LEFT JOIN WCS_TASK TASK  ON DETAIL.TASK_ID=TASK.TASK_ID " +
                            "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO "+
                            "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE "+
                            "LEFT JOIN SYS_STATION on SYS_STATION.STATION_TYPE=TASK.TASK_TYPE  and SYS_STATION.CRANE_NO=DETAIL.CRANE_NO and SYS_STATION.ITEM=DETAIL.ITEM_NO " +
                            "LEFT JOIN CMD_CELL CMD_CELL_NEW on CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE " +
                            "LEFT JOIN CMD_SHELF CMD_SHELF_NEW on CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
                            "LEFT JOIN SYS_STATION SYS_STATION_NEW ON   SYS_STATION_NEW.STATION_TYPE='14' and SYS_STATION_NEW.ITEM=3  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
                            "WHERE " + where +
                            "ORDER BY TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.BILL_NO, TASK.IS_MIX,FDETAIL.FORDER";

            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据Task获取出库信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable CraneTaskOut(string strWhere)
        {
            string where = strWhere;
            if (strWhere == "")
                where = "1=1";
            string strSQL = "SELECT TASK.TASK_ID,'' AS TASK_NO,SYS_TASK_ROUTE.ITEM_NO，''AS  ASSIGNMENT_ID,CMD_SHELF.CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION  ,'0' AS STATE,TASK.BILL_NO," +
                           "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1," +
                           "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE,CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE, " +
                           "DECODE(TASK.NEWCELL_CODE,NULL,'',  '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION,SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,'' AS ERR_CODE " +
                           "FROM WCS_TASK TASK " +
                           "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO "+
                           "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE "+ 
                           "LEFT JOIN CMD_CELL on CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
                           "LEFT JOIN CMD_SHELF on CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                           "LEFT JOIN SYS_TASK_ROUTE on SYS_TASK_ROUTE.TASK_TYPE=TASK.TASK_TYPE and SYS_TASK_ROUTE.ITEM_NO=1 " +
                           "LEFT JOIN CMD_CELL CMD_CELL_NEW on CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE "+ 
                           "LEFT JOIN CMD_SHELF CMD_SHELF_NEW on CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE "+
                           "LEFT JOIN SYS_STATION SYS_STATION on SYS_STATION.STATION_TYPE=TASK.TASK_TYPE  and SYS_STATION.CRANE_NO=cmd_shelf.CRANE_NO and SYS_STATION.ITEM=SYS_TASK_ROUTE.ITEM_NO " +
                           "LEFT JOIN SYS_STATION SYS_STATION_NEW ON   SYS_STATION_NEW.STATION_TYPE='14' and SYS_STATION_NEW.ITEM=1  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
                           "WHERE  "+ where;
            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据Task获取入库，起始位置，目标位置，及堆垛机编号
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInCraneStation(string strWhere)
        {
            string where = strWhere;
            if (strWhere == "")
                where = "1=1";
            string strSQL = "SELECT '30'||TASK.cell_code||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION AS CRANESTATION,CMD_SHELF.CRANE_NO FROM WCS_TASK TASK " +
                           "LEFT JOIN CMD_CELL on CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
                           "LEFT JOIN CMD_SHELF on CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                           "LEFT JOIN SYS_STATION SYS_STATION on SYS_STATION.STATION_TYPE=TASK.TASK_TYPE and SYS_STATION.CRANE_NO=CMD_SHELF.CRANE_NO " +
                           "WHERE  " + where;
            return ExecuteQuery(strSQL).Tables[0];
        }

        private string GetTaskDetailNo(string TaskID)
        {
            string strValue = "";
            string strSQL = string.Format("SELECT TASK_ID,TASK.BILL_NO,BILL.BTYPE_CODE,BTYPE.BILL_TYPE||BTYPE.TARGET_CODE AS TASKNOTYPE " + 
                                          "FROM WCS_TASK TASK " + 
                                          "LEFT JOIN VBILLMASTER BILL ON BILL.BILL_NO=TASK.BILL_NO " +
                                          "LEFT JOIN CMD_BILL_TYPE BTYPE ON BTYPE.BTYPE_CODE=BILL.BTYPE_CODE " +
                                          "WHERE TASK_ID='{0}'", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string mode = "";
            if (dt.Rows.Count > 0)
            {
                string BType = dt.Rows[0]["TASKNOTYPE"].ToString();
                switch (BType)
                {
                    case "2195"://紧急补料单  9000-9299
                        mode = "F";
                        break;
                    case "3195"://抽检        9300-9499 
                        mode = "R";
                        break;
                    case "2122"://倒库        9500-9799
                        mode = "B";
                        break;
                    case "4195": //盘点单     9800--9998
                        mode = "C";
                        break;
                    default:
                        mode = "M";
                        break;

                }
            }
            SysStationDao SysDao = new SysStationDao();
            strValue = SysDao.GetTaskNo(mode);
                
            return strValue;
        }

        
        /// <summary>
        /// 更新任务状态Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="state"></param>
        public void UpdateTaskState(string TaskID, string state)
        {
            string strSql = string.Format("UPDATE WCS_TASK SET STATE='{0}' WHERE TASK_ID='{1}'", state, TaskID);
            ExecuteNonQuery(strSql);
        }
        /// <summary>
        /// 更新堆垛机顺序号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateCraneQuenceNo(string TaskID, string Squenceno,string ItemNo)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET SQUENCE_NO='{0}' WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", Squenceno, TaskID,ItemNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 更新堆垛机错误编号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateCraneErrCode(string TaskID, string ItemNo,string ErrCode)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET ERR_CODE='{0}' WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", ErrCode, TaskID, ItemNo);
            ExecuteNonQuery(strSQL);
        }

        
        /// <summary>
        /// 根据条件，返回小车任务明细。
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskCarDetail(string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT WCS_TASK.TASK_ID,CMD_CELL.CELL_CODE,STATION.STATION_NO,STATION.IN_STATION,ADDRESS1.CAR_ADDRESS STATION_NO_ADDRESS,ADDRESS2.CAR_ADDRESS IN_STATION_ADDRESS,CMD_SHELF.CRANE_NO,DETAIL.TASK_NO,WCS_TASK.TASK_TYPE,DETAIL.CAR_NO, " +
                            "DETAIL.ITEM_NO,STATION.OUT_STATION_1,ADDRESS3.CAR_ADDRESS OUT_STATION_1_ADDRESS, STATION.OUT_STATION_2,ADDRESS4.CAR_ADDRESS  OUT_STATION_2_ADDRESS,'' WRITEITEM,WCS_TASK.TARGET_CODE, "+
                            "WCS_TASK.IS_MIX,WCS_TASK.FORDER,WCS_TASK.FORDERBILLNO,WCS_TASK.PRODUCT_TYPE,WCS_TASK.PRODUCT_CODE,WCS_TASK.PRODUCT_BARCODE,WCS_TASK.PALLET_CODE,DETAIL.STATE " +
                            "FROM WCS_TASK " +
                            "LEFT JOIN CMD_CELL ON WCS_TASK.CELL_CODE=CMD_CELL.CELL_CODE " +
                            "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                            "LEFT JOIN SYS_CAR_STATION STATION ON CMD_SHELF.CRANE_NO=STATION.CRANE_NO AND STATION.STATION_TYPE=WCS_TASK.TASK_TYPE " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS1 ON STATION.STATION_NO=ADDRESS1.STATION_NO " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS2 ON STATION.IN_STATION=ADDRESS2.STATION_NO " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS3 ON STATION.OUT_STATION_1=ADDRESS3.STATION_NO "+
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS4 ON STATION.OUT_STATION_2=ADDRESS4.STATION_NO "+ 
                            "INNER JOIN WCS_TASK_DETAIL DETAIL ON WCS_TASK.TASK_ID=DETAIL.TASK_ID " +
                            "WHERE " + strWhere;

            return ExecuteQuery(strSQL).Tables[0];
        }


        /// <summary>
        /// 获取堆垛机最大流水号
        /// </summary>
        /// <returns></returns>
        public string GetMaxSQUENCENO()
        {
            string strValue="";
            string strSQL = "SELECT MAX(SQUENCE_NO)  FROM WCS_TASK_DETAIL ";
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                strValue = dt.Rows[0][0].ToString();
            return strValue;
        }

       

        /// <summary>
        ///  更新任务明细状态
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TaskType"></param>
        public void UpdateTaskDetailState(string strWhere, string State)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET STATE='{0}' WHERE {1}", State, where);
            ExecuteNonQuery(strSQL);

        }
        /// <summary>
        /// 更新起始位置，目标位置
        /// </summary>
        /// <param name="FromStation"></param>
        /// <param name="ToStation"></param>
        /// <param name="strWhere"></param>
        public void UpdateTaskDetailStation(string FromStation, string ToStation, string State, string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;

            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET  FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}' WHERE {3}", new string[] { FromStation, ToStation, State, where });
            ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCar(string FromStation, string ToStation, string state, string CarNo,string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;

            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET  FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CAR_NO='{3}'  WHERE {4}", new string[] { FromStation, ToStation, state, CarNo, where });
            ExecuteNonQuery(strSQL);

        }
        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCrane(string FromStation, string ToStation, string state, string CraneNo, string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;

            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET  FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CRANE_NO='{3}' WHERE {4}", new string[] { FromStation, ToStation, state, CraneNo, where });
            ExecuteNonQuery(strSQL);

        }

        /// <summary>
        /// 分配货位,返回 0:TaskID，1:货位 
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignCell(string strWhere, string ApplyStation)
        {
            
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("找不到相关的入库单号。");
            }
            string TaskID = dt.Rows[0]["TASK_ID"].ToString();

            string billNo = dt.Rows[0]["BILL_NO"].ToString();
            string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();
            string VCell = "";
            if (dt.Rows[0]["CELL_CODE"].ToString() == "")
            {
                StoredProcParameter parameters = new StoredProcParameter();
                parameters.AddParameter("VBILLNO", billNo);
                parameters.AddParameter("VPRODUCTCODE", ProductCode);
                parameters.AddParameter("VCELL", "00000000", DbType.String, ParameterDirection.Output);
                if (ApplyStation == "131")
                    ExecuteNonQuery("APPLYPALLETCELL", parameters);
                else
                    ExecuteNonQuery("APPLYCELL", parameters);
                VCell = parameters["VCELL"].ToString();

            }
            else
            {
                VCell = dt.Rows[0]["CELL_CODE"].ToString();
            }

            if (VCell == "")
            {
                throw new Exception("没有可分配的货位！");
            }
            strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}' WHERE CELL_CODE='{0}'", VCell, billNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE='{0}' WHERE {1}", VCell, where);
            ExecuteNonQuery(strSQL);

            string[] strValue = new string[2];
            strValue[0] = TaskID;
            strValue[1] = VCell;

         
            return strValue;
        }
        public string[] GetTaskInfo(string TaskNo)
        {
            string strSQL = string.Format("SELECT DISTINCT TASK.TASK_ID,TASK.BILL_NO FROM WCS_TASK_DETAIL DETAIL " +
                            "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                            "WHERE DETAIL.TASK_NO='{0}'", TaskNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string[] str = new string[2];
            if (dt.Rows.Count > 0)
            {
                str[0] = dt.Rows[0]["TASK_ID"].ToString();
                str[1] = dt.Rows[0]["BILL_NO"].ToString();
            }
            return str;
        }
        /// <summary>
        ///  分配货位,返回 0:TaskID，1:货位 
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignCellTwo(string strWhere) //
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string TaskID = dt.Rows[0]["TASK_ID"].ToString();

            string billNo = dt.Rows[0]["BILL_NO"].ToString();
            string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();
            string VCell = "";
            if (dt.Rows[0]["CELL_CODE"].ToString() == "")
            {
                StoredProcParameter parameters = new StoredProcParameter();
                parameters.AddParameter("VBILLNO", billNo);
                parameters.AddParameter("VPRODUCTCODE", ProductCode);
                parameters.AddParameter("VCELL", "00000000", DbType.String, ParameterDirection.Output);
                ExecuteNonQuery("APPLYCELL", parameters);
                VCell = parameters["VCELL"].ToString();
            }
            else
            {
                VCell = dt.Rows[0]["CELL_CODE"].ToString();
            }

            if (VCell == "")
            {
                throw new Exception("没有可分配的货位！");
            }

            strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}' WHERE CELL_CODE='{0}'", VCell, billNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE='{0}' WHERE {1}", VCell, where);
            ExecuteNonQuery(strSQL);

            string[] strValue = new string[2];
            strValue[0] = TaskID;
            strValue[1] = VCell;
            return strValue;
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInfo(string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;


            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据单号，返回任务数量
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public int TaskCount(string BillNo)
        {

            string strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK WHERE BILL_NO='{0}'", BillNo);
            DataTable dt=ExecuteQuery(strSQL).Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 根据任务号，返回产品信息。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable GetProductInfoByTaskID(string TaskID)
        {
            string strSQL = string.Format("SELECT T.PRODUCT_CODE,T.PRODUCT_BARCODE,T.BILL_NO,P.*,CIGARETTE_NAME,G.GRADE_NAME,O.ORIGINAL_NAME,S.STYLE_NAME FROM WCS_TASK T LEFT JOIN CMD_PRODUCT P ON T.PRODUCT_CODE=P.PRODUCT_CODE " +
                            "LEFT JOIN WMS_BILL_MASTER M ON T.BILL_NO=M.BILL_NO " +
                            "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=M.CIGARETTE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_ORIGINAL O ON O.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                            "LEFT JOIN CMD_PRODUCT_GRADE G ON G.GRADE_CODE=P.GRADE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE S ON S.STYLE_NO=P.STYLE_NO WHERE  TASK_ID='{0}'", TaskID);
            return ExecuteQuery(strSQL).Tables[0];
        }
        

          /// <summary>
        /// 二楼出库--条码校验出错，记录错误标志，及新条码。
        /// </summary>
        public void UpdateTaskCheckBarCode(string TaskID, string BarCode)
        {
            string strSQL = string.Format("UPDATE WCS_TASK SET BARCODE_CHECK='1', CHECK_PRODUCT_BARCODE='{1}' WHERE TASK_ID='{0}'", TaskID, BarCode);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 分配货位,返回 0:TaskID，1:任务号，2:货物到达入库站台的目的地址--平面号,3:堆垛机入库站台，4:货位，5:堆垛机编号
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignNewCell(string strWhere, string CraneNo)
        {
            string[] strValue = new string[6];
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("找不到相关的入库单号。");
            }
            string TaskID = dt.Rows[0]["TASK_ID"].ToString();

            string billNo = dt.Rows[0]["BILL_NO"].ToString();
            string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();
            string VCell = "";
            if (dt.Rows[0]["CELL_CODE"].ToString() != "")
            {
                VCell = dt.Rows[0]["CELL_CODE"].ToString();
                CellDao cdao = new CellDao();
                DataTable dtCell = cdao.GetCellInfo(VCell);
                if (dtCell.Rows[0]["ERROR_FLAG"].ToString() == "1")
                {
                    VCell = "";
                }

            }
            if (VCell == "")
            {
                StoredProcParameter parameters = new StoredProcParameter();
                parameters.AddParameter("VPRODUCTCODE",ProductCode);
                parameters.AddParameter("VCRANENO", CraneNo);
                parameters.AddParameter("VCELL", "00000000", DbType.String, ParameterDirection.Output);

                ExecuteNonQuery("APPLYNEWCELL", parameters);
                VCell = parameters["VCELL"].ToString();
               
            }
            if (VCell == "")
            {
                throw new Exception("没有可分配的货位！");
            }
            strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}' WHERE CELL_CODE='{0}'", VCell, billNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE='{0}' WHERE {1}", VCell, where);
            ExecuteNonQuery(strSQL);


            SysStationDao sysdao = new SysStationDao();

            dt = sysdao.GetSationInfo(VCell, "11","3");
            string TaskNo = InsertTaskDetail(TaskID);

            strValue[0] = TaskID;
            strValue[1] = TaskNo;
            strValue[2] = dt.Rows[0]["STATION_NO"].ToString();
            strValue[3] = dt.Rows[0]["CRANE_POSITION"].ToString();
            strValue[4] = VCell;
            strValue[5] = dt.Rows[0]["CRANE_NO"].ToString();


            return strValue;

        }

        /// <summary>
        ///  烟包替换记录
        /// </summary>
        /// <param name="strWhere"></param>
        public void InsertChangeProduct(string ProductBarcode, string ProductCode, string NewProductBarcode, string NewProductCode)
        {
            string strSQL = string.Format("INSERT INTO WCS_CHANGEPRODUCT(PRODUCT_BARCODE,PRODUCT_CODE,NEWPRODUCT_BARCODE,NEWPRODUCT_CODE,CHANGE_TIME,IS_CHANGE) VALUES('{0}','{1}','{2}','{3}',SYSDATE,'0')", new string[] { ProductBarcode, ProductCode, NewProductBarcode, NewProductCode });
            ExecuteNonQuery(strSQL);

        }

        /// <summary>
        /// 出库任务排序，判断能否给穿梭车下达出库任务,blnCar表示小车出库
        /// </summary>
        /// <param name="ForderBillNo"></param>
        /// <param name="Forder"></param>
        /// <param name="IsMix"></param>
        /// <returns></returns>
        public bool ProductCanToCar(string ForderBillNo,string Forder,string IsMix,bool blnCar)
        {
            bool blnValue = false;
            string strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                "WHERE DETAIL.ITEM_NO={4} AND DETAIL.STATE=1 AND TASK.IS_MIX='{0}' AND TASK.FORDER ={1} AND TASK.FORDERBILLNO='{2}' ", IsMix, Forder, ForderBillNo, (blnCar ? 3 : 1));

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                blnValue = true;
            }
            if (!blnValue)
            {
                if (IsMix == "1") //混装，先判断整包是否出库完成
                {
                    strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                           "WHERE DETAIL.ITEM_NO={1} AND DETAIL.STATE=0 AND TASK.IS_MIX=0 AND TASK.FORDERBILLNO='{0}' ", ForderBillNo, (blnCar ? 3 : 1));
                    dt = ExecuteQuery(strSQL).Tables[0];
                    if (int.Parse(dt.Rows[0][0].ToString()) == 0) //整包未出完
                    {
                        strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                   "WHERE DETAIL.ITEM_NO={2} AND DETAIL.STATE=0 AND TASK.IS_MIX=1 AND TASK.FORDER<{1} AND TASK.FORDERBILLNO='{0}' ", ForderBillNo, Forder, (blnCar ? 3 : 1));
                        dt = ExecuteQuery(strSQL).Tables[0];
                        if (int.Parse(dt.Rows[0][0].ToString()) == 0) //整包未出完
                        {
                            blnValue = true;
                        }
                    }
                }
                else
                {
                    strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                  "WHERE DETAIL.ITEM_NO={2} AND DETAIL.STATE=0 AND TASK.IS_MIX=0 AND TASK.FORDER<{1} AND TASK.FORDERBILLNO='{0}' ", ForderBillNo, Forder, (blnCar ? 3 : 1));
                    dt = ExecuteQuery(strSQL).Tables[0];
                    if (int.Parse(dt.Rows[0][0].ToString()) == 0) //整包未出完
                    {
                        blnValue = true;
                    }
                }
            }

            return blnValue;
        }
        /// <summary>
        /// 判断二楼出库，任务号到达拆盘处，是否已经执行？
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool SeparateTaskDetailStart(string TaskID)
        {
            bool blnValue = false;
            string strSQL = string.Format("SELECT TASK_ID FROM WCS_TASK_DETAIL WHERE ITEM_NO=5 AND TASK_ID='{0}' AND STATE in (1,2)", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                blnValue = true;
            return blnValue;
        }

        /// <summary>
        /// 小车待任务数量
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int CarTaskInfo()
        {
            string strSQL = "SELECT TASK.TASK_ID,ITEM_NO FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID  WHERE ITEM_NO=3 AND TASK_TYPE='22' AND DETAIL.STATE=0";
            return ExecuteQuery(strSQL).Tables[0].Rows.Count;
        }

         /// <summary>
        /// 二楼托盘组入库申请，判断是否有排程，小车未接货的任务。
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetPalletInTask()
        {
            string strvalue = "";
            string strSQL = "SELECT TASK.TASK_ID,ITEM_NO FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID WHERE ITEM_NO=2 AND TASK_TYPE='21' AND DETAIL.STATE=0 AND PRODUCT_CODE='0000'";
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                strvalue = dt.Rows[0]["TASK_ID"].ToString();
            return strvalue;
        }


        public DataTable GetCraneTaskBySequenceNo(string SequenceNo)
        {
            string strSQL = string.Format("SELECT TASK_ID FROM WCS_TASK_DETAIL WHERE SQUENCE_NO='{0}'", SequenceNo);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string strWhere = "1=2";
            if (dt.Rows.Count > 0)
                strWhere = "1=1";


            return CraneTaskIn(strWhere);
        }


    }
}
