using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace THOK.XC.Dispatching.Util.LED2008
{
    public class LedCollection
    {
        private Dictionary<int, string> Leds = new Dictionary<int, string>();
        private Dictionary<int, int> Programs = new Dictionary<int, int>();

        ~LedCollection()
        {
            foreach (int CardNum in Leds.Keys)
            {
                if (Leds[CardNum] == "OPEN")
                    LED2008.User_CloseScreen(CardNum);
            }
            Leds.Clear();
        }

        internal bool Add(int CardNum)
        {
            int key = CardNum;

            if (!Leds.ContainsKey(key))
            {
                if (LED2008.User_OpenScreen(CardNum))
                    Leds.Add(key, "OPEN");
            }
            else
            {
                if (Leds[key] != "OPEN")
                {
                    if (LED2008.User_OpenScreen(CardNum))
                        Leds [key] = "OPEN" ;
                }
            }
            return Leds.ContainsKey(key) ? Leds[key] == "OPEN":false;
        }
        internal bool Remove(int CardNum)
        {
            int key = CardNum;

            if (Leds.ContainsKey(key))
            {
                if (Leds[key] == "OPEN")
                {
                    if (LED2008.User_CloseScreen(CardNum))
                        Leds[key] = "CLOSE";
                }
                return Leds[key] == "CLOSE";
            }
            else
            {
                return true;
            }
        }
        public string this[int CardNum]
        {
            get
            {
                int key = CardNum;

                if (Leds.ContainsKey(key))
                    return Leds[key];
                else
                    return null;
            }
        }

        #region 发送数据到LED
        private void AddProgram(int CardNum)
        {
            if (Add(CardNum))
            {
                if (!Programs.ContainsKey(CardNum))
                {
                    int programIndex = LED2008.User_AddProgram(CardNum,false, 10);
                    Programs.Add(CardNum, programIndex);
                }
            }
        }
        public void DelAllProgram()
        {
            foreach (int CardNum in Leds.Keys)
            {
                if (Programs.ContainsKey(CardNum))
                {
                    Programs.Remove(CardNum);
                    LED2008.User_DelAllProgram(CardNum);
                }
            }
        }

        public void AddTextToProgram(int CardNum, int iX, int iY, int iHeight, int iWidth, string strContent, int colorFont, bool isMove)
        {
            if (Add(CardNum))
            {
                AddProgram(CardNum);

                User_Text Text = new User_Text();

                Text.BkColor = 0;
                Text.chContent = strContent;

                Text.PartInfo.FrameColor = 0;
                Text.PartInfo.iFrameMode = 0;
                Text.PartInfo.iHeight = iHeight;
                Text.PartInfo.iWidth = iWidth;
                Text.PartInfo.iX = iX;
                Text.PartInfo.iY = iY;

                Text.FontInfo.bFontBold = false;
                Text.FontInfo.bFontItaic = false;
                Text.FontInfo.bFontUnderline = false;
                Text.FontInfo.colorFont = colorFont;
                Text.FontInfo.iFontSize =9;
                Text.FontInfo.strFontName = "宋体";
                Text.FontInfo.iAlignStyle = 0;
                Text.FontInfo.iVAlignerStyle = 0;
                Text.FontInfo.iRowSpace = 0;

                Text.MoveSet.bClear = false;
                Text.MoveSet.iActionSpeed = 0;
                Text.MoveSet.iActionType = 20;
                Text.MoveSet.iHoldTime = 50;

                if (!isMove)
                {
                    Text.MoveSet.iHoldTime = -1;
                }

                if (Programs.ContainsKey(CardNum))
                {
                    int m_iProgramIndex = (int)Programs[CardNum];
                    LED2008.User_AddText(CardNum, ref  Text, m_iProgramIndex);
                }
            }
        }

        public void AddSingleTextToProgram(int CardNum, int iX, int iY, int iHeight, int iWidth, string strContent, int colorFont, bool isMove)
        {
            if (Add(CardNum))
            {
                AddProgram(CardNum);

                User_SingleText SingleText = new User_SingleText();

                SingleText.BkColor = 0;
                SingleText.chContent = strContent;
                SingleText.PartInfo.iFrameMode = 0;
                SingleText.PartInfo.iHeight = iHeight;
                SingleText.PartInfo.iWidth = iWidth;
                SingleText.PartInfo.iX = iX;
                SingleText.PartInfo.iY = iY;
                SingleText.FontInfo.bFontBold = false;
                SingleText.FontInfo.bFontItaic = false;
                SingleText.FontInfo.bFontUnderline = false;
                SingleText.FontInfo.colorFont = colorFont;
                SingleText.FontInfo.iFontSize = 9;
                SingleText.PartInfo.FrameColor = colorFont;
                SingleText.FontInfo.strFontName = "宋体";
                SingleText.MoveSet.bClear = false;
                SingleText.MoveSet.iActionSpeed = 0;
                SingleText.MoveSet.iActionType = 20;
                SingleText.MoveSet.iHoldTime = 50;
                SingleText.MoveSet.iClearActionType = 0;
                SingleText.MoveSet.iClearSpeed = 0;
                SingleText.MoveSet.iFrameTime = 30;

                if (!isMove)
                {
                    SingleText.MoveSet.iHoldTime = -1;
                }

                if (Programs.ContainsKey(CardNum))
                {
                    int m_iProgramIndex = (int)Programs[CardNum];
                    LED2008.User_AddSingleText(CardNum, ref  SingleText, m_iProgramIndex);
                }
            }
        }

        public void AddImageToProgram(int CardNum, int iX, int iY, int iHeight, int iWidth, Bitmap bitmap)
        {
            if (Add(CardNum))
            {
                AddProgram(CardNum);
                int m_iProgramIndex = -1;
                int BmpZoneIndex = -1;

                //准备新的图片区
                User_PartInfo partinfo = new User_PartInfo() ;
                partinfo.iX = iX;
                partinfo.iY = iY;
                partinfo.iHeight = iHeight;
                partinfo.iWidth = iWidth;
                partinfo.iFrameMode = 0;

                User_Bmp bmp = new User_Bmp() ;
                bmp.PartInfo = partinfo;                

                //准备移动方式
                User_MoveSet moveset = new User_MoveSet() ;
                moveset.iActionType = 20;
                moveset.iActionSpeed = 30;
                moveset.bClear = false;
                moveset.iHoldTime = 30;
                moveset.iClearSpeed = 10;
                moveset.iClearActionType = 20;

                //添加新图片                
                if (Programs.ContainsKey(CardNum))
                {
                    m_iProgramIndex = (int)Programs[CardNum];
                    BmpZoneIndex = LED2008.User_AddBmpZone(CardNum, ref bmp, m_iProgramIndex);

                    HandleRef hr = new HandleRef(bitmap, bitmap.GetHicon());
                    IntPtr hBitmap = bitmap.GetHbitmap();
                    LED2008.User_AddBmp(CardNum, BmpZoneIndex, hBitmap, ref  moveset, m_iProgramIndex);
                }
            }
        }

        public void SendToScreen()
        {
            foreach (int CardNum in Leds.Keys)
            {
                if (Programs.ContainsKey(CardNum) && Leds.ContainsKey(CardNum) ? Leds[CardNum] == "OPEN" : false)
                {
                    if (!LED2008.User_SendToScreen(CardNum))
                    {
                        System.Threading.Thread.Sleep(500);
                        LED2008.User_SendToScreen(CardNum);
                    }
                }
            }
        }

        #endregion
    }
}
