using Library_Management_System.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System.Usercontrol
{
    public partial class UserControlReports : UserControl
    {
        public UserControlReports()
        {
            InitializeComponent();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
        private void LoadBooksReport()
        {
            string query = "SELECT Book_Id AS 'ID', Title AS 'Title', Author AS 'Author', book_Status AS 'Status', Category AS 'Category' FROM Books_tbl";

            PopulateDataGridView(query, dataGridViewBooks);
        }

        private void LoadMembersReport()
        {
            string query = "SELECT Member_Id AS 'ID', Name AS 'Name', Email AS 'Email', Phone AS 'Phone', member_Status AS 'Status' FROM Members_tbl";

            PopulateDataGridView(query, dataGridViewMembers);
        }

        private void LoadTransactionsReport()
        {
            string query = @"
                SELECT 
                    t.Transaction_Id AS 'Transaction ID',
                    m.Name AS 'Member',
                    b.Title AS 'Book',
                    t.Borrow_Date AS 'Borrow Date',
                    t.Due_Date AS 'Due Date',
                    t.Return_Date AS 'Return Date',
                    t.Fine AS 'Fine',
                    t.transaction_Status AS 'Status'
                FROM 
                    Transactions_tbl t
                JOIN Members_tbl m ON t.Member_Id = m.Member_Id
                JOIN Books_tbl b ON t.Book_Id = b.Book_Id";

            PopulateDataGridView(query, dataGridViewTransactions);
        }

        private void LoadOverdueBooksReport()
        {
            string query = @"
                SELECT 
                    t.Transaction_Id AS 'Transaction ID',
                    m.Name AS 'Member',
                    b.Title AS 'Book',
                    t.Due_Date AS 'Due Date',
                    DATEDIFF(CURDATE(), t.Due_Date) AS 'Overdue Days',
                    t.Fine AS 'Fine'
                FROM 
                    Transactions_tbl t
                JOIN Members_tbl m ON t.Member_Id = m.Member_Id
                JOIN Books_tbl b ON t.Book_Id = b.Book_Id
                WHERE 
                    t.transaction_Status = 'Borrowed' AND t.Due_Date < CURDATE()";

            PopulateDataGridView(query, dataGridViewOverdueBooks);
        }

        private void PopulateDataGridView(string query, DataGridView dataGridView)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControlReports_Load(object sender, EventArgs e)
        {
            LoadBooksReport();
        }

        private void tabControlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlReports.SelectedIndex)
            {
                case 0: // Books Report
                    LoadBooksReport();
                    break;
                case 1: // Members Report
                    LoadMembersReport();
                    break;
                case 2: // Transactions Report
                    LoadTransactionsReport();
                    break;
                case 3: // Overdue Books Report
                    LoadOverdueBooksReport();
                    break;
            }
        }

        private void dataGridViewMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
