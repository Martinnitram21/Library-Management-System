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
        private readonly BookRepository bookManager;
        public UserControlBooks()
        {
            InitializeComponent();
            // Initialize BookManager with the connection string
            bookManager = new BookRepository("Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;");
            LoadBooksData();
        }
        private void LoadBooksData(string searchQuery = "")
        {
            try
            {
                List<Book> books = bookManager.GetBooks(searchQuery);

                if (books != null && books.Count > 0)
                {
                    // Bind the books list to the DataGridView
                    dgvBooks.DataSource = books.Select(b => new
                    {
                        b.BookId,
                        b.Title,
                        b.Category,
                        b.ISBN,
                        b.Genre,
                        b.Author,
                        b.Publisher,
                        b.YearPublished,
                        b.Status,
                        b.Description
                    }).ToList();

                    // Hide the BookId column
                    dgvBooks.Columns["BookId"].Visible = false;
                }
                else
                {
                    dgvBooks.DataSource = null; // Clear the DataGridView
                    MessageBox.Show("No books found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControlBooks_Load(object sender, EventArgs e)
        {
            // Additional setup can go here if needed.
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (FormAddBook formAddBook = new FormAddBook())
            {
                if (formAddBook.ShowDialog() == DialogResult.OK)
                {
                    LoadBooksData(); // Refresh the DataGridView after adding a book
                }
            }
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
                if (dgvBooks.SelectedRows.Count > 0)
                {
                    // Retrieve the BookId from the selected row
                    DataGridViewRow selectedRow = dgvBooks.SelectedRows[0];
                    int bookId = Convert.ToInt32(selectedRow.Cells["BookId"].Value);

                    // Open the edit book form with the selected BookId
                    using (FormEditBook updateBookForm = new FormEditBook(bookId))
                    {
                        if (updateBookForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadBooksData(); // Refresh the DataGridView after editing a book
                        }
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
            try
            {
                if (dgvBooks.SelectedRows.Count > 0)
                {
                    int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookId"].Value);

                    DialogResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        bool isDeleted = bookManager.DeleteBook(bookId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBooksData(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a book to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvBooks.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
                {
                    string status = e.Value.ToString();
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // Apply background color based on the status value
                    if (status == "Available")
                    {
                        e.CellStyle.BackColor = Color.LightGreen; // Green for 'Available'
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else if (status == "Borrowed")
                    {
                        e.CellStyle.BackColor = Color.LightCoral; // Red for 'Borrowed'
                        e.CellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error formatting cells: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
