using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Class
{
    internal class DatabaseManager
    {
        private static string _connectionString;

        static DatabaseManager()
        {
            _connectionString = Environment.GetEnvironmentVariable("LIBRARY_DB_CONNECTION");

            // If not found, fall back to config file (for development)
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            }

            // If still null, throw error
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string not configured!");
            }
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public static string ConnectionString => _connectionString;
    }
}
