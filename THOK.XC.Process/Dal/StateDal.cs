using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.XC.Process.Dao;
using THOK.Util;

namespace THOK.XC.Process.Dal
{
    public class StateDal : BaseDal
    {
        #region//扫码状态管理器查询
        /// <summary>
        /// 查询所有扫码状态管理器
        /// </summary>
        /// <returns></returns>
        public DataTable FindScannerListTable()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindScannerListTable();
            }
            
        }

        /// <summary>
        /// 根据扫码状态管理器编号查找相应的ROW_INDEX
        /// </summary>
        /// <param name="stateCode">状态管理器编号</param>
        /// <returns></returns>
        public DataTable FindScannerIndexNoByStateCode(string stateCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindScannerIndexNoByStateCode(stateCode);
            }
        }

        /// <summary>
        /// 根据ROW_INDEX查询扫码器订单的信息
        /// </summary>
        /// <param name="indexNo">流水号</param>
        /// <returns></returns>
        public DataTable FindScannerStateByIndexNo(string indexNo, string viewName)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindScannerStateByIndexNo(indexNo, viewName);
            }
        }
        #endregion

        #region//LED状态管理器查询
        /// <summary>
        /// 查询所有LED状态管理器
        /// </summary>
        /// <returns></returns>
        public DataTable FindLedListTable()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindLedListTable();
            }
        }

        /// <summary>
        /// 根据LED状态管理器编号查找相应的ROW_INDEX和视图
        /// </summary>
        /// <param name="stateCode">状态管理器编号</param>
        /// <returns></returns>
        public DataTable FindLedIndexNoByStateCode(string stateCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindLedIndexNoByStateCode(stateCode);
            }
        }

        /// <summary>
        /// 根据ROW_INDEX查询LED订单的信息
        /// </summary>
        /// <param name="indexNo">流水号</param>
        /// <returns></returns>
        public DataTable FindLedStateByIndexNo(string indexNo, string viewName)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindLedStateByIndexNo(indexNo,viewName);
            }
        }

        #endregion

        #region//订单状态管理器查询

        /// <summary>
        /// 查询所有订单状态管理器信息
        /// </summary>
        /// <returns></returns>
        public DataTable FindOrderListTable()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindOrderListTable();
            }
        }

        /// <summary>
        /// 根据订单状态管理器编号查找相应的ROW_INDEX
        /// </summary>
        /// <param name="stateCode">状态管理器编号</param>
        /// <returns></returns>
        public DataTable FindOrderIndexNoByStateCode(string stateCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindOrderIndexNoByStateCode(stateCode);
            }
        }

        /// <summary>
        /// 根据ROW_INDEX查询订单的信息
        /// </summary>
        /// <param name="indexNo">流水号</param>
        /// <returns></returns>
        public DataTable FindOrderStateByIndexNo(string indexNo, string viewName)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StateDao dao = new StateDao();
                return dao.FindOrderStateByIndexNo(indexNo, viewName);
            }
        }

        #endregion
    }
}
