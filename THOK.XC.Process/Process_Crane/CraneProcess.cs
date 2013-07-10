using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;

namespace THOK.XC.Process.Process_Crane
{
    public class CraneProcess : AbstractProcess
    {
        private DataTable dtCrane;
        private Dictionary<string, string> dCraneState; //堆垛机状态表  ""，表示状态未知，发送报文获取堆垛机状态。 0：空闲，1：执行中
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

            
            



            string cigaretteCode = "";
            try
            {
                switch (stateItem.ItemName)
                {
                    case "Init":
                        //初始化堆垛机任务表,将获取的数据，加入datatable中，并根据 一楼出库，二楼出库，一楼入库，二楼入库，堆垛机号区分 写入不同的队列，及datatable(一楼出库。需按照等级排序)
                        //判断堆垛机是否有空闲，若空闲，则发送报文，发送报文前需要判断，若是出库报文，先判断站台是否有货物，是否与二楼出库产品顺序产生冲突。 否则，不进行往下处理。

                        

                        //1:传回的数据，插入dtCrane;
                        //2：发送报文。传入堆垛机编号。
                        

                        if (dtCrane != null)
                        {
                            if (dtCrane.Rows.Count > 0)
                            {

                            }
                            else
                            {
                                string str = "1";
                            }

                        }
                        else
                        {
                            dtCrane = new DataTable();
                            dtCrane.Columns.Add("name");
                            DataRow dr = dtCrane.NewRow();
                            dr[0] = "shjstudy";
                            dtCrane.Rows.Add(dr);

                        }
                        SendTelegram("01","");
                        SendTelegram("02","");
                        SendTelegram("03","");
                        SendTelegram("04","");
                        SendTelegram("05","");
                        SendTelegram("06","");

                        break;
                    case "FirstBatch":
                        //接收报文，若是acp，根据返回报文判断 1、 出库类型，如果是出库，则先判断所在层是否有入库任务，若有则执行，没有则判断另一楼层的入库任务，若有则执行。若没有需要先判断一楼是否有出库，有则先执行，否则继续执行下一条出库任务。
                       //                                     2、入库类型，判断一楼是否有出库，若有则执行，没有判断二楼是否有出库，有则执行出库。没有则执行执行入库计划。 
                        //AddFirstBatch();
                        break;
                    case "StockInRequest":
                        cigaretteCode = Convert.ToString(stateItem.State);
                        //StockInRequest(cigaretteCode);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }


        private void SendTelegram(string CraneNo,string TaskID)
        {
            //判断dCraneState[CraneNo] 是否空闲；
            if (!dCraneState.ContainsKey(CraneNo))
            {
                dCraneState.Add(CraneNo, "");
                //发送请求堆垛机状态报文
            }
            while (string.IsNullOrEmpty(dCraneState[CraneNo]))  //等待堆垛机应答。
            {
 
            }
            if (dCraneState[CraneNo] == "1") //堆垛机正忙。
                return;
            

           



 
        }
        /// <summary>
        /// 根据接收到的State,分析调用的堆垛机任务。插入不同的堆垛机记录中。
        /// </summary>
        private void InsertScraneTask() 
        {
 
        }
    }
}
