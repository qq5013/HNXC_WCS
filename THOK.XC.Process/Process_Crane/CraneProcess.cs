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
        private Dictionary<string, DataRow> dCraneWait = new Dictionary<string, DataRow>(); //堆垛机待入库。 0：空闲，1：执行中
        
        private DataTable dtSendCRQ;
        private DataTable dtErrMesage;
        private int NCK001;

        //process.Initialize(context);初始化的时候执行
        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                //堆垛机收发报文的错误信息代码
                CraneErrMessageDal errDal = new CraneErrMessageDal();
                dtErrMesage = errDal.GetErrMessageList();
                NCK001 = 100;

                for (int i = 1; i <= 6; i++)
                {
                    string CraneNo = i.ToString().PadLeft(2, '0');
                    if (!dCraneWait.ContainsKey(CraneNo))
                    {
                        dCraneWait.Add(CraneNo, null);
                    }
                }


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
                        string[] strdd = (string[])stateItem.State;
                        if (dtCrane != null)
                        {
                            DataRow[] drs = dtCrane.Select(string.Format("TASK_ID='{0}'", strdd[0]));
                            if (drs.Length > 0)
                            {
                                TaskDal tdal = new TaskDal();
                                tdal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=1", drs[0]["TASK_ID"].ToString()), "2");

                                ProductStateDal psdal = new ProductStateDal();
                                psdal.UpdateOutBillNo(strdd[0]);
                                dtCrane.Rows.Remove(drs[0]);

                            }
                            CraneThreadStart(); //更新完成之后，线程调用堆垛机，避免堆垛机因调度原因而是堆垛机没有任务。
                        }
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
                    case "DUU":
                        WriteToService("Crane", "DUM", "<00000CRAN30THOK01DUM0000000>");
                        break;
                    case "NCK":
                        NCK(stateItem.State);
                        break;
                    case "DEC":
                        DEC(stateItem.State);
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
            }
          

            if (string.IsNullOrEmpty(dCraneState[CraneNo]))  //等待堆垛机应答。
            {
                if (dCraneWait[CraneNo] == null && drTaskID != null)
                {
                    dCraneWait[CraneNo] = drTaskID;
                    return false;
                }
                return false;
                
            }
            if (dCraneState[CraneNo] == "1") //堆垛机正忙。
                return true;
            TaskDal dal = new TaskDal();
            DataRow drTaskCrane = null;
            if (drTaskID==null && dtCrane!=null) //出库任务调用堆垛机
            {
                //读取二楼出库站台是否有烟包，PLC
                //按照任务等级，任务时间，产品形态，
                DataRow[] drs = dtCrane.Select(string.Format("CRANE_NO='{0}' and STATE=0 and TASK_TYPE in ('12','22','13','14')", CraneNo), "TASK_LEVEL,TASK_DATE,BILL_NO,IS_MIX,PRODUCT_CODE,TASK_ID"); 
                for (int i = 0; i < drs.Length; i++)
                {
                    //判断能否出库
                    if (drs[i]["TASK_TYPE"].ToString() == "22") //二楼出库，判断能否出库
                    {
                        bool blnCan = dal.ProductCanToCar(drs[i]["FORDERBILLNO"].ToString(), drs[i]["FORDER"].ToString(), drs[i]["IS_MIX"].ToString());  //判断能否出库
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
                    string TaskType=drTaskCrane["TASK_TYPE"].ToString();
                    string ItemNo=drTaskCrane["ITEM_NO"].ToString();
                    if (TaskType.Substring(1, 1) == "2" || (TaskType.Substring(1, 1) == "3" && ItemNo == "1") || (TaskType.Substring(1, 1) == "4" && ItemNo == "1" && drTaskCrane["CRANE_NO"].ToString() != drTaskCrane["NEW_CRANE_NO"].ToString()))
                    {
                        object t = ObjectUtil.GetObject(WriteToService(drTaskCrane["SERVICE_NAME"].ToString(), drTaskCrane["ITEM_NAME_1"].ToString()));//读取当前出库站台是否有货位
                        if (t.ToString() != "0")
                        {
                            blnSend = false;
                            return blnSend;
                        }
                    }
                    if (drTaskCrane["ITEM_NO"].ToString() == "1")
                    {
                       
                        string strTaskDetailNo = dal.InsertTaskDetail(drTaskCrane["TASK_ID"].ToString());

                        DataRow[] drs = dtCrane.Select(string.Format("TASK_ID='{0}'", drTaskCrane["TASK_ID"]));
                        if (drs.Length > 0)
                        {
                            drs[0].BeginEdit();
                            drs[0]["TASK_NO"] = strTaskDetailNo;
                            drs[0]["ASSIGNMENT_ID"] = strTaskDetailNo.PadLeft(8, '0');
                            drs[0].EndEdit();
                            dtCrane.AcceptChanges();
                        }
                    }
                    SendTelegramARQ(drTaskCrane, true);//发送报文
                    blnSend = true;
                }
            }
           
            return blnSend;
        }

        ///// <summary>
        ///// 能否发送出库报文。
        ///// </summary>
        ///// <param name="TaskID"></param>
        ///// <returns></returns>
        //private bool ProductCanOut(DataRow drTaskID)
        //{
            
        //    bool blnvalue = false;
        //    DataRow[] drs = dtCrane.Select(string.Format("BILL_NO='{0}' and PRODUCT_CODE='{1}' and IS_MIX='{2}' and STATE=1", drTaskID["BILL_NO"], drTaskID["PRODUCT_CODE"], drTaskID["IS_MIX"]));   //判断当前单号，当前产品，当前形态是否有state=1的出库任务，有则返回true;
        //    if (drs.Length > 0)
        //    {
        //        blnvalue = true;
        //    }
        //    else
        //    {

        //        drs = dtOrderCrane.Select(string.Format("TASK_LEVEL={0} and TASK_DATE={1} and IS_MIX={2} and FORDER={3} and PRODUCT_CODE={4}", new object[] { drTaskID["TASK_LEVEL"], drTaskID["TASK_DATE"], drTaskID["IS_MIX"], drTaskID["FORDER"], drTaskID["PRODUCT_CODE"] }));
        //        if (drs.Length > 0)
        //        {
        //            drs = dtOrderCrane.Select(string.Format("Index<{0}", drs[0]["Index"]));
        //            if (drs.Length > 0)
        //            {
        //                for (int i = 0; i < drs.Length; i++)
        //                {
        //                    drs = dtCrane.Select(string.Format("TASK_LEVEL={0} and TASK_DATE={1} and IS_MIX={2} and FORDER={3} and PRODUCT_CODE={4} and TASK_TYPE='22' and STATE in (0,1,2)", new object[] { drTaskID["TASK_LEVEL"], drTaskID["TASK_DATE"], drTaskID["IS_MIX"], drTaskID["FORDER"], drTaskID["PRODUCT_CODE"] }));//判断小于当前Index的出库任务，是否有未完成的出库任务，如果没有，则返回True.
        //                    if (drs.Length == 0)
        //                    {
        //                        blnvalue = true;
        //                    }
        //                    else
        //                    {
        //                        blnvalue = false;
        //                        break;
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                blnvalue = true;
        //            }
        //        }                
        //    }
        //    return blnvalue;
        //}

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
                case "3":
                    blnSend = false;
                    blnSend = SendTelegram(CraneNo, null);  //查询出库报文
                    if (!blnSend)
                    {
                        drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE in ('11','21','13','14')", CraneNo));
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
                            drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE in ('11','13','14')", CraneNo),"TASK_LEVEL DESC");
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
                            drs = dtCrane.Select(string.Format("CRANE_NO={0} and TASK_TYPE  in ('11','13','14') ", CraneNo));
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
            DataRow[] drs = dt.Select("", "TASK_LEVEL desc,IS_MIX,FORDER,PRODUCT_CODE,TASK_ID");
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
        }

        /// <summary>
        /// 多线程调用6台堆垛机。
        /// </summary>
        private void CraneThreadStart()
        {
            //ThreadStart starter1 = delegate { SendTelegram("01", null); };
            //new Thread(starter1).Start();

            SendTelegram("01", null);
            SendTelegram("02", null);
            SendTelegram("03", null);
            SendTelegram("04", null);
            SendTelegram("05", null);
            SendTelegram("06", null);
 
        }
        #endregion  
       
        #region 处理接堆垛机发送的报文
        private void ACP(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            TaskDal dal = new TaskDal();
            if (!dCraneState.ContainsKey(msg["CraneNo"]))
            {
                dCraneState.Add(msg["CraneNo"], "");
            }
            DataRow dr = null;
            if (dtCrane != null)
            {
                DataRow[] drs = dtCrane.Select(string.Format("SQUENCE_NO='{0}'", msg["SequenceNo"]));
                if (drs.Length > 0)
                {
                    dr = drs[0];
                }
            }
            if (dr == null)
            {
                //根据流水号，获取资料
                DataTable dt = dal.CraneTaskIn(string.Format("DETAIL.SQUENCE_NO='{1}' AND DETAIL.CRANE_NO='{0}'", msg["CraneNo"], msg["SequenceNo"]));
                if (dt.Rows.Count > 0)
                    dr = dt.Rows[0];

            }
            string TaskType = "";
            string TaskID = ""; 
            string ItemNo = "";
            if (dr != null)
            {
                TaskType = dr["TASK_TYPE"].ToString();
                TaskID = dr["TASK_ID"].ToString();
                ItemNo = dr["ITEM_NO"].ToString();
            }
            if (msg["ReturnCode"] == "000")
            {
                #region 正常处理流程

                if (dr != null)
                {
                    if (dCraneWait[msg["CraneNo"]] != null)
                    {
                        if (dCraneWait[msg["CraneNo"]]["TASK_ID"].ToString() == TaskID)
                        {
                            dCraneWait[msg["CraneNo"]] = null;
                        }
                    }



                    string strWhere = string.Format("TASK_ID='{0}' and ITEM_NO='{1}'", TaskID, ItemNo);
                    dal.UpdateTaskDetailState(strWhere, "2"); //更新堆垛机状态
                    if (TaskType.Substring(1, 1) == "2" || (TaskType.Substring(1, 1) == "3" && ItemNo == "1")) //出库，一楼盘点出库
                    {
                        if (TaskType == "12") //一楼出库
                        {
                            CellDal Cdal = new CellDal();
                            Cdal.UpdateCellOutFinishUnLock(dr["CELL_CODE"].ToString());//货位解锁

                            ProductStateDal psdal = new ProductStateDal();
                            psdal.UpdateOutBillNo(TaskID);

                        }
                        int[] WriteValue = new int[3];
                        WriteValue[0] = int.Parse(dr["TASK_NO"].ToString());
                        if (TaskType == "22")
                            WriteValue[1] = int.Parse(dr["MEMO"].ToString());
                        else
                            WriteValue[1] = int.Parse(dr["TARGET_CODE"].ToString());
                        WriteValue[2] = int.Parse(dr["PRODUCT_TYPE"].ToString());


                        string Barcode = dr["PRODUCT_BARCODE"].ToString();
                        string PalletCode = dr["PALLET_CODE"].ToString();

                        byte[] b = new byte[190];
                        Common.ConvertStringChar.stringToByte(Barcode, 80).CopyTo(b, 0);
                        Common.ConvertStringChar.stringToByte(PalletCode, 110).CopyTo(b, 80);

                        dal.UpdateTaskDetailStation(dr["STATION_NO"].ToString(), WriteValue[1].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", TaskID));


                        WriteToService(dr["SERVICE_NAME"].ToString(), dr["ITEM_NAME_2"].ToString() + "_1", WriteValue);

                        WriteToService(dr["SERVICE_NAME"].ToString(), dr["ITEM_NAME_2"].ToString() + "_2", b);
                        WriteToService(dr["SERVICE_NAME"].ToString(), dr["ITEM_NAME_2"].ToString() + "_3", 1);



                    }
                    else if (TaskType.Substring(1, 1) == "1" || (TaskType.Substring(1, 1) == "3" && dr["ITEM_NO"].ToString() == "4")) //入库完成，更新Task任务完成。
                    {
                        dal.UpdateTaskState(TaskID, "2");//更新任务状态。
                        if (TaskType == "11")
                        {
                            CellDal Cdal = new CellDal();
                            Cdal.UpdateCellInFinishUnLock(TaskID);//入库完成，更新货位。
                        }

                        BillDal billdal = new BillDal();
                        string isBill = "1";
                        if (dr["PRODUCT_CODE"].ToString() == "0000")
                            isBill = "0";
                        billdal.UpdateInBillMasterFinished(dr["BILL_NO"].ToString(), isBill);//更新表单

                    }
                    else if (TaskType == "14")
                    {
                        #region 移库
                        if (dr["CRANE_NO"].ToString() != dr["NEW_CRANE_NO"].ToString())
                        {
                            if (ItemNo == "1")
                            {
                                CellDal Cdal = new CellDal();
                                Cdal.UpdateCellOutFinishUnLock(dr["CELL_CODE"].ToString());

                                int[] WriteValue = new int[3];
                                WriteValue[0] = int.Parse(dr["TASK_NO"].ToString());

                                WriteValue[1] = int.Parse(dr["NEW_TARGET_CODE"].ToString());

                                WriteValue[2] = int.Parse(dr["PRODUCT_TYPE"].ToString());


                                string Barcode = dr["PRODUCT_BARCODE"].ToString();
                                string PalletCode = dr["PALLET_CODE"].ToString();

                                byte[] b = new byte[190];
                                Common.ConvertStringChar.stringToByte(Barcode, 80).CopyTo(b, 0);
                                Common.ConvertStringChar.stringToByte(PalletCode, 110).CopyTo(b, 80);

                                dal.UpdateTaskDetailStation(dr["STATION_NO"].ToString(), WriteValue[1].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", TaskID));


                                WriteToService(dr["SERVICE_NAME"].ToString(), dr["ITEM_NAME_2"].ToString() + "_1", WriteValue);

                                WriteToService(dr["SERVICE_NAME"].ToString(), dr["ITEM_NAME_2"].ToString() + "_2", b);
                                WriteToService(dr["SERVICE_NAME"].ToString(), dr["ITEM_NAME_2"].ToString() + "_3", 1);

                                BillDal billdal = new BillDal();
                                billdal.UpdateBillMasterStart(dr["BILL_NO"].ToString(), true);//更新表单

                            }
                            else
                            {
                                dal.UpdateTaskState(TaskID, "2");//更新任务状态。

                                CellDal Cdal = new CellDal();
                                Cdal.UpdateCellInFinishUnLock(dr["NEWCELL_CODE"].ToString());

                                BillDal billdal = new BillDal();
                                string isBill = "1";
                                if (dr["PRODUCT_CODE"].ToString() == "0000")
                                    isBill = "0";
                                billdal.UpdateInBillMasterFinished(dr["BILL_NO"].ToString(), isBill);//更新表单

                            }


                        }
                        else
                        {
                            dal.UpdateTaskState(dr["TASK_ID"].ToString(), "2");//更新任务状态。

                            CellDal Cdal = new CellDal();
                            Cdal.UpdateCellRemoveFinish(dr["NEWCELL_CODE"].ToString(), dr["CELL_CODE"].ToString()); //入库完成，更新货位。
                            Cdal.UpdateCellOutFinishUnLock(dr["CELL_CODE"].ToString());

                            BillDal billdal = new BillDal();
                            string isBill = "1";
                            if (dr["PRODUCT_CODE"].ToString() == "0000")
                                isBill = "0";
                            billdal.UpdateInBillMasterFinished(dr["BILL_NO"].ToString(), isBill);//更新表单

                        }
                        #endregion
                    }
                    lock (dCraneState)
                    {
                        dCraneState[msg["CraneNo"]] = "0";
                    }
                    if (dtCrane != null)
                    {
                        DataRow[] drs = dtCrane.Select(string.Format("TASK_ID='{0}'", TaskID));
                        if (drs.Length > 0)
                        {
                            dtCrane.Rows.Remove(drs[0]);
                        }
                    }

                    //查找发送下条报文。
                    GetNextTaskID(msg["CraneNo"], TaskType);
                }
                #endregion
            }
            else
            {
                string ErrMsg = "";
                DataRow[] drMsgs = dtErrMesage.Select(string.Format("CODE='{0}'", msg["ReturnCode"]));
                if (drMsgs.Length > 0)
                    ErrMsg = drMsgs[0]["DESCRIPTION"].ToString();

                dal.UpdateCraneErrCode(TaskID, ItemNo, msg["ReturnCode"]);//更新堆垛机错误编号
                Logger.Error(string.Format("堆垛机{0}返回错误代码{1}:{2}", msg["CraneNo"], msg["ReturnCode"], ErrMsg));
               
            }
            SendACK(msg);
            CraneErrWriteToPLC(msg["CraneNo"], int.Parse(msg["ReturnCode"]));
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
                    {
                    
                        dCraneState[msg["CraneNo"]] = "0";

                        if (dCraneWait[msg["CraneNo"]] != null)
                        {
                            SendTelegram(msg["CraneNo"], dCraneWait[msg["CraneNo"]]);
                        }
                        else
                        {
                            SendTelegram(msg["CraneNo"], null);
                        }
                    }
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

                #region 堆垛机错误而停止，重新选择出库批次
                if (msg["ReturnCode"] == "000")
                {
                    DataRow dr = null;
                    TaskDal dal = new TaskDal();
                    if (dtCrane != null)
                    {
                        DataRow[] drs = dtCrane.Select(string.Format("SQUENCE_NO='{0}'", msg["SequenceNo"]));
                        if (drs.Length > 0)
                        {
                            dr = drs[0];
                        }
                    }
                    if (dr == null)
                    {
                        //根据流水号，获取资料
                        DataTable dt = dal.CraneTaskIn(string.Format("DETAIL.SQUENCE_NO='{0}' AND DETAIL.CRANE_NO IS NOT NULL", msg["SequenceNo"]));
                        if (dt.Rows.Count > 0)
                            dr = dt.Rows[0];
                    }
                    if (dr != null)
                    {

                        string ErrMsg = "";
                        DataRow[] drMsgs = dtErrMesage.Select(string.Format("CODE='{0}'", dr["ERR_CODE"].ToString()));
                        if (drMsgs.Length > 0)
                            ErrMsg = drMsgs[0]["DESCRIPTION"].ToString();

                        string strBillNo = "";
                        string[] strMessage = new string[4];
                        strMessage[0] = "9";
                        strMessage[1] = dr["TASK_ID"].ToString();
                        strMessage[2] = "错误代码：" + dr["ERR_CODE"] + ",错误内容：" + ErrMsg;
                        strMessage[3] = msg["CraneNo"];

                        DataTable dtProductInfo = dal.GetProductInfoByTaskID(dr["TASK_ID"].ToString());

                        while ((strBillNo = FormDialog.ShowDialog(strMessage, dtProductInfo)) != "")
                        {
                            if (strBillNo != "1")
                            {
                                BillDal bdal = new BillDal();
                                string strNewBillNo = strBillNo;
                                //产生新的出库单
                                string strOutTaskID = bdal.CreateCancelBillOutTask(dr["TASK_ID"].ToString(), dr["BILL_NO"].ToString(), strNewBillNo, msg["CraneNo"]);
                                DataTable dtOutTask = dal.CraneTaskOut(string.Format("TASK_ID='{0}'", strOutTaskID));
                                WriteToProcess("CraneProcess", "CraneInRequest", dtOutTask);

                                //取消当前单据的出货记录
                                CellDal cdal = new CellDal();
                                cdal.UpdateCellUnLock(dr["CELL_CODE"].ToString());
                                dal.UpdateTaskState(dr["TASK_ID"].ToString(), "3");
                                break;


                            }
                            else
                            {
                                break;
                            }
                        }



                    }
                    if (msg["AssignmenID"] == "00000000")
                    {
                        lock (dCraneState)
                        {
                            dCraneState[msg["CraneNo"]] = "0";
                        }
                    }
                }
                #endregion

            }
            SendACK(msg);
            CraneErrWriteToPLC(msg["CraneNo"], int.Parse(msg["ReturnCode"]));
        }
        /// <summary>
        /// 发送报文后，堆垛机发送接收确认。
        /// </summary>
        /// <param name="state"></param>
        private void ACK(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            DataRow dr = null;
            TaskDal dal = new TaskDal();
            if (dtCrane != null)
            {
                DataRow[] drs = dtCrane.Select(string.Format("SQUENCE_NO='{0}'", msg["SequenceNo"]));
                if (drs.Length > 0)
                {
                    dr = drs[0];
                }
            }
            if (dr == null)
            {
                //根据流水号，获取资料
                DataTable dt = dal.CraneTaskIn(string.Format("DETAIL.SQUENCE_NO='{0}' AND DETAIL.CRANE_NO IS NOT NULL", msg["SequenceNo"]));
                if (dt.Rows.Count > 0)
                    dr = dt.Rows[0];
            }

            #region 缓存数据存在
            if (dr != null)
            {
                string TaskType = dr["TASK_TYPE"].ToString();
                string ItemNo = dr["ITEM_NO"].ToString();
                BillDal bdal = new BillDal();
                if (TaskType.Substring(1, 1) == "2" || (TaskType.Substring(1, 1) == "3" && ItemNo == "1") || (TaskType.Substring(1, 1) == "4" && ItemNo == "1" && dr["CRANE_NO"].ToString() != dr["NEW_CRANE_NO"].ToString()))
                {
                    dal.UpdateTaskState(dr["TASK_ID"].ToString(), "1");//出库任务 更新任务状态--任务开始。
                    dal.UpdateTaskDetailCrane(dr["CELL_CODE"].ToString(), dr["STATION_NO"].ToString(), "1", dr["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO={1}", dr["TASK_ID"], dr["ITEM_NO"]));
                    bdal.UpdateBillMasterStart(dr["BILL_NO"].ToString(), dr["PRODUCT_CODE"].ToString() == "0000" ? false : true);

                }
                else if (TaskType.Substring(1, 1) == "4" && ItemNo == "1" && dr["CRANE_NO"].ToString() == dr["NEW_CRANE_NO"].ToString())
                {
                    dal.UpdateTaskState(dr["TASK_ID"].ToString(), "1");//出库任务 更新任务状态--任务开始。
                    dal.UpdateTaskDetailCrane(dr["CELL_CODE"].ToString(), dr["NEWCELL_CODE"].ToString(), "1", dr["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO={1}", dr["TASK_ID"], dr["ITEM_NO"]));
                    bdal.UpdateBillMasterStart(dr["BILL_NO"].ToString(), true);
                }
                else
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' and ITEM_NO={1}", dr["TASK_ID"], dr["ITEM_NO"]), "1");

                if (dtCrane != null)
                {
                    DataRow []drs=dtCrane.Select(string.Format("TASK_ID='{0}'",dr["TASK_ID"]));
                    if(drs.Length>0)
                    {
                        drs[0].BeginEdit();
                        drs[0]["state"]="1";
                        drs[0].EndEdit();
                        dtCrane.AcceptChanges();
                    }
                     
                }
            }
            #endregion

        }

        /// <summary>
        ///发送报文，堆垛机返回序列号错误，或Buffer已满
        /// </summary>
        /// <param name="state"></param>
        private void NCK(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            if (msg["FaultIndicator"] == "1" ) //序列号出错，重新发送报文
            {
                if (msg["SequenceNo"] != "0001")
                {
                    //重置流水号为0；
                    SysStationDal dal = new SysStationDal();
                    dal.ResetSQueNo();
                    //重新发送SYN
                    WriteToService("Crane", "DUM", "<00000CRAN30THOK01SYN0000000>");
                    NCK001 = 0;
                }
                else
                {
                    if (NCK001 == 100)
                    {
                        WriteToService("Crane", "DUM", "<00000CRAN30THOK01SYN0000000>");
                        NCK001 = 0;
                    }
                    else
                    {
                        CraneThreadStart();
                    }
                }
          
            }
        }
        /// <summary>
        ///接收删除指令返回值
        /// </summary>
        /// <param name="state"></param>
        private void DEC(object state)
        {
            Dictionary<string, string> msg = (Dictionary<string, string>)state;
            if (msg["ReturnCode"] == "000") //序列号出错，重新发送报文
            {
                TaskDal dal = new TaskDal();
                DataTable dt = dal.CraneTaskIn(string.Format("DETAIL.CRANE_NO='' AND ASSIGNMENT_ID=''", msg["CraneNo"], msg["AssignmenID"]));
                DataRow dr = null;
                if (dt.Rows.Count > 0)
                    dr = dt.Rows[0];
                if (dr != null)
                {
                    #region 错误处理
                    
                    if (dr["ERR_CODE"].ToString() == "111") //入库，货位有货,重新分配货位
                    {

                        CellDal cdal = new CellDal();
                        cdal.UpdateCellErrFlag(dr["CELL_CODE"].ToString(), "货位有货，系统无记录");

                        string[] strValue = dal.AssignNewCell(string.Format("TASK_ID='{0}'",dr["TASK_ID"].ToString()), dr["CRANE_NO"].ToString());//货位申请
                        ProductStateDal StateDal = new ProductStateDal();
                        StateDal.UpdateProductCellCode(strValue[0], strValue[1]); //更新Product_State 货位
                        
                        SysStationDal sysdal = new SysStationDal();
                        DataTable dtstation = sysdal.GetSationInfo(strValue[1], dr["TASK_TYPE"].ToString(), dr["ITEM_NO"].ToString());
                        dal.UpdateTaskDetailCrane(dtstation.Rows[0]["STATION_NO"].ToString(), strValue[1], "1", dtstation.Rows[0]["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO={1}", strValue[0], dr["ITEM_NO"].ToString()));//更新调度堆垛机的其实位置及目标地址。
                       
                        dr.BeginEdit();
                        dr["CELLSTATION"] = "30" + strValue[1] + "01";
                        dr.EndEdit();
                        SendTelegramARQ(dr, false);
                        //if (dtCrane != null)
                        //{
                        //    DataRow[] drs = dtCrane.Select(string.Format("ASSIGNMENT_ID='{0}'", msg["AssignmenID"]));
                        //    if (drs.Length > 0)
                        //        dtCrane.Rows.Remove(drs[0]);
                        //}
                    }
                    else if (dr["ERR_CODE"].ToString() == "113")//出库，货位无货，
                    {

                        string ErrMsg = "";
                        DataRow[] drMsgs = dtErrMesage.Select(string.Format("CODE='{0}'", dr["ERR_CODE"].ToString()));
                        if (drMsgs.Length > 0)
                            ErrMsg = drMsgs[0]["DESCRIPTION"].ToString();

                        string strBillNo = "";
                        string[] strMessage = new string[3];
                        strMessage[0] = "8";
                        strMessage[1] =dr["TASK_ID"].ToString();
                        strMessage[2] = "错误代码：" + dr["ERR_CODE"] + ",错误内容：" + ErrMsg;

                        DataTable dtProductInfo = dal.GetProductInfoByTaskID(dr["TASK_ID"].ToString());

                        while ((strBillNo = FormDialog.ShowDialog(strMessage, dtProductInfo)) != "")
                        {
                            BillDal bdal = new BillDal();
                            string strNewBillNo = strBillNo;

                            string strOutTaskID = bdal.CreateCancelBillOutTask(dr["TASK_ID"].ToString(), dr["BILL_NO"].ToString(), strNewBillNo);

                            DataTable dtOutTask = dal.CraneTaskOut(string.Format("TASK_ID='{0}'", strOutTaskID));

                            WriteToProcess("CraneProcess", "CraneInRequest", dtOutTask);
                            CellDal cdal = new CellDal();
                            cdal.UpdateCellErrFlag(dr["CELL_CODE"].ToString(), "货位无货，系统有记录");
                            break;
                        }
                    }
                }
                 
                #endregion
            }
        }
        #endregion

        #region 发送堆垛机报文
        private void SendTelegramARQ(DataRow dr,bool blnValue)
        {

            THOK.CRANE.TelegramData tgd = new CRANE.TelegramData();
            tgd.CraneNo = dr["CRANE_NO"].ToString();
            tgd.AssignmenID = dr["ASSIGNMENT_ID"].ToString();
            if (!blnValue)
            {
                tgd.AssignmentType = "DE";
            }
            string TaskType = dr["TASK_TYPE"].ToString();
            string ItemNo = dr["ITEM_NO"].ToString();


            if (TaskType.Substring(1, 1) == "4" && ItemNo == "1" && dr["CRANE_NO"].ToString() == dr["NEW_CRANE_NO"].ToString())
            {
                tgd.StartPosition = dr["CRANESTATION"].ToString();
                tgd.DestinationPosition = dr["NEW_TO_STATION"].ToString();
            }
            else
            {
                if (TaskType.Substring(1, 1) == "1" || (TaskType.Substring(1, 1) == "4" && ItemNo == "3") || TaskType.Substring(1, 1) == "3" && ItemNo == "4") //入库
                {
                    tgd.StartPosition = dr["CRANESTATION"].ToString();
                    tgd.DestinationPosition = dr["CELLSTATION"].ToString();
                }
                else //出库
                {
                    tgd.StartPosition = dr["CELLSTATION"].ToString();
                    tgd.DestinationPosition = dr["CRANESTATION"].ToString();
                }
            }

            THOK.CRANE.TelegramFraming tf = new CRANE.TelegramFraming();
            string QuenceNo = GetNextSQuenceNo();
            string str = tf.DataFraming("1" + QuenceNo, tgd, tf.TelegramARQ);
            WriteToService("Crane", "ARQ", str);
         

            DataRow[] drs = dtCrane.Select(string.Format("TASK_ID='{0}'", dr["TASK_ID"]));
            if (drs.Length > 0)
            {
                drs[0].BeginEdit();
                drs[0]["SQUENCE_NO"] = QuenceNo;
                drs[0].EndEdit();
                dtCrane.AcceptChanges();

                dr.BeginEdit();
                dr["SQUENCE_NO"] = QuenceNo;
                dr.EndEdit();
            }
            lock (dCraneState)
            {
                dCraneState[dr["CRANE_NO"].ToString()] = "1";
            }
           
            //更新发送报文。
            TaskDal dal = new TaskDal();
            dal.UpdateCraneQuenceNo(dr["TASK_ID"].ToString(), dr["SQUENCE_NO"].ToString(), ItemNo); //更新堆垛机序列号。并更新为1
        }
        //请求堆垛机状态
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
            dr["SQUENCE_NO"] = QuenceNo;
            dr.EndEdit();
            dtSendCRQ.Rows.Add(dr);
            dtSendCRQ.AcceptChanges();
        }

        /// <summary>
        /// 删除指令
        /// </summary>
        /// <param name="dr"></param>
        private void SendTelegramDER(DataRow dr)
        {
            THOK.CRANE.TelegramData tgd = new CRANE.TelegramData();
            tgd.CraneNo = dr["CRANE_NO"].ToString();
            tgd.AssignmenID = dr["ASSIGNMENT_ID"].ToString();
            
            THOK.CRANE.TelegramFraming tf = new CRANE.TelegramFraming();
            string QuenceNo = GetNextSQuenceNo();
            string str = tf.DataFraming("1" + QuenceNo, tgd, tf.TelegramDER);
            WriteToService("Crane", "DER", str);
        }


        private string GetNextSQuenceNo()
        {
            SysStationDal dal = new SysStationDal();
            return dal.GetTaskNo("S");
        }
        private void SendACK(Dictionary<string, string> msg)
        {
            if (msg["ConfirmFlag"] == "1")
            {
                string str = "<00000CRAN30THOK01ACK0" + msg["SeqNo"] + "00>";
                WriteToService("Crane", "ACK", str);
            }
        }
        /// <summary>
        /// 堆垛机返回错误号，写入PLC
        /// </summary>
        /// <param name="CraneNo"></param>
        /// <param name="ErrCode"></param>
        private void CraneErrWriteToPLC(string CraneNo, int ErrCode)
        {
            WriteToService("StockPLC_01", "01_2_C" + CraneNo, ErrCode);
            WriteToService("StockPLC_02", "02_2_C" + CraneNo, ErrCode);
        }
        #endregion

    }
}
