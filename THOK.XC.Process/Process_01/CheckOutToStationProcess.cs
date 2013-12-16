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
            try
            {

                object sta = ObjectUtil.GetObject(stateItem.State);

                if (sta==null || sta.ToString() == "0")
                    return;
                string[] str = new string[3];
                if (((short)sta) >= 9000 && ((short)sta) <= 9299) //补料
                    str[0] = "1";
                else if (((short)sta) >= 9300 && ((short)sta) <= 9499)//抽检
                    str[0] = "2";
                else if (((short)sta) >= 9800 && ((short)sta) < 9999) //盘点
                    str[0] = "6";
                str[1] = "";
                str[2] = "";
                TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
                string[] strInfo = dal.GetTaskInfo(sta.ToString().PadLeft(4, '0'));
                DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));
                DataTable dtProductInfo = dal.GetProductInfoByTaskID(strInfo[0]);
                 //线程停止
                string strValue = "";
                while ((strValue = FormDialog.ShowDialog(str, dtProductInfo)) != "")
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                    string writeItem = "01_2_195_";
                    if (str[0] == "1" || str[0] == "2")  //抽检，补料
                    {
                        dal.UpdateTaskState(strInfo[0], "2");

                        BillDal billdal = new BillDal();
                        billdal.UpdateBillMasterFinished(strInfo[1],"1");

                        int[] ServiceW = new int[3];
                        ServiceW[0] = int.Parse(strInfo[1]); //任务号
                        ServiceW[1] = 131;//目的地址
                        ServiceW[2] = 4;

                        WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务

                        WriteToService("StockPLC_01", writeItem + "2", 1); //PLC写入任务
                    }
                    else  //盘点
                    {
                        DataTable dtTask = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));

                        DataRow dr = dtTask.Rows[0];
                        SysStationDal sysdal = new SysStationDal();
                        DataTable dtstation = sysdal.GetSationInfo(dr["CELL_CODE"].ToString(), "11", "3");

                        if (strValue != "1")
                        {
                            CellDal celldal = new CellDal();
                            celldal.UpdateCellErrFlag(dr["CELL_CODE"].ToString(), "条码扫描不一致");
                        }


                        int[] ServiceW = new int[3];
                        ServiceW[0] = int.Parse(strInfo[1]); //任务号
                        ServiceW[1] = int.Parse(dtstation.Rows[0]["STATION_NO"].ToString());//目的地址
                        ServiceW[2] = 1;

                        WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务
                        WriteToService("StockPLC_01", writeItem + "2", 1); //PLC写入任务

                        dal.UpdateTaskDetailStation("195", dtstation.Rows[0]["STATION_NO"].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=3", strInfo[0]));//更新货位到达入库站台，
                       
                    }


                   //线程继续。
                    break;
                }
              
            }
            catch (Exception ex)
            {
                Logger.Error("THOK.XC.Process.Process_01.CheckOutToStationProcess:" + ex.Message);
            }

        }
    }
}

