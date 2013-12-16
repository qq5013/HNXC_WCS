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
    }
}
