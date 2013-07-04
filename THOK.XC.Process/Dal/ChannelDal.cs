using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.XC.Process.Dao;
using THOK.Util;

namespace THOK.XC.Process.Dal
{
    public class ChannelDal : BaseDal
    {
        public void Delete()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                channelDao.Delete();
            }
        }

        public void InsertChannel(DataTable channelTable)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                channelDao.InsertChannel(channelTable);
            }
        }

        public void InsertChannelUSED(DataTable channelTable)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                channelDao.InsertChannelUSED(channelTable);
            }
        }

        public void InsertMixChannel(DataTable mixTable)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                channelDao.InsertMixChannel(mixTable);
            }
        }

        public DataTable FindAll()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindAll();
            }
        }

        public string FindLed(string channelCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindLed(channelCode);
            }
        }

        public DataTable FindChannelForCigaretteCode(string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelForCigaretteCode(cigaretteCode);
            }
        }

        #region ½»»»·Ö¼ðÑÌµÀ      
        
        public DataTable GetChannelUSED(string lineCode, string channelCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelUSED(lineCode, channelCode);
            }
        }

        public DataTable GetEmptyChannelUSED(string lineCode, string channelCode, int channelGroup, string channelType)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindEmptyChannel(lineCode,channelCode, channelGroup, channelType);
            }
        }

        public void ExechangeChannelUSED(string lineCode, string sourceChannel, string targetChannel)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                SupplyDao supplyDao = new SupplyDao();
                try
                {
                    pm.BeginTransaction();

                    DataTable channelTableSource = channelDao.FindChannelUSED(lineCode, sourceChannel);
                    DataTable channelTableTarget = channelDao.FindChannelUSED(lineCode, targetChannel);

                    channelDao.UpdateChannelUSED(lineCode, targetChannel,
                        channelTableSource.Rows[0]["CIGARETTECODE"].ToString(),
                        channelTableSource.Rows[0]["CIGARETTENAME"].ToString(),
                        Convert.ToInt32(channelTableSource.Rows[0]["QUANTITY"]),
                        channelTableSource.Rows[0]["SORTNO"].ToString());

                    channelDao.UpdateChannelUSED(lineCode, sourceChannel,
                        channelTableTarget.Rows[0]["CIGARETTECODE"].ToString(),
                        channelTableTarget.Rows[0]["CIGARETTENAME"].ToString(),
                        Convert.ToInt32(channelTableTarget.Rows[0]["QUANTITY"]),
                        channelTableTarget.Rows[0]["SORTNO"].ToString());

                    supplyDao.UpdateChannelUSED(lineCode, sourceChannel, "0000", channelTableTarget.Rows[0]["GROUPNO"].ToString());
                    supplyDao.UpdateChannelUSED(lineCode, targetChannel, sourceChannel, channelTableSource.Rows[0]["GROUPNO"].ToString());
                    supplyDao.UpdateChannelUSED(lineCode, "0000", targetChannel, channelTableTarget.Rows[0]["GROUPNO"].ToString());

                    pm.Commit();
                }
                catch
                {
                    pm.Rollback();
                }
            }
        }

        public DataTable GetChannelUSED()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelUSED();
            }
        }

        #endregion
    }
}
