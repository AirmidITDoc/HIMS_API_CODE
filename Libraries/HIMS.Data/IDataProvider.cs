using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HIMS.Core;

namespace HIMS.Data
{
    /// <summary>
    /// Represents a data provider
    /// </summary>
    public partial interface IDataProvider
    {
        #region Methods

        /// <summary>
        /// Gets the name of an index
        /// </summary>
        /// <param name="targetTable">Target table name</param>
        /// <param name="targetColumn">Target column name</param>
        /// <returns>Name of an index</returns>
        string GetIndexName(string targetTable, string targetColumn);

        /// <summary>
        /// Re-index database tables
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task ReIndexTablesAsync();

        
        /// <summary>
        /// Executes command asynchronously and returns number of affected records
        /// </summary>
        /// <param name="sql">Command text</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of records, affected by command execution.
        /// </returns>
        Task<int> ExecuteNonQueryAsync(string sql, params DataParameter[] parameters);

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
        Task<T> ExecuteNonQueryAsync<T>(string sql, params DataParameter[] parameters);
        Task<object> ExecuteAsync(string sql, params DataParameter[] parameters);

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
        Task<IList<T>> QueryAsync<T>(string sql, params DataParameter[] parameters);

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
        Task<int> ExecuteStoredProcedureAsync(string procedureName, params DataParameter[] parameters);

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
        Task<T> ExecuteStoredProcedureAsync<T>(string procedureName, params DataParameter[] parameters);

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
        Task<IList<T>> QueryProcAsync<T>(string procedureName, params DataParameter[] parameters);
        IList<T> QueryProc<T>(string procedureName, params DataParameter[] parameters);
        System.Data.DataSet ExecuteStoredProcedureForDataSetAsync(string procedureName, params DataParameter[] parameters);

        #endregion

        #region Properties

        /// <summary>
        /// Name of database provider
        /// </summary>
        string ConfigurationName { get; }

        /// <summary>
        /// Gets allowed a limit input value of the data for hashing functions, returns 0 if not limited
        /// </summary>
        int SupportedLengthOfBinaryHash { get; }

        /// <summary>
        /// Gets a value indicating whether this data provider supports backup
        /// </summary>
        bool BackupSupported { get; }

        #endregion

        #region Log Methods

     
        #endregion
    }
}