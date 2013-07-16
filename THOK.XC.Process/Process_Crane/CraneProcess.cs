using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_Crane
{
    public class CraneProcess : AbstractProcess
    {
        private DataTable dtCrane;
        private Dictionary<string, string> dCraneState = new Dictionary<string, string>(); //堆垛机状态表  ""，表示状态未知，发送报文获取堆垛机状态。 0：空闲，1：执行中
        private DataTable dtOrderCrane;
        private string strMaxSQuenceNo = "";
        private DataTable dtSendCRQ;
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

            if (strMaxSQuenceNo == "")
            {
                TaskDal dal = new TaskDal();
                strMaxSQuenceNo = dal.GetMaxSQUENCENO();
            }
            if (strMaxSQuenceNo == "")
            {
                strMaxSQuenceNo = DateTime.Now.ToString("yyyyMMdd") + "0000";
            }
          
            try
            {
                switch (stateItem.ItemName)
                {
                    case "CraneOutRequest": //开始出库，主动调用。
                        DataTable[] dtSend = (DataTable[])stateItem.State;
                        if (dtSend[1] != null)
                        {
                            InsertCraneQuene(dtSend[1]);
                        }
                        InsertCraneQuene(dtSend[0]);
                        SendTelegram("01",null);
                        SendTelegram("02",null);
                        SendTelegram("03",null);
                        SendTelegram("04",null);
                        SendTelegram("05",null);
                        SendTelegram("06",null);

                        break;
                    case "CraneInRequest":  //货物到达入库站台，调用堆垛机
                        DataTable[] dtInCrane = (DataTable[])stateItem.State;
                        InsertCraneQuene(dtInCrane[0]);
                        SendTelegram(dtInCrane[0].Rows[0]["CRANE_NO"].ToString(), dtInCrane[0].Rows[0]);
                        break;
                    case "ACP":
                        ACP(stateItem.State);
                        break;
                    case "CSR":
                        CSR(stateItem.State);
                        break;
                    case "ACK":
                        ACK(stateItem.State);
                        break;
                    case "DUM":
                        THOK.CRANE.TelegramData tgd = new CRANE.TelegramData();
                        THOK.CRANE.TelegramFraming tf = new CRANE.TelegramFraming();
                        string str = tf.DataFraming("00000", tgd, tf.TelegramDUA);
                        WriteToService("Crane", "DUA", str);
                        break;
                    case "NCK":
                        NCK(stateItem.State);
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


        #region 其它函数
        /// <summary>
        /// 发送报文，并返回发送成功。
        /// </summary>
        /// <param name="CraneNo"></param>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        private bool SendTelegram(string CraneNo, DataRow  drTaskID)
        {
            bool blnSend = false;
            //判断dCraneState[CraneNo] 是否空闲；
            if (!dCraneState.ContainsKey(CraneNo))
            {
                dCraneState.Add(CraneNo, "");
                SendTelegramCRQ(CraneNo);
                //发送请求堆垛机状态报文
            }
            while (string.IsNullOrEmpty(dCraneState[CraneNo]))  //等待堆垛机应答。
            {

            }
            if (dCraneState[CraneNo] == "1") //堆垛机正忙。
                return true;

            if (drTaskID==null) //出库任务调用堆垛机
            {
                //读取二楼出库站台是否有烟包，PLC
                DataRow[] drs = dtCrane.Select(string.Format("CRANE_NO='{0}' and STATE=0 and TASK_TYPE in ('12','22')", CraneNo), "TASK_LEVEL,TASK_DATE,BILL_NO,ISMIX,PRODUCT_CODE,TASK_ID"); //按照任务等级，任务时间，产品形态，
                for (int i = 0; i < drs.Length; i++)
                {
                    //判断能否出库
                    if (drs[i]["TASK_TYPE"].ToString() == "22") //二楼出库，判断能否出库
                    {
                        bool blnCan = ProductCanOut(drs[i]);  //判断能否出库
                        if (!blnCan)
                            continue;
                    }

                    object t = WriteToService(drs[i]["SERVICE_NAME"].ToString(), drs[i]["ITEM_NAME"].ToString());  //读取当前出库站台是否有货位
                    if (t == null && dCraneState[CraneNo] == "0") 
                    {

                        SendTelegramARQ(drs[i]); //发送报文
                        blnSend = true;
                        break;
                    }
                }
            }
            else  //根据任务编号发送报文。
            {
                object t = WriteToService(drTaskID["SERVICE_NAME"].ToString(), drTaskID["ITEM_NAME"].ToString());//读取当前出库站台是否有货位
                if (t == null && dCraneState[CraneNo] == "0")
                {
                    SendTelegramARQ(drTaskID);//发送报文
                    blnSend = true;
                }
            }
            return blnSend;
        }

        /// <summary>
        /// 能否发送出库报文。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        private bool ProductCanOut(DataRow drTaskID)
        {
            
            bool blnvalue = false;
            DataRow[] drs = dtCrane.Select(string.Format("BILL_NO='{0}' and PRODUCT_CODE='{1}' and IS_MIX='{2}' and STATE=1", drTaskID["BILL_NO"], drTaskID["PRODUCT_CODE"], drTaskID["IS_MIX"]));   //判断当前单号，当前产品，当前形态是否有state=1的出库任务，有则返回true;
            if (drs.Length > 0)
            {
                blnvalue = true;
            }
            else
            {

                drs = dtOrderCrane.Select(string.Format("TASK_LEVEL={0} and TASK_DATE={1} and ISMIX={2} and SORT_LEVEL={3} and PRODUCT_CODE={4}", new object[] { drTaskID["TASK_LEVEL"], drTaskID["TASK_DATE"], drTaskID["ISMIX"], drTaskID["SORT_LEVEL"], drTaskID["PRODUCT_CODE"] }));
                if (drs.Length > 0)
                {
                    drs = dtOrderCrane.Select(string.Format("Index<{0}", drs[0]["Index"]));
                    for (int i = 0; i < drs.Length; i++)
                    {
                        drs = dtCrane.Select(string.Format("TASK_LEVEL={0} and TASK_DATE={1} and ISMIX={2} and SORT_LEVEL={3} and PRODUCT_CODE={4} and TASK_TYPE='22' and STATE in (0,1,2)", new object[] { drTaskID["TASK_LEVEL"], drTaskID["TASK_DATE"], drTaskID["ISMIX"], drTaskID["SORT_LEVEL"], drTaskID["PRODUCT_CODE"] }));//判断小于当前Index的出库任务，是否有未完成的出库任务，如果没有，则返回True.
                        if (drs.Length == 0)
                        {
                            blnvalue = true;
                        }
                        else
                        {
                            blnvalue = false;
                            break;
                        }

                    }
                }                
            }
            return blnvalue;
        }

        /// <summary>
        ///  接收ACP后，根据获取的任务类型，重新获取新的TaskID;
        /// </summary>
        /// <param name="CraneNo"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        ///    1、 出库类型，如果是出库，则先判断所在层是否有入库任务，若有则执行，没有则判断另一楼层的入库任务，若有则执行。若没有需要先判断一楼是否有出库，有则先执行，否则继续执行下一条出库任务。
        ///    2、入库类型，判断一楼是否有出库，若有则执行，没有判断二楼是否有出库，有则执行出库。没有则执行执行入库计划。 
        private void GetNextTaskID(string CraneNo, string TaskType)
        {
            DataRow[] drs;
            bool blnSend = false;
            string type = TaskType.PadRight(2, '0').Substring(1, 1);
            switch (type)
            {
                case "1":
                    blnSend = false;
                    blnSend = SendTelegram(CraneNo, null);  //查询出库报文
                    if (!blnSend)
                    {
                        drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE='11'", CraneNo));
                        if (drs.Length > 0)
                        {
                            blnSend = SendTelegram(CraneNo, drs[0]);
                        }
                    }
                    if (!blnSend)
                    {
                        drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE='21'", CraneNo));
                        if (drs.Length > 0)
                        {
                            blnSend = SendTelegram(CraneNo, drs[0]);
                        }
                    }
                    break;
                case "2":
                    blnSend = false;
                    if (TaskType == "12")
                    {
                        if (!blnSend)
                        {
                            drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE='11'", CraneNo));
                            if (drs.Length > 0)
                            {
                                blnSend = SendTelegram(CraneNo, drs[0]);
                            }
                        }
                        if (!blnSend)
                        {
                            drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE='21'", CraneNo));
                            if (drs.Length > 0)
                            {
                                blnSend = SendTelegram(CraneNo, drs[0]);
                            }
                        }
                    }
                    else
                    {
                        if (!blnSend)
                        {
                            drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE='21'", CraneNo));
                            if (drs.Length > 0)
                            {
                                blnSend = SendTelegram(CraneNo, drs[0]);
                            }
                        }
                        if (!blnSend)
                        {
                            drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE='11'", CraneNo));
                            if (drs.Length > 0)
                            {
                                blnSend = SendTelegram(CraneNo, drs[0]);
                            }
                        }

                    }
                    if (!blnSend)
                        blnSend = SendTelegram(CraneNo, null);
                    break;
            }
        }

        /// <summary>
        /// 插入dtCrane
        /// </summary>
        /// <param name="dt"></param>
        private void InsertCraneQuene(DataTable dt)
        {
            if (dtCrane == null)
            {

                dtCrane = dt.Clone();
                DataColumn dc = new DataColumn("Index", Type.GetType("System.Int32"));
                dtCrane.Columns.Add(dc);
            }
            DataRow[] drs = dt.Select("", "TASK_LEVEL desc,TASK_DATE,BILL_NO,ISMIX,SORT_LEVEL,PRODUCT_CODE,TASK_ID");
            for (int i = 0; i < drs.Length; i++)
            {
                DataRow dr = dtCrane.NewRow();

                dr["Index"] = dtCrane.Rows.Count + 1;
                foreach (DataColumn dc in dt.Columns)
                {
                    dr[dc.ColumnName] = drs[i][dc.ColumnName];
                }

                dtCrane.Rows.Add(dr);
            }
            dtCrane.AcceptChanges();
            if (drs.Length > 0) //重新排序
            {
                DataTable dtOrder = dtCrane.DefaultView.ToTable(true, new string[] {"TASK_TYPE", "TASK_LEVEL", "TASK_DATE", "ISMIX", "SORT_LEVEL", "PRODUCT_CODE" });
                dtOrderCrane = new DataTable();
                dtOrderCrane = dtOrder.Clone();
                DataColumn dc = new DataColumn("Index", Type.GetType("System.Int32"));
                dtOrderCrane.Columns.Add(dc);

                drs = dtOrder.Select("TASK_TYPE=22", "TASK_LEVEL desc,TASK_DATE,ISMIX,SORT_LEVEL,PRODUCT_CODE");
                for (int i = 0; i < drs.Length; i++)
                {
                    DataRow dr = dtOrderCrane.NewRow();

                    dr["Index"] = dtOrderCrane.Rows.Count + 1;
                    foreach (DataColumn dcorder in dtOrder.Columns)
                    {
                        dr[dc.ColumnName] = drs[i][dc.ColumnName];
                    }

                    dtOrderCrane.Rows.Add(dr);
                }

            }
        }
        #endregion  
       
        #region 处理接堆垛机发送的报文
        private void ACP(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            if (!dCraneState.ContainsKey(msg["CraneNo"]))
            {
                dCraneState.Add(msg["CraneNo"], "");
            }
            if (msg["ReturnCode"] == "000")
            {
                DataRow[] drs = dtCrane.Select(string.Format("ASSIGNMENT_ID='{0}'", msg["AssignmenID"]));
                drs[0].BeginEdit();
                drs[0]["state"] = "2";
                drs[0].EndEdit();
                dtCrane.AcceptChanges();
                string TaskType = drs[0]["TASK_TYPE"].ToString();
                TaskDal dal = new TaskDal();
                dal.UpdateCraneFinshedState(drs[0]["TASK_ID"].ToString(), drs[0]["TASK_TYPE"].ToString());
                if (TaskType.PadRight(2, '0').Substring(1, 1) == "2")
                {
                    //通知PLC发送任务。
                }
                dCraneState[msg["CraneNo"]] = "0";
                //查找发送下条报文。
                GetNextTaskID(msg["CraneNo"], TaskType);
            }
            else
            {
                dCraneState[msg["CraneNo"]] = "1";
                Logger.Error(string.Format("堆垛机{0}返回错误代码{1}{2}", msg["CraneNo"], msg["ReturnCode"], ""));
            }
        }
        /// <summary>
        /// 堆垛机状态。
        /// </summary>
        /// <param name="state"></param>
        private void CSR(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            if (!dCraneState.ContainsKey(msg["CraneNo"]))
            {
                dCraneState.Add(msg["CraneNo"], "");
            }
            if (msg["ReturnCode"] == "000")
            {
                if (msg["AssignmenID"] == "00000000" && msg["CraneMode"] == "1")
                    dCraneState[msg["CraneNo"]] = "0";
                else
                    dCraneState[msg["CraneNo"]] = "1";
            }
            else
            {
                dCraneState[msg["CraneNo"]] = "1";
                Logger.Error(string.Format("堆垛机{0}返回错误代码{1}{2}", msg["CraneNo"], msg["ReturnCode"], ""));
            }

        }
        /// <summary>
        /// 发送报文后，堆垛机发送接收确认。
        /// </summary>
        /// <param name="state"></param>
        private void ACK(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            
            DataRow[] drs = dtCrane.Select(string.Format("TASK_NO='{0}'", msg["SequenceNo"]));
            drs[0].BeginEdit();
            drs[0]["state"] = "1";
            drs[0].EndEdit();
            dtCrane.AcceptChanges();
            TaskDal dal = new TaskDal();
            dal.UpdateCraneStarState(drs[0]["TASK_ID"].ToString());
        }

        /// <summary>
        ///发送报文，堆垛机返回序列号错误，或Buffer已满
        /// </summary>
        /// <param name="state"></param>
        private void NCK(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            if (msg["FaultIndicator"] == "1") //序列号出错，重新发送报文
            {
                DataRow[] drs = dtCrane.Select(string.Format("substring(SQUENCE_NO,9,4)='0003'", msg["SequenceNo"]));
                if (drs.Length > 0)
                {
                    dCraneState[drs[0]["CRANE_NO"].ToString()] = "0";
                    SendTelegram(drs[0]["CRANE_NO"].ToString(), drs[0]);
                }
                else
                {
                    drs = dtSendCRQ.Select(string.Format("substring(SQUENCE_NO,9,4)='0003'", msg["SequenceNo"]));
                    if (drs.Length > 0)
                    {
                        SendTelegramCRQ(drs[0]["CRANE_NO"].ToString());
                    }
                }
            }
        }
        #endregion

        #region 发送堆垛机报文
        private void SendTelegramARQ(DataRow dr)
        {

            THOK.CRANE.TelegramData tgd = new CRANE.TelegramData();
            tgd.CraneNo = dr["CRANE_NO"].ToString();
            tgd.AssignmenID = dr["ASSIGNMENT_ID"].ToString();
            tgd.StartPosition = dr["FROM_STATION"].ToString();
            tgd.DestinationPosition = dr["TO_STATION"].ToString();

            THOK.CRANE.TelegramFraming tf = new CRANE.TelegramFraming();
            string QuenceNo = GetNextSQuenceNo();
            string str = tf.DataFraming("1" + QuenceNo, tgd, tf.TelegramCRQ);
            WriteToService("Crane", "ARQ", str);
            dr.BeginEdit();
            dr["SQUENCE_NO"] = DateTime.Now.ToString("yyyyMMdd") + QuenceNo;
            dr.EndEdit();
            dCraneState[dr["CRANE_NO"].ToString()] = "1"; 
            dtCrane.AcceptChanges();
            //更新发送报文。
            TaskDal dal = new TaskDal();
            dal.UpdateCraneQuenceNo(dr["TASK_ID"].ToString(),dr["SQUENCE_NO"].ToString());
            
        }
        private void SendTelegramCRQ(string CraneNo)
        {
            THOK.CRANE.TelegramData tgd = new CRANE.TelegramData();
            tgd.CraneNo = CraneNo;
            THOK.CRANE.TelegramFraming tf = new CRANE.TelegramFraming();
            string QuenceNo = GetNextSQuenceNo();
            string str = tf.DataFraming("1" + QuenceNo, tgd, tf.TelegramCRQ);
            WriteToService("Crane", "CRQ", str);
            //记录发送的CRQ报文，预防堆垛机返回错误序列号的NCK。
            if (dtSendCRQ == null)
            {
                dtSendCRQ = new DataTable();
                dtSendCRQ.Columns.Add("CRANE_NO", Type.GetType("System.String"));
                dtSendCRQ.Columns.Add("SQUENCE_NO", Type.GetType("System.String"));
            }

            DataRow dr = dtSendCRQ.NewRow();
            dr.BeginEdit();
            dr["CRANE_NO"] = CraneNo;
            dr["SQUENCE_NO"] = DateTime.Now.ToString("yyyyMMdd") + QuenceNo;
            dr.EndEdit();
            dtSendCRQ.Rows.Add(dr);
            dtSendCRQ.AcceptChanges();
        }

        private string GetNextSQuenceNo()
        {
            string QuenceNo = strMaxSQuenceNo.Substring(8, 4);
            int i = int.Parse(QuenceNo) + 1;
            if (i == 0)
            {
                i = 1; 
            }
            if (i > 9999)
            {
                i = 1;
            }
            strMaxSQuenceNo = DateTime.Now.ToString("yyyyMMdd") + i.ToString().PadLeft(4, '0');
            return i.ToString().PadLeft(4, '0');
        }
        #endregion

    }
}
