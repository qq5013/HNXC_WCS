namespace THOK.Util
{
    using System;
    using System.Text;

    public class SqlCreate
    {
        private StringBuilder fieldBuilder = new StringBuilder();
        private SqlType sqlType;
        private string tableName = "";
        private StringBuilder updateBuilder = new StringBuilder();
        private StringBuilder valueBuilder = new StringBuilder();

        public SqlCreate(string tableName, SqlType sqlType)
        {
            this.tableName = tableName;
            this.sqlType = sqlType;
            if (sqlType == SqlType.UPDATE)
            {
                this.updateBuilder.AppendFormat("UPDATE {0} SET ", tableName);
            }
        }

        public void Append(string fieldName, object fieldValue)
        {
            if (this.sqlType == SqlType.UPDATE)
            {
                this.updateBuilder.AppendFormat("{0}={1},", fieldName, fieldValue);
            }
            else
            {
                this.fieldBuilder.AppendFormat("{0},", fieldName);
                this.valueBuilder.AppendFormat("{0},", fieldValue);
            }
        }

        public void AppendQuote(string fieldName, object fieldValue)
        {
            if (this.sqlType == SqlType.UPDATE)
            {
                this.updateBuilder.AppendFormat("{0}='{1}',", fieldName, fieldValue);
            }
            else
            {
                this.fieldBuilder.AppendFormat("{0},", fieldName);
                this.valueBuilder.AppendFormat("'{0}',", fieldValue);
            }
        }

        public void AppendWhere(string fieldName, object fieldValue)
        {
            if (this.sqlType == SqlType.UPDATE)
            {
                this.updateBuilder.Remove(this.updateBuilder.Length - 1, 1);
                this.updateBuilder.AppendFormat(" WHERE {0}{1}{2}", fieldName, "=", fieldValue);
            }
        }

        public void AppendWhereQuote(string fieldName, object fieldValue)
        {
            if (this.sqlType == SqlType.UPDATE)
            {
                this.updateBuilder.Remove(this.updateBuilder.Length - 1, 1);
                this.updateBuilder.AppendFormat(" WHERE {0}{1}'{2}'", fieldName, "=", fieldValue);
            }
        }

        public string GetSQL()
        {
            string str = this.fieldBuilder.ToString();
            string str2 = this.valueBuilder.ToString();
            if (this.sqlType != SqlType.UPDATE)
            {
                return string.Format("INSERT INTO {0}({1}) VALUES({2})", this.tableName, str.Substring(0, str.Length - 1), str2.Substring(0, str2.Length - 1));
            }
            return this.updateBuilder.ToString();
        }
    }
}

