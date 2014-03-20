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
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.TaskOutToDetail();
            }
        }
         /// <summary>
        /// 获取入库的堆垛机信息
        /// </summary>
        /// <returns></returns>
        public DataTable CraneTaskIn(string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.CraneTaskIn(strWhere);
            }
        }
         /// <summary>
        /// 获取出库的堆垛机信息。主表，WCS_TASK，Item_NO=1堆垛机为起始动作
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable CraneTaskOut(string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.CraneTaskOut(strWhere);
            }
        }
        /// <summary>
        /// 更新Task_Detail状态 State
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="State"></param>
        public void UpdateTaskDetailState(string strWhere, string State)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateTaskDetailState(strWhere, State);
            }
        }
       /// <summary>
       /// 更新堆垛机顺序号
       /// </summary>
       /// <param name="TaskID"></param>
       /// <param name="QueueNO"></param>
       /// <param name="ItemNo"></param>
        public void UpdateCraneQuenceNo(string TaskID,string QueueNO,string ItemNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateCraneQuenceNo(TaskID, QueueNO, ItemNo);
            }
        }
        /// <summary>
        /// 更新堆垛机错误编号
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="QueueNO"></param>
        /// <param name="ItemNo"></param>
        public void UpdateCraneErrCode(string TaskID, string ItemNo, string ErrCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateCraneErrCode(TaskID, ItemNo, ErrCode);
            }
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="state"></param>
        public void UpdateTaskState(string TaskID, string state)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateTaskState(TaskID, state);
            }
        }

        /// <summary>
        /// 获取堆垛机最大流水号
        /// </summary>
        /// <returns></returns>
        public string GetMaxSQUENCENO()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.GetMaxSQUENCENO();
            }
        }

      
        /// <summary>
        /// 根据条件，返回小车任务明细。
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskCarDetail(string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.TaskCarDetail(strWhere);
            }
        }

          /// <summary>
        /// 插入明细Task_Detail。
        /// </summary>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public string InsertTaskDetail(string task_id)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.InsertTaskDetail(task_id);
            }
        }
        /// <summary>
        /// 更新起始位置，目标位置
        /// </summary>
        /// <param name="FromStation"></param>
        /// <param name="ToStation"></param>
        /// <param name="strWhere"></param>
        public void UpdateTaskDetailStation(string FromStation, string ToStation, string state, string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateTaskDetailStation(FromStation, ToStation, state, strWhere);
            }
        }

        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCar(string FromStation, string ToStation, string state, string CarNo, string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateTaskDetailCar(FromStation, ToStation, state, CarNo, strWhere);
            }
        }
        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCrane(string FromStation, string ToStation, string state, string CraneNo, string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateTaskDetailCrane(FromStation, ToStation, state, CraneNo, strWhere);
            }
        }

         /// <summary>
        ///  分配货位,返回 0:TaskID，1:货位;
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignCell(string strWhere,string ApplyStation)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.AssignCell(strWhere, ApplyStation);
            }
            
        }

         /// <summary>
        /// 二楼分配货位,更新货位信息，返回 0:TaskID，1:任务号，2:货物到达入库站台的目的地址--平面号,3:堆垛机入库站台，4:货位，5:堆垛机编号,6:小车站台
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignCellTwo(string strWhere) //
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
               return dao.AssignCellTwo(strWhere);
            }
        }
        /// <summary>
        /// 根据任务号返回的任务号TaskID,及单号Bill_NO
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <returns></returns>
        public string[] GetTaskInfo(string TaskNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.GetTaskInfo(TaskNo);
            }
        }

          /// <summary>
        /// 根据Task获取入库，起始位置，目标位置，及堆垛机编号
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInCraneStation(string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.TaskInCraneStation(strWhere);
            }
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInfo(string strWhere)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.TaskInfo(strWhere);
            }
 
        }
        /// <summary>
        /// 小车待出库任务数量
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int CarTaskInfo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.CarTaskInfo();
            }
        }

        /// <summary>
        /// 根据单号，返回任务数量
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public int TaskCount(string BillNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.TaskCount(BillNo);
            }
 
        }
         /// <summary>
        /// 根据任务号，返回产品信息。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable GetProductInfoByTaskID(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.GetProductInfoByTaskID(TaskID);
            }
        }
        /// <summary>
        /// 二楼出库--条码校验出错，记录错误标志，及新条码。
        /// </summary>
        public void UpdateTaskCheckBarCode(string TaskID,string BarCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.UpdateTaskCheckBarCode(TaskID, BarCode);
            }
        }

        /// <summary>
        ///  分配货位,返回 0:TaskID，1:货位 
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignNewCell(string strWhere, string CranNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.AssignCell(strWhere, CranNo);
            }

        }

        /// <summary>
        ///  烟包替换记录
        /// </summary>
        /// <param name="strWhere"></param>
        public void InsertChangeProduct(string ProductBarcode,string ProductCode,string NewProductBarcode,string NewProductCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                dao.InsertChangeProduct(ProductBarcode, ProductCode, NewProductBarcode, NewProductCode);
            }

        }

         /// <summary>
        /// 出库任务排序，判断能否给穿梭车下达出库任务
        /// </summary>
        /// <param name="ForderBillNo"></param>
        /// <param name="Forder"></param>
        /// <param name="IsMix"></param>
        /// <returns></returns>
        public bool ProductCanToCar(string ForderBillNo, string Forder, string IsMix, bool blnCar, bool blnOutOrder)
        {
            if (!blnOutOrder) //不需要排序
            {
                return true;
            }
            else
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    TaskDao dao = new TaskDao();
                    return dao.ProductCanToCar(ForderBillNo, Forder, IsMix, blnCar);
                }
            }
        }

         /// <summary>
        /// 判断二楼出库，任务号到达拆盘处，是否已经执行？
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool SeparateTaskDetailStart(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.SeparateTaskDetailStart(TaskID);
            }
        }
        /// <summary>
        /// 二楼托盘组入库申请，判断是否有排程，小车未接货的任务。
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public  string GetPalletInTask()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                TaskDao dao = new TaskDao();
                return dao.GetPalletInTask();
            }

        }

    }
}
