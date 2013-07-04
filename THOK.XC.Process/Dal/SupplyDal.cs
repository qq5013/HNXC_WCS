using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.XC.Process.Dao;

namespace THOK.XC.Process.Dal
{
    public class SupplyDal : BaseDal
    {
        
        public void Delete()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                dao.Delete();
            }
        }

        public void Insert(DataTable supplyTable)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                dao.Delete();
            }
        }

        public int FindCount()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindCount();
            }
        }



        public DataTable FindSupplyBatch(string lineCode, string sortNo, string channelGroup)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindSupplyBatch(lineCode, sortNo, channelGroup);
            }
        }

        public DataTable FindSupply(string lineCode, string sortNo, string channelGroup)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindSupply(lineCode, sortNo, channelGroup);
            }
        }

        public void UpdateChannelUSED(string lineCode, string sourceChannel, string targetChannel,
                                      string targetChannelGroupNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                dao.UpdateChannelUSED(lineCode, sourceChannel, targetChannel,targetChannelGroupNo);
            }
        }

        public DataTable FindCigarette()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindCigarette();
            }
        }

        public DataTable FindCigarette(string cigaretteCode, string quantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindCigarette(cigaretteCode, quantity);
            }
        }

        public DataTable FindCigaretteAll()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindCigaretteAll();
            }
        }

        public DataTable FindCigaretteAll(string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindCigaretteAll(cigaretteCode);
            }
        }

        public bool Exist(string barcode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.Exist(barcode);
            }
        }

        public int FindSortNoForSupply(string lineCode, string sortNo, string channelGroup, int supplyAheadCount)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindSortNoForSupply(lineCode, sortNo, channelGroup, supplyAheadCount);
            }
        }

        //zys_2011-10-06
        public DataTable FindFirstSupply()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindFirstSupply();
            }
        }

        //zys_2011-10-05
        public DataTable FindNextSupply(string lineCode, string channelGroup, string channelType, int sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao dao = new SupplyDao();
                return dao.FindNextSupply(lineCode, channelGroup, channelType, sortNo);
            }
        }
    }
}
