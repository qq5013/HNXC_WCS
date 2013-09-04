using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class CheckOutToStationProcess : AbstractProcess
    {
           /*  处理事项：
            *  抽检，补料，盘点  烟包到达，195
            */
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {

            object sta = ObjectUtil.GetObject(stateItem.State);


            string[] str = new string[3];
            if (((int)sta) >= 9000 && ((int)sta) <= 9299) //补料
                str[0] = "1";
            else if (((int)sta) >= 9300 && ((int)sta) <= 9499)//抽检
                str[0] = "2";
            else if (((int)sta) >= 9800 && ((int)sta) < 9999) //盘点
                str[0] = "3";
            str[1] = "";
            str[2] = "";
            TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
            string[] strInfo = dal.GetTaskInfo(sta.ToString().PadLeft(4, '0'));
            DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));
            DataTable dtProductInfo = dal.GetProductInfoByTaskID(strInfo[0]);
            this.Stop(); //线程停止
            string strValue = "";
            while ((strValue= FormDialog.ShowDialog(str, dtProductInfo)) != "")
            {
                dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                string writeItem = "01_2_195_";
                if (str[0] == "1" || str[0] == "2")  //抽检，补料
                {
                    dal.UpdateTaskState(strInfo[0], "2");

                    BillDal billdal = new BillDal();
                    billdal.UpdateBillMasterFinished(strInfo[1]);
                    WriteToService("StockPLC_01", writeItem + "1", 1); //PLC写入任务
                }
                else  //盘点
                {
                  

                    DataTable dtTask = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));

                    DataRow dr = dtTask.Rows[0];
                    SysStationDal sysdal = new SysStationDal();
                    DataTable dtstation = sysdal.GetSationInfo(dr["CELL_CODE"].ToString(), "11");

                    if (strValue != "1")
                    {
                        CellDal celldal = new CellDal();
                        celldal.UpdateCellNewPalletCode(dr["CELL_CODE"].ToString(), strValue);
                    }


                    int[] ServiceW = new int[3];
                    ServiceW[0] = int.Parse(strInfo[1]); //任务号
                    ServiceW[1] = int.Parse(dtstation.Rows[0]["STATION_NO"].ToString());//目的地址
                    ServiceW[2] = 1;

                     WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务
                     WriteToService("StockPLC_01", writeItem + "3", 1); //PLC写入任务

                    dal.UpdateTaskDetailStation("195", dtstation.Rows[0]["STATION_NO"].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=3", strInfo[0]));//更新货位到达入库站台，
                    dal.UpdateTaskDetailCrane(dtstation.Rows[0]["STATION_NO"].ToString(), dr["CELL_CODE"].ToString(), "0", dtstation.Rows[0]["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO=4", strInfo[0]));//更新调度堆垛机的其实位置及目标地址。
                }

               
              
                break;
            }
            this.Resume();//线程继续。

        }
    }
}

