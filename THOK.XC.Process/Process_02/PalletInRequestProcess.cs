using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class PalletInRequestProcess : AbstractProcess
    {
     
        private string TaskID = "";
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二楼空托盘组入库
             * 生成入库单，入库作业。
            */
            try
            {
                object[] obj = ObjectUtil.GetObjects(stateItem.State);
                if (obj[0] == null || obj[0].ToString() == "0")
                    return;


                int PalletCount = int.Parse(obj[1].ToString());

             

                //判断是否还有出库任务
                TaskDal dal = new TaskDal();
                int TaskOutCount = dal.CarTaskInfo();

                if (PalletCount >= 4 || TaskOutCount <= 4)
                {
                    PalletBillDal Billdal = new PalletBillDal();
                    TaskID = Billdal.CreatePalletInBillTask(false);


                    string strWhere = string.Format("TASK_ID='{0}'", TaskID);
                    string[] CellValue = dal.AssignCellTwo(strWhere);//货位申请
                    string TaskNo = dal.InsertTaskDetail(CellValue[0]);

                    dal.UpdateTaskState(CellValue[0], "1");//更新任务开始执行

                    ProductStateDal StateDal = new ProductStateDal();
                    StateDal.UpdateProductCellCode(CellValue[0], CellValue[1]); //更新Product_State 货位
                    dal.UpdateTaskDetailStation("357", "359", "2", string.Format("TASK_ID='{0}' AND ITEM_NO=1", CellValue[0])); //更新货位申请起始地址及目标地址。



                    strWhere = string.Format("WCS_TASK.TASK_ID='{0}' AND ITEM_NO=2 AND DETAIL.STATE=0 ", TaskID);
                    DataTable dt = dal.TaskCarDetail(strWhere);
                    WriteToProcess("CarProcess", "CarInRequest", dt);
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.PalletInRequestProcess：" + e.Message);
            }
        }

      
    }
}
