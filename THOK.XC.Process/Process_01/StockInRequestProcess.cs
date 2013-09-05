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
                object[] o = ObjectUtil.GetObjects(stateItem.State);
                int intRequest = (short)o[0];
                string BarCode = ""; //读取PLC，获得产品编码
                if (intRequest != 1) //申请位为0
                    return;

                switch (stateItem.ItemName)
                {
                    case "01_1_218_1":
                        FromStation = "210";
                        ToStation = "218";
                        writeItem = "01_2_218_";
                        BarCode = (string)WriteToService("StockPLC_01", "01_1_218_2");
                        break;
                    case "01_1_110_1":
                        FromStation = "101";
                        ToStation = "110";
                        writeItem = "01_2_110_";
                        BarCode = (string)WriteToService("StockPLC_01", "01_1_110_2");
                        break;
                    case "01_1_126_1":
                        FromStation = "124";
                        ToStation = "126";
                        writeItem = "01_2_126_";
                        BarCode = (string)WriteToService("StockPLC_01", "01_1_126_2");
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
                string[] strValue = dal.AssignCell(strWhere, ToStation);//货位申请

                dal.UpdateTaskState(strValue[0], "1");//更新任务开始执行
                ProductStateDal StateDal = new ProductStateDal();
                StateDal.UpdateProductCellCode(strValue[0], strValue[4]); //更新Product_State 货位
                dal.UpdateTaskDetailStation(FromStation, ToStation, "2", string.Format("TASK_ID='{0}' AND ITEM_NO=1", strValue[0])); //更新货位申请起始地址及目标地址。

                int[] ServiceW = new int[2];
                ServiceW[0] = int.Parse(strValue[1]); //任务号
                ServiceW[1] = int.Parse(strValue[2]);//目的地址
                //if (stateItem.ItemName == "01_1_131")
                //    ServiceW[2] = 2;                 //货物类型
                //else
                //    ServiceW[2] = 1;
                WriteToService("StockPLC_01", writeItem + "1", ServiceW); //PLC写入任务
                if (stateItem.ItemName == "01_1_131")
                {
                    WriteToService("StockPLC_01", writeItem + "2", 1);
                }
                else
                {
                    WriteToService("StockPLC_01", writeItem + "2", BarCode); //PLC写入任务
                    WriteToService("StockPLC_01", writeItem + "3", 1); //PLC写入任务
                }
                dal.UpdateTaskDetailStation(ToStation, strValue[2], "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", strValue[0]));//更新货位到达入库站台，
                dal.UpdateTaskDetailCrane(strValue[3], "30" + strValue[4], "0", strValue[5], string.Format("TASK_ID='{0}' AND ITEM_NO=3", strValue[0]));//更新调度堆垛机的其实位置及目标地址。

            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.StockInRequestProcess：" + e.Message);
            }
        }
    }
}
