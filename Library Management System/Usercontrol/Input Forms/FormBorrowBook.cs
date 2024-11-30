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

namespace Library_Management_System.UI
{
    public partial class FormBorrowBook : Form
    {
        public FormBorrowBook()
        {
            InitializeComponent();
            LoadBooks();
            LoadMembers();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadBooks()
        {
            /*
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Book_Id, Title FROM Books_tbl WHERE book_Status = 'Available'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //cmbBooks.Items.Add(new { Text = reader["Title"].ToString(), Value = reader["Book_Id"] });
                            cmbBooks.Items.Add(new { ID = reader["Book_Id"], Title = reader["Title"].ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Book_Id, Title FROM Books_tbl WHERE book_Status = 'Available'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var books = new List<KeyValuePair<string, int>>();
                        while (reader.Read())
                        {
                            books.Add(new KeyValuePair<string, int>(reader["Title"].ToString(), Convert.ToInt32(reader["Book_Id"])));
                        }
                        cmbBooks.DataSource = books;
                        cmbBooks.DisplayMember = "Key"; // Use the Key (Title) for display
                        cmbBooks.ValueMember = "Value"; // Use the Value (BookId) for selection
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadMembers()
        {
            /*
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Member_Id, Name FROM Members_tbl WHERE member_Status = 'Active'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //cmbMembers.Items.Add(new { Text = reader["Name"].ToString(), Value = reader["Member_Id"] });
                            cmbMembers.Items.Add(new { Value = reader["Member_Id"], Text = reader["Name"].ToString(),  });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Member_Id, Name FROM Members_tbl WHERE member_Status = 'Active'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var members = new List<KeyValuePair<string, int>>();
                        while (reader.Read())
                        {
                            // Add each member's FullName and MemberId to the ComboBox data source
                            members.Add(new KeyValuePair<string, int>(reader["Name"].ToString(), Convert.ToInt32(reader["Member_Id"])));
                        }
                        cmbMembers.DataSource = members;
                        cmbMembers.DisplayMember = "Key";  // Use the FullName (Key) for display
                        cmbMembers.ValueMember = "Value";  // Use the MemberId (Value) for selection
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbBooks.SelectedItem == null || cmbMembers.SelectedItem == null)
            {
                MessageBox.Show("Please select both a book and a member.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dynamic selectedBook = cmbBooks.SelectedItem;
            dynamic selectedMember = cmbMembers.SelectedItem;

            int bookId = selectedBook.Value;
            int memberId = selectedMember.Value;
            DateTime dueDate = dtpDueDate.Value;

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Insert transaction
                    string transactionQuery = "INSERT INTO Transactions_tbl (Member_Id, Book_Id, Borrow_Date, Due_Date, transaction_Status) " +
                                               "VALUES (@MemberId, @BookId, NOW(), @DueDate, 'Borrowed')";
                    MySqlCommand transactionCmd = new MySqlCommand(transactionQuery, conn);
                    transactionCmd.Parameters.AddWithValue("@MemberId", memberId);
                    transactionCmd.Parameters.AddWithValue("@BookId", bookId);
                    transactionCmd.Parameters.AddWithValue("@DueDate", dueDate);
                    transactionCmd.ExecuteNonQuery();

                    // Update book status
                    string updateBookQuery = "UPDATE Books_tbl SET book_Status = 'Borrowed' WHERE Book_Id = @BookId";
                    MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn);
                    updateBookCmd.Parameters.AddWithValue("@BookId", bookId);
                    updateBookCmd.ExecuteNonQuery();

                    MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
