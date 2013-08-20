namespace THOK.XC.Dispatching.Util
{
    public class Character
    {
        // Fields
        private int _c1;
        private int _c2;
        private int _Height;
        private int _Width;
        private string _ID;
        private int _Itailc;
        private int _Orientation;
        private int _pLeft;
        private string _printData;
        private int _pTop;
        private string _SysFontName;
        private int _XZoom;
        private int _YZoom;

        // Methods
        public Character(int pLeft, int pTop, string Data, string ID, int Height, int Width, string SysFontName, int Itailc, int Orientation, int XZoom, int YZoom)
        {
            this._Height = Height;//字体高度
            this._Width = Width;//字体宽度
            this._ID = ID;
            this._Itailc = Itailc;//斜体
            this._Orientation = Orientation;//打印内容的旋转角度
            this._pLeft = pLeft;//距离打印纸左边的长度
            this._printData = Data;//显示内容
            this._pTop = pTop;//距离打印顶端的长度
            this._SysFontName = SysFontName;//字体名称
            this._XZoom = XZoom;//X轴的缩放程度
            this._YZoom = YZoom;//Y轴的缩放程度
        }

        // Properties
        public int c1
        {
            get
            {
                return 0;
            }
        }

        public int c2
        {
            get
            {
                return 0;
            }
        }

        public int Height
        {
            get
            {
                return this._Height;
            }
        }

        public int Width
        {
            get
            {
                return this._Width;
            }
        }

        public string ID
        {
            get
            {
                return this._ID;
            }
        }

        public int Itailc
        {
            get
            {
                return this._Itailc;
            }
        }

        public int Orientation
        {
            get
            {
                return this._Orientation;
            }
        }

        public int pLeft
        {
            get
            {
                return this._pLeft;
            }
        }

        public string printData
        {
            get
            {
                return this._printData;
            }
        }

        public int pTop
        {
            get
            {
                return this._pTop;
            }
        }

        public string SysFontName
        {
            get
            {
                return this._SysFontName;
            }
        }

        public int XZoom
        {
            get
            {
                return this._XZoom;
            }
        }

        public int YZoom
        {
            get
            {
                return this._YZoom;
            }
        }
    }
}
