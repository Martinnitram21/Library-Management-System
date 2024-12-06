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

namespace Library_Management_System.Usercontrol.StaffUserControl
{
    public partial class ViewLogUserControl : UserControl
    {
        public ViewLogUserControl()
        {
            InitializeComponent();
            LoadTransactionsReport();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

            PopulateDataGridView(query, searchQuery);
        }

        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

        private void PopulateDataGridView(string query, string searchQuery = "")
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

                        // Populate the DataGridView with the result
                        dataGridViewTransactions.DataSource = dataTable;

                        // Format the columns
                        FormatDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatDataGridView()
        {
            // Assuming dataGridViewTransactions is the name of your DataGridView

            // Set the width of each column according to the content or default setting
            dataGridViewTransactions.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Set the 'Book' column to fill the remaining space
            dataGridViewTransactions.Columns["Book"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Optionally, set other columns' AutoSizeMode (e.g., for 'Transaction ID' column to be fixed size)
            dataGridViewTransactions.Columns["Transaction ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }
}
