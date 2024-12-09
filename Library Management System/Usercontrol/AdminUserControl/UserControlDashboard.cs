using Library_Management_System.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System.Usercontrol
{
    public partial class UserControlDashboard : UserControl
    {
        public UserControlDashboard()
        {
            InitializeComponent();
            LoadDashboardData();
        }
        // Connection string (replace with your actual connection string)
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
        private void LoadDashboardData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch data from the database
                    int totalBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM books_tbl");
                    //int borrowedBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM transactions_tbl WHERE transaction_Status = 'Borrowed'");
                    int borrowedBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM books_tbl WHERE book_status = 'Borrowed'");
                    //int overdueBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM dues_tbl WHERE dues_Status = 'Borrowed' AND due_date < NOW()");
                    int overdueBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM dues_tbl WHERE dues_Status = 'Unpaid'");
                    int totalMembers = GetScalarValue(conn, "SELECT COUNT(*) FROM members_tbl");

                    // Set the values to labels or other controls in your user control
                    lblTotalBooks.Text = totalBooks.ToString();
                    lblBorrowedBooks.Text = borrowedBooks.ToString();
                    lblOverdueBooks.Text = overdueBooks.ToString();
                    lblTotalMembers.Text = totalMembers.ToString();
                }
            }
            catch (Exception ex)
            {
                // Display a message box in case of errors
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetScalarValue(MySqlConnection conn, string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
    }
}
