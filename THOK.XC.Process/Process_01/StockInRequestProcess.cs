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
            string FromStation = "";
            string writeItem = "";
            string ToStation = "";
            string TaskID = "";
            string strRequest = "";



















            if (strRequest == "0") //申请位为0
                return;
            try
            {
                switch (stateItem.ItemName)
                {
                    case "01_1_218":
                        FromStation = "210";
                        ToStation = "218";
                        writeItem = "01_2_218";
                        break;
                    case "01_1_110":
                        FromStation = "101";
                        ToStation = "110";
                        writeItem = "01_2_110";
                        break;
                    case "01_1_126":
                        FromStation = "124";
                        ToStation = "126";
                        writeItem = "01_2_126";
                        break;
                    case "01_1_131": //空托盘组盘入库单

                        BillDal Billdal = new BillDal();
                        TaskID = Billdal.CreatePalletInBillTaskDetail(); //空托盘组入库单，生成Task.
                        FromStation = "124";
                        ToStation = "131";
                        writeItem = "01_2_131";
                        break;
                    case "PllateInRequest":
                        break;
                }
                string BarCode = ""; //读取PLC，获得产品编码
                string strWhere = "";
                if (TaskID == "")
                    strWhere = string.Format("PRODUCT_BARCODE='{0}'", BarCode);
                else
                    strWhere = string.Format("TASK_ID='{0}'", TaskID);
                TaskDal dal = new TaskDal();
                string[] strValue = dal.AssignCell(strWhere);//货位申请
                dal.InsertTaskDetail(strValue[0]); //插入明细
                dal.UpdateTaskState(strValue[0], "1");//更新任务开始执行
                ProductStateDal StateDal = new ProductStateDal();
                StateDal.UpdateProductCellCode(strValue[0], strValue[4]); //更新Product_State 货位
                dal.UpdateTaskDetailStation(FromStation, ToStation, "2", string.Format("TASK_ID='{0}' AND ITEM_NO=1", strValue[0])); //更新货位申请起始地址及目标地址。







                // 缺少写入Service的内容








                WriteToService("StockPLC_01", writeItem, ""); //PLC写入任务
                dal.UpdateTaskDetailStation(ToStation, strValue[2], "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", strValue[0]));//更新货位到达入库站台，
                dal.UpdateTaskDetailCrane(strValue[3], "30" + strValue[4], "0", strValue[5], string.Format("TASK_ID='{0}' AND ITEM_NO=3", strValue[0]));//更新调度堆垛机的其实位置及目标地址。

            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
