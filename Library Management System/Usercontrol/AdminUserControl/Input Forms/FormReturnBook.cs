 using Library_Management_System.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System.UI
{
    public partial class FormReturnBook : Form
    {
        public FormReturnBook()
        {
            InitializeComponent();
            LoadMembers();
            timer1.Start();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
        private void LoadMembers()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Query to select the full name of the active members
                    string query = "SELECT Member_Id, CONCAT(first_name, ' ', last_name) AS FullName FROM Members_tbl WHERE member_Status = 'Active'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var members = new List<KeyValuePair<string, int>>();
                        while (reader.Read())
                        {
                            members.Add(new KeyValuePair<string, int>(reader["FullName"].ToString(), Convert.ToInt32(reader["Member_Id"])));
                        }
                        cmbMembers.DataSource = members;
                        cmbMembers.DisplayMember = "Key"; // Display the full name (Key)
                        cmbMembers.ValueMember = "Value"; // Use Member_Id (Value)
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBorrowedBooks();
        }
        private void LoadBorrowedBooks()
        {
            try
            {
                if (cmbMembers.SelectedItem == null) return;

                dynamic selectedMember = cmbMembers.SelectedItem;
                int memberId = selectedMember.Value;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Query to select the borrowed books for the selected member
                    string query = @"
                SELECT b.Book_Id, b.Title 
                FROM Books_tbl b
                JOIN Transactions_tbl t ON b.Book_Id = t.Book_Id
                WHERE t.Member_Id = @MemberId AND t.transaction_Status = 'Borrowed'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MemberId", memberId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var borrowedBooks = new List<KeyValuePair<string, int>>();
                        while (reader.Read())
                        {
                            borrowedBooks.Add(new KeyValuePair<string, int>(reader["Title"].ToString(), Convert.ToInt32(reader["Book_Id"])));
                        }
                        cmbBorrowedBooks.DataSource = borrowedBooks;
                        cmbBorrowedBooks.DisplayMember = "Key"; // Display the book title (Key)
                        cmbBorrowedBooks.ValueMember = "Value"; // Use Book_Id (Value)
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowed books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (cmbBorrowedBooks.SelectedItem == null || cmbMembers.SelectedItem == null)
            {
                MessageBox.Show("Please select both a member and a borrowed book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dynamic selectedBook = cmbBorrowedBooks.SelectedItem;
            dynamic selectedMember = cmbMembers.SelectedItem;

            int bookId = selectedBook.Value;
            int memberId = selectedMember.Value;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch the due date for the selected book
                    string query = "SELECT Due_Date FROM Borrowers_tbl WHERE Book_Id = @BookId AND Member_Id = @MemberId AND Return_Date IS NULL";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@MemberId", memberId);

                    object dueDateObj = cmd.ExecuteScalar();
                    if (dueDateObj != null)
                    {
                        DateTime dueDate = Convert.ToDateTime(dueDateObj);
                        DateTime currentDate = DateTime.Now;

                        // Calculate fine if the book is overdue
                        if (currentDate > dueDate)
                        {
                            int overdueDays = (currentDate - dueDate).Days;
                            decimal fine = overdueDays * 5; // Assume fine is $5 per overdue day

                            // Show message with fine
                            MessageBox.Show($"This book is overdue by {overdueDays} days. Fine: ${fine}", "Overdue Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // Update fine in Borrowers_tbl
                            string updateFineQuery = "UPDATE Borrowers_tbl SET Fine = @Fine WHERE Book_Id = @BookId AND Member_Id = @MemberId AND Return_Date IS NULL";
                            MySqlCommand updateFineCmd = new MySqlCommand(updateFineQuery, conn);
                            updateFineCmd.Parameters.AddWithValue("@Fine", fine);
                            updateFineCmd.Parameters.AddWithValue("@BookId", bookId);
                            updateFineCmd.Parameters.AddWithValue("@MemberId", memberId);
                            updateFineCmd.ExecuteNonQuery();
                        }
                    }

                    // Mark the book as returned in Borrowers_tbl and set the return date to NOW()
                    string updateBorrowersQuery = "UPDATE Borrowers_tbl SET Return_Date = NOW() WHERE Book_Id = @BookId AND Member_Id = @MemberId AND Return_Date IS NULL";
                    MySqlCommand updateBorrowersCmd = new MySqlCommand(updateBorrowersQuery, conn);
                    updateBorrowersCmd.Parameters.AddWithValue("@BookId", bookId);
                    updateBorrowersCmd.Parameters.AddWithValue("@MemberId", memberId);
                    updateBorrowersCmd.ExecuteNonQuery();

                    // Update the book status to 'Available' in Books_tbl
                    string updateBookQuery = "UPDATE Books_tbl SET book_Status = 'Available' WHERE Book_Id = @BookId";
                    MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn);
                    updateBookCmd.Parameters.AddWithValue("@BookId", bookId);
                    updateBookCmd.ExecuteNonQuery();

                    MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateNow.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }
    }
}
