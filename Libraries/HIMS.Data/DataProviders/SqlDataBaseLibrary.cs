using HIMS.Core.Domain.Grid;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HIMS.Data.DataProviders
{
    public class DatabaseHelper : IDisposable
    {
        private readonly SqlConnection objConnection;

        public static string? ConnectionString { get; set; }
        public SqlBulkCopy? bulkCopy;
        public bool HasError { get; set; } = false;

        // Added by vimal on 05/09/2025 => add transactions for multiple operations between ADO.net & entity framework.
        #region :: Traction Operations ::
        private DbConnection _connection;
        private DbTransaction _transaction;

        public void SetConnection(DbConnection connection)
        {
            _connection = connection;
        }

        public void SetTransaction(DbTransaction transaction)
        {
            _transaction = transaction;
        }

        public string ExecuteNonQueryNew(string storedProcName, CommandType cmdType, string outputParamName, Dictionary<string, object> parameters)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = cmdType;
            cmd.Transaction = _transaction; //ensure same transaction

            var outputParam = cmd.CreateParameter();
            if (parameters != null)
            {
                // Handle output param (if any)
                if (!string.IsNullOrWhiteSpace(outputParamName))
                {
                    outputParam.ParameterName = "@" + outputParamName;
                    outputParam.Direction = ParameterDirection.Output;
                    outputParam.DbType = DbType.Int32;
                    cmd.Parameters.Add(outputParam);
                    parameters.Remove(outputParamName);
                }
                foreach (var kvp in parameters.Where(x => x.Value != null))
                {
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@" + kvp.Key;
                    param.Value = kvp.Value ?? DBNull.Value;
                    cmd.Parameters.Add(param);
                }
            }
            cmd.ExecuteNonQuery();
            return outputParam.Value?.ToString();

        }

        #endregion
        public DatabaseHelper()
        {
            objConnection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            Command = new SqlCommand
            {
                CommandTimeout = 120,
                Connection = objConnection
            };
        }

        public DatabaseHelper(string strDBConn, bool IsBulCopy = false, bool IsKeepIdentityBulCopy = false)
        {
            objConnection = new SqlConnection(strDBConn);
            Command = new SqlCommand
            {
                CommandTimeout = 120,
                Connection = objConnection
            };
            if (IsBulCopy)
            {
                if (IsKeepIdentityBulCopy)
                {
                    bulkCopy = new SqlBulkCopy(objConnection, SqlBulkCopyOptions.KeepIdentity, null);
                }
                else
                {
                    bulkCopy = new SqlBulkCopy(objConnection, SqlBulkCopyOptions.UseInternalTransaction, null);
                }
            }
        }

        public SqlParameter AddParameter(string name, object value)
        {
            SqlParameter p = Command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            if ((p.SqlDbType == SqlDbType.VarChar) || (p.SqlDbType == SqlDbType.NVarChar))
            {
                p.Size = (p.SqlDbType == SqlDbType.VarChar) ? 8000 : 4000;

                if (value != null && value is not DBNull && value.ToString()!.Length > p.Size)
                    p.Size = -1;
            }
            return Command.Parameters.Add(p);
        }

        public SqlParameter AddParameter(string name, object value, ParamType type)
        {
            SqlParameter p = Command.CreateParameter();
            if (type == ParamType.Output)
                p.Direction = ParameterDirection.Output;
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            if ((p.SqlDbType == SqlDbType.VarChar) || (p.SqlDbType == SqlDbType.NVarChar))
            {
                p.Size = (p.SqlDbType == SqlDbType.VarChar) ? 8000 : 4000;

                if (value != null && value is not DBNull && value.ToString()!.Length > p.Size)
                    p.Size = -1;
            }
            return Command.Parameters.Add(p);
        }

        public SqlParameter AddParameter(SqlParameter parameter)
        {
            return Command.Parameters.Add(parameter);
        }

        public void ClearParameters()
        {
            Command.Parameters.Clear();
        }

        public SqlCommand Command { get; }

        public bool ExecuteBulkCopy(DataTable dt)
        {
            bool result = false;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }

                bulkCopy.BulkCopyTimeout = 0;

                bulkCopy.WriteToServer(dt);

                result = true;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, "ExecuteBulkCopy");
            }
            finally
            {
                bulkCopy.Close();

                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return result;
        }

        public void BeginTransaction()
        {
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
            Command.Transaction = objConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Command.Transaction.Commit();
            objConnection.Close();
        }

        public void RollbackTransaction()
        {
            Command.Transaction.Rollback();
            objConnection.Close();
        }

        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text);
        }

        public int ExecuteNonQuery(string query, CommandType commandtype, SqlParameter[] para = null)
        {
            Command.Parameters.Clear();
            Command.CommandText = query;
            Command.CommandType = commandtype;
            if (para?.Length > 0)
            {
                Command.Parameters.AddRange(para);
            }
            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SqlException sx = (SqlException)ex;
                if (sx.Number == 547)       // Foreign Key Error
                    i = sx.Number;
                else
                    HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return i;
        }

        public string ExecuteNonQuery(string query, CommandType commandtype, string outputParam, Dictionary<string, object> entity = null)
        {
            Command.Parameters.Clear();
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@" + outputParam,
                Value = 0,
                Direction = ParameterDirection.Output
            };
            if (!string.IsNullOrEmpty(outputParam))
            {
                entity.Remove(outputParam);
            }
            int sp_Para = 0;
            SqlParameter[] para = new SqlParameter[entity.Count];
            foreach (var property in entity)
            {
                var param = new SqlParameter
                {
                    ParameterName = "@" + property.Key,
                    Value = (object)property.Value
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            Command.CommandText = query;
            Command.CommandType = commandtype;
            string outputParamValue = "";
            if (para?.Length > 0)
            {
                Command.Parameters.AddRange(para);
                if (!string.IsNullOrEmpty(outputParam))
                {
                    Command.Parameters.Add(outputId);
                }
            }
            int i;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = Command.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(outputParam))
                {
                    outputParamValue = Command.Parameters[outputId.ParameterName].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                SqlException sx = (SqlException)ex;
                if (sx.Number == 547)       // Foreign Key Error
                    i = sx.Number;
                else
                    HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return outputParamValue;
        }

        public int ExecuteNonQuery(string query, CommandType commandtype, Dictionary<string, object> entity)
        {
            Command.Parameters.Clear();
            int sp_Para = 0;
            SqlParameter[] para = new SqlParameter[entity.Count];
            foreach (var property in entity)
            {
                var param = new SqlParameter
                {
                    ParameterName = "@" + property.Key,
                    Value = (object)property.Value
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            Command.CommandText = query;
            Command.CommandType = commandtype;
            if (para?.Length > 0)
            {
                Command.Parameters.AddRange(para);
            }
            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SqlException sx = (SqlException)ex;
                if (sx.Number == 547)       // Foreign Key Error
                    i = sx.Number;
                else
                    HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return i;
        }

        public string ExecuteNonQuery(string query, CommandType commandtype, string outputparam, SqlParameter[] para = null)
        {
            Command.CommandText = query;
            Command.CommandType = commandtype;
            string outputParamValue = "";
            if (para?.Length > 0)
            {
                Command.Parameters.AddRange(para);
            }
            int i;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = Command.ExecuteNonQuery();
                outputParamValue = Command.Parameters[outputparam].Value.ToString();
            }
            catch (Exception ex)
            {
                SqlException sx = (SqlException)ex;
                if (sx.Number == 547)       // Foreign Key Error
                    i = sx.Number;
                else
                    HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return outputParamValue;
        }

        public string ExecuteNonQuery(string query, CommandType commandtype, string outputparam)
        {
            Command.CommandText = query;
            Command.CommandType = commandtype;
            string outputParamValue = "";
            int i;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = Command.ExecuteNonQuery();
                outputParamValue = Command.Parameters[outputparam].Value.ToString();
            }
            catch (Exception ex)
            {
                SqlException sx = (SqlException)ex;
                if (sx.Number == 547)       // Foreign Key Error
                {
                    i = sx.Number;
                }
                else
                {
                    Command.Transaction.Rollback();
                    HandleExceptions(ex, query);
                }
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return outputParamValue;
        }

        public object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text);
        }

        public object ExecuteScalar(string query, CommandType commandtype, SqlParameter[] para = null)
        {
            Command.CommandText = query;
            Command.CommandType = commandtype;
            if (para?.Length > 0)
            {
                Command.Parameters.AddRange(para);
            }
            object o = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                o = Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return o;
        }

        #region ExecuteScalarAsync

        public async static Task<object> ExecuteScalarAsync(string commandText, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            var mustCloseConnection = await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters).ConfigureAwait(false);
            var retval = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }
        public async static Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType = CommandType.StoredProcedure, SqlParameter[] commandParameters = null)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            var mustCloseConnection = await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters).ConfigureAwait(false);
            var retval = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }


        #endregion ExecuteScalarAsync        



        public DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text);
        }


        #region ExecuteDataTableAsync
        private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
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
        private async static Task<bool> PrepareCommandAsync(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
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
        public async static Task<DataSet> FetchDataSetBySPAsync(string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            var mustCloseConnection = await PrepareCommandAsync(cmd, connection, null, CommandType.StoredProcedure, commandText, commandParameters).ConfigureAwait(false);
            using var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds);
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return ds;
        }

        #endregion ExecuteDatasetAsync 

        public DataTable FetchDataTableBySP(string Spname, SqlParameter[] para, bool IsForMoneyToDecimal = false)
        {
            DataTable dt = new();
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            SqlDataAdapter da = new(Command);
            try
            {
                objConnection.Open();
                da.Fill(dt);
                if (IsForMoneyToDecimal)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.DataType == typeof(decimal))
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[column] != DBNull.Value)
                                {
                                    row[column] = Math.Round((decimal)row[column], 2);
                                }
                            }
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                Command.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return dt;
        }

        public DataTable FetchDataTableByQuery(string Qry, SqlParameter[] para = null)
        {
            DataTable dt = new();
            Command.CommandType = CommandType.Text;
            if (para != null)
                Command.Parameters.AddRange(para);
            Command.CommandText = Qry;
            SqlDataAdapter da = new(Command);
            try
            {
                objConnection.Open();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Qry);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return dt;
        }

        #region ExecuteDataTableAsync

        public async static Task<DataTable> FetchDataTableBySPAsync(string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            DataTable dt = new();
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            await PrepareCommandAsync(cmd, connection, null, CommandType.StoredProcedure, commandText, commandParameters).ConfigureAwait(false);
            using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            dt.Load(reader);
            return dt;
        }

        public async static Task<DataTable> FetchDataTableByQueryAsync(string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            DataTable dt = new();
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            await PrepareCommandAsync(cmd, connection, null, CommandType.Text, commandText, commandParameters).ConfigureAwait(false);
            using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            dt.Load(reader);
            return dt;
        }

        #endregion ExecuteDatasetAsync 
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

        public List<T> FetchListBySP<T>(string Spname, SqlParameter[] para)
        {
            List<T> lst = new();
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            try
            {
                objConnection.Open();
                using var dr = Command.ExecuteReader();
                while (dr.Read())
                {
                    T item = GetListItem<T>(dr);
                    lst.Add(item);
                }
                return lst;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return lst;
        }

        #region :: Multiple Result Sets ::

        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            SqlDataAdapter adapter = new();
            Command.CommandText = query;
            Command.CommandType = commandtype;
            adapter.SelectCommand = Command;
            DataSet ds = new();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }

        public DataSet FetchDataSetBySP(string Spname, SqlParameter[] para)
        {
            DataSet ds = new();
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            SqlDataAdapter da = new(Command);
            try
            {
                objConnection.Open();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }
        public async Task<Tuple<List<T>, List<T1>>> Get2ResultsFromSp<T, T1>(string Spname, SqlParameter[] para) where T : new() where T1 : new()
        {
            Tuple<List<T>, List<T1>> result = null;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            try
            {
                objConnection.Open();
                using var reader = await Command.ExecuteReaderAsync();
                // Table 1
                var item1 = reader.MapToList<T>();

                // Table 2
                await reader.NextResultAsync();
                var item2 = reader.MapToList<T1>();

                result = new Tuple<List<T>, List<T1>>(item1, item2);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }

            return result;
        }
        public async Task<Tuple<List<T>, List<T1>, List<T2>>> Get2ResultsFromSp<T, T1, T2>(string Spname, SqlParameter[] para) where T : new() where T1 : new() where T2 : new()
        {
            Tuple<List<T>, List<T1>, List<T2>> result = null;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            try
            {
                objConnection.Open();
                using var reader = await Command.ExecuteReaderAsync();
                // Table 1
                var item1 = reader.MapToList<T>();

                // Table 2
                await reader.NextResultAsync();
                var item2 = reader.MapToList<T1>();
                // Table 3
                await reader.NextResultAsync();
                var item3 = reader.MapToList<T2>();

                result = new Tuple<List<T>, List<T1>, List<T2>>(item1, item2, item3);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }

            return result;
        }
        public async Task<Tuple<List<T>, List<T1>, List<T2>, List<T3>>> Get2ResultsFromSp<T, T1, T2, T3>(string Spname, SqlParameter[] para) where T : new() where T1 : new() where T2 : new() where T3 : new()
        {
            Tuple<List<T>, List<T1>, List<T2>, List<T3>> result = null;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            try
            {
                objConnection.Open();
                using var reader = await Command.ExecuteReaderAsync();
                // Table 1
                var item1 = reader.MapToList<T>();

                // Table 2
                await reader.NextResultAsync();
                var item2 = reader.MapToList<T1>();
                // Table 3
                await reader.NextResultAsync();
                var item3 = reader.MapToList<T2>();
                // Table 4
                await reader.NextResultAsync();
                var item4 = reader.MapToList<T3>();

                result = new Tuple<List<T>, List<T1>, List<T2>, List<T3>>(item1, item2, item3, item4);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }

            return result;
        }
        public async Task<Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>>> Get2ResultsFromSp<T, T1, T2, T3, T4>(string Spname, SqlParameter[] para) where T : new() where T1 : new() where T2 : new() where T3 : new() where T4 : new()
        {
            Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>> result = null;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            try
            {
                objConnection.Open();
                using var reader = await Command.ExecuteReaderAsync();
                // Table 1
                var item1 = reader.MapToList<T>();

                // Table 2
                await reader.NextResultAsync();
                var item2 = reader.MapToList<T1>();
                // Table 3
                await reader.NextResultAsync();
                var item3 = reader.MapToList<T2>();
                // Table 4
                await reader.NextResultAsync();
                var item4 = reader.MapToList<T3>();
                // Table 5
                await reader.NextResultAsync();
                var item5 = reader.MapToList<T4>();

                result = new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>>(item1, item2, item3, item4, item5);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }

            return result;
        }
        public async Task<Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>>> Get2ResultsFromSp<T, T1, T2, T3, T4, T5>(string Spname, SqlParameter[] para) where T : new() where T1 : new() where T2 : new() where T3 : new() where T4 : new() where T5 : new()
        {
            Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>> result = null;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            try
            {
                objConnection.Open();
                using var reader = await Command.ExecuteReaderAsync();
                // Table 1
                var item1 = reader.MapToList<T>();

                // Table 2
                await reader.NextResultAsync();
                var item2 = reader.MapToList<T1>();
                // Table 3
                await reader.NextResultAsync();
                var item3 = reader.MapToList<T2>();
                // Table 4
                await reader.NextResultAsync();
                var item4 = reader.MapToList<T3>();
                // Table 5
                await reader.NextResultAsync();
                var item5 = reader.MapToList<T4>();
                // Table 6
                await reader.NextResultAsync();
                var item6 = reader.MapToList<T5>();

                result = new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>>(item1, item2, item3, item4, item5, item6);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }

            return result;
        }
        public async Task<Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>>> Get7ResultsFromSp<T, T1, T2, T3, T4, T5,T6>(string Spname, SqlParameter[] para) where T : new() where T1 : new() where T2 : new() where T3 : new() where T4 : new() where T5 : new() where T6 : new()
        {
            Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>> result = null;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            try
            {
                objConnection.Open();
                using var reader = await Command.ExecuteReaderAsync();
                // Table 1
                var item1 = reader.MapToList<T>();

                // Table 2
                await reader.NextResultAsync();
                var item2 = reader.MapToList<T1>();
                // Table 3
                await reader.NextResultAsync();
                var item3 = reader.MapToList<T2>();
                // Table 4
                await reader.NextResultAsync();
                var item4 = reader.MapToList<T3>();
                // Table 5
                await reader.NextResultAsync();
                var item5 = reader.MapToList<T4>();
                // Table 6
                await reader.NextResultAsync();
                var item6 = reader.MapToList<T5>();
                // Table 6
                await reader.NextResultAsync();
                var item7 = reader.MapToList<T6>();

                result = new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>>(item1, item2, item3, item4, item5, item6, item7);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }

            return result;
        }

        #endregion :: Multiple Result Sets ::
        public List<T> FetchListByQuery<T>(string Qry, SqlParameter[] para = null)
        {
            List<T> lst = new();
            Command.CommandType = CommandType.Text;
            Command.Parameters.AddRange(para);
            Command.CommandText = Qry;
            try
            {
                objConnection.Open();
                using var dr = Command.ExecuteReader();
                while (dr.Read())
                {
                    T item = GetListItem<T>(dr);
                    lst.Add(item);
                }
                return lst;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Qry);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return lst;
        }

        #region ExecuteTAsync        

        public async static Task<List<T>> FetchListBySPAsync<T>(string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            await PrepareCommandAsync(cmd, connection, null, CommandType.StoredProcedure, commandText, commandParameters).ConfigureAwait(false);
            using var dr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            List<T> lst = new();
            while (await dr.ReadAsync().ConfigureAwait(false))
            {
                T item = GetListItem<T>(dr);
                lst.Add(item);
            }
            return lst;
        }

        public async static Task<List<T>> FetchListByQueryAsync<T>(string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180
            };
            await PrepareCommandAsync(cmd, connection, null, CommandType.Text, commandText, commandParameters).ConfigureAwait(false);
            using var dr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            List<T> lst = new();
            while (await dr.ReadAsync().ConfigureAwait(false))
            {
                T item = GetListItem<T>(dr);
                lst.Add(item);
            }
            return lst;
        }

        #endregion ExecuteDatasetAsync 

        #region Grid By SP
        public static async Task<IPagedList<T>> GetGridDataBySp<T>(GridRequestModel objGrid, string procedureName)
        {
            List<SqlParameter> parameters = new();
            foreach (var filter in objGrid.Filters)
            {
                parameters.Add(new SqlParameter
                {
                    ParameterName = "@" + filter.FieldName,
                    Value = filter.FieldValue
                });
            }
            parameters.Add(new SqlParameter() { ParameterName = "@Start", Value = objGrid.First });
            parameters.Add(new SqlParameter() { ParameterName = "@Length", Value = objGrid.Rows });
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using var dataContext = new SqlConnection(ConnectionStrings.MainDbConnectionString);
            var cmd = new SqlCommand
            {
                CommandTimeout = 180,
                CommandText = procedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = dataContext
            };
            await dataContext.OpenAsync();
            cmd.Parameters.AddRange(parameters.ToArray());
            List<T> results = new();
            int totalRows = 0;
            using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
            {
                bool IsSetTotal = false;
                do
                {
                    if (IsSetTotal)
                    {
                        results = DataReaderMapToList<T>(dataReader);
                    }
                    else
                    {
                        if (await dataReader.ReadAsync())
                        {
                            string[] fieldNames = Enumerable.Range(0, dataReader.FieldCount).Select(i => dataReader.GetName(i)).ToArray();

                            if (fieldNames[0].ToLower() == "total_row")
                            {
                                totalRows = Convert.ToInt32(dataReader.GetValue(0));
                                IsSetTotal = true;
                            }
                        }
                    }
                } while (await dataReader.NextResultAsync());
            }
            await dataContext.CloseAsync();
            var data = new PagedList<T>(results, objGrid.First, objGrid.Rows, totalRows);
            return data;
        }
        public static List<T> DataReaderMapToList<T>(SqlDataReader dr)
        {
            List<T> list = new();
            while (dr.Read())
            {
                T obj = GetListItem<T>(dr);
                list.Add(obj);
            }

            return list;
        }
        #endregion

        public DataSet SpExecuteDataSet(string SpName, SqlParameter[] parameter)
        {
            SqlDataAdapter adapter = new();
            Command.CommandText = SpName;
            Command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter sParam in parameter)
            {
                Command.Parameters.Add(sParam);
            }
            adapter.SelectCommand = Command;
            DataSet ds = new();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, SpName);
            }
            finally
            {
                if (Command.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }

        private void HandleExceptions(Exception ex, string query)
        {
            HasError = true;
            WriteToLog(ex.Message, query);
        }

        private static void WriteToLog(string msg, string query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (msg == null)
                throw new ArgumentNullException(nameof(msg));

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Command.Parameters.Clear();
            objConnection.Close();
            objConnection.Dispose();
            Command.Dispose();

        }


        public enum ParamType
        {
            Input, Output, InputOutput
        }
    }
}

