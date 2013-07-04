namespace THOK.Util
{
    using System;
    using System.Data;

    public class BaseDao
    {
        private bool fromPool = true;
        private PersistentManager persistentManager;

        protected void BatchInsert(DataTable dataTable, string tableName)
        {
            try
            {
                this.InitPM();
                this.persistentManager.BatchInsert(dataTable, tableName);
                this.ReleasePM();
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
        }

        protected DataSet ExecuteEmptyDataSet(string tableName)
        {
            DataSet set2;
            try
            {
                this.InitPM();
                DataSet set = this.persistentManager.ExecuteEmptyDataSet(tableName);
                this.ReleasePM();
                set2 = set;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return set2;
        }

        protected int ExecuteNonQuery(string sqlString)
        {
            int num = 0;
            try
            {
                this.InitPM();
                num = this.persistentManager.ExecuteNonQuery(sqlString);
                this.ReleasePM();
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return num;
        }

        protected void ExecuteNonQuery(string procedureName, StoredProcParameter param)
        {
            try
            {
                this.InitPM();
                this.persistentManager.ExecuteNonQuery(procedureName, param);
                this.ReleasePM();
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
        }

        protected DataSet ExecuteQuery(string sqlString)
        {
            DataSet set2;
            try
            {
                this.InitPM();
                DataSet set = this.persistentManager.ExecuteQuery(sqlString);
                this.ReleasePM();
                set2 = set;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return set2;
        }

        protected DataSet ExecuteQuery(string sqlString, string tableName)
        {
            DataSet set2;
            try
            {
                this.InitPM();
                DataSet set = this.persistentManager.ExecuteQuery(sqlString, tableName);
                this.ReleasePM();
                set2 = set;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return set2;
        }

        protected DataSet ExecuteQuery(string procedureName, StoredProcParameter param)
        {
            DataSet set2;
            try
            {
                this.InitPM();
                DataSet set = this.persistentManager.ExecuteQuery(procedureName, param);
                this.ReleasePM();
                set2 = set;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return set2;
        }

        protected DataSet ExecuteQuery(string procedureName, string tableName, StoredProcParameter param)
        {
            DataSet set2;
            try
            {
                this.InitPM();
                DataSet set = this.persistentManager.ExecuteQuery(procedureName, tableName, param);
                this.ReleasePM();
                set2 = set;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return set2;
        }

        protected DataSet ExecuteQuery(string sql, string tableName, int startRecord, int count)
        {
            DataSet set2;
            try
            {
                this.InitPM();
                DataSet set = this.persistentManager.ExecuteQuery(sql, tableName, startRecord, count);
                this.ReleasePM();
                set2 = set;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return set2;
        }

        protected IDataReader ExecuteReader(string sqlString)
        {
            IDataReader reader2;
            try
            {
                this.InitPM();
                IDataReader reader = this.persistentManager.ExecuteReader(sqlString);
                this.ReleasePM();
                reader2 = reader;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return reader2;
        }

        protected IDataReader ExecuteReader(string procedureName, StoredProcParameter param)
        {
            IDataReader reader2;
            try
            {
                this.InitPM();
                IDataReader reader = this.persistentManager.ExecuteReader(procedureName, param);
                this.ReleasePM();
                reader2 = reader;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return reader2;
        }

        protected object ExecuteScalar(string sqlString)
        {
            object obj3;
            try
            {
                this.InitPM();
                object obj2 = this.persistentManager.ExecuteScalar(sqlString);
                this.ReleasePM();
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return obj3;
        }

        protected object ExecuteScalar(string procedureName, StoredProcParameter param)
        {
            object obj3;
            try
            {
                this.InitPM();
                object obj2 = this.persistentManager.ExecuteScalar(procedureName, param);
                this.ReleasePM();
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                this.ReleasePM();
                throw new Exception(exception.Message);
            }
            return obj3;
        }

        private void InitPM()
        {
            if (this.fromPool)
            {
                this.persistentManager = PMFactory.GetPM();
            }
        }

        private void ReleasePM()
        {
            if (this.fromPool)
            {
                PMFactory.Remove();
            }
        }

        public void SetPersistentManager(PersistentManager persistentManager)
        {
            this.persistentManager = persistentManager;
            this.fromPool = false;
        }
    }
}

