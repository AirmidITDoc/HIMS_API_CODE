using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using LinqToDB.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HIMS.Core;
using HIMS.Core.Infrastructure;


namespace HIMS.Data.DataProviders
{
    public abstract class BaseDataProvider
    {
        #region Utils

        /// <summary>
        /// Gets an additional mapping schema
        /// </summary>
        private MappingSchema GetMappingSchema()
        {
            if (Singleton<MappingSchema>.Instance is null)
            {
                Singleton<MappingSchema>.Instance = new MappingSchema(ConfigurationName)
                {
                    //  MetadataReader = new FluentMigratorMetadataReader()
                };
            }

            return Singleton<MappingSchema>.Instance;

        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<DataConnection> CreateDataConnectionAsync()
        {
            return await CreateDataConnectionAsync(LinqToDbDataProvider);
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        protected virtual DataConnection CreateDataConnection()
        {
            return CreateDataConnection(LinqToDbDataProvider);
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the database connection
        /// </returns>
        protected virtual async Task<DataConnection> CreateDataConnectionAsync(LinqToDB.DataProvider.IDataProvider dataProvider)
        {
            DataConnection dataContext = new();
            return dataContext;
        }

        /// <summary>
        /// Creates the log database connection
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the database connection
        /// </returns>
        protected virtual async Task<DataConnection> CreateLogDataConnectionAsync(LinqToDB.DataProvider.IDataProvider dataProvider)
        {
            DataConnection dataContext = new();

            return dataContext;
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <returns>Database connection</returns>
        protected virtual DataConnection CreateDataConnection(LinqToDB.DataProvider.IDataProvider dataProvider)
        {
            if (dataProvider is null)
            {
                throw new ArgumentNullException(nameof(dataProvider));
            }

            DataConnection dataContext = new();

            return dataContext;
        }

        /// <summary>
        /// Creates a connection to a database
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the connection to a database
        /// </returns>
        protected virtual async Task<IDbConnection> CreateDbConnectionAsync(string connectionString = null)
        {
            DbConnection dbConnection = null;

            return dbConnection;
        }

        /// <summary>
        /// Creates a connection to a database
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection to a database</returns>
        protected virtual IDbConnection CreateDbConnection(string connectionString = null)
        {
            DbConnection dbConnection = null;

            return dbConnection;
        }

        #endregion

        #region Methods



        /// <summary>
        /// Executes command asynchronously and returns number of affected records
        /// </summary>
        /// <param name="sql">Command text</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of records, affected by command execution.
        /// </returns>
        public virtual async Task<int> ExecuteNonQueryAsync(string sql, params DataParameter[] parameters)
        {
            using DataConnection dataContext = await CreateDataConnectionAsync();
            CommandInfo command = new(dataContext, sql, parameters);
            int affectedRecords = await command.ExecuteAsync();
            //UpdateOutputParameters(dataContext, parameters);
            return affectedRecords;
        }

        /// <summary>
        /// Executes command asynchronously and  and returns
        /// single value
        /// </summary>
        /// <param name="sql">Command text</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the value of specified type
        /// </returns>
        public virtual async Task<T> ExecuteNonQueryAsync<T>(string sql, params DataParameter[] parameters)
        {
            using DataConnection dataContext = await CreateDataConnectionAsync();
            CommandInfo command = new(dataContext, sql, parameters);
            T affectedRecords = await command.ExecuteAsync<T>();
            //UpdateOutputParameters(dataContext, parameters);
            return affectedRecords;
        }

        public virtual async Task<object> ExecuteAsync(string sql, params DataParameter[] parameters)
        {
            using DataConnection dataContext = await CreateDataConnectionAsync();
            CommandInfo command = new(dataContext, sql, parameters);
            object affectedRecords = await command.ExecuteAsync<object>();
            //UpdateOutputParameters(dataContext, parameters);
            return affectedRecords;
        }

        /// <summary>
        /// Executes SQL command and returns results as collection of values of specified type
        /// </summary>
        /// <typeparam name="T">Type of result items</typeparam>
        /// <param name="sql">SQL command text</param>
        /// <param name="parameters">Parameters to execute the SQL command</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the collection of values of specified type
        /// </returns>
        public virtual async Task<IList<T>> QueryAsync<T>(string sql, params DataParameter[] parameters)
        {
            using DataConnection dataContext = await CreateDataConnectionAsync();
            return dataContext.Query<T>(sql, parameters).ToList();
        }

        /// <summary>
        /// Executes command using LinqToDB.Mapping.StoredProcedure command type and returns
        /// number of affected records.
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of records, affected by command execution.
        /// </returns>
        public virtual async Task<int> ExecuteStoredProcedureAsync(string procedureName, params DataParameter[] parameters)
        {
            using DataConnection dataContext = await CreateDataConnectionAsync();
            CommandInfo command = new(dataContext, procedureName, parameters);
            int affectedRecords = await command.ExecuteProcAsync();
            //UpdateOutputParameters(dataContext, parameters);
            return affectedRecords;
        }

        /// <summary>
        /// Executes command using LinqToDB.Mapping.StoredProcedure command type and returns
        /// single value
        /// </summary>
        /// <typeparam name="T">Result record type</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the value of specified type
        /// </returns>
        public virtual async Task<T> ExecuteStoredProcedureAsync<T>(string procedureName, params DataParameter[] parameters)
        {
            using DataConnection dataContext = await CreateDataConnectionAsync();
            CommandInfo command = new(dataContext, procedureName, parameters);
            T result = await command.ExecuteProcAsync<T>();
            //UpdateOutputParameters(dataContext, parameters);
            return result;
        }

        /// <summary>
        /// Executes command using System.Data.CommandType.StoredProcedure command type and
        /// returns results as collection of values of specified type
        /// </summary>
        /// <typeparam name="T">Result record type</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the returns collection of query result records
        /// </returns>
        public virtual async Task<IList<T>> QueryProcAsync<T>(string procedureName, params DataParameter[] parameters)
        {
            using DataConnection dataContext = await CreateDataConnectionAsync();
            CommandInfo command = new(dataContext, procedureName, parameters);
            List<T> rez = command.QueryProc<T>().ToList();
            //UpdateOutputParameters(dataContext, parameters);
            return rez;
        }
        public virtual IList<T> QueryProc<T>(string procedureName, params DataParameter[] parameters)
        {
            using DataConnection dataContext = CreateDataConnection();
            CommandInfo command = new(dataContext, procedureName, parameters);
            List<T> rez = command.QueryProc<T>().ToList();
            //UpdateOutputParameters(dataContext, parameters);
            return rez;
        }
        public virtual System.Data.DataSet ExecuteStoredProcedureForDataSetAsync(string procedureName, params DataParameter[] parameters)
        {
            using DataConnection dataContext = CreateDataConnection();
            dataContext.CommandTimeout = 200;
            System.Data.DataSet ds = new();

            using (DataReader dataReader = dataContext.ExecuteReader(procedureName, System.Data.CommandType.StoredProcedure, System.Data.CommandBehavior.Default, parameters))
            {
                do
                {
                    if (dataReader.Reader.Read())
                    {
                        var dataTable = new DataTable();
                        string[] fieldNames = Enumerable.Range(0, dataReader.Reader.FieldCount).Select(i => dataReader.Reader.GetName(i)).ToArray();
                        foreach (string field in fieldNames)
                        {
                            dataTable.Columns.Add(field);
                        }

                        do
                        {
                            DataRow dr = dataTable.NewRow();
                            for (int index = 0; index < fieldNames.Length; index++)
                            {
                                dr[fieldNames[index]] = dataReader.Reader.GetValue(index);
                            }
                            dataTable.Rows.Add(dr);
                        } while (dataReader.Reader.Read());
                        ds.Tables.Add(dataTable);
                    }

                } while (dataReader.Reader.NextResult());
            }
            return ds;
        }

        #endregion



        #region Properties

        /// <summary>
        /// Linq2Db data provider
        /// </summary>
        protected abstract LinqToDB.DataProvider.IDataProvider LinqToDbDataProvider { get; }


        /// <summary>
        /// Database connection string
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected static async Task<string> GetCurrentConnectionStringAsync()
        {
            return (await DataSettingsManager.LoadSettingsAsync()).ConnectionString;
        }

        /// <summary>
        /// Database connection string
        /// </summary>
        protected static string GetCurrentConnectionString()
        {
            return DataSettingsManager.LoadSettings().ConnectionString;
        }

        /// <summary>
        /// Log Database connection string
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected static async Task<string> GetLogConnectionStringAsync()
        {
            return (await DataSettingsManager.LoadSettingsAsync()).LogConnectionString;
        }

        /// <summary>
        /// Database connection string
        /// </summary>
        protected static string GetLogConnectionString()
        {
            return DataSettingsManager.LoadSettings().LogConnectionString;
        }

        /// <summary>
        /// Name of database provider
        /// </summary>
        public string ConfigurationName => LinqToDbDataProvider.Name;

        #endregion
    }
}