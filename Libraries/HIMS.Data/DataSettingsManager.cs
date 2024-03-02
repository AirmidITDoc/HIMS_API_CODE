using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;

namespace HIMS.Data
{
    /// <summary>
    /// Represents the database settings manager
    /// </summary>
    public static class DataSettingsManager
    {
        #region Methods

        /// <summary>
        /// Load database settings
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the data settings
        /// </returns>
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public static async Task<DataSettings> LoadSettingsAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            if (Singleton<DataSettings>.Instance != null)
            {
                return Singleton<DataSettings>.Instance;
            }

            return null;
        }

        /// <summary>
        /// Load database settings
        /// </summary>
        /// <returns>Data settings</returns>
        public static DataSettings LoadSettings()
        {
            if (Singleton<DataSettings>.Instance != null)
            {
                return Singleton<DataSettings>.Instance;
            }

            return null;
        }

        /// <summary>
        /// Initialize database settings
        /// </summary>
        public static void IntiDatabaseSettings(IServiceCollection services, DataSettings DBSettings)
        {
            SetSettings(DBSettings);
        }


        /// <summary>
        /// Set database settings
        /// </summary>
        /// <param name="connectionString">Primary database settings</param>
        /// <param name="logConnectionString">Log database settings</param>
        public static void SetSettings(DataSettings DBSettings)
        {
            DataSettings dataSettings = new()
            {
                ConnectionString = DBSettings.ConnectionString,
                LogConnectionString = DBSettings.LogConnectionString
            };

            Singleton<DataSettings>.Instance = dataSettings;
        }

        /// <summary>
        /// Gets the command execution timeout.
        /// </summary>
        /// <value>
        /// Number of seconds. Negative timeout value means that a default timeout will be used. 0 timeout value corresponds to infinite timeout.
        /// </value>
        /// <returns>A task that represents the asynchronous operation</returns>
        public static async Task<int> GetSqlCommandTimeoutAsync()
        {
            return (await LoadSettingsAsync())?.SQLCommandTimeout ?? -1;
        }

        /// <summary>
        /// Gets the command execution timeout.
        /// </summary>
        /// <value>
        /// Number of seconds. Negative timeout value means that a default timeout will be used. 0 timeout value corresponds to infinite timeout.
        /// </value>
        public static int GetSqlCommandTimeout()
        {
            return (LoadSettings())?.SQLCommandTimeout ?? -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets data provider
        /// </summary>
        public static IDataProvider DataProvider => new MsSqlDataProvider(); //MySqlDataProvider()

        #endregion
    }

    /// <summary>
    /// Represents the database settings
    /// </summary>
    public partial class DataSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets a connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets a log connection string
        /// </summary>
        public string LogConnectionString { get; set; }


        /// <summary>
        /// Gets or sets the wait time (in seconds) before terminating the attempt to execute a command and generating an error.
        /// By default, timeout isn't set and a default value for the current provider used. 
        /// Set 0 to use infinite timeout.
        /// </summary>
        public int? SQLCommandTimeout { get; set; }

        #endregion
    }
}