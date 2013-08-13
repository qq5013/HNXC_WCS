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
        private DataTable dtOrder;
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            switch (stateItem.ItemName)
            {
                case "CarOutRequest":
                    InsertdtCar((DataTable)stateItem.State);
                    break;
                case "CarInRequest":

                    InsertdtCar((DataTable)stateItem.State);
                    break;
                case "02_1_C01":
                case "02_1_C02":
                case "02_1_C03":
                case "02_1_C04":

                    
                    break;
                  
             
                    
            }
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
            }

           //插入

         //调用小车任务插入缓存表
          
            object[] obj = new object[dt.Columns.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow[] drsExist = dtCar.Select(string.Format("TASK_ID='{0}'", dt.Rows[i]["TASK_ID"]));
                if (drsExist.Length > 0)
                    continue;

                dt.Rows[i].ItemArray.CopyTo(obj, 0);
                dtCar.Rows.Add(obj);
              
            }
            dtCar.AcceptChanges();
            DataRow dr = dt.Rows[0];
            int CurPostion = 0;
            int ToPostion = 0;
            string FromStation = "";
            string ToStation = "";
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
                ToStation = dr["IN_STATION"].ToString();
                FromStation = dr["OUT_STATION_1"].ToString();
            }

            while (dr["CAR_NO"].ToString() == "")
            {
                DataTable dtorder = GetCarOrder(CurPostion);
                DataRow[] drsOrder = dtorder.Select("", "orderNo desc");
                for (int i = 0; i < drsOrder.Length; i++)
                {
                    if (drsOrder[i]["State"].ToString() == "0") //小车空闲
                    {

                        int[] WriteValue = new int[4];
                        WriteValue[0] = int.Parse(dr["TASK_NO"].ToString());
                        WriteValue[1] = CurPostion;
                        WriteValue[2] = ToPostion;
                        WriteValue[3] = int.Parse(dr["PRODUCT_TYPE"].ToString());
                        WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_1", WriteValue);//下达小车任务。
                        //条码及RFID








                        string BarCode="";
                        WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_2", BarCode);
                        WriteToService("StockPLC_02", drsOrder[i]["WriteItem"].ToString() + "_3", 1);

                        dr.BeginEdit();
                        dr["CARNO"] = drsOrder[i]["CARNO"].ToString();
                        dr.EndEdit();
                        TaskDal dal = new TaskDal();
                        dal.UpdateTaskDetailCar(FromStation, ToStation, "1", dr["CARNO"].ToString(), string.Format("TASK_ID='{0}' and ITEM_NO='{1}'", dr["TASK_ID"], dr["ITEM_NO"]));
                        break;
                    }
                    else
                    {
                        if (int.Parse(drsOrder[i]["ToStation"].ToString()) < CurPostion) //小车不空闲，但是目的地小于当前位置
                        {

                            if (dtCar.Select(string.Format("STATE=0 and CarNo='{0}'", drsOrder[i]["CarNo"].ToString())).Length == 0) //判断当前小车，是否已经有分配未执行的任务，则给小车分配任务
                            {
                                DataRow[] drs = dtCar.Select(string.Format("TASK_ID='{0}'",dr["TASK_ID"]));
                                drs[0].BeginEdit();
                                drs[0]["CARNO"] = drsOrder[i]["CarNo"];
                                drs[0]["WriteItem"] = drsOrder[i]["WriteItem"];
                                drs[0].EndEdit();
                              
                                dr.BeginEdit();
                                dr["CARNO"] = drsOrder[i]["CarNo"];
                                dr.EndEdit();
                                break;
                            }
                        }
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
            if (dtCarOrder == null)
            {
                dtCarOrder = new DataTable();
                DataColumn dc1 = new DataColumn("CarNo", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("State", Type.GetType("System.String"));
                DataColumn dc3 = new DataColumn("CurStation", Type.GetType("System.Int16"));
                DataColumn dc4 = new DataColumn("OrderNo", Type.GetType("System.Int16"));
                DataColumn dc5 = new DataColumn("ToStation", Type.GetType("System.Int16"));
                DataColumn dc6 = new DataColumn("WriteItem", Type.GetType("System.String"));

                dtCarOrder.Columns.Add(dc1);
                dtCarOrder.Columns.Add(dc2);
                dtCarOrder.Columns.Add(dc3);
                dtCarOrder.Columns.Add(dc4);
                dtCarOrder.Columns.Add(dc5);
            }
            DataTable dt = new DataTable();
            dt = dtCarOrder.Clone();



            int[] obj =(int []) WriteToService("StockPLC_02", "02_1_C01"); //第一辆小车

            InsertCarOrder(dt, "01","02_2_C01", CurStation, obj);
           
            obj = (int[])WriteToService("StockPLC_02", "02_1_C02"); //第二两小车
            InsertCarOrder(dt, "02","02_2_C02", CurStation, obj);

            obj = (int[])WriteToService("StockPLC_02", "02_1_C03"); //第二两小车
            InsertCarOrder(dt, "03", "02_2_C03",CurStation, obj);
            obj = (int[])WriteToService("StockPLC_02", "02_1_C04"); //第二两小车
            InsertCarOrder(dt, "04","02_2_C04", CurStation, obj);
            return dt;
        }
        private void InsertCarOrder(DataTable dt, string CarNo, string WriteItem, int CurStation,  int[] obj)
        {
            DataRow dr = dt.NewRow();
            dr["CarNo"] = CarNo;
            dr["State"] = obj[0];
            dr["WriteItem"] = WriteItem;
            dr["CurStation"] = obj[1];
            if (obj[1] <= CurStation)
                dr["OrderNo"] = obj[1] + 10000; //小车位置小于当前位置，加上最大码尺地址。





            else
                dr["OrderNo"] = obj[1];
            dr["ToStation"] = obj[2];
            dt.Rows.Add(dr);
        }

        private bool CanProductToCar(DataRow drTaskID)
        {
            bool blnvalue = false;
            DataRow[] drs = dtCar.Select(string.Format("BILL_NO='{0}' and PRODUCT_CODE='{1}' and IS_MIX='{2}' and STATE=1", drTaskID["BILL_NO"], drTaskID["PRODUCT_CODE"], drTaskID["IS_MIX"]));   //判断当前单号，当前产品，当前形态是否有state=1的出库任务，有则返回true;
            if (drs.Length > 0)
            {
                blnvalue = true;
            }
            else
            {

                drs = dtOrder.Select(string.Format("TASK_LEVEL={0} and TASK_DATE={1} and ISMIX={2} and SORT_LEVEL={3} and PRODUCT_CODE={4}", new object[] { drTaskID["TASK_LEVEL"], drTaskID["TASK_DATE"], drTaskID["ISMIX"], drTaskID["SORT_LEVEL"], drTaskID["PRODUCT_CODE"] }));
                if (drs.Length > 0)
                {
                    drs = dtOrder.Select(string.Format("Index<{0}", drs[0]["Index"]));
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs = dtOrder.Select(string.Format("TASK_LEVEL={0} and TASK_DATE={1} and ISMIX={2} and SORT_LEVEL={3} and PRODUCT_CODE={4} and TASK_TYPE='22' and STATE in (0,1,2)", new object[] { drTaskID["TASK_LEVEL"], drTaskID["TASK_DATE"], drTaskID["ISMIX"], drTaskID["SORT_LEVEL"], drTaskID["PRODUCT_CODE"] }));//判断小于当前Index的出库任务，是否有未完成的出库任务，如果没有，则返回True.
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

        private void CarStateChange(string Objstate,string CarNO,string CarItem)
        {
            
            string CarNo = "";
            if (Objstate == "0") //空任务
            {
                DataRow[] drexist = dtCar.Select(string.Format("CARNO='{0}' and STATE=1", CarNo));//获取小车开始执行完毕之后
                if (drexist.Length > 0)
                {
                    dtCar.Rows.Remove(drexist[0]);
                }

                DataRow[] drs = dtCar.Select(string.Format("CARNO='{0}' and STATE=0", CarNo));
                if (drs.Length > 0) //有待分配的任务
                {
                    DataRow dr = drs[0];
                    dr.BeginEdit();
                    dr["state"] = 1;
                    dr.EndEdit();
                   
                    int CurPostion = 0;
                    int ToPostion = 0;
                    string FromStation = "";
                    string ToStation = "";
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
                        FromStation = dr["STATION_NO"].ToString();
                        ToStation = dr["OUT_STATION_1"].ToString();
                       
                    }


                    int[] WriteValue = new int[4];
                    WriteValue[0] = int.Parse(dr["TASK_NO"].ToString());
                    WriteValue[1] = CurPostion;
                    WriteValue[2] = ToPostion;
                    WriteValue[3] = int.Parse(dr["PRODUCT_TYPE"].ToString());
                    WriteToService("StockPLC_02", dr["WriteItem"].ToString() + "_1", WriteValue);//下达小车任务。
                    //条码及RFID








                    string BarCode = "";
                    WriteToService("StockPLC_02", dr["WriteItem"].ToString() + "_2", BarCode);
                    WriteToService("StockPLC_02", dr["WriteItem"].ToString() + "_3", 1);


                    TaskDal dal = new TaskDal();
                    dal.UpdateTaskDetailCar(FromStation, ToStation, "1", dr["CARNO"].ToString(), string.Format("TASK_ID='{0}' and ITEM_NO='{1}'", dr["TASK_ID"], dr["ITEM_NO"]));

                }
                else
                {
                    object obj = WriteToService("StockPLC_02", CarItem);//读取小车位置

                    DataTable dtOrder = GetCarOrder(0);
                    //按照最大目的地址倒排。最大目的地址大于当前位置，则下任务给小车移动到最大目的地址+1个工位。
                    WriteToService("", "", "");//直接下任务。
                }
            }
            else
            {
                if (Objstate == "2")//烟包接货完成，处理目前位置与目的地之间的小车
                {
                    DataTable dtOrder = GetCarOrder(0);
                    for (int i = 0; i < dtOrder.Rows.Count; i++)
                    {
                        if (true)//小车空闲，且位置在当前位置与目标位置区间
                        {
                            WriteToService("", "", ""); //下达空车跑任务。
                        }
                    }
                }
            }
 
        }


    }
}

