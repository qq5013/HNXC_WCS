using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class SysStationDao : BaseDao
    {
        /// <summary>
        ///根据货位信息，任务类别， 获取站台信息
        /// </summary>
        /// <param name="CellCode"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataTable GetSationInfo(string CellCode, string TaskType,string Item)
        {
            string strSQL = string.Format("SELECT CELL_CODE, SYS_STATION.STATION_NO,CMD_SHELF.CRANE_NO,SYS_STATION.CRANE_POSITION,SYS_STATION.MEMO AS CAR_STATION " + 
                                          "FROM CMD_CELL " +
                                          "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                                          "LEFT JOIN SYS_STATION ON CMD_SHELF.CRANE_NO=SYS_STATION.CRANE_NO " +
                                          "WHERE CELL_CODE='{0}' AND  SYS_STATION.STATION_TYPE='{1}' AND ITEM='{2}' ", CellCode, TaskType, Item);
           return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 获取小车出入库位置
        /// </summary>
        /// <param name="CellCode"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataTable GetCarSationInfo(string CellCode, string TaskType)
        {
            string strSQL = string.Format("SELECT STATION.STATION_NO,STATION.IN_STATION,ADDRESS1.CAR_ADDRESS STATION_NO_ADDRESS,ADDRESS2.CAR_ADDRESS IN_STATION_ADDRESS," +
                            "STATION.OUT_STATION_1,ADDRESS3.CAR_ADDRESS  OUT_STATION_1_ADDRESS, STATION.OUT_STATION_2,ADDRESS4.CAR_ADDRESS  OUT_STATION_2_ADDRESS " +
                            "FROM CMD_CELL  " +
                            "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                            "LEFT JOIN SYS_CAR_STATION STATION ON CMD_SHELF.CRANE_NO=STATION.CRANE_NO AND STATION.STATION_TYPE={1} " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS1 ON STATION.STATION_NO=ADDRESS1.STATION_NO " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS2 ON STATION.IN_STATION=ADDRESS2.STATION_NO " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS3 ON STATION.OUT_STATION_1=ADDRESS3.STATION_NO  " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS4 ON STATION.OUT_STATION_2=ADDRESS4.STATION_NO " +
                            "WHERE CMD_CELL.CELL_CODE='{0}'", CellCode, TaskType);
            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据类型返回任务号
        /// </summary>
        /// <param name="Module"></param>
        /// <returns></returns>
        public string GetTaskNo(string Module)
        {
            string strValue = "";
            int intValue;

            string strSQL = string.Format("SELECT * FROM SYS_TASKORDER WHERE TASK_MODE='{0}'", Module);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            int TaskNo = int.Parse(dt.Rows[0]["TASK_NO"].ToString());
            int MinNo = int.Parse(dt.Rows[0]["MIN_NO"].ToString());
            int MaxNo = int.Parse(dt.Rows[0]["MAX_NO"].ToString());
            if (TaskNo + 1 > MaxNo)
                intValue = MinNo;
            else
                intValue = TaskNo + 1;

            if (Module != "S")
            {
                bool blnvalue = false;
                while (!blnvalue)
                {
                    strSQL = string.Format("SELECT * FROM WCS_TASK_DETAIL WHERE TASK_NO='{0}'", intValue.ToString().PadLeft(4, '0'));
                    DataTable dtNew = ExecuteQuery(strSQL).Tables[0];
                    if (dtNew.Rows.Count > 0)
                    {
                        intValue++;
                        if (intValue > MaxNo)
                            intValue = MinNo;

                    }
                    else
                        blnvalue = true;

                }
            }
            strValue = intValue.ToString().PadLeft(4, '0');

            strSQL = string.Format("UPDATE SYS_TASKORDER SET TASK_NO='{0}' WHERE TASK_MODE='{1}'", strValue, Module);
            ExecuteNonQuery(strSQL);
            return strValue;

        }
        /// <summary>
        /// 堆垛机流水号报错，重置0；
        /// </summary>
        public void ResetSQueNo()
        {
           string strSQL = string.Format("UPDATE SYS_TASKORDER SET TASK_NO='{0}' WHERE TASK_MODE='{1}'", "0000", "S");
            ExecuteNonQuery(strSQL);
        }
    }
}
