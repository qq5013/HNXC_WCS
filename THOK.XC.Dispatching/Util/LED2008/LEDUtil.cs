using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace THOK.XC.Dispatching.Util.LED2008
{
    public class LedItem
    {
        public string Name;
        public int Count = 0;
        public override string ToString()
        {
            return string.Format("{0}-{1}",Count>0? Count.ToString().PadLeft(2, ' '):"", Name);
        }
    }

    public class LEDUtil
    {
        private LedCollection leds = new LedCollection();
        public Dictionary<int, string> isActiveLeds = new Dictionary<int, string>();

        public LEDUtil()
        {
            //初始化LED屏            
            leds.DelAllProgram();
        }

        public void Release()
        {
            //释放LED屏资源
            leds = null;
        } 

        public void RefreshStockInLED(DataTable table,string ledNo)
        {
            int cardNum = Convert.ToInt32(ledNo);

            if (!IsOnLineLed(cardNum))
            {
                return;
            }

            int i = 1;
            leds.DelAllProgram();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, row["QUANTITY"].ToString() + "-" + row["CIGARETTENAME"].ToString(),i == 1 ? LED2008.GREEN : LED2008.RED, false);
                    i++;
                }
            }
            else
            {
                leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, "当前已无请求任务！", LED2008.GREEN, false);
            }       
                
            leds.SendToScreen();
        }

        public void RefreshStockInLED( string ledNo,string cigaretteName )
        {
            int cardNum = Convert.ToInt32(ledNo);

            if (!IsOnLineLed(cardNum))
            {
                return;
            }

            int i = 1;
            leds.DelAllProgram();

            leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, "缺烟提醒,请入库：", LED2008.RED , false);
            i++;
            leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, cigaretteName, LED2008.GREEN, false);

            leds.SendToScreen();
        }

        private bool IsOnLineLed(int ledNo)
        {
            if (isActiveLeds.ContainsKey(ledNo))
            {
                Microsoft.VisualBasic.Devices.Network network = new Microsoft.VisualBasic.Devices.Network();
                if (!network.Ping(isActiveLeds[ledNo]))
                {
                    THOK.MCP.Logger.Error(ledNo + "号LED屏故障，请检查！IP:[" + isActiveLeds[ledNo] + "]");
                    return false;
                }
                else
                    return true;
            }
            else 
                return false;
        }

        internal void Show(string ledCode, LedItem[] ledItems)
        {
            int ledno = 0;
            if (int.TryParse(ledCode, out ledno))
            {
                if (IsOnLineLed(ledno))
                {
                    leds.DelAllProgram();

                    int i = 1;
                    if (ledItems.Length > 0)
                    {
                        foreach (LedItem item in ledItems)
                        {
                            leds.AddTextToProgram(ledno, 0, (i - 1) * 16, 16, 128, item.ToString(), i == 1 ? LED2008.GREEN : LED2008.RED, false);
                            i++;
                        }
                    }
                    else
                        leds.AddTextToProgram(ledno, 0, (i - 1) * 16, 16, 128, "当前已无任务！", LED2008.GREEN, false);

                    leds.SendToScreen();
                }
            }
        }
    }
}
