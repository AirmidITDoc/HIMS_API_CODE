using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Core;

namespace HIMS.Data.DataProviders
{
    /// <summary>
    /// Represents the MS SQL Server data provider
    /// </summary>
    public partial class MsSqlDataProvider : BaseDataProvider, IDataProvider
    {
        #region Fields

       // private static readonly Lazy<LinqToDB.DataProvider.IDataProvider> _dataProvider = new(() => new SqlServerDataProvider(ProviderName.SqlServer, SqlServerVersion.v2012, SqlServerProvider.SystemDataSqlClient), true);

        #endregion


        #region Methods


        /// <summary>
        /// Re-index database tables
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task ReIndexTablesAsync()
        {
            using DataConnection currentConnection = await CreateDataConnectionAsync();
            string commandText = $@"
                    DECLARE @TableName sysname 
                    DECLARE cur_reindex CURSOR FOR
                    SELECT table_name
                    FROM [{currentConnection.Connection.Database}].information_schema.tables
                    WHERE table_type = 'base table'
                    OPEN cur_reindex
                    FETCH NEXT FROM cur_reindex INTO @TableName
                    WHILE @@FETCH_STATUS = 0
                        BEGIN
                            exec('ALTER INDEX ALL ON [' + @TableName + '] REBUILD')
                            FETCH NEXT FROM cur_reindex INTO @TableName
                        END
                    CLOSE cur_reindex
                    DEALLOCATE cur_reindex";

            await currentConnection.ExecuteAsync(commandText);
        }

        /// <summary>
        /// Gets the name of an index
        /// </summary>
        /// <param name="targetTable">Target table name</param>
        /// <param name="targetColumn">Target column name</param>
        /// <returns>Name of an index</returns>
        public virtual string GetIndexName(string targetTable, string targetColumn)
        {
            return $"IX_{targetTable}_{targetColumn}";
        }


        #endregion

        #region Properties

        /// <summary>
        /// Sql server data provider
        /// </summary>
       // protected override LinqToDB.DataProvider.IDataProvider LinqToDbDataProvider => _dataProvider.Value;

        /// <summary>
        /// Gets allowed a limit input value of the data for hashing functions, returns 0 if not limited
        /// </summary>
        public int SupportedLengthOfBinaryHash { get; } = 8000;

        /// <summary>
        /// Gets a value indicating whether this data provider supports backup
        /// </summary>
        public virtual bool BackupSupported => true;

        protected override LinqToDB.DataProvider.IDataProvider LinqToDbDataProvider => throw new NotImplementedException();

        #endregion
    }
}
