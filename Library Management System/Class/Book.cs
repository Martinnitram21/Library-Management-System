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
        // Properties
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; } // Optional: Use AuthorId or Author Name depending on the context
        public int PublisherId { get; set; }
        public string Publisher { get; set; } // Optional: Use PublisherId or Publisher Name depending on the context
        public string ISBN { get; set; }
        public string Category { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string YearPublished { get; set; }

        // Default constructor
        public Book()
        {
        }

        // Parameterized constructor with BookId (useful for updates)
        public Book(int bookId, string title, int authorId, string author, int publisherId, string publisher, string isbn, string category, string genre, string status, string description, string yearPublished)
        {
            BookId = bookId;
            Title = title;
            AuthorId = authorId;
            Author = author;
            PublisherId = publisherId;
            Publisher = publisher;
            ISBN = isbn;
            Category = category;
            Genre = genre;
            Status = status;
            Description = description;
            YearPublished = yearPublished;
        }

        // Parameterized constructor without BookId (for new books)
        public Book(string title, int authorId, string author, int publisherId, string publisher, string isbn, string category, string genre, string description, string yearPublished)
        {
            Title = title;
            AuthorId = authorId;
            Author = author;
            PublisherId = publisherId;
            Publisher = publisher;
            ISBN = isbn;
            Category = category;
            Genre = genre;
            Description = description;
            YearPublished = yearPublished;
        }
    }
}
