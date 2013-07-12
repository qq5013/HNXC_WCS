using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

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
            //处理当前出库单中，有出库，但堆垛机不可用的任务。
            string strSQL = "select * from wcs_task where state='0' and task_type in ('12','22') ";
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    string strTaskDetailNo = GetTaskDetailNo();
                    strSQL = string.Format("insert into wcs_task_detail(task_id,item_no,task_no,assignment_id,crane_no,from_station,to_station,state,description,bill_no) " +
                             "select wcs_task.task_id,sys_task_route.item_no,'{1}','{2}',case sys_task_route.item_no when 1 then  cmd_shelf.crane_no else '' end  ," +
                             "case sys_task_route.item_no when 1 then '30'||wcs_task.cell_code||'01' else '' end ,case sys_task_route.item_no when 1 then sys_station.crane_position else '' end,'0," +
                             "sys_task_route.description,wcs_task.bill_no  from wcs_task " +
                             "left join cmd_cell on cmd_cell.cell_code=wcs_task.cell_code " +
                             "left join cmd_shelf on cmd_cell.shelf_code=cmd_shelf.shelf_code " +
                             "left join sys_task_route on sys_task_route.task_type=wcs_task.task_type " +
                             "left join sys_station on sys_station.station_type=wcs_task.task_type and sys_station.crane_no=cmd_shelf.crane_no " +
                             "where wcs_task.task_id='{0}' and  wcs_task.task_type in ('12','22') and wcs_task.state='0'" +
                             "order by sys_task_route.item_no", dt.Rows[i]["task_id"].ToString(), strTaskDetailNo, "0000" + strTaskDetailNo);

                    ExecuteNonQuery(strSQL);

                    strSQL = string.Format("Update wcs_task set state='1' where task_id='{0}'", dt.Rows[i]["task_id"].ToString());
                    ExecuteNonQuery(strSQL);
                }
            }
            return TaskCraneDetail("12,22", "1", "0");
        }

        /// <summary>
        /// 系统重新启动时，获取正在出库，或者出库完成的Task_Detail
        /// </summary>
        /// <returns></returns>
        public DataTable TaskCraneDetail(string TaskType,string ItemNo,string state )
        {
            string strSQL = string.Format("SELECT TASK.TASK_ID,DETAIL.TASK_NO, DETAIL.ITEM_NO,DETAIL.ASSIGNMENT_ID,DETAIL.CRANE_NO,DETAIL.FROM_STATION,DETAIL.TO_STATION,DETAIL.STATE,TASK.BILL_NO,TASK.PRODUCT_CODE," +
                            "TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL,TASK.TASK_DATE,STYLE.SORT_LEVEL,TASK.IS_MIX,PRODUCT.STYLE_NO,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,DETAIL.SQUENCE_NO " +
                            "FROM WCS_TASK_DETAIL DETAIL " +
                            "LEFT JOIN WCS_TASK TASK  ON DETAIL.TASK_ID=TASK.TASK_ID " +
                            "LEFT JOIN CMD_PRODUCT  PRODUCT ON TASK.PRODUCT_CODE=PRODUCT.PRODUCT_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE STYLE ON STYLE.STYLE_NO=PRODUCT.STYLE_NO " +
                            "LEFT JOIN SYS_STATION ON DETAIL.CRANE_NO=SYS_STATION.CRANE_NO AND SYS_STATION.STATION_TYPE=TASK.TASK_TYPE " +
                            "WHERE TASK.TASK_TYPE IN ({0}) AND DETAIL.ITEM_NO={1} AND DETAIL.STATE IN ({2}) " +
                            "ORDER BY TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.BILL_NO, TASK.IS_MIX,TASK.PRODUCT_CODE,TASK_ID", TaskType, ItemNo, state);

            return ExecuteQuery(strSQL).Tables[0];
        }
        private string GetTaskDetailNo()
        {
            return "";
        }


        public void UpdateCraneFinshedState(string TaskID, string TaskType, string ItemNo)
        {
 
        }
        public void UpdateCraneStarState(string TaskID, string ItemNo)
        {

        }
        /// <summary>
        /// 获取堆垛机最大流水号
        /// </summary>
        /// <returns></returns>
        public string GetMaxSQUENCENO()
        {
            return "";
        }

    }
}
