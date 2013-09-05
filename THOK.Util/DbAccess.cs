namespace THOK.Util
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Xml;
    

    public class DbAccess
    {
        private static IDictionary<string, IDictionary<string, string>> cnnConfig;
        private string cnnConfigLock;
        private DbConnection connection;
        private string connectionString;
        private string databaseType;
        private DbTransaction transaction;

        public DbAccess()
        {
            this.cnnConfigLock = "";
            this.GetParameter();
            IDictionary<string, string> dictionary = cnnConfig["DefaultConnection"];
            this.databaseType = dictionary["DatabaseType"].ToString();
            this.connectionString = dictionary["ConnectionString"].ToString();
        }

        public DbAccess(string name)
        {
            this.cnnConfigLock = "";
            this.GetParameter();
            IDictionary<string, string> dictionary = cnnConfig[name];
            this.databaseType = dictionary["DatabaseType"].ToString();
            this.connectionString = dictionary["ConnectionString"].ToString();
        }

        public DbAccess(string databaseType, string connectionString)
        {
            this.cnnConfigLock = "";
            this.databaseType = databaseType;
            this.connectionString = connectionString;
        }

        public void BatchInsert(DataTable dataTable, string tableName)
        {
            if (!(this.connection is SqlConnection))
            {
                throw new Exception("此方法只支持SQL Server数据库。");
            }
            SqlBulkCopy copy = new SqlBulkCopy((SqlConnection) this.connection) {
                DestinationTableName = tableName
            };
            copy.WriteToServer(dataTable);
            copy.Close();
        }

        public void BeginTransaction()
        {
            if (this.connection.State == ConnectionState.Closed)
            {
                this.connection.Open();
            }
            this.transaction = this.connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            if ((this.connection != null) && (this.connection.State == ConnectionState.Open))
            {
                this.connection.Close();
            }
        }

        public void Commit()
        {
            this.transaction.Commit();
        }

        private DbCommand CreateCommand(string strCmd)
        {
            DbCommand command = this.connection.CreateCommand();
            command.CommandText = strCmd;
            return command;
        }

        private DbConnection CreateConnection()
        {
            DbProviderFactory factory = null;
            DbConnection connection = null;
            string databaseType = this.databaseType;
            if (databaseType != null)
            {
                if (!(databaseType == "SQLSERVER"))
                {
                    if (databaseType == "OLEDB")
                    {
                        factory = DbProviderFactories.GetFactory("System.Data.OLEDB");
                    }
                    else if (databaseType == "ORACLE")
                    {
                        factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
                    }
                    else if (databaseType == "DB2")
                    {
                        factory = DbProviderFactories.GetFactory("IBM.Data.DB2");
                    }
                }
                else
                {
                    factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                }
            }
            if (factory == null)
            {
                throw new Exception("无法根据数据库类型参数创建DbConnection对象，请检查数据库类型参数是否正确");
            }
            connection = factory.CreateConnection();
            connection.ConnectionString = this.connectionString;
            return connection;
        }

        private DbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            DbProviderFactory factory = null;
            string databaseType = this.databaseType;
            if (databaseType != null)
            {
                if (!(databaseType == "SQLSERVER"))
                {
                    if (databaseType == "OLEDB")
                    {
                        factory = DbProviderFactories.GetFactory("System.Data.OLEDB");
                    }
                    else if (databaseType == "ORACLE")
                    {
                        factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
                    }
                    else if (databaseType == "DB2")
                    {
                        factory = DbProviderFactories.GetFactory("IBM.Data.DB2");
                    }
                }
                else
                {
                    factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                }
            }
            if (factory == null)
            {
                throw new Exception("无法根据数据库类型参数创建DbDataAdapter对象，请检查数据库类型参数是否正确");
            }
            return factory.CreateDataAdapter();
        }

        public DataSet ExecuteEmptyDataSet(string tableName)
        {
            string sql = string.Format("SELECT TOP 0 * FROM {0}", tableName);
            return this.ExecuteQuery(sql, tableName);
        }

        public int ExecuteNonQuery(string sql)
        {
            DbCommand command = this.CreateCommand(sql);
            if (this.transaction != null)
            {
                command.Transaction = this.transaction;
            }
            return command.ExecuteNonQuery();
        }

        public void ExecuteNonQuery(string procedureName, StoredProcParameter param)
        {
            DbCommand dbCommand = this.CreateCommand(procedureName);
            dbCommand.CommandType = CommandType.StoredProcedure;
            this.SetParameter(dbCommand, param);
            if (this.transaction != null)
            {
                dbCommand.Transaction = this.transaction;
            }
            dbCommand.ExecuteNonQuery();
            foreach (IDbDataParameter parameter in dbCommand.Parameters)
            {
                param.Parameters[parameter.ParameterName].ParameterValue = parameter.Value;
            }
        }

        public DataSet ExecuteQuery(string sql)
        {
            DataSet dataSet = new DataSet();
            DbCommand command = this.CreateCommand(sql);
            if (this.transaction != null)
            {
                command.Transaction = this.transaction;
            }
            DbDataAdapter adapter = this.CreateDataAdapter(command);
            adapter.SelectCommand = command;
            adapter.Fill(dataSet);
            return dataSet;
        }

        public DataSet ExecuteQuery(string sql, string tableName)
        {
            DataSet dataSet = new DataSet();
            DbCommand command = this.CreateCommand(sql);
            if (this.transaction != null)
            {
                command.Transaction = this.transaction;
            }
            DbDataAdapter adapter = this.CreateDataAdapter(command);
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
            return dataSet;
        }

        public DataSet ExecuteQuery(string procedureName, StoredProcParameter param)
        {
            DataSet dataSet = new DataSet();
            DbCommand dbCommand = this.CreateCommand(procedureName);
            if (this.transaction != null)
            {
                dbCommand.Transaction = this.transaction;
            }
            dbCommand.CommandType = CommandType.StoredProcedure;
            this.SetParameter(dbCommand, param);
            DbDataAdapter adapter = this.CreateDataAdapter(dbCommand);
            adapter.SelectCommand = dbCommand;
            adapter.Fill(dataSet);
            return dataSet;
        }

        public DataSet ExecuteQuery(string sql, int start, int count)
        {
            return this.ExecuteQuery(sql, "TABLE", start, count);
        }

        public DataSet ExecuteQuery(string procedureName, string tableName, StoredProcParameter param)
        {
            DataSet dataSet = new DataSet();
            DbCommand dbCommand = this.CreateCommand(procedureName);
            if (this.transaction != null)
            {
                dbCommand.Transaction = this.transaction;
            }
            dbCommand.CommandType = CommandType.StoredProcedure;
            this.SetParameter(dbCommand, param);
            DbDataAdapter adapter = this.CreateDataAdapter(dbCommand);
            adapter.SelectCommand = dbCommand;
            adapter.Fill(dataSet, tableName);
            return dataSet;
        }

        public DataSet ExecuteQuery(string sql, string tableName, int start, int count)
        {
            DataSet dataSet = new DataSet();
            DbCommand command = this.CreateCommand(sql);
            if (this.transaction != null)
            {
                command.Transaction = this.transaction;
            }
            DbDataAdapter adapter = this.CreateDataAdapter(command);
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, start, count, tableName);
            return dataSet;
        }

        public DbDataReader ExecuteReader(string sql)
        {
            DbCommand command = this.CreateCommand(sql);
            if (this.transaction != null)
            {
                command.Transaction = this.transaction;
            }
            return command.ExecuteReader();
        }

        public DbDataReader ExecuteReader(string procedureName, StoredProcParameter param)
        {
            DbCommand dbCommand = this.CreateCommand(procedureName);
            if (this.transaction != null)
            {
                dbCommand.Transaction = this.transaction;
            }
            dbCommand.CommandType = CommandType.StoredProcedure;
            this.SetParameter(dbCommand, param);
            return dbCommand.ExecuteReader();
        }

        public object ExecuteScalar(string sql)
        {
            DbCommand command = this.CreateCommand(sql);
            if (this.transaction != null)
            {
                command.Transaction = this.transaction;
            }
            return command.ExecuteScalar();
        }

        public object ExecuteScalar(string procedureName, StoredProcParameter param)
        {
            DbCommand dbCommand = this.CreateCommand(procedureName);
            if (this.transaction != null)
            {
                dbCommand.Transaction = this.transaction;
            }
            dbCommand.CommandType = CommandType.StoredProcedure;
            this.SetParameter(dbCommand, param);
            return dbCommand.ExecuteScalar();
        }

        private void GetParameter()
        {
            lock (this.cnnConfigLock)
            {
                if (cnnConfig == null)
                {
                    cnnConfig = new Dictionary<string, IDictionary<string, string>>();
                    XmlDocument document = new XmlDocument();
                    try
                    {
                        document.Load("DB.xml");
                    }
                    catch
                    {
                        document.Load(AppDomain.CurrentDomain.BaseDirectory + "DB.xml");
                    }
                    foreach (XmlNode node in document.GetElementsByTagName("Connection"))
                    {
                        string key = node.Attributes["Name"].Value;
                        string str2 = node.Attributes["DatabaseType"].Value;
                        string str3 = "";
                        string str4 = "";
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2.Name == "ConnectionString")
                            {
                                str3 = node2.Attributes["Value"].Value;
                            }
                            else if (node2.Name == "Password")
                            {
                                string str5 = node2.Attributes["Name"].Value;
                                string str6 = Coding.Decoding(node2.Attributes["Value"].Value);
                                str4 = string.Format("{0}={1}", str5, str6);
                            }
                        }
                        if (str3.EndsWith(";"))
                        {
                            str3 = str3 + str4;
                        }
                        else
                        {
                            str3 = str3 + ";" + str4;
                        }
                        IDictionary<string, string> dictionary = new Dictionary<string, string>();
                        dictionary.Add("DatabaseType", str2);
                        dictionary.Add("ConnectionString", str3);
                        cnnConfig.Add(key, dictionary);
                    }
                }
            }
        }

        public void OpenConnection()
        {
            if (this.connection == null)
            {
                this.connection = this.CreateConnection();
            }
            if (this.connection.State == ConnectionState.Closed)
            {
                this.connection.Open();
            }
        }

        public void Rollback()
        {
            this.transaction.Rollback();
        }

        private void SetParameter(IDbCommand dbCommand, StoredProcParameter param)
        {
            if (param != null)
            {
                foreach (Parameter parameter in param.Parameters.Values)
                {
                    IDbDataParameter parameter2 = dbCommand.CreateParameter();
                    parameter2.ParameterName = parameter.ParameterName;
                    parameter2.Value = parameter.ParameterValue;
                    parameter2.DbType = parameter.ParameterType;
                    parameter2.Direction = parameter.ParameterDirectioin;
                    if (dbCommand is System.Data.OracleClient.OracleCommand && parameter.ParameterDirectioin == ParameterDirection.Output && parameter.ParameterType == System.Data.DbType.String)
                    {
                        parameter2.Size = parameter.ParameterValue.ToString().Length;
                    }
                    dbCommand.Parameters.Add(parameter2);
                   
                    
                   
                }
            }
        }
    }
}

