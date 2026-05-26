using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLLogin_Database
{
    public class DBEngine
    {
        private SqlConnection _conn;

        public DBEngine(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
        }

        public void Open()
        {
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
        }

        public void Close()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
                _conn.Close();
        }

        public DataTable GetDatabases()
        {
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();

            return _conn.GetSchema("Databases");
        }

        public void ChangeDatabase(string databaseName)
        {
            _conn.ChangeDatabase(databaseName);
        }

        public bool IsConnected()
        {
            return _conn != null && _conn.State == ConnectionState.Open;
        }

        public string GetCurrentDatabase()
        {
            return _conn.Database;
        }
    }
}