using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.XC.Process.Dao;
using System.Data;
namespace THOK.XC.Process.Dal
{
    public class TaskDal : BaseDal
    {
        public DataTable TaskOutToDetail()
        {
            TaskDao dao = new TaskDao();
            return dao.TaskOutToDetail();
        }
         /// <summary>
        /// 系统重新启动时，获取正在出库，或者出库完成的Task_Detail
        /// </summary>
        /// <returns></returns>
        public DataTable TaskCraneDetail(string TaskType, string ItemNo, string state)
        {
            TaskDao dao = new TaskDao();
            return dao.TaskCraneDetail(TaskType, ItemNo, state);
        }

        public void UpdateCraneFinshedState(string TaskID, string TaskType, string ItemNo)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateCraneFinshedState(TaskID, TaskType, ItemNo);
        }
        public void UpdateCraneStarState(string TaskID, string ItemNo)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateCraneStarState(TaskID, ItemNo);
        }
        /// <summary>
        /// 获取堆垛机最大流水号
        /// </summary>
        /// <returns></returns>
        public string GetMaxSQUENCENO()
        {
            TaskDao dao = new TaskDao();
            return dao.GetMaxSQUENCENO();
        }
      
    }
}
