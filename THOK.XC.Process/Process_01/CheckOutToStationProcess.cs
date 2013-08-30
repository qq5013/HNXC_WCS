using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class CheckOutToStationProcess : AbstractProcess
    {
           /*  处理事项：
            *  抽签烟包到达，195
            */
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            object sta = ObjectUtil.GetObject(stateItem.State);

            TaskDal dal = new TaskDal(); //更具任务号，获取TaskID及BILL_NO
            string[] strInfo = dal.GetTaskInfo(sta.ToString());
            dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
            DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strInfo[0]));
           
        }
    }
}

