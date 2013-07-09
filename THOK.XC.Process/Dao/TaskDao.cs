using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public  class TaskDao : BaseDao
    {
        public DataTable TaskOutToDetail()
        {
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

            strSQL = " select sys_station.item_name,sys_station.crane_no,detail.from_station,detail.to_station,wcs_task.product_code,cmd_product.style_no,detail.bill_no,wcs_task.task_type,wcs_task.task_level " +
                     " from wcs_task_detail detail " +
                     "left join wcs_task on detail.task_id=wcs_task.task_id" +
                     "left join cmd_product on wcs_task.product_code=cmd_product.product_code" +
                     "left join cmd_product_style style on style.style_no=cmd_product.style_no" +
                     "left join sys_station on sys_station.station_type=wcs_task.task_type and detail.crane_no=sys_station.crane_no" +
                     "where wcs_task.task_type in ('12','22') and detail.item_no=1 and detail.state='0'";


            return ExecuteQuery(strSQL).Tables[0];
        }

        private string GetTaskDetailNo()
        {
            return "";
        }
    }
}
