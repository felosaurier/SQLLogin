using System;
using System.Data;
using System.Data.SqlClient;
using SQLLogin_Database;

namespace SQLLogin_Logik
{
    public class ConnectionManager
    {
        private DBEngine _dbEngine;

        public string BuildConnectionString(string host, string user, string password, bool useWindowsAuth)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = host;

            if (useWindowsAuth)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = user;
                builder.Password = password;
            }

            return builder.ConnectionString;
        }

        public void Connect(string connectionString)
        {
            _dbEngine = new DBEngine(connectionString);
            _dbEngine.Open();
        }

        public void Disconnect()
        {
            if (_dbEngine != null)
                _dbEngine.Close();
        }

        public DataTable GetDatabases()
        {
            return _dbEngine.GetDatabases();
        }

        public void ChangeDatabase(string databaseName)
        {
            _dbEngine.ChangeDatabase(databaseName);
        }

        public bool IsConnected()
        {
            return _dbEngine != null && _dbEngine.IsConnected();
        }

        public string GetCurrentDatabase()
        {
            return _dbEngine.GetCurrentDatabase();
        }
    }
}