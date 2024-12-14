using Google.Protobuf.WellKnownTypes;
using Library_Management_System.Class;
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
        private readonly BookRepository _bookRepository;
        public FormAddBook()
        {
            InitializeComponent();
            SetupYearPicker();
            // Initialize the repository with the connection string
            string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
            _bookRepository = new BookRepository(connectionString);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddBook();
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
        private void AddBook()
        {
            // Collect data from form inputs
            string title = txtTitle.Text.Trim();
            string authorName = txtAuthor.Text.Trim();
            string publisherName = txtPublisher.Text.Trim();
            string category = comboCategory.Text.Trim();
            string genre = comboGenre.Text.Trim();
            string isbn = txtISBN.Text.Trim();
            string yearPublished = txtYear.Value.ToString("yyyy");
            string description = rtbDescription.Text.Trim();

            // Validate that all fields are filled
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(authorName) || string.IsNullOrWhiteSpace(publisherName) ||
                string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(yearPublished))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get or insert AuthorId and PublisherId
                int authorId = _bookRepository.GetOrInsertAuthor(authorName);
                int publisherId = _bookRepository.GetOrInsertPublisher(publisherName);

                // Create a new Book object
                Book book = new Book
                {
                    Title = title,
                    AuthorId = authorId,
                    PublisherId = publisherId,
                    Category = category,
                    ISBN = isbn,
                    Genre = genre,
                    YearPublished = yearPublished,
                    Description = description
                };

                // Add the book to the database
                if (_bookRepository.AddBook(book))
                {
                    MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields(); // Clear input fields
                }
                else
                {
                    MessageBox.Show("Failed to add the book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
