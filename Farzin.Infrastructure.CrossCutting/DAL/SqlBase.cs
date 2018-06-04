using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Farzin.Infrastructure.DAL
{
    public abstract class SqlBase : IDisposable
    {
        protected SqlConnection _sqlConnection;
        private string _connectionString;
        public SqlBase(string connection)
        {
            if (connection.IndexOf(';') != -1)
                _connectionString = connection;
            else
                _connectionString = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }

        private List<T> getList<T>(SqlDataReader reader)
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();
            bool isTValueType = properties.Length == 0 && typeof(T).IsValueType;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                if (isTValueType && reader.FieldCount == 1)
                {
                    if (!reader.IsDBNull(0))
                    {
                        item = (T)reader[0];
                    }
                }
                else
                {
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                        }
                    }
                }
                results.Add(item);
            }
            return results;
        }
        protected List<T> executeQuery<T>(string query, bool keepConnectionOpen)
        {
            List<T> result;
            tryOpenConnection();
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = query;
            try
            {
                using (var dr = cmd.ExecuteReader())
                {
                    result = getList<T>(dr);
                }
            }
            catch
            {
                tryDisposeConnection();
                throw;
            }
            finally
            {
                cmd.Dispose();
                if (keepConnectionOpen == false)
                    tryDisposeConnection();
            }
            return result;
        }
        protected void executeCommand(string commandText, bool keepConnectionOpen)
        {
            tryOpenConnection();
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = commandText;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                tryDisposeConnection();
                throw;
            }
            finally
            {
                cmd.Dispose();
                if (keepConnectionOpen == false)
                    tryDisposeConnection();
            }
        }
        protected void tryOpenConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open();
            }
        }
        protected void tryDisposeConnection()
        {
            if (_sqlConnection != null)
            {
                try
                {
                    _sqlConnection.Dispose();
                }
                catch { }

                _sqlConnection = null;
            }
        }
        public void CloseConnection()
        {
            tryDisposeConnection();
        }
        public void OpenConnection()
        {
            tryOpenConnection();
        }
        public void Dispose()
        {
            tryDisposeConnection();
        }
    }
}
