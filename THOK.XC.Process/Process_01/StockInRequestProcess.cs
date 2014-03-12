using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class StockInRequestProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {

            /* 
             * 一楼入库货位申请
             */
            try
            {
                string FromStation = "";
                string writeItem = "";

                string ToStation = "";
                string TaskID = "";
                object obj = ObjectUtil.GetObject(stateItem.State);
                if (obj == null || obj.ToString() == "0")
                    return;


                string BarCode = ""; //读取PLC，获得产品编码
                switch (stateItem.ItemName)
                {
                    case "01_1_218_1":
                        FromStation = "210";
                        ToStation = "218";
                        writeItem = "01_2_218_";
                        BarCode = Common.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(WriteToService("StockPLC_01", "01_1_218_2")));
                        break;
                    case "01_1_110_1":
                        FromStation = "101";
                        ToStation = "110";
                        writeItem = "01_2_110_";
                        BarCode = Common.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(WriteToService("StockPLC_01", "01_1_110_2")));
                        break;
                    case "01_1_126_1":
                        FromStation = "124";
                        ToStation = "126";
                        writeItem = "01_2_126_";
                        BarCode = Common.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(WriteToService("StockPLC_01", "01_1_126_2")));
                        break;
                    case "01_1_131": //空托盘组盘入
                        PalletBillDal Billdal = new PalletBillDal();
                        TaskID = Billdal.CreatePalletInBillTask(true); //空托盘组入库单，生成Task.
                        FromStation = "124";
                        ToStation = "131";
                        writeItem = "01_2_131_";
                        break;
                    case "PllateInRequest":
                        break;
                }

                string strWhere = "";
                if (TaskID == "")
                    strWhere = string.Format("PRODUCT_BARCODE='{0}'", BarCode);
                else
                    strWhere = string.Format("TASK_ID='{0}'", TaskID);

                TaskDal dal = new TaskDal();
                string[] CellValue = dal.AssignCell(strWhere, ToStation);//货位申请

                string TaskNo = dal.InsertTaskDetail(CellValue[0]);
                SysStationDal sysDal = new SysStationDal();
                DataTable dt = sysDal.GetSationInfo(CellValue[1], "11","3");

                DataTable dtTask = dal.TaskInfo(string.Format("TASK_ID='{0}'", CellValue[0]));

                dal.UpdateTaskState(CellValue[0], "1");//更新任务开始执行
                ProductStateDal StateDal = new ProductStateDal();
                StateDal.UpdateProductCellCode(CellValue[0], CellValue[1]); //更新Product_State 货位

                dal.UpdateTaskDetailStation(FromStation, ToStation, "2", string.Format("TASK_ID='{0}' AND ITEM_NO=1", CellValue[0])); //更新货位申请起始地址及目标地址。

                int[] ServiceW = new int[3];
                ServiceW[0] = int.Parse(TaskNo); //任务号
                ServiceW[1] = int.Parse(dt.Rows[0]["STATION_NO"].ToString());//目的地址
                if (stateItem.ItemName == "01_1_131")
                    ServiceW[2] = 2;                 //货物类型
                else
                    ServiceW[2] = 1;
                WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务
                if (stateItem.ItemName == "01_1_131")
                {
                    WriteToService("StockPLC_01", writeItem + "2", 1);
                }
                else
                {
                    byte[] b = new byte[110];
                    Common.ConvertStringChar.stringToByte(dtTask.Rows[0]["PALLET_CODE"].ToString(), 110).CopyTo(b, 0);

                    WriteToService("StockPLC_01", writeItem + "2", b); //PLC写入任务
                    WriteToService("StockPLC_01", writeItem + "3", 1); //PLC写入任务

                    //更新RFID
                }
                BillDal Bdal = new BillDal();
                Bdal.UpdateBillMasterStart(dtTask.Rows[0]["BILL_NO"].ToString(), ServiceW[2] == 1 ? true : false);
                dal.UpdateTaskDetailStation(ToStation, dt.Rows[0]["STATION_NO"].ToString(), "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", CellValue[0]));//更新货位到达入库站台，


            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.StockInRequestProcess：" + e.Message);
            }
        }
    }
}
