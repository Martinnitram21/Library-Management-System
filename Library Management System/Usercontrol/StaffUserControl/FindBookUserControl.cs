﻿using Library_Management_System.UI;
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
        private int bookId = -1;

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchBookId = txtBookID.Text.Trim(); // Avoid shadowing class-level bookId

            if (string.IsNullOrEmpty(searchBookId))
            {
                MessageBox.Show("Please enter a Book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query for book details and join with the author_tbl
                    string bookQuery = @"
                    SELECT 
                        b.book_id,
                        b.title,
                        b.category,
                        b.genre,
                        b.ISBN,
                        b.year_published,
                        b.book_status,
                        a.author_name,
                        b.description
                    FROM books_tbl b
                    LEFT JOIN authors_tbl a ON b.author_id = a.author_id
                    WHERE b.book_id = @BookID";

                    using (MySqlCommand bookCmd = new MySqlCommand(bookQuery, conn))
                    {
                        bookCmd.Parameters.AddWithValue("@BookID", searchBookId);

                        using (MySqlDataReader reader = bookCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bookId = Convert.ToInt32(reader["book_id"]); // Assign bookId at the class level

                                // Populate Book Details
                                lblTitle.Text = reader["title"].ToString();
                                lblAuthor.Text = reader["author_name"].ToString();
                                lblCategory.Text = reader["category"].ToString();
                                lblGenre.Text = reader["genre"].ToString();
                                lblISBN.Text = reader["ISBN"].ToString();
                                lblYear.Text = reader["year_published"].ToString();
                                lblStatus.Text = reader["book_status"].ToString();
                                lblDescription.Text = reader["description"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    // Query for borrower details related to the book
                    string borrowerQuery = @"
                    SELECT m.member_id, m.last_name, m.email, m.phone
                    FROM members_tbl m
                    JOIN borrowers_tbl b ON m.member_id = b.member_id
                    WHERE b.book_id = @BookID AND b.return_date IS NULL";

                    using (MySqlCommand borrowerCmd = new MySqlCommand(borrowerQuery, conn))
                    {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void FindBookUserControl_Load(object sender, EventArgs e)
        {

        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            FormAddBook formAddBook = new FormAddBook();
            formAddBook.ShowDialog();
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (bookId != -1) // Check if a valid bookId is available
            {
                // Open the edit form and pass the book ID
                FormEditBook formEditBook = new FormEditBook(bookId);
                formEditBook.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a book to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
