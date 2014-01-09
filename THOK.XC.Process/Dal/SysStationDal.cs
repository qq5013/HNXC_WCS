using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.XC.Process.Dao;
using System.Data;

namespace THOK.XC.Process.Dal
{
    public class SysStationDal:BaseDal
    { 
        /// <summary>
        ///根据货位信息，任务类别， 获取站台信息
        /// </summary>
        /// <param name="CellCode"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataTable GetSationInfo(string CellCode, string TaskType,string Item)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SysStationDao dao = new SysStationDao();
                return dao.GetSationInfo(CellCode, TaskType,Item);
            }
        }

        /// <summary>
        /// 获取小车出入库位置
        /// </summary>
        /// <param name="CellCode"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public DataTable GetCarSationInfo(string CellCode, string TaskType)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SysStationDao dao = new SysStationDao();
                return dao.GetCarSationInfo(CellCode, TaskType);
            }
        }

         /// <summary>
        /// 根据类型返回任务号
        /// </summary>
        /// <param name="Module"></param>
        /// <returns></returns>
        public string GetTaskNo(string Module)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SysStationDao dao = new SysStationDao();
                return dao.GetTaskNo(Module);
            }
        }
           /// <summary>
        /// 堆垛机流水号报错，重置0；
        /// </summary>
        public void ResetSQueNo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SysStationDao dao = new SysStationDao();
                dao.ResetSQueNo();
            }
        }
    }
}
