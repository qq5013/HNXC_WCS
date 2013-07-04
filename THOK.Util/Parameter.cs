namespace THOK.Util
{
    using System;
    using System.Data;

    public class Parameter
    {
        public ParameterDirection ParameterDirectioin = ParameterDirection.Input;
        public string ParameterName;
        public DbType ParameterType = DbType.String;
        public object ParameterValue;
    }
}

