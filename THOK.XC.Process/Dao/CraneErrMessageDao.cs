using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class CraneErrMessageDao : BaseDao
    {
         /// <summary>
        /// 返回堆垛机错误列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetErrMessageList()
        {
            string strSQL = "SELECT * FROM SYS_ERROR_CODE";
            return ExecuteQuery(strSQL).Tables[0];
        }
    }
}