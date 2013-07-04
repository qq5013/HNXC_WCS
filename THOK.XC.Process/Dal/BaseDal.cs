using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.Dal
{
    public class BaseDal
    {
        public void SetPersistentManager(PersistentManager persistentManager)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BaseDao dao = new BaseDao();
                dao.SetPersistentManager(persistentManager);
            }
        }
    }
}
