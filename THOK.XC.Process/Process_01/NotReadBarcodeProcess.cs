using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class NotReadBarcodeProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 一楼入库烟包处理
            */

            try
            {

                string strBadFlag = "";
                int obj = (short)ObjectUtil.GetObject(stateItem.State);
                if (obj == 0)
                    return;
                switch (obj)
                {
                    case 1:
                        strBadFlag = "左边条码无法读取";
                        break;
                    case 2:
                        strBadFlag = "右边条码无法读取";
                        break;
                    case 3:
                        strBadFlag = "两边条码无法读取";
                        break;
                    case 4:
                        strBadFlag = "两边条码不一致";
                        break;
                }
                string strBarCode;
               string[]  strMessage=new string[3];
               strMessage[0] = "3";
               strMessage[1] = strBadFlag;
               while ((strBarCode = FormDialog.ShowDialog(strMessage,null)) != "")
               {
                   this.Stop();
                   WriteToProcess("StockPLC_01", "01_2_124_1", strBarCode); //写入条码
                   WriteToProcess("StockPLC_01", "01_2_124_2", 1);//写入标识。
                   break;
               }
               this.Resume();
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.NotReadBarcodeProcess:" + e.Message);
            }
        }
    }
}
