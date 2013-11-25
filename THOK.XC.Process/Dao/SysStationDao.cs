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
            string strSQL = string.Format("SELECT CELL_CODE, SYS_STATION.STATION_NO,CMD_SHELF.CRANE_NO,SYS_STATION.CRANE_POSITION,SYS_STATION.MEMO AS CAR_STATION FROM CMD_CELL " +
                   "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                   "LEFT JOIN SYS_STATION ON CMD_SHELF.CRANE_NO=SYS_STATION.CRANE_NO " +
                   "WHERE CELL_CODE='{0}' AND  SYS_STATION.STATION_TYPE='{1}' AND ITEM='{2}' ", CellCode, TaskType, Item);
           return ExecuteQuery(strSQL).Tables[0];
        }
    }
}
