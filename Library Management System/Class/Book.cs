using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Class
{
    internal class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int YearPublished { get; set; }
        public bool IsAvailable { get; set; } // Indicates if the book is available for checkout

        // Constructor
        public Book(int bookId, string title, string author, string category, string genre, string isbn, int yearPublished, bool isAvailable)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            Category = category;
            Genre = genre;
            ISBN = isbn;
            YearPublished = yearPublished;
            IsAvailable = isAvailable;
        }
        public void AddBookToDatabase(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Books (Title, Author, Category, Genre, ISBN, YearPublished, IsAvailable) " +
                                   "VALUES (@Title, @Author, @Category, @Genre, @ISBN, @YearPublished, @IsAvailable)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", Title);
                        cmd.Parameters.AddWithValue("@Author", Author);
                        cmd.Parameters.AddWithValue("@Category", Category);
                        cmd.Parameters.AddWithValue("@Genre", Genre);
                        cmd.Parameters.AddWithValue("@ISBN", ISBN);
                        cmd.Parameters.AddWithValue("@YearPublished", YearPublished);
                        cmd.Parameters.AddWithValue("@IsAvailable", IsAvailable);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Book added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error adding book.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public static Book GetBookFromDatabase(int bookId, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT BookId, Title, Author, Category, Genre, ISBN, YearPublished, IsAvailable " +
                                   "FROM Books WHERE BookId = @BookId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookId", bookId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Book(
                                    reader.GetInt32(0), // BookId
                                    reader.GetString(1), // Title
                                    reader.GetString(2), // Author
                                    reader.GetString(3), // Category
                                    reader.GetString(4), // Genre
                                    reader.GetString(5), // ISBN
                                    reader.GetInt32(6), // YearPublished
                                    reader.GetBoolean(7) // IsAvailable
                                );
                            }
                            else
                            {
                                Console.WriteLine("Book not found.");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
        public void UpdateBookAvailability(int bookId, bool isAvailable, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Books SET IsAvailable = @IsAvailable WHERE BookId = @BookId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        cmd.Parameters.AddWithValue("@IsAvailable", isAvailable);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Book availability updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error updating book availability.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
