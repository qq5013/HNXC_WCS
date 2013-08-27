using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;
namespace THOK.XC.Process.Process_02
{
    public class StockOutToCarStationProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 
             *  stateItem.ItemName ：
             *  Init - 初始化。
             *      FirstBatch - 生成第一批入库请求任务。
             *      StockInRequest - 根据请求，生成入库任务。
             * 
             *  stateItem.State ：参数 - 请求的卷烟编码。        
            */
          //烟包托盘到达出库站台，根据返回的任务号，判断是否正常烟包：
           // 1、正常烟包，更新原有CranProcess的datatable将状态更改为3，并更改数据库状态。调用WriteToProcess(穿梭车Process).
           // 2、错误烟包，写入移库单，产生任务，调用调用WriteToProcess(穿梭车Process)。写入出库单，产生任务，并下达出库任务。
            try
            {
                string ToStation = "";
                string FromStation = "";
                string ReadItem2 = "";
                switch (stateItem.ItemName)
                {
                    case "02_1_304_1":
                        FromStation = "303";
                        ToStation = "304";
                        ReadItem2 = "02_1_304_2";
                        break;
                    case "02_1_308_1":
                        FromStation = "307";
                        ToStation = "308";
                        ReadItem2 = "02_1_308_2";
                        break;
                    case "02_1_312_1":
                        FromStation = "311";
                        ToStation = "313";
                        ReadItem2 = "02_1_312_2";
                        break;
                    case "02_1_316_1":
                        FromStation = "315";
                        ToStation = "316";
                        ReadItem2 = "02_1_316_2";
                        break;
                    case "02_1_320_1":
                        FromStation = "319";
                        ToStation = "320";
                        ReadItem2 = "02_1_320_2";
                        break;
                    case "02_1_322_1":
                        FromStation = "321";
                        ToStation = "322";
                        ReadItem2 = "02_1_322_2";
                        break;

                }


                object[] obj = ObjectUtil.GetObjects(stateItem.State);
                string NewPalletCode = (string)WriteToService("StockPLC_02", ReadItem2);
                string[] StationState = new string[2];

                TaskDal dal = new TaskDal();
                string[] strTask = dal.GetTaskInfo(obj[0].ToString().PadLeft(4, '0'));

                dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strTask[0]), "2");


                DataTable dtTask = dal.TaskInfo(string.Format("TASK_ID='{0}'", strTask[0]));
                string CellCode = dtTask.Rows[0]["CELL_CODE"].ToString();
                CellDal Celldal = new CellDal(); //更新货位，新托盘RFID，错误标志。

                if (obj[1].ToString() == "1") //正常烟包
                {
                    StationState[0] = strTask[0];//任务号;
                    StationState[1] = "3";
                    WriteToProcess("CraneProcess", "StockOutToCarStation", StationState); //更新堆垛机Process 状态为3.
                    Celldal.UpdateCellOutUnLock(CellCode);//解除货位锁定

                    DataTable dt = dal.TaskCarDetail(string.Format("WCS_TASK.TASK_ID='{0}' AND ITEM_NO=3", strTask[0])); //获取任务ID
                    WriteToProcess("CarProcess", "CarOutRequest", dt);  //调度小车；

                }
                else //错误烟包
                {



                    //生成二楼退库单
                    BillDal bdal = new BillDal();
                    string CancelTaskID = bdal.CreateCancelBillInTask(strTask[0], strTask[1], NewPalletCode);//产生退库单，并生成明细。
                    Celldal.UpdateCellNewPalletCode(CellCode, NewPalletCode);//更新货位错误标志。


                    dal.UpdateTaskDetailStation(FromStation, ToStation, "2", string.Format("TASK_ID='{0}' AND ITEM_NO=1", CancelTaskID)); //更新申请货位完成。

                    dal.UpdateTaskState(strTask[0], "2");//更新出库任务完成

                    string strWhere = string.Format("WCS_TASK.TASK_ID='{0}' AND ITEM_NO=2", CancelTaskID);
                    DataTable dt = dal.TaskCarDetail(strWhere);
                    WriteToProcess("CarProcess", "CarInRequest", dt);//调度穿梭车入库。


                    View.CannelBillSelect frm = new View.CannelBillSelect(strTask[0]);
                    frm.strBadCode = "";





                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string strNewBillNo = frm.strBillNo;

                        string strOutTaskID = bdal.CreateCancelBillOutTask(strTask[0], strTask[1], strNewBillNo);
                        DataTable dtOutTask = dal.CraneOutTask(string.Format("TASK.TASK_ID='{0}'", strOutTaskID));

                        WriteToProcess("CraneProcess", "PalletOutRequest", dtOutTask);


                      
                        StationState[0] = strTask[0];//TaskID;
                        StationState[1] = "4";
                        WriteToProcess("CraneProcess", "StockOutRequest", StationState); //更新堆垛机Process 状态为4.
                    }


                }
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }

        private DataTable dtCarInfo(string str)
        {
            //根据位置获取小车信息，位置小于当前位置的，加上总长度。按照 位置 desc 排序
            return null;
        }

        private string GetCarNo()
        {
            DataTable dt = dtCarInfo("");//已经按照位置顺序排序
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool blnB = false;//true：忙碌，false：空闲
                int Curd=0;//当前申请小车位置
                int intd;//位置
                if (blnB)
                {
                    intd = 0;//目的地
                }
                else
                {
                    intd = 0;//当前位置
                }
                if (Math.Abs(Curd - intd) > 100)
                {
                    //获取当前小车
                    break;
                }

                
                 
            }
            return "";
        }

        
    }
}
