using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using THOK.MCP;
using THOK.MCP.View;
using THOK.Util;
using THOK.XC.Process.Dal;

namespace THOK.XC.Dispatching.View
{
    public partial class ButtonArea : ProcessControl
    {
        private int IndexStar = 0;
        public ButtonArea()
        {
            InitializeComponent();
            this.btnStop.Enabled = false;
            this.btnSimulate.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnStop.Enabled)
            {
                MessageBox.Show("先停止出库才能退出系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("您确定要退出备货监控系统吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                THOK.XC.Dispatching.Util.LogFile.DeleteFile();
                Application.Exit();
            }
        }

        private void btnOperate_Click(object sender, EventArgs e)
        {
            try
            {
                THOK.AF.Config config = new THOK.AF.Config();
                THOK.AF.MainFrame mainFrame = new THOK.AF.MainFrame(config);
                mainFrame.Context = Context;
                mainFrame.ShowInTaskbar = false;
                mainFrame.Icon = new Icon(@"./App.ico");
                mainFrame.ShowIcon = true;
                mainFrame.StartPosition = FormStartPosition.CenterScreen;
                mainFrame.WindowState = FormWindowState.Maximized;
                mainFrame.ShowDialog();
            }
            catch (Exception ee)
            {
                Logger.Error("操作作业处理失败，原因：" + ee.Message);
            }
        }

       

       
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;

                TaskDal taskDal = new TaskDal();
                DataTable dt = taskDal.TaskOutToDetail();
                DataTable dt2 = null;
                if (IndexStar == 0)
                {
                    string strWhere = string.Format("TASK_TYPE IN ({0}) AND DETAIL.STATE IN ({1})  AND DETAIL.CRANE_NO IS NOT NULL ", "11,21,12,13,14", "0,1");
                    dt2 = taskDal.TaskCraneDetail(strWhere);
                    strWhere = string.Format("TASK_TYPE IN ({0}) AND DETAIL.STATE IN ({1}) AND DETAIL.CRANE_NO IS NOT NULL ", "22", "1");
                    DataTable dtout = taskDal.TaskCraneDetail(strWhere);
                    dt2.Merge(dtout);
                }
                DataTable[] dtSend = new DataTable[2];
                dtSend[0] = dt;
                dtSend[1] = dt2;
                Context.Processes["CraneProcess"].Start();
                Context.ProcessDispatcher.WriteToProcess("CraneProcess", "StockOutRequest", dtSend);
                IndexStar++;
               
                timer1.Enabled = true;
                timer1.Start();
                timer1.Interval = 3000000;
                timer1.Tick += new EventHandler(timer1_Tick);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Context.Processes["CraneProcess"] != null)
            {
                Context.Processes["CraneProcess"].Suspend();
            }

            SwitchStatus(false);

            this.btnStop.Enabled = false;
            this.btnStart.Enabled = false;
            this.btnSimulate.Enabled = true;
            timer1.Enabled = false;
            timer1.Stop();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "help.chm");
        }

        private void SwitchStatus(bool isStart)
        {
             
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {            
            try
            {
                if (Context.Processes["CraneProcess"] != null)
                {
                    Context.Processes["CraneProcess"].Resume();
                }

                SwitchStatus(false);
                this.btnStop.Enabled = true;
                this.btnStart.Enabled = false;
                this.btnSimulate.Enabled = false;
                timer1.Enabled = true;
                timer1.Start();
            }
            catch (Exception ee)
            {
                Logger.Error("恢复出库任务失败：" + ee.Message);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TaskDal taskDal = new TaskDal();
            DataTable dt = taskDal.TaskOutToDetail();
            DataTable dt2 = null;
            if (IndexStar == 0)
            {
                string strWhere = string.Format("TASK_TYPE IN ({0}) AND DETAIL.STATE IN ({1})  AND DETAIL.CRANE_NO IS NOT NULL ", "11,21,12,13,14", "0,1");
                dt2 = taskDal.TaskCraneDetail(strWhere);
                strWhere = string.Format("TASK_TYPE IN ({0}) AND DETAIL.STATE IN ({1}) AND DETAIL.CRANE_NO IS NOT NULL ", "22", "0,1,2");
                DataTable dtout = taskDal.TaskCraneDetail(strWhere);
                dt2.Merge(dtout);
            }
            DataTable[] dtSend = new DataTable[2];
            dtSend[0] = dt;
            dtSend[1] = dt2;
            Context.Processes["CraneProcess"].Start();
            Context.ProcessDispatcher.WriteToProcess("CraneProcess", "StockOutRequest", dtSend);
            IndexStar++;
        }
        
        /// <summary>
        /// 托盘入库方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPalletIn_Click(object sender, EventArgs e)
        {
             PalletSelect frm = new PalletSelect();

          

             if (frm.ShowDialog() == DialogResult.OK)
             {
                 object obj = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService("StockPLC_01", "01_1_122_1"));
                 if (obj == null || obj.ToString() != "0")
                     return;
                 if (frm.Flag == 1) //单托盘入库
                 {
                     string writeItem = "01_2_122_";
                     int[] ServiceW = new int[3];
                     ServiceW[0] =9999; //任务号
                     ServiceW[1] = 131;//目的地址
                     ServiceW[2] = 4;


                     Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务
                     Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "2", 1); //PLC写入任务
                 }
                 else if (frm.Flag == 2)
                 {
                     PalletBillDal Billdal = new PalletBillDal();
                     string TaskID = Billdal.CreatePalletInBillTask(true); //空托盘组入库单，生成Task.
                     string FromStation = "122";
                     string ToStation = "122";
                     string writeItem = "01_2_122_";

                     string strWhere = string.Format("TASK_ID='{0}'", TaskID);
                     TaskDal dal = new TaskDal();
                     string[] CellValue = dal.AssignCell(strWhere, ToStation);//货位申请

                     string TaskNo = dal.InsertTaskDetail(CellValue[0]);
                     SysStationDal sysDal = new SysStationDal();
                     DataTable dt = sysDal.GetSationInfo(CellValue[1], "11");


                     dal.UpdateTaskState(CellValue[0], "1");//更新任务开始执行
                     ProductStateDal StateDal = new ProductStateDal();
                     StateDal.UpdateProductCellCode(CellValue[0], CellValue[1]); //更新Product_State 货位

                     dal.UpdateTaskDetailStation(FromStation, ToStation, "2", string.Format("TASK_ID='{0}' AND ITEM_NO=1", CellValue[0])); //更新货位申请起始地址及目标地址。
                     int[] ServiceW = new int[3];
                     ServiceW[0] = int.Parse(TaskNo); //任务号
                     ServiceW[1] = int.Parse(dt.Rows[0]["STATION_NO"].ToString());//目的地址
                     ServiceW[2] = 2;

                     Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务
                     Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "2", 1); //PLC写入任务
                     dal.UpdateTaskDetailStation(ToStation, dt.Rows[0]["STATION_NO"].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", CellValue[0]));//更新货位到达入库站台，
                     
                     //更新单据开始
                 
                 }
             }

        }
        /// <summary>
        /// 抽检，补料托盘入库；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpotCheck_Click(object sender, EventArgs e)
        {
            object obj = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService("StockPLC_01", "01_1_195"));
            if (obj == null || obj.ToString() == "0")
                return;
            string strTaskNo = obj.ToString().PadLeft(4, '0');

            string[] str = new string[3];
            if (int.Parse(strTaskNo) >= 9000 && int.Parse(strTaskNo) <= 9299) //补料
                str[0] = "1";
            else if (int.Parse(strTaskNo) >= 9300 && int.Parse(strTaskNo) <= 9499)//抽检
                str[0] = "2";
            
            str[1] = "";
            str[2] = "";
            TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
            string[] strInfo = dal.GetTaskInfo(strTaskNo);
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
                    Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "1", 1); //PLC写入任务
                }
                break;
            }
            
        }
        /// <summary>
        /// 盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckScan_Click(object sender, EventArgs e)
        {
            object obj= ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService("StockPLC_01", "01_1_195"));
            if (obj == null || obj.ToString() == "0")
                return;
            string strTaskNo = obj.ToString().PadLeft(4, '0');

            if ( int.Parse(obj.ToString()) >= 9800 && int.Parse(obj.ToString()) < 9999) //盘点
            {
                string[] str = new string[3];

                str[0] = "6";


                str[1] = "";
                str[2] = "";
                TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
                string[] strInfo = dal.GetTaskInfo(strTaskNo);
                DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));
                DataTable dtProductInfo = dal.GetProductInfoByTaskID(strInfo[0]);
                //线程停止
                string strValue = "";
                while ((strValue = FormDialog.ShowDialog(str, dtProductInfo)) != "")
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                    string writeItem = "01_2_195_";

                    DataTable dtTask = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));

                    DataRow dr = dtTask.Rows[0];
                    SysStationDal sysdal = new SysStationDal();
                    DataTable dtstation = sysdal.GetSationInfo(dr["CELL_CODE"].ToString(), "11");

                    if (strValue != "1")
                    {
                        CellDal celldal = new CellDal();
                        celldal.UpdateCellErrFlag(dr["CELL_CODE"].ToString(), "条码扫描不一致");
                    }


                    int[] ServiceW = new int[3];
                    ServiceW[0] = int.Parse(strInfo[1]); //任务号
                    ServiceW[1] = int.Parse(dtstation.Rows[0]["STATION_NO"].ToString());//目的地址
                    ServiceW[2] = 1;

                    Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务
                    Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "3", 1); //PLC写入任务

                    dal.UpdateTaskDetailStation("195", dtstation.Rows[0]["STATION_NO"].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=3", strInfo[0]));//更新货位到达入库站台，
                    dal.UpdateTaskDetailCrane(dtstation.Rows[0]["STATION_NO"].ToString(), dr["CELL_CODE"].ToString(), "0", dtstation.Rows[0]["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO=4", strInfo[0]));//更新调度堆垛机的其实位置及目标地址。
                    break;
                }
            }
            
        }

        /// <summary>
        /// 条码故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBarcodeScan_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService("StockPLC_01", "01_1_124"));
                if (obj == null || obj.ToString() == "0")
                    return;

                string strBadFlag = "";

                switch (obj.ToString())
                {
                    case "1":
                        strBadFlag = "左边条码无法读取";
                        break;
                    case "2":
                        strBadFlag = "右边条码无法读取";
                        break;
                    case "3":
                        strBadFlag = "两边条码无法读取";
                        break;
                    case "4":
                        strBadFlag = "两边条码不一致";
                        break;
                }
                string strBarCode;
                string[] strMessage = new string[3];
                strMessage[0] = "3";
                strMessage[1] = strBadFlag;
                while ((strBarCode = FormDialog.ShowDialog(strMessage, null)) != "")
                {
                    sbyte[] b = THOK.XC.Process.Common.ConvertStringChar.stringToBytes(strBarCode, 40);
                    Context.ProcessDispatcher.WriteToService("StockPLC_01", "01_2_124_1", b); //写入条码  
                    Context.ProcessDispatcher.WriteToService("StockPLC_01", "01_2_124_2", 1);//写入标识。
                    Context.Processes["NotReadBarcodeProcess"].Resume();
                    break;
                }

            }
            catch (Exception ex)
            {
                Logger.Error("THOK.XC.Process.Process_01.NotReadBarcodeProcess:" + ex.Message);
            }
        }
        /// <summary>
        /// 校验处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerficate_Click(object sender, EventArgs e)
        {
            string ServiceName = "StockPLC_02";
            string[] ItemName = new string[6];
            ItemName[0] = "02_1_304_1";
            ItemName[1] = "02_1_308_1";
            ItemName[2] = "02_1_312_1";
            ItemName[3] = "02_1_316_1";
            ItemName[4] = "02_1_320_1";
            ItemName[5] = "02_1_322_1";
            for (int i = 0; i < ItemName.Length; i++)
            {
                object[] obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(ServiceName, ItemName[i]));
                if (obj[0] == null || obj[0].ToString() == "0")
                    continue;
                if (obj[1].ToString() == "1")
                    continue;

              
                string ReadItem2 = "";

                switch (ItemName[i])
                {
                    case "02_1_304_1":
                        ReadItem2 = "02_1_304_2";
                        break;
                    case "02_1_308_1":
                        ReadItem2 = "02_1_308_2";
                        break;
                    case "02_1_312_1":
                        ReadItem2 = "02_1_312_2";
                        break;
                    case "02_1_316_1":
                        ReadItem2 = "02_1_316_2";
                        break;
                    case "02_1_320_1":
                        ReadItem2 = "02_1_320_2";
                        break;
                    case "02_1_322_1":
                        ReadItem2 = "02_1_322_2";
                        break;

                }

                TaskDal dal = new TaskDal();
                string[] strTask = dal.GetTaskInfo(obj[0].ToString().PadLeft(4, '0'));
                if (!string.IsNullOrEmpty(strTask[0]))
                {
                    string NewPalletCode = THOK.XC.Process.Common.ConvertStringChar.BytesToString((object[])ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("StockPLC_02", ReadItem2)));
                    string[] StationState = new string[2];

                    DataTable dtProductInfo = dal.GetProductInfoByTaskID(strTask[0]);
                    DataTable dtTask = dal.TaskInfo(string.Format("TASK_ID='{0}'", strTask[0]));
                    string CellCode = dtTask.Rows[0]["CELL_CODE"].ToString();
                    string strBillNo = "";
                    string[] strMessage = new string[3];
                    strMessage[0] = "5";
                    strMessage[1] = strTask[0];
                    strMessage[2] = NewPalletCode;
                    ProductStateDal psdal = new ProductStateDal();
                    if (psdal.ExistsPalletCode(dtTask.Rows[0]["PALLET_CODE"].ToString())) //已经产生替代单号
                        continue;
                    while ((strBillNo = FormDialog.ShowDialog(strMessage, dtProductInfo)) != "")
                    {

                        string strNewBillNo = strBillNo;
                        if (string.IsNullOrEmpty(strNewBillNo))
                        {
                            if (strNewBillNo == "1")
                            {
                                StationState[0] = obj[0].ToString();//任务号;
                                StationState[1] = "3";

                                //this.Context.Processes["CraneProcess"].Start();
                              Context.ProcessDispatcher.WriteToProcess("CraneProcess", "StockOutToCarStation", StationState); //更新堆垛机Process 状态为3.
                              THOK.XC.Process.Dal.CellDal Celldal = new CellDal();
                                Celldal.UpdateCellOutFinishUnLock(CellCode);//解除货位锁定

                                psdal.UpdateOutBillNo(strTask[0]); //更新出库单

                                DataTable dtCar = dal.TaskCarDetail(string.Format("WCS_TASK.TASK_ID='{0}' AND ITEM_NO=3", strTask[0])); //获取任务ID
                                Context.ProcessDispatcher.WriteToProcess("CarProcess", "CarOutRequest", dtCar);  //调度小车；
                            }
                            else
                            {
                                BillDal bdal = new BillDal();
                                string strOutTaskID = bdal.CreateCancelBillOutTask(strTask[0], strTask[1], strNewBillNo, dtTask.Rows[0]["PALLET_CODE"].ToString());
                                DataTable dtOutTask = dal.CraneOutTask(string.Format("TASK_ID='{0}'", strOutTaskID));

                                Context.ProcessDispatcher.WriteToProcess("CraneProcess", "CraneInRequest", dtOutTask);
                                int jj = 0;
                                while (jj < 100)  //延迟
                                {
                                    jj++;
                                }
                                StationState[0] = strTask[0];//TaskID;
                                StationState[1] = "4";
                                Context.ProcessDispatcher. WriteToProcess("CraneProcess", "StockOutToCarStation", StationState); //更新堆垛机Process 状态为4.

                            }
                        }

                        break;
                    }
                }
            }

        }
        /// <summary>
        /// 倒库出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveOut_Click(object sender, EventArgs e)
        {
            object obj = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService("StockPLC_01", "01_1_122"));

            if (obj == null || obj.ToString() == "0")
                return;
            string[] str = new string[3];
            str[0] = "4";
            str[1] = "";
            str[2] = "";

            TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
            string[] strInfo = dal.GetTaskInfo(obj.ToString().PadLeft(4, '0'));
            DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));
            DataTable dtProductInfo = dal.GetProductInfoByTaskID(strInfo[0]);
            ; //线程停止
            while (FormDialog.ShowDialog(str, dtProductInfo) != "")
            {
                dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                dal.UpdateTaskState(strInfo[0], "2");

                BillDal billdal = new BillDal();
                billdal.UpdateBillMasterFinished(strInfo[1], "1");

                string writeItem = "01_2_122_";

                int[] ServiceW = new int[3];
                ServiceW[0] = int.Parse(strInfo[1]); //任务号
                ServiceW[1] = 131;//目的地址
                ServiceW[2] = 4;

                Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务

                Context.ProcessDispatcher.WriteToService("StockPLC_01", writeItem + "2", 1); //PLC写入任务
                break;
            }
        }
    }
}
