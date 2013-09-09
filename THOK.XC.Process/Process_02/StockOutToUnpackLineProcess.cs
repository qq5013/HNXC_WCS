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

            string[] StationState = new string[2];
            string cigaretteCode = "";
            try
            {
               
            }
            catch (Exception e)
            {
                Logger.Error("入库任务请求批次生成处理失败，原因：" + e.Message);
            }
        }

        private DataTable dtCarInfo(string str)
        {
            //根据位置获取小车信息，位置小于当前位置的，加上总长度。按照 位置 desc 排序
            return null;
        }

        private string GetCarNo()
        {
            DataTable dt = dtCarInfo("");//已经按照位置顺序排序
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool blnB = false;//true：忙碌，false：空闲
                int Curd=0;//当前申请小车位置
                int intd;//位置
                if (blnB)
                {
                    intd = 0;//目的地
                }
                else
                {
                    intd = 0;//当前位置
                }
                if (Math.Abs(Curd - intd) > 100)
                {
                    //获取当前小车
                    break;
                }

                
                 
            }
            return "";
        }

        
    }
}
