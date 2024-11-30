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
        private void LoadBooksReport(string searchQuery = "")
        {
            string query = @"
                SELECT 
                    Book_Id AS 'book_ID', 
                    Title AS 'Title', 
                    Author AS 'Author', 
                    Category AS 'Category', 
                    Genre AS 'Genre', 
                    book_Status AS 'Status' 
                FROM Books_tbl";

            // Add a WHERE clause if there's a search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += @"
                    WHERE book_ID LIKE @Search 
                    OR Title LIKE @Search 
                    OR Author LIKE @Search
                    OR Category LIKE @Search
                    OR Genre LIKE @Search
                    OR book_status LIKE @Search";
            }

            PopulateDataGridView(query, dataGridViewBooks, searchQuery);
        }

        private void LoadMembersReport(string searchQuery = "")
        {
            //string query = "SELECT Member_Id AS 'ID', Name AS 'Name', Email AS 'Email', Phone AS 'Phone', member_Status AS 'Status' FROM Members_tbl";
            string query = @"
        SELECT 
            member_id AS 'ID', 
            name AS 'Name', 
            Email AS 'Email', 
            Phone AS 'Phone',  
            member_Status AS 'member_Status' 
        FROM members_tbl";

            // Add a WHERE clause if there's a search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += @"
                    WHERE member_ID LIKE @Search 
                    OR name LIKE @Search 
                    OR Email LIKE @Search
                    OR Phone LIKE @Search
                    OR member_status LIKE @Search";
            }

            PopulateDataGridView(query, dataGridViewMembers, searchQuery);
        }

        private void LoadTransactionsReport(string searchQuery = "")
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

            // Add a WHERE clause if there's a search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += @"
                    WHERE m.Name LIKE @Search 
                    OR b.Title LIKE @Search 
                    OR t.Transaction_Id LIKE @Search
                    OR t.transaction_Status LIKE @Search";
            }

            PopulateDataGridView(query, dataGridViewTransactions, searchQuery);
        }

        private void LoadOverdueBooksReport(string searchQuery = "")
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

            // Add a WHERE clause if there's a search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += @"
                    WHERE t.transaction_ID LIKE @Search 
                    OR m.name LIKE @Search 
                    OR b.Title LIKE @Search";
            }

            PopulateDataGridView(query, dataGridViewOverdueBooks, searchQuery);
        }

        private void PopulateDataGridView(string query, DataGridView dataGridView, string searchQuery = "")
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // If there's a search query, bind the parameter
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                        }

                        // Use MySqlCommand with the DataAdapter
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            switch (tabControlReports.SelectedIndex)
            {
                case 0: // Books Report
                    LoadBooksReport(searchQuery);
                    break;
                case 1: // Members Report
                    LoadMembersReport(searchQuery);
                    break;
                case 2: // Transactions Report
                    LoadTransactionsReport(searchQuery);
                    break;
                case 3: // Overdue Books Report
                    LoadOverdueBooksReport(searchQuery);
                    break;
            }
        }

        private void dataGridViewBooks_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
