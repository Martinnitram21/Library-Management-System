using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Class
{
    internal class DatabaseHelper
    {
        // Get the connection string from app.config
        public static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;

            // Check if the connection string is null or empty
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is not found in app.config.");
            }

            return connectionString;
        }

        // Method to create and return a new MySqlConnection
        public static MySqlConnection GetConnection()
        {
            string connectionString = GetConnectionString();
            return new MySqlConnection(connectionString);
        }
    }
}
