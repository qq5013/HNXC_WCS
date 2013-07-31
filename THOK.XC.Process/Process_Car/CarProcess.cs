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
                    string sta = (string)stateItem.State;
                    string CarNo = "";
                    if (sta == "0") //空任务
                    {
                        DataRow[] drexist = dtCar.Select(string.Format("CARNO='{0}' and STATE=1", CarNo));//获取小车开始执行完毕之后
                        if (drexist.Length > 0)
                        {
                            dtCar.Rows.Remove(drexist[0]);
                        }

                        DataRow[] drs = dtCar.Select(string.Format("CARNO='{0}' and STATE=0", CarNo));
                        if (drs.Length > 0) //有待分配的任务
                        {
                            drs[0].BeginEdit();
                            drs[0]["state"] = 1;
                            drs[0].EndEdit();
                            dtCar.AcceptChanges();
                            TaskDal dal = new TaskDal();
                            dal.UpdateTaskDetailState("", "1");
                            WriteToService("", "", "");//直接下任务。
                        }
                        else
                        {
                            object obj = WriteToService("", stateItem.ItemName);//读取小车位置
                            DataTable dtOrder = GetCarOrder(0);
                            //按照最大目的地址倒排。最大目的地址大于当前位置，则下任务给小车移动到最大目的地址+1个工位。
                            WriteToService("", "", "");//直接下任务。
                        }
                    }
                    else
                    {
                        if (sta == "2")//烟包接货完成，处理目前位置与目的地之间的小车
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

            //if (blnAdd) //重新排序
            //{
            //    DataTable dtnew = dtCar.DefaultView.ToTable(true, new string[] { "TASK_TYPE", "TASK_LEVEL", "TASK_DATE", "ISMIX", "SORT_LEVEL", "PRODUCT_CODE" });
            //    dtOrder = new DataTable();
            //    dtOrder = dtnew.Clone();
            //    DataColumn dc = new DataColumn("Index", Type.GetType("System.Int32"));
            //    dtOrder.Columns.Add(dc);

            //    drs = dtOrder.Select("TASK_TYPE=22", "TASK_LEVEL desc,TASK_DATE,ISMIX,SORT_LEVEL,PRODUCT_CODE");
            //    obj = new object[dtOrderCrane.Columns.Count];
            //    for (int i = 0; i < drs.Length; i++)
            //    {
            //        drs[i].ItemArray.CopyTo(obj, 0);
            //        obj[dtOrderCrane.Columns.Count] = i + 1;
            //        dtOrderCrane.Rows.Add(obj);
            //    }
            //}




            DataRow dr = dtCar.Select("")[0];

            while (dr[""].ToString() == "")
            {
                DataTable dtorder = GetCarOrder(int.Parse(dr[""].ToString()));
                DataRow[] drsOrder = dtorder.Select("", "orderNo");
                for (int i = 0; i < drsOrder.Length; i++)
                {
                    if (drsOrder[i][""].ToString() == "0") //小车空闲
                    {
                        

                        WriteToService("", "", "");//下达小车任务。

                        dr.BeginEdit();
                        dr[""] = "";
                        dr.EndEdit();
                        TaskDal dal = new TaskDal();
                        dal.UpdateTaskDetailCar("", "");//更新任务列表小车编号
                        dal.UpdateTaskDetailState("", "");//更新任务类表状态。
                        break;
                    }
                    else
                    {
                        if (int.Parse(drsOrder[i][""].ToString()) < int.Parse(dr[""].ToString())) //小车不空闲，但是目的地小于当前位置
                        {

                            if (dtCar.Select("STATE=0 and ").Length == 0) //判断当前小车，是否已经有分配未执行的任务，则给小车分配任务
                            {
                                TaskDal dal = new TaskDal();
                                dal.UpdateTaskDetailCar("", "");//更新任务列表小车编号
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

                dtCarOrder.Columns.Add(dc1);
                dtCarOrder.Columns.Add(dc2);
                dtCarOrder.Columns.Add(dc3);
                dtCarOrder.Columns.Add(dc4);
                dtCarOrder.Columns.Add(dc5);
            }
            DataTable dt = new DataTable();
            dt = dtCarOrder.Clone();



            object obj = WriteToService("StockPLC_02", "02_1_C01"); //第一辆小车
            //dt插入行，小车当前位置<CurStation 则加上 总长度。

            obj = WriteToService("StockPLC_02", "02_1_C02"); //第二两小车
            //dt插入行，小车当前位置<CurStation 则加上 总长度。

            obj = WriteToService("StockPLC_02", "02_1_C03"); //第二两小车
            //dt插入行，小车当前位置<CurStation 则加上 总长度。

            obj = WriteToService("StockPLC_02", "02_1_C04"); //第二两小车
            //dt插入行，小车当前位置<CurStation 则加上 总长度。
            return dt;
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

        


    }
}

