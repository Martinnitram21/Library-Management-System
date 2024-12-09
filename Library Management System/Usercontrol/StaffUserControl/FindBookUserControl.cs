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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query for book details
                    // Query for book details and join with the author_tbl
                    string bookQuery = @"
                SELECT 
                    b.title,
                    b.category,
                    b.genre,
                    b.ISBN,
                    b.year_published,
                    b.book_status,
                    a.author_name
                FROM books_tbl b
                LEFT JOIN authors_tbl a ON b.author_id = a.author_id
                WHERE b.book_id = @BookID";
                    MySqlCommand bookCmd = new MySqlCommand(bookQuery, conn);
                    bookCmd.Parameters.AddWithValue("@BookID", bookId);

                    using (MySqlDataReader reader = bookCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate Book Details
                            lblTitle.Text = reader["title"].ToString();
                            lblAuthor.Text = reader["author_name"].ToString();
                            lblCategory.Text = reader["category"].ToString();
                            lblGenre.Text = reader["genre"].ToString();
                            lblISBN.Text = reader["ISBN"].ToString();
                            lblYear.Text = reader["year_published"].ToString();
                            lblStatus.Text = reader["book_status"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Query for borrower details related to the book
                    string borrowerQuery = @"
                SELECT m.member_id, m.last_name, m.email, m.phone
                FROM members_tbl m
                JOIN borrowers_tbl b ON m.member_id = b.member_id
                WHERE b.book_id = @BookID AND b.return_date IS NULL";
                    MySqlCommand borrowerCmd = new MySqlCommand(borrowerQuery, conn);
                    borrowerCmd.Parameters.AddWithValue("@BookID", bookId);

                    using (MySqlDataReader borrowerReader = borrowerCmd.ExecuteReader())
                    {
                        if (borrowerReader.Read())
                        {
                            // Populate Borrower Details
                            lblStudentID.Text = borrowerReader["member_id"].ToString();
                            lblStudentName.Text = borrowerReader["last_name"].ToString();
                            lblEmail.Text = borrowerReader["email"].ToString();
                            lblPhone.Text = borrowerReader["phone"].ToString();
                        }
                        else
                        {
                            // Clear labels if no borrower found
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
