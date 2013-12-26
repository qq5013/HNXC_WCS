using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;
namespace THOK.XC.Process.Process_02
{
    public class StockOutToUnpackLineProcess : AbstractProcess
    {

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
            * 二层出库到开包线
            *         
           */
           
                object obj = ObjectUtil.GetObject(stateItem.State);
                if (obj == null || obj.ToString() == "0")
                    return;
                try
                {
                    string TaskNo = obj.ToString().PadLeft(4, '0');
                    TaskDal dal = new TaskDal();
                    string[] strValue = dal.GetTaskInfo(TaskNo);
                    string WriteItem = "";
                    string ReadItem = "";
                    string PackLine = "";
                    if (!string.IsNullOrEmpty( strValue[0]))
                    {

                        switch (stateItem.ItemName)
                        {
                            case "02_1_250_1":
                                WriteItem = "02_2_250";
                                ReadItem = "02_1_250_2";
                                PackLine = "制丝1号线";
                                break;
                            case "02_1_251_1":
                                WriteItem = "02_2_251";
                                ReadItem = "02_1_251_2";
                                PackLine = "制丝2号线";
                                break;
                            case "02_1_252_1":
                                WriteItem = "02_2_252";
                                ReadItem = "02_1_252_2";
                                PackLine = "制丝3号线";
                                break;
                            case "02_1_253_1":
                                WriteItem = "02_2_253";
                                ReadItem = "02_1_253_2";
                                PackLine = "制丝1号线";
                                break;
                            case "02_1_254_1":
                                WriteItem = "02_2_254";
                                ReadItem = "02_1_254_2";
                                PackLine = "制丝2号线";
                                break;
                            case "02_1_255_1":
                                WriteItem = "02_2_255";
                                ReadItem = "02_1_255_2";
                                PackLine = "制丝3号线";
                                break;
                        }
                        object objCheck = ObjectUtil.GetObject(WriteToService("StockPLC_02", ReadItem ));
                        if (objCheck.ToString() == "0")
                        {
                            Logger.Error(PackLine + "校验出错，请人工处理。");
                        }
                        dal.UpdateTaskState(strValue[0], "2");

                        BillDal bDal = new BillDal();
                        bDal.UpdateOutBillMasterFinished(strValue[1]);

                        WriteToService("StockPLC_02", WriteItem, 1);


                    }

                }
                catch (Exception e)
                {
                    Logger.Error("THOK.XC.Process.Process_02.StockOutToUnpackLineProcess，原因：" + e.Message);
                }
        }
    }
}
