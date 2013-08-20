using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.XC.Process.Dao;
using System.Data;

namespace THOK.XC.Process.Dal
{
    public class SysCarAddressDal : BaseDal
    {
        public DataTable CarAddress()
        {
            SysCarAddressDao dao = new SysCarAddressDao();
            return dao.CarAddress();
        }
    }
}
