using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_Car
{
    public class CarProcess : AbstractProcess
    {
        private DataTable dtCar;
        private DataTable dtCarOrder;
        private DataTable dtCarAddress;
        private Dictionary<string, string> dBillTargetCode = new Dictionary<string, string>();
        private Dictionary<string, bool> dBillUseTarget = new Dictionary<string, bool>();
        private int OrderIndex;

        public override void Initialize(Context context)
        {
            OrderIndex = 0;
            if (dtCarAddress == null)
            {
                SysCarAddressDal cad = new SysCarAddressDal();
                dtCarAddress = cad.CarAddress();
            }
            if (dtCarOrder == null)
            {
                dtCarOrder = new DataTable();
                DataColumn dc1 = new DataColumn("CAR_NO", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("State", Type.GetType("System.String"));
                DataColumn dc3 = new DataColumn("CurStation", Type.GetType("System.Int32"));
                DataColumn dc4 = new DataColumn("OrderNo", Type.GetType("System.Int32"));
                DataColumn dc5 = new DataColumn("ToStation", Type.GetType("System.Int32"));

                DataColumn dc6 = new DataColumn("WriteItem", Type.GetType("System.String"));
                DataColumn dc7 = new DataColumn("ToStationOrder", Type.GetType("System.Int32"));

                dtCarOrder.Columns.Add(dc1);
                dtCarOrder.Columns.Add(dc2);
                dtCarOrder.Columns.Add(dc3);
                dtCarOrder.Columns.Add(dc4);
                dtCarOrder.Columns.Add(dc5);
                dtCarOrder.Columns.Add(dc6);
                dtCarOrder.Columns.Add(dc7);
            }

            base.Initialize(context);
        }


        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            bool blnChange = false;
            string strState = "";
            string strCarNo = "";
            string strReadItem = "";
            string strWriteItem = "";
            switch (stateItem.ItemName)
            {
                case "CarOutRequest":
                case "CarInRequest":
                    OrderIndex++;
                    blnChange = false;
                    InsertdtCar((DataTable)stateItem.State);
                    break;
                case "02_1_C01_3":
                    strState = ObjectUtil.GetObject(stateItem.State).ToString();
                    blnChange = true;
                    strCarNo = "01";
                    strReadItem = "02_1_C01";
                    strWriteItem = "02_2_C01";
                    break;
                case "02_1_C02_3":
                    strState = ObjectUtil.GetObject(stateItem.State).ToString();
                    blnChange = true;
                    strCarNo = "02";
                    strReadItem = "02_1_C02";
                    strWriteItem = "02_2_C02";
                    break;
                case "02_1_C03_3":
                    strState = ObjectUtil.GetObject(stateItem.State).ToString();
                     blnChange = true;
                    strCarNo = "03";
                    strReadItem = "02_1_C03";
                    strWriteItem = "02_2_C03";
                    break;
                case "02_1_C04_3":
                    strState = ObjectUtil.GetObject(stateItem.State).ToString();
                     blnChange = true;
                    strCarNo = "04";
                    strWriteItem = "02_2_C04";
                    strReadItem = "02_1_C04";
                    break;
            }
            if (!blnChange)
                return;
            CarStateChange(strState, strCarNo, strReadItem, strWriteItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void InsertdtCar(DataTable dt)
        {
            if (dtCar == null)
            {
                dtCar = new DataTable();
                dtCar = dt.Clone();
                DataColumn dcIndex = new DataColumn("Index", Type.GetType("System.Int16"));
                dtCar.Columns.Add(dcIndex);
            }
            if (dt.Rows.Count == 0)
                return;


            //插入

            //调用小车任务插入缓存表

            object[] obj = new object[dt.Columns.Count + 1];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow[] drsExist = dtCar.Select(string.Format("TASK_ID='{0}'", dt.Rows[i]["TASK_ID"]));
                if (drsExist.Length > 0)
                    continue;

                dt.Rows[i].ItemArray.CopyTo(obj, 0);
                obj[dt.Columns.Count] = OrderIndex + i;
                dtCar.Rows.Add(obj);

            }
            dtCar.AcceptChanges();
            DataRow[] drsTask = dtCar.Select(string.Format("TASK_ID='{0}'", dt.Rows[0]["TASK_ID"]));
            if (drsTask.Length > 0)
            {
                DataRow dr = drsTask[0];

                int CurPostion = 0;
                int ToPostion = 0;
                string FromStation = "";
                string ToStation = "";
                string TargetCode = "";
                if (dr["TASK_TYPE"].ToString() == "21")
                {

                    CurPostion = int.Parse(dr["IN_STATION_ADDRESS"].ToString());
                    ToPostion = int.Parse(dr["STATION_NO_ADDRESS"].ToString());
                    FromStation = dr["IN_STATION"].ToString();
                    ToStation = dr["STATION_NO"].ToString();

                }
                else
                {
                    CurPostion = int.Parse(dr["STATION_NO_ADDRESS"].ToString());
                    ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                    ToStation = dr["OUT_STATION_1"].ToString();

                    FromStation = dr["STATION_NO"].ToString();
                }


                TaskDal dal = new TaskDal();

                DataTable dtorder = GetCarOrder(CurPostion);
                DataRow[] drsOrder = dtorder.Select("", "orderNo desc");
                for (int i = 0; i < drsOrder.Length; i++)
                {
                    if (drsOrder[i]["State"].ToString() == "0") //小车空闲
                    {
                        #region 小车空闲
                        if (dr["TASK_TYPE"].ToString() == "22")
                        {
                            ToPostion = -1;

                            //判断二楼能否出库
                            bool blnCan = dal.ProductCanToCar(dr["FORDERBILLNO"].ToString(), dr["FORDER"].ToString(), dr["IS_MIX"].ToString());
                            if (blnCan)
                            {
                                if (!dBillUseTarget.ContainsKey(dr["FORDERBILLNO"].ToString()))
                                {
                                    dBillUseTarget.Add(dr["FORDERBILLNO"].ToString(), false);
                                    dBillTargetCode.Add(dr["FORDERBILLNO"].ToString(), "");
                                }

                                if (dBillUseTarget[dr["FORDERBILLNO"].ToString()]) //已经使用过两次
                                {
                                    if (dBillTargetCode[dr["FORDERBILLNO"].ToString()] == "370")
                                    {
                                        ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                        ToStation = dr["OUT_STATION_1"].ToString();
                                    }
                                    else
                                    {
                                        ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                        ToStation = dr["OUT_STATION_2"].ToString();
                                    }


                                }
                                else
                                {
                                    if (dr["TARGET_CODE"].ToString().Trim() == "01")
                                    {
                                        int objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_370")).ToString());
                                        if (objstate == 0)
                                        {
                                            ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                            ToStation = dr["OUT_STATION_1"].ToString();
                                            TargetCode = "370";
                                        }
                                        else
                                        {
                                            objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_390")).ToString());
                                            if (objstate == 0)
                                            {
                                                ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                                ToStation = dr["OUT_STATION_2"].ToString();
                                                TargetCode = "390";
                                            }

                                        }
                                    }
                                    else
                                    {
                                        int objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_390")).ToString());
                                        if (objstate == 0)
                                        {
                                            ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                            ToStation = dr["OUT_STATION_2"].ToString();
                                            TargetCode = "390";
                                        }
                                        else
                                        {
                                            objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_370")).ToString());
                                            if (objstate == 0)
                                            {
                                                ToStation = dr["OUT_STATION_1"].ToString();
                                                ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                                TargetCode = "370";
                                            }

                                        }
                                    }

                                    if (dBillTargetCode[dr["FORDERBILLNO"].ToString()] != "" && dBillTargetCode[dr["FORDERBILLNO"].ToString()] != TargetCode)
                                    {
                                        dBillUseTarget[dr["FORDERBILLNO"].ToString()] = true;
                                        dBillTargetCode[dr["FORDERBILLNO"].ToString()] = TargetCode;

                                    }
                                    else
                                    {
                                        dBillTargetCode[dr["FORDERBILLNO"].ToString()] = TargetCode;
                                    }
                                }
                            }
                        }
                        if (ToPostion != -1)
                        {

                            int[] WriteValue = new int[2];

                            WriteValue[0] = CurPostion;
                            WriteValue[1] = ToPostion;

                            int TaskNo = int.Parse(dr["TASK_NO"].ToString());

                            int ProductType = int.Parse(dr["PRODUCT_TYPE"].ToString());

                            string barcode = "";
                            string palletcode = "";
                            if (dr["PRODUCT_CODE"].ToString() != "0000") //
                            {
                                barcode = dr["PRODUCT_BARCODE"].ToString();
                                palletcode = dr["PALLET_CODE"].ToString();
                            }

                            sbyte[] b = new sbyte[190];
                            Common.ConvertStringChar.stringToBytes(barcode, 80).CopyTo(b, 0);
                            Common.ConvertStringChar.stringToBytes(palletcode, 110).CopyTo(b, 80);
                            WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_1", TaskNo);//任务号。
                            WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_2", WriteValue);//地址。
                            WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_3", ProductType);//任务号。
                            WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_4", b);
                            WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_5", 1);

                            dr.BeginEdit();
                            dr["CAR_NO"] = drsOrder[i]["CAR_NO"].ToString();
                            dr["STATE"] = 1;
                            dr.EndEdit();

                            dal.UpdateTaskDetailCar(FromStation, ToStation, "1", dr["CAR_NO"].ToString(), string.Format("TASK_ID='{0}' and ITEM_NO='{1}'", dr["TASK_ID"], dr["ITEM_NO"]));

                        }
                        break;
                        #endregion
                    }
                    else
                    {
                        #region 小车不空闲，但是目的地小于当前位置
                        if (int.Parse(drsOrder[i]["ToStation"].ToString()) < CurPostion) //小车不空闲，但是目的地小于当前位置
                        {

                            if (dtCar.Select(string.Format("STATE=0 and CAR_NO='{0}'", drsOrder[i]["CAR_NO"].ToString())).Length == 0) //判断当前小车，是否已经有分配未执行的任务，则给小车分配任务
                            {
                                DataRow[] drs = dtCar.Select(string.Format("TASK_ID='{0}'", dr["TASK_ID"]));
                                //判断二楼能否出库
                                bool blnCan = false;
                                if (drs[0]["TASK_TYPE"].ToString() == "21")
                                    blnCan = true;
                                else
                                    blnCan = dal.ProductCanToCar(drs[0]["FORDERBILLNO"].ToString(), drs[0]["FORDER"].ToString(), drs[0]["IS_MIX"].ToString());
                                if (blnCan)
                                {

                                    drs[0].BeginEdit();
                                    drs[0]["CAR_NO"] = drsOrder[i]["CAR_NO"];
                                    drs[0]["WriteItem"] = drsOrder[i]["WriteItem"];
                                    drs[0].EndEdit();

                                    dr.BeginEdit();
                                    dr["CAR_NO"] = drsOrder[i]["CAR_NO"];
                                    dr.EndEdit();
                                    break;
                                }
                            }
                        }
                        #endregion
                    }


                }
            }
        }
        /// <summary>
        /// 根据当前位置，获取小车顺序。
        /// </summary>
        /// <param name="CurStation"></param>
        /// <returns></returns>
        private DataTable GetCarOrder(int CurStation)
        {
            DataTable dt = new DataTable();
            dt = dtCarOrder.Clone();


            string ReadItem =   "02_1_C01"; //第一辆小车

            InsertCarOrder(dt, "01", "02_2_C01", CurStation, ReadItem);

            ReadItem ="02_1_C02"; //第二辆小车
            InsertCarOrder(dt, "02", "02_2_C02", CurStation, ReadItem);

            ReadItem =  "02_1_C03"; //第三辆小车
            InsertCarOrder(dt, "03", "02_2_C03", CurStation, ReadItem);
            ReadItem = "02_1_C04"; //第四辆小车
            InsertCarOrder(dt, "04", "02_2_C04", CurStation, ReadItem);
            return dt;
        }
        
        private void InsertCarOrder(DataTable dt, string CarNo, string WriteItem, long CurStation, string ReadItem)
        {


            object[] obj1 = ObjectUtil.GetObjects(WriteToService("StockPLC_02", ReadItem + "_1"));//小车任务号， 状态
            object[] obj2 = ObjectUtil.GetObjects(WriteToService("StockPLC_02", ReadItem + "_2"));//小车位置,目标地址


            long Position = long.Parse(obj2[0].ToString()); //小车位置
            int Status = int.Parse(obj1[1].ToString());//
            long DesPosition = long.Parse(obj2[1].ToString());//小车目的地址


            if (Status != 2)//故障
            {
                DataRow dr = dt.NewRow();
                dr["CAR_NO"] = CarNo;
                dr["State"] = Status; //状态
                dr["WriteItem"] = WriteItem;
                dr["CurStation"] = Position;//当前位置
                if (Position <= CurStation)
                    dr["OrderNo"] = Position + 10000; //小车位置小于当前位置，加上最大码尺地址。





                else
                    dr["OrderNo"] = Position;
                dr["ToStation"] = DesPosition; //目的地
                dr["ToStationOrder"] = DesPosition;
                if (DesPosition < 5000)
                    dr["ToStationOrder"] = DesPosition + 10000;//最大码尺地址
                dt.Rows.Add(dr);
            }
        }



        private void CarStateChange(string Objstate, string CarNO, string CarItem, string WriteItem)
        {

            string CarNo = "";
            if (Objstate == "2") //送货完成
            {
                TaskDal dal = new TaskDal();

                #region 送货完成,写入站台
                object[] obj1 = ObjectUtil.GetObjects(WriteToService("StockPLC_02", CarItem + "_1"));//小车任务号，状态
                object[] obj2 = ObjectUtil.GetObjects(WriteToService("StockPLC_02", CarItem+"_2"));//读取小车位置,目标地址

                DataRow[] drsAddress = dtCarAddress.Select(string.Format("CAR_ADDRESS='{0}'", obj2[1].ToString()));
                if (drsAddress.Length > 0)
                {
                    string strStationNo = drsAddress[0]["STATION_NO"].ToString();
                    string strItemName = "";
                    string strItemState = "02_1_" + strStationNo;
                    if (strStationNo == "340" || strStationNo == "360")
                    {
                        strItemName = "StockOutCarFinishProcess";

                    }
                    else
                    {
                        if (strStationNo == "301" || strStationNo == "305" || strStationNo == "309" || strStationNo == "313" || strStationNo == "317" || strStationNo == "323")
                        {
                            strItemName = "PalletToCarStationProcess";
                        }
                    }

                    if (strItemName != "")
                        WriteToProcess(strItemName, strItemState, obj1[0].ToString());
                }

                string FinshedTaskType = "";
                if (dtCar == null)
                    return;

                DataRow[] drexist = dtCar.Select(string.Format("CAR_NO='{0}' and STATE=1", CarNo));//获取小车开始执行完毕之后
                if (drexist.Length > 0)
                {
                    FinshedTaskType = drexist[0]["TASK_TYPE"].ToString();
                    dtCar.Rows.Remove(drexist[0]);
                }
                #endregion

                DataRow[] drs = dtCar.Select(string.Format("CAR_NO='{0}' and STATE=0", CarNo));
                if (drs.Length > 0) //有待分配的任务
                {
                    #region 有待分配的任务
                    DataRow dr = drs[0];
                    dr.BeginEdit();
                    dr["STATE"] = 1;
                    dr.EndEdit();

                    int CurPostion = 0;
                    int ToPostion = 0;
                    string FromStation = "";
                    string ToStation = "";
                    string TargetCode = "";
                    if (dr["TASK_TYPE"].ToString() == "21")
                    {

                        CurPostion = int.Parse(dr["IN_STATION_ADDRESS"].ToString());
                        ToPostion = int.Parse(dr["STATION_NO_ADDRESS"].ToString());
                        FromStation = dr["IN_STATION"].ToString();
                        ToStation = dr["STATION_NO"].ToString();

                    }
                    else
                    {
                        CurPostion = int.Parse(dr["STATION_NO_ADDRESS"].ToString());
                        ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                        FromStation = dr["STATION_NO"].ToString();
                        ToStation = dr["OUT_STATION_1"].ToString();



                        if (!dBillUseTarget.ContainsKey(dr["FORDERBILLNO"].ToString()))
                        {
                            dBillUseTarget.Add(dr["FORDERBILLNO"].ToString(), false);
                            dBillTargetCode.Add(dr["FORDERBILLNO"].ToString(), "");
                        }

                        if (dBillUseTarget[dr["FORDERBILLNO"].ToString()]) //已经使用过两次
                        {
                            if (dBillTargetCode[dr["FORDERBILLNO"].ToString()] == "370")
                            {
                                ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                ToStation = dr["OUT_STATION_1"].ToString();
                            }
                            else
                            {
                                ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                ToStation = dr["OUT_STATION_2"].ToString();
                            }


                        }
                        else
                        {
                            if (dr["TARGET_CODE"].ToString().Trim() == "01")
                            {
                                int objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_370")).ToString());
                                if (objstate == 0)
                                {
                                    ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                    ToStation = dr["OUT_STATION_1"].ToString();
                                    TargetCode = "370";
                                }
                                else
                                {
                                    objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_390")).ToString());
                                    if (objstate == 0)
                                    {
                                        ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                        ToStation = dr["OUT_STATION_2"].ToString();
                                        TargetCode = "390";
                                    }

                                }
                            }
                            else
                            {
                                int objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_390")).ToString());
                                if (objstate == 0)
                                {
                                    ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                    ToStation = dr["OUT_STATION_2"].ToString();
                                    TargetCode = "390";
                                }
                                else
                                {
                                    objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_370")).ToString());
                                    if (objstate == 0)
                                    {
                                        ToStation = dr["OUT_STATION_1"].ToString();
                                        ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                        TargetCode = "370";
                                    }

                                }
                            }

                            if (dBillTargetCode[dr["FORDERBILLNO"].ToString()] != "" && dBillTargetCode[dr["FORDERBILLNO"].ToString()] != TargetCode)
                            {
                                dBillUseTarget[dr["FORDERBILLNO"].ToString()] = true;
                                dBillTargetCode[dr["FORDERBILLNO"].ToString()] = TargetCode;

                            }
                            else
                            {
                                dBillTargetCode[dr["FORDERBILLNO"].ToString()] = TargetCode;
                            }
                        }
                    }
                    int[] WriteValue = new int[2];

                    WriteValue[0] = CurPostion;
                    WriteValue[1] = ToPostion;

                    int TaskNo = int.Parse(dr["TASK_NO"].ToString());

                    int ProductType = int.Parse(dr["PRODUCT_TYPE"].ToString());
                    int[] WriteTaskValue = new int[2];
                    WriteTaskValue[0] = TaskNo;
                    WriteTaskValue[1] = ProductType;

                    string barcode = "";
                    string palletcode = "";
                    if (dr["PRODUCT_CODE"].ToString() != "0000") //
                    {
                        barcode = dr["PRODUCT_BARCODE"].ToString();
                        palletcode = dr["PALLET_CODE"].ToString();
                    }

                    sbyte[] b = new sbyte[190];
                    Common.ConvertStringChar.stringToBytes(barcode, 80).CopyTo(b, 0);
                    Common.ConvertStringChar.stringToBytes(palletcode, 110).CopyTo(b, 80);
                    WriteToService("StockPLC_02", dr["WriteItem"].ToString() + "_1", WriteTaskValue);//任务号。
                    WriteToService("StockPLC_02", dr["WriteItem"].ToString() + "_2", WriteValue);//地址。
                    WriteToService("StockPLC_02", dr["WriteItem"].ToString() + "_3", b);
                    WriteToService("StockPLC_02", dr["WriteItem"].ToString() + "_4", 1);






                    dal.UpdateTaskDetailCar(FromStation, ToStation, "1", dr["CAR_NO"].ToString(), string.Format("TASK_ID='{0}' and ITEM_NO='{1}'", dr["TASK_ID"], dr["ITEM_NO"]));

                    #endregion
                }
                else  //小车空闲，且没任务。
                {
                    #region 小车空闲，且没任务。 按顺序查找任务
                    int CurPostion = 0;
                    int ToPostion = -1;
                    string FromStation = "";
                    string ToStation = "";
                    string TargetCode = "";

                    DataRow[] drsNotCar = dtCar.Select("CAR_NO='' and STATE=0", "Index");
                    if (drsNotCar.Length > 0)
                    {



                        for (int i = 0; i < drs.Length; i++)
                        {

                            DataRow dr = drs[i];

                            if (dr["TASK_TYPE"].ToString() == "21")
                            {

                                CurPostion = int.Parse(dr["IN_STATION_ADDRESS"].ToString());
                                ToPostion = int.Parse(dr["STATION_NO_ADDRESS"].ToString());
                                FromStation = dr["IN_STATION"].ToString();
                                ToStation = dr["STATION_NO"].ToString();

                            }
                            else
                            {
                                CurPostion = int.Parse(dr["STATION_NO_ADDRESS"].ToString());
                                //判断使用哪个出口？

                                ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                ToStation = dr["OUT_STATION_1"].ToString();

                                FromStation = dr["STATION_NO"].ToString();



                                ToPostion = -1;

                                //判断二楼能否出库
                                bool blnCan = false;
                                 
                                blnCan = dal.ProductCanToCar(dr["FORDERBILLNO"].ToString(), dr["FORDE"].ToString(), dr["IS_MIX"].ToString());
                                if (blnCan)
                                {
                                    if (!dBillUseTarget.ContainsKey(dr["FORDERBILLNO"].ToString()))
                                    {
                                        dBillUseTarget.Add(dr["FORDERBILLNO"].ToString(), false);
                                        dBillTargetCode.Add(dr["FORDERBILLNO"].ToString(), "");
                                    }

                                    if (dBillUseTarget[dr["FORDERBILLNO"].ToString()]) //已经使用过两次
                                    {
                                        if (dBillTargetCode[dr["FORDERBILLNO"].ToString()] == "370")
                                        {
                                            ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                            ToStation = dr["OUT_STATION_1"].ToString();
                                        }
                                        else
                                        {
                                            ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                            ToStation = dr["OUT_STATION_2"].ToString();
                                        }


                                    }
                                    else
                                    {
                                        if (dr["TARGET_CODE"].ToString().Trim() == "01")
                                        {
                                            int objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_370")).ToString());
                                            if (objstate == 0)
                                            {
                                                ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                                ToStation = dr["OUT_STATION_1"].ToString();
                                                TargetCode = "370";
                                            }
                                            else
                                            {
                                                objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_390")).ToString());
                                                if (objstate == 0)
                                                {
                                                    ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                                    ToStation = dr["OUT_STATION_2"].ToString();
                                                    TargetCode = "390";
                                                }

                                            }
                                        }
                                        else
                                        {
                                            int objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_390")).ToString());
                                            if (objstate == 0)
                                            {
                                                ToPostion = int.Parse(dr["OUT_STATION_2_ADDRESS"].ToString());
                                                ToStation = dr["OUT_STATION_2"].ToString();
                                                TargetCode = "390";
                                            }
                                            else
                                            {
                                                objstate = int.Parse(ObjectUtil.GetObject(WriteToService("StockPLC_02", "02_1_370")).ToString());
                                                if (objstate == 0)
                                                {
                                                    ToStation = dr["OUT_STATION_1"].ToString();
                                                    ToPostion = int.Parse(dr["OUT_STATION_1_ADDRESS"].ToString());
                                                    TargetCode = "370";
                                                }

                                            }
                                        }

                                        if (dBillTargetCode[dr["FORDERBILLNO"].ToString()] != "" && dBillTargetCode[dr["FORDERBILLNO"].ToString()] != TargetCode)
                                        {
                                            dBillUseTarget[dr["FORDERBILLNO"].ToString()] = true;
                                            dBillTargetCode[dr["FORDERBILLNO"].ToString()] = TargetCode;

                                        }
                                        else
                                        {
                                            dBillTargetCode[dr["FORDERBILLNO"].ToString()] = TargetCode;
                                        }
                                    }

                                }
                            }

                            if (ToPostion != -1)
                            {
                                int[] WriteValue = new int[2];
                               
                                WriteValue[0] = CurPostion;
                                WriteValue[1] = ToPostion;

                                int TaskNo = int.Parse(dr["TASK_NO"].ToString());

                                int ProductType = int.Parse(dr["PRODUCT_TYPE"].ToString());

                                int[] WriteTaskValue = new int[2];
                                WriteTaskValue[0] = TaskNo;
                                WriteTaskValue[1] = ProductType;

                                string barcode = "";
                                string palletcode = "";
                                if (dr["PRODUCT_CODE"].ToString() != "0000") //
                                {
                                    barcode = dr["PRODUCT_BARCODE"].ToString();
                                    palletcode = dr["PALLET_CODE"].ToString();
                                }

                                sbyte[] b = new sbyte[190];
                                Common.ConvertStringChar.stringToBytes(barcode, 80).CopyTo(b, 0);
                                Common.ConvertStringChar.stringToBytes(palletcode, 110).CopyTo(b, 80);
                                WriteToService("StockPLC_02", WriteItem + "_1", WriteTaskValue);//任务号。
                                WriteToService("StockPLC_02", WriteItem + "_2", WriteValue);//地址。
                                WriteToService("StockPLC_02", WriteItem + "_3", b);
                                WriteToService("StockPLC_02", WriteItem + "_4", 1);

                                dal.UpdateTaskDetailCar(FromStation, ToStation, "1", dr["CAR_NO"].ToString(), string.Format("TASK_ID='{0}' and ITEM_NO='{1}'", dr["TASK_ID"], dr["ITEM_NO"]));
                                break;
                            }
                        }


                    }
                    #endregion

                    #region 小车空闲，且找不到任务，则移动到最大目的地址的下一个工位
                    if (ToPostion == -1)
                    {
                        obj2 = ObjectUtil.GetObjects(WriteToService("StockPLC_02", CarItem+"_2"));//读取小车位置
                        //判断当前位
                        DataTable dtOrder = GetCarOrder(int.Parse(obj2[0].ToString()));
                        DataRow[] drMax = dtOrder.Select("state=1", "ToStationOrder desc");
                        //按照最大目的地址倒排。最大目的地址大于当前位置，则下任务给小车移动到最大目的地址+1个工位。
                        if (drMax.Length > 0)
                        {
                            if ((int)drMax[0]["ToStation"] > int.Parse(obj2[1].ToString()))
                            {
                                int[] WriteValue = new int[2];

                                WriteValue[0] = int.Parse(obj2[0].ToString()); ;
                                WriteValue[1] = (int)drMax[0]["ToStation"] + 10;//下任务给小车移动到最大目的地址+1个工位。;

                                int TaskNo = 9999;

                                int ProductType = 5;
                                int[] WriteTaskValue = new int[2];
                                WriteTaskValue[0] = TaskNo;
                                WriteTaskValue[1] = ProductType;

                                string barcode = "";
                                string palletcode = "";

                              


                                sbyte[] b = new sbyte[190];
                                Common.ConvertStringChar.stringToBytes(barcode, 80).CopyTo(b, 0);
                                Common.ConvertStringChar.stringToBytes(palletcode, 110).CopyTo(b, 80);
                                WriteToService("StockPLC_02", WriteItem + "_1", WriteTaskValue);//任务号。
                                WriteToService("StockPLC_02", WriteItem + "_2", WriteValue);//地址。
                                WriteToService("StockPLC_02", WriteItem + "_3", b);
                                WriteToService("StockPLC_02", WriteItem + "_4", 1);

                            }

                        }

                    }
                    #endregion
                }
            }
            else
            {
                #region 烟包接货完成，处理目前位置与目的地之间的空闲小车
                if (Objstate == "1")//
                {
                    object[] obj = ObjectUtil.GetObjects(WriteToService("StockPLC_02", CarItem+"_2"));//读取小车位置
                    //判断当前位置

                    DataTable dtOrder = GetCarOrder(int.Parse(obj[0].ToString()));
                    DataRow[] drMax = dtOrder.Select(string.Format("state=0 and CurStation>={0} and CurStation<={1}", obj[1], obj[2]), "orderNo desc");

                    //按照最大目的地址倒排。最大目的地址大于当前位置，则下任务给小车移动到最大目的地址+1个工位。
                    if (drMax.Length > 0)
                    {
                        for (int i = 0; i < drMax.Length; i++)
                        {
                            int[] WriteValue = new int[2];

                            WriteValue[0] = (int)drMax[i]["CurStation"]; 
                            WriteValue[1] = int.Parse(obj[1].ToString()) + (drMax.Length - i) * 10;//下任务给小车移动到最大目的地址+1个工位。;

                            int TaskNo = 9999;

                            int ProductType = 5;
                            int[] WriteTaskValue = new int[2];
                            WriteTaskValue[0] = TaskNo;
                            WriteTaskValue[1] = ProductType;

                            string barcode = "";
                            string palletcode = "";

                            sbyte[] b = new sbyte[190];
                            Common.ConvertStringChar.stringToBytes(barcode, 80).CopyTo(b, 0);
                            Common.ConvertStringChar.stringToBytes(palletcode, 110).CopyTo(b, 80);
                            WriteToService("StockPLC_02", drMax[i]["WriteItem"].ToString() + "_1", WriteTaskValue);//任务号。
                            WriteToService("StockPLC_02", drMax[i]["WriteItem"].ToString() + "_2", WriteValue);//地址。
                            WriteToService("StockPLC_02", drMax[i]["WriteItem"].ToString() + "_3", b);
                            WriteToService("StockPLC_02", drMax[i]["WriteItem"].ToString() + "_4", 1);

                        }


                    }

                }
                #endregion
            }


        }


    }
}

