using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System.Class
{
    internal class BookRepository
    {
        private readonly string connectionString;

        public BookRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        // Load books data from the database
        public List<Book> GetBooks(string searchQuery = "")
        {
            List<Book> books = new List<Book>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                    SELECT 
                        b.Book_Id, 
                        b.Title, 
                        b.Category,
                        b.ISBN,
                        b.Genre,
                        a.Author_Name, 
                        p.Publisher_Name, 
                        b.Year_Published,
                        b.Book_Status,
                        b.Description
                    FROM Books_tbl b
                    LEFT JOIN Authors_tbl a ON b.Author_Id = a.Author_Id
                    LEFT JOIN Publishers_tbl p ON b.Publisher_Id = p.Publisher_Id";

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

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                books.Add(new Book
                                {
                                    BookId = Convert.ToInt32(reader["Book_Id"]),
                                    Title = reader["Title"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    ISBN = reader["ISBN"].ToString(),
                                    Genre = reader["Genre"].ToString(),
                                    Author = reader["Author_Name"].ToString(),
                                    Publisher = reader["Publisher_Name"].ToString(),
                                    YearPublished = reader["Year_Published"].ToString(),
                                    Status = reader["Book_Status"].ToString(),
                                    Description = reader["Description"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return books;
        }

        // Add a new book to the database
        public bool AddBook(Book book)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                    INSERT INTO books_tbl (Title, Author_Id, Publisher_Id, Category, ISBN, Genre, Year_Published, Description) 
                    VALUES (@Title, @AuthorId, @PublisherId, @Category, @ISBN, @Genre, @YearPublished, @Description)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", book.Title);
                        cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                        cmd.Parameters.AddWithValue("@PublisherId", book.PublisherId);
                        cmd.Parameters.AddWithValue("@Category", book.Category);
                        cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                        cmd.Parameters.AddWithValue("@Genre", book.Genre);
                        cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                        cmd.Parameters.AddWithValue("@Description", book.Description);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Delete a book from the database
        public bool DeleteBook(int bookId)
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
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Get or insert an author and return its ID
        public int GetOrInsertAuthor(string authorName)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string selectQuery = "SELECT Author_Id FROM Authors_tbl WHERE Author_Name = @Name";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", authorName);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }

                string insertQuery = "INSERT INTO Authors_tbl (Author_Name) VALUES (@Name); SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", authorName);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Get or insert a publisher and return its ID
        public int GetOrInsertPublisher(string publisherName)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string selectQuery = "SELECT Publisher_Id FROM Publishers_tbl WHERE Publisher_Name = @Name";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", publisherName);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }

                string insertQuery = "INSERT INTO Publishers_tbl (Publisher_Name) VALUES (@Name); SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", publisherName);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}