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
                    if (strValue[0] != "")
                    {

                        switch (stateItem.ItemName)
                        {
                            case "02_1_250":
                                WriteItem = "02_2_250";
                                break;
                            case "02_1_251":
                                WriteItem = "02_2_251";
                                break;
                            case "02_1_252":
                                WriteItem = "02_2_252";
                                break;
                            case "02_1_253":
                                WriteItem = "02_2_253";
                                break;
                            case "02_1_254":
                                WriteItem = "02_2_254";
                                break;
                            case "02_1_255":
                                WriteItem = "02_2_255";
                                break;
                        }

                        dal.UpdateTaskState(strValue[0], "2");

                        BillDal bDal = new BillDal();
                        bDal.UpdateBillMasterFinished(strValue[1], "1");

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
