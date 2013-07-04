namespace THOK.Util
{
    using System;
    using System.Data;
    using System.Threading;

    public class PersistentManager : IDisposable
    {
        internal DbAccess dbAccess;

        public PersistentManager()
        {
            this.dbAccess = new DbAccess();
            this.dbAccess.OpenConnection();
            PMFactory.AddPM(this);
        }

        public PersistentManager(string connectionString)
        {
            this.dbAccess = new DbAccess(connectionString);
            this.dbAccess.OpenConnection();
        }

        public PersistentManager(string databaseType, string connectionString)
        {
            this.dbAccess = new DbAccess(databaseType, connectionString);
            this.dbAccess.OpenConnection();
        }

        internal void BatchInsert(DataTable dataTable, string tableName)
        {
            this.dbAccess.BatchInsert(dataTable, tableName);
        }

        public void BeginTransaction()
        {
            this.dbAccess.BeginTransaction();
        }

        public void Commit()
        {
            this.dbAccess.Commit();
        }

        public void Dispose()
        {
            this.dbAccess.CloseConnection();
            this.dbAccess = null;
            PMFactory.Remove(Thread.CurrentThread.ManagedThreadId);
        }

        internal DataSet ExecuteEmptyDataSet(string tableName)
        {
            return this.dbAccess.ExecuteEmptyDataSet(tableName);
        }

        internal int ExecuteNonQuery(string sqlString)
        {
            return this.dbAccess.ExecuteNonQuery(sqlString);
        }

        internal void ExecuteNonQuery(string procedureName, StoredProcParameter param)
        {
            this.dbAccess.ExecuteNonQuery(procedureName, param);
        }

        internal DataSet ExecuteQuery(string sql)
        {
            return this.dbAccess.ExecuteQuery(sql);
        }

        internal DataSet ExecuteQuery(string sql, string tableName)
        {
            return this.dbAccess.ExecuteQuery(sql, tableName);
        }

        internal DataSet ExecuteQuery(string procedureName, StoredProcParameter param)
        {
            return this.dbAccess.ExecuteQuery(procedureName, param);
        }

        internal DataSet ExecuteQuery(string procedureName, string tableName, StoredProcParameter param)
        {
            return this.dbAccess.ExecuteQuery(procedureName, tableName, param);
        }

        internal DataSet ExecuteQuery(string sql, string tableName, int start, int count)
        {
            return this.dbAccess.ExecuteQuery(sql, tableName, start, count);
        }

        internal IDataReader ExecuteReader(string sqlString)
        {
            return this.dbAccess.ExecuteReader(sqlString);
        }

        internal IDataReader ExecuteReader(string procedureName, StoredProcParameter param)
        {
            return this.dbAccess.ExecuteReader(procedureName, param);
        }

        internal object ExecuteScalar(string sql)
        {
            return this.dbAccess.ExecuteScalar(sql);
        }

        internal object ExecuteScalar(string procedureName, StoredProcParameter param)
        {
            return this.dbAccess.ExecuteScalar(procedureName, param);
        }

        public void Rollback()
        {
            this.dbAccess.Rollback();
        }
    }
}

