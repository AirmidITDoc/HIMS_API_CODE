using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HIMS.Data.DataProviders
{
    public class DatabaseHelper : IDisposable
    {
        private readonly SqlConnection objConnection;

        public static string ConnectionString { get; set; }
        public SqlBulkCopy bulkCopy;
        public bool HasError { get; set; } = false;

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
            p.Value = value;
            if ((p.SqlDbType == SqlDbType.VarChar) || (p.SqlDbType == SqlDbType.NVarChar))
            {
                p.Size = (p.SqlDbType == SqlDbType.VarChar) ? 8000 : 4000;

                if ((value != null) && !(value is DBNull) && (value.ToString().Length > p.Size))
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
            p.Value = value;
            if ((p.SqlDbType == SqlDbType.VarChar) || (p.SqlDbType == SqlDbType.NVarChar))
            {
                p.Size = (p.SqlDbType == SqlDbType.VarChar) ? 8000 : 4000;

                if ((value != null) && !(value is DBNull) && (value.ToString().Length > p.Size))
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

        public string ExecuteNonQuery(string query, CommandType commandtype, string outputParam ,Dictionary<string, object> entity = null)
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
            int i = -1;
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

        public string ExecuteNonQuery(string query, CommandType commandtype, string outputparam, SqlParameter[] para = null)
        {
            Command.CommandText = query;
            Command.CommandType = commandtype;
            string outputParamValue = "";
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
            using (var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString))
            {
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
        }
        public async static Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType = CommandType.StoredProcedure, SqlParameter[] commandParameters=null)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using (var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString))
            {
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
        }

        
        #endregion ExecuteScalarAsync        

        public SqlDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text);
        }

        public SqlDataReader ExecuteReader(string query, CommandType commandtype)
        {
            Command.CommandText = query;
            Command.CommandType = commandtype;
            SqlDataReader reader = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                reader = Command.ExecuteReader(CommandBehavior.CloseConnection);
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
            return reader;
        }

        public DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text);
        }

        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            Command.CommandText = query;
            Command.CommandType = commandtype;
            adapter.SelectCommand = Command;
            DataSet ds = new DataSet();
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
            DataSet ds = new DataSet();
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            SqlDataAdapter da = new SqlDataAdapter(Command);
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
            using (var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var cmd = new SqlCommand
                {
                    CommandTimeout = 180
                };
                var mustCloseConnection = await PrepareCommandAsync(cmd, connection, null, CommandType.StoredProcedure, commandText, commandParameters).ConfigureAwait(false);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                    if (mustCloseConnection)
                        connection.Close();
                    return ds;
                }
            }
        }

        #endregion ExecuteDatasetAsync 

        public DataTable FetchDataTableBySP(string Spname, SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(para);
            Command.CommandText = Spname;
            SqlDataAdapter da = new SqlDataAdapter(Command);
            try
            {
                objConnection.Open();
                da.Fill(dt);
                return dt;
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
            return dt;
        }

        public DataTable FetchDataTableByQuery(string Qry, SqlParameter[] para = null)
        {
            DataTable dt = new DataTable();
            Command.CommandType = CommandType.Text;
            if (para != null)
                Command.Parameters.AddRange(para);
            Command.CommandText = Qry;
            SqlDataAdapter da = new SqlDataAdapter(Command);
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

        public async static Task<DataTable> FetchDataTableBySPAsync(string commandText,  params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using (var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString))
            {
                DataTable dt = new DataTable();
                var cmd = new SqlCommand
                {
                    CommandTimeout = 180
                };
                await PrepareCommandAsync(cmd, connection, null, CommandType.StoredProcedure, commandText, commandParameters).ConfigureAwait(false);
                var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                dt.Load(reader);
                return dt;
            }
        }

        public async static Task<DataTable> FetchDataTableByQueryAsync(string commandText,  params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using (var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString))
            {
                DataTable dt = new DataTable();
                var cmd = new SqlCommand
                {
                    CommandTimeout = 180
                };
                await PrepareCommandAsync(cmd, connection, null, CommandType.Text, commandText, commandParameters).ConfigureAwait(false);
                var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                dt.Load(reader);
                return dt;
            }
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
                                pro.SetValue(obj, new byte[0]);
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
                var dr = Command.ExecuteReader();
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

        public List<T> FetchListByQuery<T>(string Qry, SqlParameter[] para = null)
        {
            List<T> lst = new List<T>();
            Command.CommandType = CommandType.Text;
            Command.Parameters.AddRange(para);
            Command.CommandText = Qry;
            try
            {
                objConnection.Open();
                var dr = Command.ExecuteReader();
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
            using (var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString))
            {
                var cmd = new SqlCommand
                {
                    CommandTimeout = 180
                };
                await PrepareCommandAsync(cmd, connection, null, CommandType.StoredProcedure, commandText, commandParameters).ConfigureAwait(false);
                var dr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                List<T> lst = new List<T>();
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                        T item = GetListItem<T>(dr);
                        lst.Add(item);
                }
                return lst;
            }
        }

        public async static Task<List<T>> FetchListByQueryAsync<T>(string commandText, params SqlParameter[] commandParameters) 
        {
            if (string.IsNullOrEmpty(ConnectionStrings.MainDbConnectionString)) throw new ArgumentNullException("connectionString");
            using (var connection = new SqlConnection(ConnectionStrings.MainDbConnectionString))
            {
                var cmd = new SqlCommand
                {
                    CommandTimeout = 180
                };
                await PrepareCommandAsync(cmd, connection, null, CommandType.Text, commandText, commandParameters).ConfigureAwait(false);
                var dr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                List<T> lst = new();
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                        T item = GetListItem<T>(dr);
                        lst.Add(item);
                }
                return lst;
            }
        }

        #endregion ExecuteDatasetAsync 

        public DataSet SpExecuteDataSet(string SpName, SqlParameter[] parameter)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            Command.CommandText = SpName;
            Command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter sParam in parameter)
            {
                Command.Parameters.Add(sParam);
            }
            adapter.SelectCommand = Command;
            DataSet ds = new DataSet();
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
                //objCommand.Parameters.Clear();
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

        private void WriteToLog(string msg, string query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (msg == null)
                throw new ArgumentNullException(nameof(msg));

            try
            {
                /*string strLogFile = CurrentContext.Config.UploadPath + "Logfile.txt";
                if (strLogFile != "")
                {
                    System.IO.StreamWriter writer = System.IO.File.AppendText(strLogFile);
                    writer.WriteLine("Date and Time : " + DateTime.Now.ToString() + " - " + msg);
                    writer.WriteLine("Error in Query : " + query);
                    writer.WriteLine("");
                    writer.Close();
                }*/
            }
            catch { }
        }

        public void Dispose()
        {
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

