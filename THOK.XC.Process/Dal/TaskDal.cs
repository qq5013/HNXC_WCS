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
        public DataTable TaskCraneDetail(string strWhere)
        {
            TaskDao dao = new TaskDao();
            return dao.TaskCraneDetail(strWhere);
        }
         /// <summary>
        /// 根据Task获取出库信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable CraneOutTask(string strWhere)
        {
            TaskDao dao = new TaskDao();
            return dao.CraneOutTask(strWhere);
        }
        /// <summary>
        /// 更新Task_Detail状态 State
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="State"></param>
        public void UpdateTaskDetailState(string strWhere, string State)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateTaskDetailState(strWhere, State);
        }
       
        public void UpdateCraneQuenceNo(string TaskID,string QueueNO)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateCraneQuenceNo(TaskID, QueueNO);
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="state"></param>
        public void UpdateTaskState(string TaskID, string state)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateTaskState(TaskID, state);
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

        /// <summary>
        /// 更新 货物到达小车站台 完成标志。 起始地址，目的地址
        /// </summary>
        /// <param name="TaskID"></param>
        public void UpdateStockOutToStationState(string TaskID, string ItemName)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateStockOutToStationState(TaskID, ItemName);

        }
        /// <summary>
        /// 根据条件，返回小车任务明细。
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskCarDetail(string strWhere)
        {
            TaskDao dao = new TaskDao();
            return dao.TaskCarDetail(strWhere);
        }

          /// <summary>
        /// 插入明细Task_Detail。
        /// </summary>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public string InsertTaskDetail(string task_id)
        {
            TaskDao dao = new TaskDao();
            return dao.InsertTaskDetail(task_id);
        }
        /// <summary>
        /// 更新起始位置，目标位置
        /// </summary>
        /// <param name="FromStation"></param>
        /// <param name="ToStation"></param>
        /// <param name="strWhere"></param>
        public void UpdateTaskDetailStation(string FromStation, string ToStation, string state, string strWhere)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateTaskDetailStation(FromStation, ToStation, state, strWhere);
        }

        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCar(string FromStation, string ToStation, string state, string CarNo, string strWhere)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateTaskDetailCar(FromStation, ToStation, state,CarNo, strWhere);
        }
        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCrane(string FromStation, string ToStation, string state, string CraneNo, string strWhere)
        {
            TaskDao dao = new TaskDao();
            dao.UpdateTaskDetailCrane(FromStation, ToStation, state, CraneNo, strWhere);
        }

         /// <summary>
        ///  分配货位,返回 0:TaskID，1:任务号，2:货物到达入库站台的目的地址--平面号,3:堆垛机入库站台，4:货位，5:堆垛机编号
        /// </summary>
        /// <param name="strWhere"></param>
        public string [] AssignCell(string strWhere)
        {
            TaskDao dao = new TaskDao();
            return dao.AssignCell(strWhere);
            
        }

         /// <summary>
        /// 二楼分配货位,更新货位信息，插入Task_Detail明细,更新货位申请完成。
        /// </summary>
        /// <param name="strWhere"></param>
        public void AssignCellTwo(string strWhere) //
        {
            TaskDao dao = new TaskDao();
            dao.AssignCellTwo(strWhere);
        }
        /// <summary>
        /// 根据任务号返回的任务号TaskID,及单号Bill_NO
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <returns></returns>
        public string[] GetTaskInfo(string TaskNo)
        {
            TaskDao dao = new TaskDao();
            return dao.GetTaskInfo(TaskNo);
        }

          /// <summary>
        /// 根据Task获取入库，起始位置，目标位置，及堆垛机编号
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInCraneStation(string strWhere)
        {
            TaskDao dao = new TaskDao();
            return dao.TaskInCraneStation(strWhere);
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInfo(string strWhere)
        {
            TaskDao dao = new TaskDao();
            return dao.TaskInfo(strWhere);
 
        }

    }
}
