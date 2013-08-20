using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class SysCarAddressDao : BaseDao
    {
        public DataTable CarAddress()
        {
            string strSQL = "SELECT * FROM SYS_CAR_ADDRESS";
            return ExecuteQuery(strSQL).Tables[0];
        }

    }
}
