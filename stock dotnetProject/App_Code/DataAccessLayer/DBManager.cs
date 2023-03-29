using System;
using System.Collections;
using System.Data;

namespace DataAccessLayer
{
	public sealed class DBManager : IDBManager, IDisposable
	{
		private IDbConnection idbConnection;

		private IDataReader idataReader;

		private IDbCommand idbCommand;

		private DataProvider providerType;

		private IDataParameter idataParameter;

		private IDbTransaction idbTransaction;

		private IDbDataParameter[] idbInParameters;

		private IDbDataParameter[] idbOutParameters;

		private string strConnection;

		public IDbCommand Command
		{
			get
			{
				return this.idbCommand;
			}
		}

		public IDbConnection Connection
		{
			get
			{
				return this.idbConnection;
			}
		}

		public string ConnectionString
		{
			get
			{
				return this.strConnection;
			}
			set
			{
				this.strConnection = value;
			}
		}

		public IDataParameter DataParameter
		{
			get
			{
				return this.idataParameter;
			}
			set
			{
				this.idataParameter = value;
			}
		}

		public IDataReader DataReader
		{
			get
			{
				return JustDecompileGenerated_get_DataReader();
			}
			set
			{
				JustDecompileGenerated_set_DataReader(value);
			}
		}

		public IDataReader JustDecompileGenerated_get_DataReader()
		{
			return this.idataReader;
		}

		public void JustDecompileGenerated_set_DataReader(IDataReader value)
		{
			this.idataReader = value;
		}

		public IDbDataParameter[] inParameters
		{
			get
			{
				return this.idbInParameters;
			}
		}

		public IDbDataParameter[] outParameters
		{
			get
			{
				return this.idbOutParameters;
			}
		}

		public DataProvider ProviderType
		{
			get
			{
				return this.providerType;
			}
			set
			{
				this.providerType = value;
			}
		}

		public IDbTransaction Transaction
		{
			get
			{
				return this.idbTransaction;
			}
		}

		public DBManager()
		{
		}

		public DBManager(DataProvider providerType)
		{
			this.providerType = providerType;
		}

		public DBManager(DataProvider providerType, string connectionString)
		{
			this.providerType = providerType;
			this.strConnection = connectionString;
		}

		public void AddInParameters(int index, string paramName, object objValue)
		{
			if (index < (int)this.idbInParameters.Length)
			{
				this.idbInParameters[index].ParameterName = paramName;
				this.idbInParameters[index].Value = objValue;
			}
		}

		public void AddOutParameters(int index, string paramName, int typ, int size)
		{
			if (index < (int)this.idbOutParameters.Length)
			{
				this.idbOutParameters[index].ParameterName = paramName;
				this.idbOutParameters[index].Direction = ParameterDirection.Output;
				switch (typ)
				{
					case 1:
					{
						this.idbOutParameters[index].DbType = DbType.String;
						break;
					}
					case 2:
					{
						this.idbOutParameters[index].DbType = DbType.Int16;
						break;
					}
				}
				if (size > 0)
				{
					this.idbOutParameters[index].Size = size;
				}
			}
		}

		private void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
		{
			IDbDataParameter[] dbDataParameterArray = commandParameters;
			for (int i = 0; i < (int)dbDataParameterArray.Length; i++)
			{
				IDbDataParameter value = dbDataParameterArray[i];
				if (value.Direction == ParameterDirection.InputOutput && value.Value == null)
				{
					value.Value = DBNull.Value;
				}
				command.Parameters.Add(value);
			}
		}

		public void BeginTransaction()
		{
			if (this.idbTransaction == null)
			{
				this.idbTransaction = DBManagerFactory.GetTransaction(this.ProviderType);
			}
			this.idbCommand.Transaction = this.idbTransaction;
		}

		public void Close()
		{
			if (this.idbConnection.State != ConnectionState.Closed)
			{
				this.idbConnection.Close();
			}
		}

		public void CloseReader()
		{
			if (this.DataReader != null)
			{
				this.DataReader.Close();
			}
		}

		public void CommitTransaction()
		{
			if (this.idbTransaction != null)
			{
				this.idbTransaction.Commit();
			}
			this.idbTransaction = null;
		}

		public void CreateInParameters(int paramsCount)
		{
			this.idbInParameters = new IDbDataParameter[paramsCount];
			this.idbInParameters = DBManagerFactory.GetParameters(this.ProviderType, paramsCount);
		}

		public void CreateOutParameters(int paramsCount)
		{
			this.idbOutParameters = new IDbDataParameter[paramsCount];
			this.idbOutParameters = DBManagerFactory.GetParameters(this.ProviderType, paramsCount);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Close();
			this.idbCommand = null;
			this.idbTransaction = null;
			this.idbConnection = null;
		}

		public DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
			this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
			this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.inParameters, this.outParameters);
			IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
			dataAdapter.SelectCommand = this.idbCommand;
			DataSet dataSet = new DataSet();
			dataAdapter.Fill(dataSet);
			this.idbCommand.Parameters.Clear();
			return dataSet;
		}

		public void ExecuteNonQuery(CommandType commandType, string commandText)
		{
			this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
			this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.inParameters, this.outParameters);
			this.idbCommand.ExecuteNonQuery();
			this.idbCommand.Parameters.Clear();
		}

		public IDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
			this.idbCommand.Connection = this.Connection;
			this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.inParameters, this.outParameters);
			this.DataReader = this.idbCommand.ExecuteReader();
			this.idbCommand.Parameters.Clear();
			return this.DataReader;
		}

		public object ExecuteScalar(CommandType commandType, string commandText)
		{
			this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
			this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.inParameters, this.outParameters);
			object obj = this.idbCommand.ExecuteScalar();
			this.idbCommand.Parameters.Clear();
			return obj;
		}

		public void Open()
		{
			this.idbConnection = DBManagerFactory.GetConnection(this.providerType);
			this.idbConnection.ConnectionString = this.ConnectionString;
			if (this.idbConnection.State != ConnectionState.Open)
			{
				this.idbConnection.Open();
			}
			this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
		}

		private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandInParameters, IDbDataParameter[] commandOutParameters)
		{
			command.Connection = connection;
			command.CommandText = commandText;
			command.CommandType = commandType;
			if (transaction != null)
			{
				command.Transaction = transaction;
			}
			if (commandInParameters != null)
			{
				this.AttachParameters(command, commandInParameters);
			}
			if (commandOutParameters != null)
			{
				this.AttachParameters(command, commandOutParameters);
			}
		}
	}
}