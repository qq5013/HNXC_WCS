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
                DataTable dtTask = dal.TaskInfo("TASK_TYPE='22' AND STATE IN (0,1)");

                if (PalletCount >= 5 || dtTask.Rows.Count <= 4)
                {
                    PalletBillDal Billdal = new PalletBillDal();
                    TaskID = Billdal.CreatePalletInBillTask(false);


                    string strWhere = string.Format("TASK_ID='{0}'", TaskID);
                    string[] strValue = dal.AssignCellTwo(strWhere);//货位申请

                    dal.UpdateTaskState(strValue[0], "1");//更新任务开始执行
                    ProductStateDal StateDal = new ProductStateDal();
                    StateDal.UpdateProductCellCode(strValue[0], strValue[4]); //更新Product_State 货位
                    dal.UpdateTaskDetailStation("357", "359", "2", string.Format("TASK_ID='{0}' AND ITEM_NO=1", strValue[0])); //更新货位申请起始地址及目标地址。


                    //dal.UpdateTaskDetailStation("359", strValue[6], "1", string.Format("TASK_ID='{0}' AND ITEM_NO=2", strValue[0]));//更新货位到达入库站台，
                    //dal.UpdateTaskDetailStation(strValue[6], strValue[2], "0", string.Format("TASK_ID='{0}' AND ITEM_NO=3", strValue[0]));//更新货位到达入库站台，
                    //dal.UpdateTaskDetailCrane(strValue[3], "30" + strValue[4], "0", strValue[5], string.Format("TASK_ID='{0}' AND ITEM_NO=4", strValue[0]));//更新调度堆垛机的其实位置及目标地址。



                    strWhere = string.Format("WCS_TASK.TASK_ID='{0}' AND ITEM_NO=2", TaskID);
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
