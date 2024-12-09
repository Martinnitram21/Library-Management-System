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
            DisplayDueDate();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadBooks()
        {
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
                        cmbBooks.DisplayMember = "Key"; // Use the Title (Key) for display
                        cmbBooks.ValueMember = "Value"; // Use Book_Id (Value) for selection
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
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Member_Id, CONCAT(first_name, ' ', last_name) AS FullName FROM Members_tbl WHERE member_Status = 'Active'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var members = new List<KeyValuePair<string, int>>();
                        while (reader.Read())
                        {
                            // Add combined first and last name along with MemberId to the ComboBox data source
                            members.Add(new KeyValuePair<string, int>(reader["FullName"].ToString(), Convert.ToInt32(reader["Member_Id"])));
                        }
                        cmbMembers.DataSource = members;
                        cmbMembers.DisplayMember = "Key"; // Use the FullName (Key) for display
                        cmbMembers.ValueMember = "Value"; // Use the MemberId (Value) for selection
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

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Insert into Borrowers_tbl and retrieve the borrow_id
                    string borrowQuery = @"
            INSERT INTO Borrowers_tbl (Book_Id, Member_Id, Borrow_Date, Due_Date)
            VALUES (@BookId, @MemberId, NOW(), DATE_ADD(NOW(), INTERVAL 7 DAY));
            SELECT LAST_INSERT_ID();";
                    MySqlCommand borrowCmd = new MySqlCommand(borrowQuery, conn);
                    borrowCmd.Parameters.AddWithValue("@BookId", bookId);
                    borrowCmd.Parameters.AddWithValue("@MemberId", memberId);
                    int borrowId = Convert.ToInt32(borrowCmd.ExecuteScalar());

                    // Insert initial dues record into dues_tbl using borrow_id
                    string duesQuery = @"
            INSERT INTO dues_tbl (Borrow_Id, Fine_amount, dues_status)
            VALUES (@BorrowId, 0, 'Unpaid')";
                    MySqlCommand duesCmd = new MySqlCommand(duesQuery, conn);
                    duesCmd.Parameters.AddWithValue("@BorrowId", borrowId);
                    duesCmd.ExecuteNonQuery();

                    // Update the book status to 'Borrowed'
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

        private void DisplayDueDate()
        {
            DateTime dueDate = DateTime.Now.AddDays(7);
            lblDueDate.Text = $" {dueDate:MMMM dd, yyyy}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
