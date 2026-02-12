using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Data
{
    public static class DatabaseHelper
    {

        public async static Task<bool> PrepareCommandAsync(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException("connection does not connect");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(commandText);
            var mustCloseConnection = false;
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                await connection.OpenAsync().ConfigureAwait(false);
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return mustCloseConnection;
        }
        private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException("connection does not connect");
            if (commandParameters == null) return;
            foreach (var p in commandParameters.Where(p => p != null))
            {
                if ((p.Direction == ParameterDirection.InputOutput ||
                     p.Direction == ParameterDirection.Input) &&
                    (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }
        }
        public static T GetListItem<T>(SqlDataReader dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower().Split('_')[0] == dr.GetName(i).ToLower().Split('_')[0])
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr[i])))
                        {
                            if (pro.PropertyType.Name == "String")
                                pro.SetValue(obj, Convert.ToString(dr[i]));
                            else if (pro.PropertyType.Name == "Byte[]" && string.IsNullOrEmpty(Convert.ToString(dr[i])))
                                pro.SetValue(obj, Array.Empty<byte>());
                            else
                                pro.SetValue(obj, dr[i]);
                        }
                        else
                        {
                            if (pro.PropertyType.Name == "String")
                                pro.SetValue(obj, "");
                        }
                        break;
                    }
                }
            }
            return obj;
        }
        public async static Task<DataTable> FetchDataTableAsync(string commandText, SqlParameter[] commandParameters, CommandType cmdType = CommandType.StoredProcedure)
        {
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            DataTable dt = new();
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            await PrepareCommandAsync(cmd, connection, null, cmdType, commandText, commandParameters).ConfigureAwait(false);
            var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            dt.Load(reader);
            return dt;
        }
        public async static Task IUD(string commandText, SqlParameter[] commandParameters)
        {
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            await PrepareCommandAsync(cmd, connection, null, CommandType.Text, commandText, commandParameters).ConfigureAwait(false);
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
        public async static Task<DataSet> FetchDataSetBySPAsync(string commandText, SqlParameter[] commandParameters)
        {
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            var mustCloseConnection = await PrepareCommandAsync(cmd, connection, null, CommandType.StoredProcedure, commandText, commandParameters).ConfigureAwait(false);
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds);
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                await connection.CloseAsync();
            return ds;
        }

    }
}
