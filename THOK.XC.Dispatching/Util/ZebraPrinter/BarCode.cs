namespace THOK.XC.Dispatching.Util
{
    public class BarCode
    {
        // Fields
        private string _barcode;

        // Methods
        public BarCode(int lLeft, int lTop, string Data, int lFontHigh, string BarFontName, string ShowData, string Above, int Orientation, string CheckDigit, int pZoom)
        {
            string orientation = "";
            //打印内容的旋转角度

            switch (Orientation)
            {
                case 180:
                    orientation = "I";
                    break;

                case 270:
                    orientation = "B";
                    break;

                case 0:
                    orientation = "N";
                    break;

                case 90:
                    orientation = "R";
                    break;
            }
            //条码字体名称

            switch (BarFontName)
            {
                case "CODE128":
                    this._barcode = this.CODE128(lLeft.ToString(), lTop.ToString(), Data, (long)lFontHigh, orientation, ShowData, Above, CheckDigit, pZoom.ToString());
                    return;

                case "EAN128":
                    this._barcode = this.EAN128(lLeft.ToString(), lTop.ToString(), Data, (long)lFontHigh, orientation, ShowData, Above, CheckDigit, pZoom.ToString());
                    return;

                case "CODE39":
                    this._barcode = this.CODE39(lLeft.ToString(), lTop.ToString(), Data, (long)lFontHigh, orientation, ShowData, Above, CheckDigit, pZoom.ToString());
                    return;
            }
            this._barcode = "";
        }

        private string CODE128(string pLeft, string pTop, string Data, long FontSize, string Orientation, string ShowData, string Above, string CheckDigit, string pZoom)
        {
            return string.Concat(new object[] { 
            "^FO", pLeft, ",", pTop, "^BY", pZoom, "^BC", Orientation, ",", FontSize, ",", ShowData, ",", Above, ",N^FD", Data, 
            "^FS"
         });
        }

        private string CODE39(string pLeft, string pTop, string Data, long FontSize, string Orientation, string ShowData, string Above, string CheckDigit, string pZoom)
        {
            return string.Concat(new object[] { 
            "^FO", pLeft, ",", pTop, "^BY", pZoom, "^B3", Orientation, ",", CheckDigit, ",", FontSize, ",", ShowData, ",", Above, 
            "^FD", Data, "^FS"
         });
        }

        private string EAN128(string pLeft, string pTop, string Data, long FontSize, string Orientation, string ShowData, string Above, string CheckDigit, string pZoom)
        {
            return string.Concat(new object[] { 
            "^FO", pLeft, ",", pTop, "^BY", pZoom, "^BC", Orientation, ",", FontSize, ",", ShowData, ",", Above, ",", CheckDigit, 
            ",D^FD", Data, "^FS"
         });
        }

        // Properties
        public string barcode
        {
            get
            {
                return this._barcode;
            }
        }
    }
}
 
