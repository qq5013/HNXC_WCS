using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class StateDao:BaseDao
    {
        #region//扫码状态管理器查询
        /// <summary>
        /// 查询所有扫码状态管理器
        /// </summary>
        /// <returns></returns>
        public DataTable FindScannerListTable()
        {
            string sql = "SELECT STATECODE,STATECODE + '|' + REMARK AS STATENAME FROM AS_STATEMANAGER_SCANNER";
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据扫码状态管理器编号查找相应的ROW_INDEX
        /// </summary>
        /// <param name="stateCode">状态管理器编号</param>
        /// <returns></returns>
        public DataTable FindScannerIndexNoByStateCode(string stateCode)
        {
            string sql = string.Format("SELECT ROW_INDEX,VIEWNAME FROM dbo.AS_STATEMANAGER_SCANNER WHERE STATECODE='{0}'",stateCode);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据ROW_INDEX查询扫码器订单的信息
        /// </summary>
        /// <param name="indexNo">流水号</param>
        /// <returns></returns>
        public DataTable FindScannerStateByIndexNo(string indexNo,string viewName)
        {
            string sql = string.Format(@"SELECT ROW_INDEX,LINECODE,STOCKOUTID,CIGARETTECODE,CIGARETTENAME,CHANNELCODE,SORTCHANNELCODE,CHANNELNAME,
                            CASE CHANNELTYPE 
                                WHEN '3' THEN '通道机'
                                WHEN '2' THEN '立式机'
                                END CHANNELTYPENAME,
                            CASE 
                                WHEN ROW_INDEX < {0} THEN '已扫描'
                                WHEN ROW_INDEX = {0} THEN '已扫描'
                                WHEN ROW_INDEX > {0} THEN '未扫描'
                                END STATE
                            FROM {1} ",indexNo,viewName);
            return ExecuteQuery(sql).Tables[0];
        } 
       #endregion

        #region//LED状态管理器查询
        /// <summary>
        /// 查询所有LED状态管理器
        /// </summary>
        /// <returns></returns>
        public DataTable FindLedListTable()
        {
            string sql = "SELECT STATECODE,STATECODE + '|' + REMARK AS STATENAME FROM AS_STATEMANAGER_LED";
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据LED状态管理器编号查找相应的ROW_INDEX和视图
        /// </summary>
        /// <param name="stateCode">状态管理器编号</param>
        /// <returns></returns>
        public DataTable FindLedIndexNoByStateCode(string stateCode)
        {
            string sql = string.Format("SELECT ROW_INDEX,VIEWNAME FROM dbo.AS_STATEMANAGER_LED WHERE STATECODE='{0}'", stateCode);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据ROW_INDEX查询LED订单的信息
        /// </summary>
        /// <param name="indexNo">流水号</param>
        /// <returns></returns>
        public DataTable FindLedStateByIndexNo(string indexNo, string viewName)
        {
            string sql = string.Format(@"SELECT ROW_INDEX,LINECODE,STOCKOUTID,CIGARETTECODE,CIGARETTENAME,CHANNELCODE,SORTCHANNELCODE,CHANNELNAME,
                            CASE CHANNELTYPE 
                                WHEN '3' THEN '通道机'
                                WHEN '2' THEN '立式机'
                                END CHANNELTYPENAME,
                            CASE 
                                WHEN ROW_INDEX < {0} THEN '已通过'
                                WHEN ROW_INDEX = {0} THEN '已通过'
                                WHEN ROW_INDEX > {0} THEN '未通过'
                                END STATE
                            FROM {1} ",indexNo,viewName);
            return ExecuteQuery(sql).Tables[0];
        }

        #endregion

        #region//订单状态管理器查询

        /// <summary>
        /// 查询所有订单状态管理器信息
        /// </summary>
        /// <returns></returns>
        public DataTable FindOrderListTable()
        {
            string sql = "SELECT STATECODE,STATECODE + '|' + REMARK AS STATENAME FROM AS_STATEMANAGER_ORDER";
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据订单状态管理器编号查找相应的ROW_INDEX
        /// </summary>
        /// <param name="stateCode">状态管理器编号</param>
        /// <returns></returns>
        public DataTable FindOrderIndexNoByStateCode(string stateCode)
        {
            string sql = string.Format("SELECT ROW_INDEX,VIEWNAME FROM dbo.AS_STATEMANAGER_ORDER WHERE STATECODE='{0}'", stateCode);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据ROW_INDEX查询订单的信息
        /// </summary>
        /// <param name="indexNo">流水号</param>
        /// <returns></returns>
        public DataTable FindOrderStateByIndexNo(string indexNo,string viewName)
        {
            string sql = string.Format(@"SELECT ROW_INDEX,LINECODE,STOCKOUTID,CIGARETTECODE,CIGARETTENAME,CHANNELCODE,SORTCHANNELCODE,CHANNELNAME,
                            CASE CHANNELTYPE 
                                WHEN '3' THEN '通道机'
                                WHEN '2' THEN '立式机'
                                END CHANNELTYPENAME,
                            CASE 
                                WHEN ROW_INDEX < {0} THEN '已下单'
                                WHEN ROW_INDEX = {0} THEN '已下单'
                                WHEN ROW_INDEX > {0} THEN '未下单'
                                END STATE
                            FROM {1} ", indexNo, viewName);
            return ExecuteQuery(sql).Tables[0];
        }

        #endregion
    }
}
