using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using System;

namespace THOK.XC.Dispatching.Util
{
    public class zebraPrint
    {
        // Fields
        private ArrayList BarCodeArr = new ArrayList();
        private ArrayList BarCodeArrList = new ArrayList();
        private StringBuilder cBuf = new StringBuilder(0x5208);
        private string ID = "";
        public string LabelPrinterName = "ZDesigner 105SL 203DPI (1)";//标签打印机名称
        private int LoadBarFontName = 128;//条码字体名称
        private int nCount = 0;
        private int prtCharID = 0;
        private ArrayList WindowsArrList = new ArrayList();

        // Methods
        //获取条码

        public void addBarCode(int pLeft, int pTop, string Data, int FontHigh, string BarFontName, string ShowData, string Above, int Orientation, string CheckDigit, int pZoom)
        {
            BarCode code = new BarCode(pLeft, pTop, Data, FontHigh, BarFontName, ShowData, Above, Orientation, CheckDigit, pZoom);
            this.BarCodeArrList.Add(code);
        }

        //获取字符
        public void addCharacter(int pleft, int pTop, string Data, int Height, int Width, string SysFontName, int Itailc, int Orientation, int XZoom, int YZoom)
        {
            //if (Encoding.Default.GetBytes(Data).Length > 20)
            //{
            //    Data.Substring(0,20);
            //}
            this.prtCharID++;
            this.ID = "ID" + this.prtCharID.ToString();
            Character character = new Character(pleft, pTop, Data, this.ID, Height, Width, SysFontName, Itailc, Orientation, XZoom, YZoom);
            this.WindowsArrList.Add(character);
        }

        [DllImport("Fnthex32.dll", CharSet = CharSet.Ansi)]
        public static extern int GETFONTHEX(string chnstr, string fontname, int orient, int height, int width, int bold, int italic, StringBuilder hexbuf);
        public int Print(int PageNumber)
        {
            int num = 1;
            string szString = "";
            for (int i = 0; i < PageNumber; i++)
            {
                if (!this.LoadBarFontName.Equals("1"))
                {
                    num = 2;
                }
                try
                {
                    for (int j = 0; j < this.WindowsArrList.Count; j++)
                    {
                        Character character = null;
                        character = (Character)this.WindowsArrList[j];
                        String chnstr = character.printData;
                        String fontname = character.SysFontName;
                        String chnname = character.ID;
                        this.nCount = GETFONTHEX(chnstr, fontname, character.Orientation, character.Height, character.Width, character.c1, character.Itailc, this.cBuf);
                        IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(this.cBuf.ToString());
                        RawPrinterHelper.SendBytesToPrinter(this.LabelPrinterName.ToString(), pBytes, this.nCount);
                    }
                    for (int k = 0; k < this.WindowsArrList.Count; k++)
                    {
                        Character character2 = null;
                        character2 = (Character)this.WindowsArrList[k];
                        szString = string.Concat(new object[] { szString, "^FO", character2.pLeft.ToString(), ", ", character2.pTop.ToString(), "^XGOUTSTR01", ",", character2.XZoom.ToString(), ",", character2.YZoom.ToString(), "^FS" });
                    }
                    for (int m = 0; m < this.BarCodeArrList.Count; m++)
                    {
                        BarCode code = (BarCode)this.BarCodeArrList[m];
                        szString = szString + code.barcode;
                    }
                    szString = "^XA" + szString + "^XZ";
                    if (RawPrinterHelper.SendStringToPrinter(this.LabelPrinterName.ToString(), szString))
                    {
                        num = 0;
                    }
                }
                catch (Exception exception)
                {
                    exception.Message.ToString();
                    num = 3;
                }
            }
            return num;
        }
        //测试

        public void setPrinter(string printerName)
        {
            this.LabelPrinterName = printerName;
        }
    }
}
