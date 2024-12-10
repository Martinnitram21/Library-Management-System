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
            try
            {
                // Ensure connection string is valid
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Connection string is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if DataGridView is initialized
                if (dgvBooks == null)
                {
                    MessageBox.Show("DataGridView is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Updated query to join with Authors_tbl and Publishers_tbl
                    string query = @"
                SELECT 
                    b.Book_Id AS 'Book ID', 
                    b.Title AS 'Title', 
                    b.Category AS 'Category',
                    b.ISBN AS 'ISBN',
                    b.Genre AS 'Genre',
                    a.Author_Name AS 'Author Name', 
                    p.Publisher_Name AS 'Publisher Name', 
                    b.Year_Published AS 'Year Published',
                    b.Book_Status AS 'Status',
                    b.description AS 'Description'
                FROM Books_tbl b
                LEFT JOIN Authors_tbl a ON b.Author_Id = a.Author_Id
                LEFT JOIN Publishers_tbl p ON b.Publisher_Id = p.Publisher_Id";

                    // Add search filter
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += @"
                    WHERE b.Book_Id LIKE @Search
                    OR b.Title LIKE @Search
                    OR a.Author_Name LIKE @Search
                    OR p.Publisher_Name LIKE @Search
                    OR b.Category LIKE @Search
                    OR b.Genre LIKE @Search
                    OR b.Book_Status LIKE @Search";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@Search", $"%{searchQuery}%");
                        }

                        // Load the data into a DataTable and bind it to the DataGridView
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable != null)
                        {
                            dgvBooks.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("No data returned from the database.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
            try
            {
                if (dgvBooks.SelectedRows.Count > 0) // Ensure a row is selected
                {
                    // Retrieve the Book_Id from the DataGridView
                    DataGridViewRow selectedRow = dgvBooks.SelectedRows[0];

                    // Check if the column exists before accessing
                    if (dgvBooks.Columns.Contains("Book ID"))
                    {
                        int bookId = Convert.ToInt32(selectedRow.Cells["Book ID"].Value);

                        // Open the update book form with the selected Book_Id
                        FormEditBook updateBookForm = new FormEditBook(bookId);
                        updateBookForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("The column 'Book_Id' does not exist in the DataGridView. Ensure your query includes it.",
                                        "Column Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["Book ID"].Value);

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

        private void dgvBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the column being formatted is the 'Status' column
            if (dgvBooks.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();

                // Apply background color based on the status value
                if (status == "Available")
                {
                    e.CellStyle.BackColor = Color.LightGreen; // Green for 'Available'
                    e.CellStyle.ForeColor = Color.White;      // Optional: Black text color
                }
                else if (status == "Borrowed")
                {
                    e.CellStyle.BackColor = Color.LightCoral; // Red for 'Borrowed'
                    e.CellStyle.ForeColor = Color.White;      // Optional: White text color
                }
            }
        }
    }
}
