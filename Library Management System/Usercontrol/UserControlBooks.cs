using Library_Management_System.Class;
using Library_Management_System.UI;
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
    public partial class UserControlBooks : UserControl
    {
        public UserControlBooks()
        {
            InitializeComponent();
            LoadBooksData();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
        private void LoadBooksData(string searchQuery = "")
        {
            //string query = "SELECT Title, Author, Category, ISBN, Genre, Year_Published, book_status FROM books_tbl";
            /*
            try
            {
                // Open the database connection and fetch data
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable booksTable = new DataTable();
                    adapter.Fill(booksTable);

                    dgvBooks.DataSource = booksTable; // Bind data to DataGridView
                }
            }
            catch (Exception ex)
            {
                // Show error message
                MessageBox.Show($"Error loading books data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    Book_Id AS 'book_id', 
                    Title AS 'Title', 
                    Author AS 'Author', 
                    Category AS 'Category',
                    ISBN AS 'ISBN',
                    Genre AS 'Genre',
                    Year_Published AS 'Year_Published',
                    book_status AS 'book_Status'
                FROM Books_tbl";

                    // Add search filter
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE book_id LIKE @Search OR Title LIKE @Search OR Author LIKE @Search OR Category LIKE @Search OR Genre LIKE @Search OR book_status LIKE @Search";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@Search", $"%{searchQuery}%");
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvBooks.DataSource = dataTable;
                    }
                }           
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControlBooks_Load(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAddBook formAddBook = new FormAddBook();
            formAddBook.ShowDialog();
        }

        private void btnSearchBooks_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchBooks.Text.Trim();
            LoadBooksData(searchQuery);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["book_id"].Value);
                FormEditBook formEditBook = new FormEditBook(bookId);
                formEditBook.ShowDialog();
                LoadBooksData(); // Refresh the grid after editing
            }
            else
            {
                MessageBox.Show("Please select a book to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["book_id"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteBookFromDatabase(bookId);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteBookFromDatabase(int bookId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM books_tbl WHERE Book_Id = @BookId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookId", bookId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBooksData(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
