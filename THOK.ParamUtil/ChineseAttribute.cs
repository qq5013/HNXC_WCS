namespace THOK.ParamUtil
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class ChineseAttribute : Attribute
    {
        private string sChineseChar = "";

        public ChineseAttribute(string sChineseChar)
        {
            this.sChineseChar = sChineseChar;
        }

        public override string ToString()
        {
            return this.sChineseChar;
        }

        public string ChineseChar
        {
            get
            {
                return this.sChineseChar;
            }
        }
    }
}

