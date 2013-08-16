using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;
namespace THOK.XC.Process.Process_02
{
    public class StockOutToUnpackLineProcess : AbstractProcess
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



            string[] StationState = new string[2];
            string cigaretteCode = "";
            try
            {
                object str = State;
                if (str.ToString() == "1") //正常烟包
                {
                    TaskDal dal = new TaskDal();
                    dal.UpdateStockOutToStationState("", stateItem.ItemName);//更新Task_Detail  货物到达小车站台 
                    DataTable dt = dal.TaskCarDetail(""); //获取任务ID

                    StationState[0] = "3";
                    StationState[1] = "";//任务号;
                    WriteToProcess("CraneProcess", "StockOutToCarStation", StationState); //更新堆垛机Process 状态为3.
                    //解除货位锁定。



                    WriteToProcess("CarProcess", "CarOutRequest", dt);
                    //调度小车；
                }
                else //错误烟包
                {

                    TaskDal dal = new TaskDal();
                    dal.UpdateStockOutToStationState("", stateItem.ItemName);//更新Task_Detail  货物到达小车站台 
                    DataTable dt = dal.TaskCarDetail(""); //获取任务ID

                    DataTable dtCraneOut = dal.TaskOutToDetail();

                    DataTable[] dtSend = new DataTable[2];
                    dtSend[0] = dt;
                    WriteToProcess("CraneProcess", "StockOutRequest", dtSend); // 出库任务调用。

                    WriteToProcess("CarProcess", "CarOutRequest", dt); //调用堆垛机。
                   


                    StationState[0] = "4";
                    StationState[1] = "";//TaskID;
                    WriteToProcess("CraneProcess", "StockOutToCarStation", StationState); //更新堆垛机Process 状态为4.
                    //产生新的入库单，并生成入库任务。Task_Detail.
                    //生成新的出库单，并生成Task。

                   


 
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
