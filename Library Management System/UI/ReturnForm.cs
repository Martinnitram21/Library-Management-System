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
    public partial class ReturnForm : Form
    {
        public ReturnForm()
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
                    string query = "SELECT Member_Id, Name FROM Members_tbl WHERE member_Status = 'Active'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var members = new List<KeyValuePair<string, int>>();
                        while (reader.Read())
                        {
                            members.Add(new KeyValuePair<string, int>(reader["Name"].ToString(), Convert.ToInt32(reader["Member_Id"])));
                        }
                        cmbMembers.DataSource = members;
                        cmbMembers.DisplayMember = "Key";
                        cmbMembers.ValueMember = "Value";
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
                    string query = "SELECT Book_Id, Title FROM Books_tbl " +
                                   "WHERE Book_Id IN (SELECT Book_Id FROM Transactions_tbl WHERE Member_Id = @MemberId AND transaction_Status = 'Borrowed')";
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
                        cmbBorrowedBooks.DisplayMember = "Key";
                        cmbBorrowedBooks.ValueMember = "Value";
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
            /*
            if (cmbBorrowedBooks.SelectedItem == null || cmbMembers.SelectedItem == null)
            {
                MessageBox.Show("Please select a member and a borrowed book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    // Update Transactions table
                    string updateTransactionQuery = "UPDATE Transactions_tbl SET transaction_Status = 'Returned' WHERE Book_Id = @BookId AND Member_Id = @MemberId AND transaction_Status = 'Borrowed'";
                    MySqlCommand updateTransactionCmd = new MySqlCommand(updateTransactionQuery, conn);
                    updateTransactionCmd.Parameters.AddWithValue("@BookId", bookId);
                    updateTransactionCmd.Parameters.AddWithValue("@MemberId", memberId);
                    updateTransactionCmd.ExecuteNonQuery();

                    // Update Books table
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
            */

            if (cmbBorrowedBooks.SelectedItem == null || cmbMembers.SelectedItem == null)
            {
                MessageBox.Show("Please select a member and a borrowed book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    // Fetch the due date for the selected book and calculate fine if overdue
                    string query = "SELECT Due_Date FROM Transactions_tbl WHERE Book_Id = @BookId AND Member_Id = @MemberId AND transaction_Status = 'Borrowed'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@MemberId", memberId);

                    object dueDateObj = cmd.ExecuteScalar();
                    if (dueDateObj != null)
                    {
                        DateTime dueDate = Convert.ToDateTime(dueDateObj);
                        DateTime currentDate = DateTime.Now;

                        if (currentDate > dueDate)
                        {
                            int overdueDays = (currentDate - dueDate).Days;
                            decimal fine = overdueDays * 5; // Assume fine is $5 per overdue day

                            MessageBox.Show($"This book is overdue by {overdueDays} days. Fine: ${fine}", "Overdue Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // Update fine in Transactions table
                            string updateFineQuery = "UPDATE Transactions_tbl SET Fine = @Fine WHERE Book_Id = @BookId AND Member_Id = @MemberId AND transaction_Status = 'Borrowed'";
                            MySqlCommand updateFineCmd = new MySqlCommand(updateFineQuery, conn);
                            updateFineCmd.Parameters.AddWithValue("@Fine", fine);
                            updateFineCmd.Parameters.AddWithValue("@BookId", bookId);
                            updateFineCmd.Parameters.AddWithValue("@MemberId", memberId);
                            updateFineCmd.ExecuteNonQuery();
                        }
                    }

                    // Update Transactions table to mark the book as returned
                    string updateTransactionQuery = "UPDATE Transactions_tbl SET transaction_Status = 'Returned' WHERE Book_Id = @BookId AND Member_Id = @MemberId AND transaction_Status = 'Borrowed'";
                    MySqlCommand updateTransactionCmd = new MySqlCommand(updateTransactionQuery, conn);
                    updateTransactionCmd.Parameters.AddWithValue("@BookId", bookId);
                    updateTransactionCmd.Parameters.AddWithValue("@MemberId", memberId);
                    updateTransactionCmd.ExecuteNonQuery();

                    // Update Books table to mark the book as available
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
