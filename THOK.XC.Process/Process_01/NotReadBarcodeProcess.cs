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

                View.ReadBarcode frm = new View.ReadBarcode();
                frm.strBadFlag = strBadFlag;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    WriteToProcess("StockPLC_01", "01_2_124_1", frm.strBarCode); //写入条码
                    WriteToProcess("StockPLC_01", "01_2_124_2", 1);//写入标识。
                }
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }
    }
}
