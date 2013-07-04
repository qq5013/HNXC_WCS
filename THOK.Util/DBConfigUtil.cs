namespace THOK.Util
{
    using System;
    using System.Data.Common;
    using System.Xml;

    public class DBConfigUtil
    {
        private DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
        private string dbType;
        private XmlDocument doc = new XmlDocument();
        private string fileName;
        private string name;

        public DBConfigUtil(string name, string dbType)
        {
            this.name = name;
            this.dbType = dbType;
            try
            {
                this.doc.Load("DB.xml");
                this.fileName = "DB.xml";
            }
            catch
            {
                this.doc.Load(Environment.SystemDirectory + @"\DB.xml");
                this.fileName = Environment.SystemDirectory + @"\DB.xml";
            }
            foreach (XmlNode node in this.doc.GetElementsByTagName("Connection"))
            {
                if (node.Attributes["Name"].InnerText.Trim().Equals(name) && node.Attributes["DatabaseType"].InnerText.Trim().Equals(dbType))
                {
                    string str = "";
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.Equals("ConnectionString"))
                        {
                            str = str + node2.Attributes["Value"].InnerText;
                        }
                        else if (node2.Name.Equals("Password"))
                        {
                            str = str + node2.Attributes["Name"].InnerText + "=";
                            str = str + node2.Attributes["Value"].InnerText;
                        }
                    }
                    this.builder.ConnectionString = str;
                    break;
                }
            }
        }

        public void Save()
        {
            this.doc.Load(this.fileName);
            foreach (XmlNode node in this.doc.GetElementsByTagName("Connection"))
            {
                if (node.Attributes["Name"].InnerText.Trim().Equals(this.name) && node.Attributes["DatabaseType"].InnerText.Trim().Equals(this.dbType))
                {
                    string str = "";
                    string str2 = "";
                    string str3 = "";
                    foreach (string str4 in this.builder.Keys)
                    {
                        if (str4.ToUpper().Equals("PASSWORD"))
                        {
                            str = str4;
                            str2 = this.builder[str4].ToString();
                        }
                        else
                        {
                            str3 = str3 + string.Format("{0}={1};", str4, this.builder[str4]);
                        }
                    }
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.Equals("ConnectionString"))
                        {
                            node2.Attributes["Value"].InnerText = str3;
                        }
                        else if (node2.Name.Equals("Password"))
                        {
                            node2.Attributes["Name"].InnerText = str;
                            node2.Attributes["Value"].InnerText = str2;
                        }
                    }
                    break;
                }
            }
            this.doc.Save(this.fileName);
        }

        public DbConnectionStringBuilder Parameters
        {
            get
            {
                return this.builder;
            }
        }
    }
}

