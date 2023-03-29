using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public sealed class DBManagerFactory
	{
		private DBManagerFactory()
		{
		}

		public static IDbCommand GetCommand(DataProvider providerType)
		{
			switch (providerType)
			{
				case DataProvider.SqlServer:
				{
					return new SqlCommand();
				}
				case DataProvider.OleDb:
				{
					return new OleDbCommand();
				}
				case DataProvider.Odbc:
				{
					return new OdbcCommand();
				}
			}
			return null;
		}

		public static IDbConnection GetConnection(DataProvider providerType)
		{
			IDbConnection sqlConnection = null;
			switch (providerType)
			{
				case DataProvider.SqlServer:
				{
					sqlConnection = new SqlConnection();
					break;
				}
				case DataProvider.OleDb:
				{
					sqlConnection = new OleDbConnection();
					break;
				}
				case DataProvider.Odbc:
				{
					sqlConnection = new OdbcConnection();
					break;
				}
				default:
				{
					return null;
				}
			}
			return sqlConnection;
		}

		public static IDbDataAdapter GetDataAdapter(DataProvider providerType)
		{
			switch (providerType)
			{
				case DataProvider.SqlServer:
				{
					return new SqlDataAdapter();
				}
				case DataProvider.OleDb:
				{
					return new OleDbDataAdapter();
				}
				case DataProvider.Odbc:
				{
					return new OdbcDataAdapter();
				}
			}
			return null;
		}

		public static IDataParameter GetParameter(DataProvider providerType)
		{
			IDataParameter sqlParameter = null;
			switch (providerType)
			{
				case DataProvider.SqlServer:
				{
					sqlParameter = new SqlParameter();
					break;
				}
				case DataProvider.OleDb:
				{
					sqlParameter = new OleDbParameter();
					break;
				}
				case DataProvider.Odbc:
				{
					sqlParameter = new OdbcParameter();
					break;
				}
			}
			return sqlParameter;
		}

		public static IDbDataParameter[] GetParameters(DataProvider providerType, int paramsCount)
		{
			IDbDataParameter[] sqlParameter = new IDbDataParameter[paramsCount];
			switch (providerType)
			{
				case DataProvider.SqlServer:
				{
					for (int i = 0; i < paramsCount; i++)
					{
						sqlParameter[i] = new SqlParameter();
					}
					break;
				}
				case DataProvider.OleDb:
				{
					for (int j = 0; j < paramsCount; j++)
					{
						sqlParameter[j] = new OleDbParameter();
					}
					break;
				}
				case DataProvider.Odbc:
				{
					for (int k = 0; k < paramsCount; k++)
					{
						sqlParameter[k] = new OdbcParameter();
					}
					break;
				}
				default:
				{
					sqlParameter = null;
					break;
				}
			}
			return sqlParameter;
		}

		public static IDbTransaction GetTransaction(DataProvider providerType)
		{
			return DBManagerFactory.GetConnection(providerType).BeginTransaction();
		}
	}
}