﻿using Google.Protobuf.WellKnownTypes;
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
    public partial class FormAddBook : Form
    {
        public FormAddBook()
        {
            InitializeComponent();
            SetupYearPicker();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddBookToDatabase();
            this.Close();
        }
        private void SetupYearPicker()
        {
            // Set the format to Custom
            txtYear.Format = DateTimePickerFormat.Custom;

            // Set the custom format to display only Month and Year
            txtYear.CustomFormat = "yyyy";

            // Optional: Prevent the user from selecting a day (ensure they pick only month and year)
            txtYear.ShowUpDown = true;  // Optional: Use up/down arrows for selection
        }
        private void AddBookToDatabase()
        {
            string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

            // Collect data from form inputs
            string title = txtTitle.Text.Trim();
            string authorName = txtAuthor.Text.Trim();
            string publisherName = txtPublisher.Text.Trim();
            string category = comboCategory.Text.Trim();
            string genre = comboGenre.Text.Trim();
            string isbn = txtISBN.Text.Trim();
            string yearPublished = txtYear.Text.Trim();
            string description = rtbDescription.Text.Trim();

            // Validate that all fields are filled
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(authorName) || string.IsNullOrWhiteSpace(publisherName)  || string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(yearPublished))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get or insert AuthorId
                    int authorId = GetOrInsertAuthor(conn, authorName);

                    // Get or insert PublisherId
                    int publisherId = GetOrInsertPublisher(conn, publisherName);

                    // SQL query to insert the new book
                    string query = @"INSERT INTO books_tbl (Title, Author_Id, Publisher_Id, Category, ISBN, Genre, Year_Published, description) 
                             VALUES (@Title, @AuthorId, @PublisherId, @Category, @ISBN, @Genre, @YearPublished, @Description)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@AuthorId", authorId);
                        cmd.Parameters.AddWithValue("@PublisherId", publisherId);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        cmd.Parameters.AddWithValue("@Genre", genre);
                        cmd.Parameters.AddWithValue("@YearPublished", yearPublished);
                        cmd.Parameters.AddWithValue("@Description", description);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields(); // Clear input fields
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFields()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            comboCategory.SelectedIndex = -1;
            comboGenre.SelectedIndex = -1;
            comboCategory.Text = "";
            comboGenre.Text = "";
            txtISBN.Clear();
            txtYear.Value = DateTime.Now;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int GetOrInsertAuthor(MySqlConnection conn, string authorName)
        {
            // Check if author exists
            string selectQuery = "SELECT Author_Id FROM Authors_tbl WHERE author_name = @Name";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Name", authorName);
                object result = cmd.ExecuteScalar();

                if (result != null) // Author exists
                {
                    return Convert.ToInt32(result);
                }
            }

            // Insert new author
            string insertQuery = "INSERT INTO Authors_tbl (author_Name) VALUES (@Name); SELECT LAST_INSERT_ID();";
            using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Name", authorName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int GetOrInsertPublisher(MySqlConnection conn, string publisherName)
        {
            // Check if publisher exists
            string selectQuery = "SELECT Publisher_Id FROM Publishers_tbl WHERE publisher_Name = @Name";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Name", publisherName);
                object result = cmd.ExecuteScalar();

                if (result != null) // Publisher exists
                {
                    return Convert.ToInt32(result);
                }
            }

            // Insert new publisher
            string insertQuery = "INSERT INTO Publishers_tbl (publisher_Name) VALUES (@Name); SELECT LAST_INSERT_ID();";
            using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Name", publisherName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
