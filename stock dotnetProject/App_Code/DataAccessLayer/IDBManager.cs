using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public interface IDBManager
    {
        IDbCommand Command
        {
            get;
        }

        IDbConnection Connection
        {
            get;
        }

        string ConnectionString
        {
            get;
            set;
        }

        IDataReader DataReader
        {
            get;
        }

        IDbDataParameter[] inParameters
        {
            get;
        }

        IDbDataParameter[] outParameters
        {
            get;
        }

        DataProvider ProviderType
        {
            get;
            set;
        }

        IDbTransaction Transaction
        {
            get;
        }

        void AddInParameters(int index, string paramName, object objValue);

        void AddOutParameters(int index, string paramName, int typ, int size);

        void BeginTransaction();

        void Close();

        void CloseReader();

        void CommitTransaction();

        void CreateInParameters(int paramsCount);

        void CreateOutParameters(int paramsCount);

        void Dispose();

        DataSet ExecuteDataSet(CommandType commandType, string commandText);

        void ExecuteNonQuery(CommandType commandType, string commandText);

        IDataReader ExecuteReader(CommandType commandType, string commandText);

        object ExecuteScalar(CommandType commandType, string commandText);

        void Open();

    }
}