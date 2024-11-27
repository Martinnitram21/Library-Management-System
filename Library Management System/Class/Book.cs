using System;
using System.Collections.Generic;
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
    }
}
