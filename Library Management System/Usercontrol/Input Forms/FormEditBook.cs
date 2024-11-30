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

namespace Library_Management_System.Usercontrol
{
    public partial class FormEditBook : Form
    {
        public FormEditBook()
        {
            InitializeComponent();
        }
        private readonly int _bookId; // Book ID to identify which record to edit
        private readonly string _connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

        public FormEditBook(int bookId)
        {
            InitializeComponent();
            _bookId = bookId;
            SetupYearPicker();
            LoadBookData();
        }

        private void SetupYearPicker()
        {
            txtYear.Format = DateTimePickerFormat.Custom;
            txtYear.CustomFormat = "yyyy";
            txtYear.ShowUpDown = true;
        }
        private void LoadBookData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT Title, Author, Category, ISBN, Genre, Year_Published FROM books_tbl WHERE Book_Id = @BookId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookId", _bookId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTitle.Text = reader["Title"].ToString();
                                txtAuthor.Text = reader["Author"].ToString();
                                comboCategory.Text = reader["Category"].ToString();
                                comboGenre.Text = reader["Genre"].ToString();
                                txtISBN.Text = reader["ISBN"].ToString();
                                // Safely parse Year_Published as DateTime
                                string yearPublished = reader["Year_Published"].ToString();
                                if (DateTime.TryParse(yearPublished, out DateTime parsedYear))
                                {
                                    txtYear.Value = parsedYear;
                                }
                                else
                                {
                                    // Handle cases where the year is not a valid DateTime
                                    txtYear.Value = new DateTime(DateTime.Now.Year, 1, 1); // Default to current year
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateBookInDatabase();
        }
        private void UpdateBookInDatabase()
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string category = comboCategory.Text.Trim();
            string genre = comboGenre.Text.Trim();
            string isbn = txtISBN.Text.Trim();
            string yearPublished = txtYear.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(yearPublished))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE books_tbl SET Title = @Title, Author = @Author, Category = @Category, " +
                                   "Genre = @Genre, ISBN = @ISBN, Year_Published = @YearPublished WHERE Book_Id = @BookId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Author", author);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Genre", genre);
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        cmd.Parameters.AddWithValue("@YearPublished", yearPublished);
                        cmd.Parameters.AddWithValue("@BookId", _bookId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
