using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.AS.Stocking.Dao;
using THOK.MCP;
using THOK.Util;

namespace THOK.AS.Stocking.StockOutProcess
{
    class SortStateProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string lineCode = stateItem.ItemName.Split("_"[0])[0];
                string channelGroup = stateItem.ItemName.Split("_"[0])[1];
                object obj = ObjectUtil.GetObject(stateItem.State);
                int sortNo = obj != null ? Convert.ToInt32(obj) : 0;

                using (PersistentManager pm = new PersistentManager())
                {
                    SupplyDao supplyDao = new SupplyDao();
                    int sortNo1 = supplyDao.FindSortNoForSupply(lineCode, sortNo.ToString(), channelGroup, Convert.ToInt32(Context.Attributes["SupplyAheadCount-" + lineCode +"-" + channelGroup]));
                    sortNo = sortNo > sortNo1 ? sortNo : sortNo1;
                }

                Dictionary<string, string> parameter = new Dictionary<string, string>();
                parameter.Add("OrderDate", "");
                parameter.Add("BatchNo", "");
                parameter.Add("LineCode", lineCode);
                parameter.Add("ChannelGroup", channelGroup);
                parameter.Add("SortNo", sortNo.ToString());

                WriteToProcess("SupplyRequestProcess", "SupplyRequest", parameter);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
