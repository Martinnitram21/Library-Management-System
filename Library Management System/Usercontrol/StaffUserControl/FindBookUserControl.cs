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
    public partial class FindBookUserControl : UserControl
    {
        public FindBookUserControl()
        {
            InitializeComponent();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string bookId = txtBookID.Text.Trim();

            if (string.IsNullOrEmpty(bookId))
            {
                MessageBox.Show("Please enter a Book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Connection string to the database
                //string connectionString = "your_connection_string";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query for book details
                    string bookQuery = "SELECT * FROM books_tbl WHERE book_id = @BookID";
                    MySqlCommand bookCmd = new MySqlCommand(bookQuery, conn);
                    bookCmd.Parameters.AddWithValue("@BookID", bookId);

                    using (MySqlDataReader reader = bookCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate Book Details
                            lblTitle.Text = reader["title"].ToString();
                            lblAuthor.Text = reader["author"].ToString();
                            lblCategory.Text = reader["category"].ToString();
                            lblGenre.Text = reader["genre"].ToString();
                            lblISBN.Text = reader["ISBN"].ToString();
                            lblYear.Text = reader["year_published"].ToString();
                            lblStatus.Text = reader["book_status"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Query for student details related to the book
                    string studentQuery = @"
                        SELECT m.member_id, m.name, m.email, m.phone
                        FROM members_tbl m
                        JOIN transactions_tbl t ON m.member_id = t.member_id
                        WHERE t.book_id = @BookID";
                    MySqlCommand studentCmd = new MySqlCommand(studentQuery, conn);
                    studentCmd.Parameters.AddWithValue("@BookID", bookId);

                    using (MySqlDataReader studentReader = studentCmd.ExecuteReader())
                    {
                        if (studentReader.Read())
                        {
                            // Populate Student Details
                            lblStudentID.Text = studentReader["member_id"].ToString();
                            lblStudentName.Text = studentReader["name"].ToString();
                            lblEmail.Text = studentReader["email"].ToString();
                            lblPhone.Text = studentReader["phone"].ToString();
                        }
                        else
                        {
                            lblStudentID.Text = "Not Found";
                            lblStudentName.Text = "Not Found";
                            lblEmail.Text = "Not Found";
                            lblPhone.Text = "Not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
