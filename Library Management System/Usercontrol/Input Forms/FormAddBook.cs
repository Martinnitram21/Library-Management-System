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
            // Define your connection string (replace with your actual credentials)
            string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

            // Collect data from form inputs
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string category = comboCategory.Text.Trim();
            string genre = comboGenre.Text.Trim();
            string isbn = txtISBN.Text.Trim();
            string yearPublished = txtYear.Text.Trim();

            // Validate that all fields are filled
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(yearPublished))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query to insert the new book
            string query = "INSERT INTO books_tbl (Title, Author, Category, ISBN, Genre, Year_Published) " +
                           "VALUES (@Title, @Author, @Category, @ISBN, @Genre, @YearPublished)";

            try
            {
                // Establish the connection
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Create the command and set the parameters
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Author", author);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        cmd.Parameters.AddWithValue("@Genre", genre);
                        cmd.Parameters.AddWithValue("@YearPublished", yearPublished);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Optionally, clear the input fields after successful insertion
                            ClearFields();
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
                // Handle any errors
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
    }
}
