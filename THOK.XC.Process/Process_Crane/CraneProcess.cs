using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;
using System.Threading;

namespace THOK.XC.Process.Process_Crane
{
    public class CraneProcess : AbstractProcess
    {
        private DataTable dtCrane;
        private Dictionary<string, string> dCraneState = new Dictionary<string, string>(); //堆垛机状态表  ""，表示状态未知，发送报文获取堆垛机状态。 0：空闲，1：执行中
        private DataTable dtOrderCrane;

        private string strMaxSQuenceNo = "";
        private DataTable dtSendCRQ;
        private DataTable dtErrMesage;
        
        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
               
                if (strMaxSQuenceNo == "")
                {
                    TaskDal dal = new TaskDal();
                    strMaxSQuenceNo = dal.GetMaxSQUENCENO();
                }
                if (strMaxSQuenceNo == "")
                {
                    strMaxSQuenceNo = DateTime.Now.ToString("yyyyMMdd") + "0000";
                }

                CraneErrMessageDal errDal = new CraneErrMessageDal();
                dtErrMesage = errDal.GetErrMessageList();
            }
            catch (Exception ex)
            {
                Logger.Error("THOK.XC.Process.Process_Crane.CraneProcess堆垛机初始化出错，原因：" + ex.Message);
            }

        }


      
        
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 
             *  堆垛机任务处理
             *  出库任务传入Task 需要产生TaskDetail，并更新起始位置及目的地。
             *  入库任务传入TaskDetail 
             *  Init - 初始化。
             *      FirstBatch - 生成第一批入库请求任务。
             *      StockInRequest - 根据请求，生成入库任务。
             * 
             *  stateItem.State ：参数 - 请求的卷烟编码。        
            */

           
          
           

            try
            {
                switch (stateItem.ItemName)
                {
                    case "StockOutRequest": //开始出库，主动调用。
                        DataTable[] dtSend = (DataTable[])stateItem.State;
                        if (dtSend[1] != null)
                        {
                            InsertCraneQuene(dtSend[1]);
                        }
                        InsertCraneQuene(dtSend[0]);
                        CraneThreadStart();//线程调度堆垛机
                      
                        break;
                    case "StockOutToCarStation": //烟包经过扫描，正确烟包更新为3，错误更新为4.
                        string []strdd = (string[])stateItem.State;
                        DataRow[] drs = dtCrane.Select(string.Format("TASK_ID='{0}'", strdd[0]));
                        if (drs.Length > 0)
                        {
                            TaskDal tdal = new TaskDal();
                            tdal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=1", drs[0]["TASK_ID"].ToString()), "2");
                            if (strdd[1] == "3")
                            {
                                CellDal Cdal = new CellDal();
                                Cdal.UpdateCellOutUnLock(drs[0]["CELL_CODE"].ToString()); //出库完成，货位解锁
                            }
                            dtCrane.Rows.Remove(drs[0]);
                        }
                        CraneThreadStart(); //更新完成之后，线程调用堆垛机，避免堆垛机因调度原因而是堆垛机没有任务。

                        break;

                    case "CraneInRequest":  //货物到达入库站台，调用堆垛机
                        DataTable dtInCrane = (DataTable)stateItem.State;
                        InsertCraneQuene(dtInCrane);
                        SendTelegram(dtInCrane.Rows[0]["CRANE_NO"].ToString(), dtInCrane.Rows[0]);
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
                Logger.Error("THOK.XC.Process.Process_Crane.CraneProcess，原因：" + e.Message);
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

            DataRow drTaskCrane = null;
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
                    drTaskCrane=drs[i];
                    break;
                }
            }
            else  //根据任务编号发送报文。
            {
                drTaskCrane = drTaskID;
            }
            if (drTaskCrane != null)
            {
                if (dCraneState[CraneNo] == "0")
                {
                    if (drTaskCrane["TASK_TYPE"].ToString() == "22" || drTaskCrane["TASK_TYPE"].ToString() == "12")
                    {
                        object t = ObjectUtil.GetObject(WriteToService(drTaskCrane["SERVICE_NAME"].ToString(), drTaskCrane["ITEM_NAME_1"].ToString()));//读取当前出库站台是否有货位
                        if (t.ToString() != "0")
                        {
                            blnSend = false;
                            return blnSend;
                        }
                        TaskDal dal = new TaskDal();
                        string strTaskDetailNo = dal.InsertTaskDetail(drTaskCrane["TASK_ID"].ToString());
                        drTaskCrane.BeginEdit();
                        drTaskCrane["TASK_NO"] = strTaskDetailNo;
                        drTaskCrane["ASSIGNMENT_ID"] = strTaskDetailNo.PadLeft(8, '0');
                        drTaskCrane.EndEdit();
                    }
                    SendTelegramARQ(drTaskCrane);//发送报文
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
                    if (drs.Length > 0)
                    {
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
                    else
                    {
                        blnvalue = true;
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
            }
            DataRow[] drs = dt.Select("", "TASK_LEVEL desc,IS_MIX,SORT_LEVEL,PRODUCT_CODE,TASK_ID");
            object[] obj = new object[dt.Columns.Count]; 
            for (int i = 0; i < drs.Length; i++)
            {
                DataRow[] drsExist = dtCrane.Select(string.Format("TASK_ID='{0}'", drs[i]["TASK_ID"]));
                if (drsExist.Length > 0)
                    continue;
                
                drs[i].ItemArray.CopyTo(obj,0);
                dtCrane.Rows.Add(obj);
            }
            dtCrane.AcceptChanges();
            if (drs.Length > 0) //重新排序
            {
                DataTable dtOrder = dtCrane.DefaultView.ToTable(true, new string[] {"TASK_TYPE", "TASK_LEVEL", "IS_MIX", "SORT_LEVEL", "PRODUCT_CODE" });
                dtOrderCrane = new DataTable();
                dtOrderCrane = dtOrder.Clone();
                DataColumn dc = new DataColumn("Index", Type.GetType("System.Int32"));
                dtOrderCrane.Columns.Add(dc);

                drs = dtOrder.Select("TASK_TYPE=22", "TASK_LEVEL desc,IS_MIX,SORT_LEVEL,PRODUCT_CODE");
                obj = new object[dtOrderCrane.Columns.Count];
                for (int i = 0; i < drs.Length; i++)
                {
                    drs[i].ItemArray.CopyTo(obj, 0);
                    obj[dtOrderCrane.Columns.Count] = i + 1;
                    dtOrderCrane.Rows.Add(obj);
                }
            }
        }

        /// <summary>
        /// 多线程调用6台堆垛机。
        /// </summary>
        private void CraneThreadStart()
        {
            ThreadStart starter1 = delegate { SendTelegram("01", null); };
            new Thread(starter1).Start();

            ThreadStart starter2 = delegate { SendTelegram("02", null); };
            new Thread(starter2).Start();

            ThreadStart starter3 = delegate { SendTelegram("03", null); };
            new Thread(starter3).Start();

            ThreadStart starter4 = delegate { SendTelegram("04", null); };
            new Thread(starter4).Start();

            ThreadStart starter5 = delegate { SendTelegram("05", null); };
            new Thread(starter5).Start();

            ThreadStart starter6 = delegate { SendTelegram("06", null); };
            new Thread(starter6).Start();
 
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
                string TASK_ID = drs[0]["TASK_ID"].ToString();
                TaskDal dal = new TaskDal();
                string strWhere = string.Format("TASK_ID='{0}' and CRANE_NO is not null", drs[0]["TASK_ID"]);
                dal.UpdateTaskDetailState(strWhere, "2"); //更新堆垛机状态
                if (TaskType.PadRight(2, '0').Substring(1, 1) == "2")
                {
                    if (TaskType == "12") //一楼出库
                    {
                        CellDal Cdal = new CellDal();
                        Cdal.UpdateCellOutUnLock(drs[0][""].ToString());//货位解锁
                    }
                    int[] WriteValue = new int[3];
                    WriteValue[0] = int.Parse(drs[0]["TASK_NO"].ToString());
                    if (TaskType == "12")
                        WriteValue[1] = int.Parse(drs[0]["TARGET_CODE"].ToString());
                    else
                        WriteValue[1] = int.Parse(drs[0]["MEMO"].ToString());
                    WriteValue[2] = int.Parse(drs[0]["PRODUCT_TYPE"].ToString());


                    string Barcode = "";














                    dal.UpdateTaskDetailStation(drs[0]["STATION_NO"].ToString(), WriteValue[1].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", TASK_ID));

                    WriteToService(drs[0]["SERVICE_NAME"].ToString(), drs[0]["ITEM_NAME_2"].ToString() + "_1", WriteValue);

                    WriteToService(drs[0]["SERVICE_NAME"].ToString(), drs[0]["ITEM_NAME_2"].ToString() + "_2", Barcode);
                    WriteToService(drs[0]["SERVICE_NAME"].ToString(), drs[0]["ITEM_NAME_2"].ToString() + "_3", 1);
                  
                

                }
                else if(TaskType.PadRight(2, '0').Substring(1, 1) == "1") //入库完成，更新Task任务完成。
                {
                    dal.UpdateTaskState(drs[0]["TASK_ID"].ToString(), "2");//更新任务状态。
                    CellDal Cdal = new CellDal();
                    Cdal.UpdateCellInLock(); //入库完成，更新货位。

                    BillDal billdal = new BillDal();
                    billdal.UpdateBillMasterFinished(drs[0]["BILL_NO"].ToString());//更新表单

                }
                lock (dCraneState)
                {
                    dCraneState[msg["CraneNo"]] = "0";
                }
                if (TaskType != "22")
                {
                    dtCrane.Rows.Remove(drs[0]);
                }

                //查找发送下条报文。
                GetNextTaskID(msg["CraneNo"], TaskType);
            }
            else
            {
                lock (dCraneState)
                {
                    dCraneState[msg["CraneNo"]] = "1";
                }
                string strMessage = "";
                DataRow[] drs = dtErrMesage.Select(string.Format("CODE='{0}'", msg["ReturnCode"]));
                if (drs.Length > 0)
                    strMessage = drs[0]["DESCRIPTION"].ToString();
                Logger.Error(string.Format("堆垛机{0}返回错误代码{1}:{2}", msg["CraneNo"], msg["ReturnCode"], strMessage));
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
                lock (dCraneState)
                {
                    if (msg["AssignmenID"] == "00000000" && msg["CraneMode"] == "1")
                        dCraneState[msg["CraneNo"]] = "0";
                    else
                        dCraneState[msg["CraneNo"]] = "1";
                }
            }
            else
            {
                lock (dCraneState)
                {
                    dCraneState[msg["CraneNo"]] = "1";
                }
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
            
            DataRow dr = dtCrane.Select(string.Format("TASK_NO='{0}'", msg["SequenceNo"]))[0];

            TaskDal dal = new TaskDal();
            if (dr["TASK_TYPE"].ToString() == "22" || dr["TASK_TYPE"].ToString() == "12")
                dal.UpdateTaskDetailCrane(dr["FROM_STATION"].ToString(), dr["TO_STATION"].ToString(), "1", dr["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO={1}", dr["TASK_ID"], dr["ITEM_NO"]));
            else
                dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' and ITEM_NO={1}", dr["TASK_ID"], dr["ITEM_NO"]), "1");
 

            dr.BeginEdit();
            dr["state"] = "1";
            dr.EndEdit();
            dtCrane.AcceptChanges();
          
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
                DataRow[] drs = dtCrane.Select(string.Format("substring(SQUENCE_NO,9,4)='{0}'", msg["SequenceNo"]));
                if (drs.Length > 0)
                {
                    lock (dCraneState)
                    {
                        dCraneState[drs[0]["CRANE_NO"].ToString()] = "0";
                    }
                    SendTelegram(drs[0]["CRANE_NO"].ToString(), drs[0]);
                }
                else
                {
                    drs = dtSendCRQ.Select(string.Format("substring(SQUENCE_NO,9,4)='{0}'", msg["SequenceNo"]));
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
            lock (dCraneState)
            {
                dCraneState[dr["CRANE_NO"].ToString()] = "1";
            }
            dtCrane.AcceptChanges();
            //更新发送报文。
            TaskDal dal = new TaskDal();
            string TaskType = dr["TASK_TYPE"].ToString();
            if (TaskType.PadRight(2, '0').Substring(1, 1) == "2")
                dal.UpdateTaskState(dr["TASK_ID"].ToString(), "1");//出库任务 更新任务状态--任务开始。
            
            dal.UpdateCraneQuenceNo(dr["TASK_ID"].ToString(), dr["SQUENCE_NO"].ToString()); //更新堆垛机序列号。并更新为1
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
