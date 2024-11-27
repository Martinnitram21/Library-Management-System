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
        // Method to get the connection string from App.config
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
        }

        // Method to create and return a new MySqlConnection
        public static MySqlConnection GetConnection()
        {
            string connectionString = GetConnectionString();
            return new MySqlConnection(connectionString);
        }
    }
}
