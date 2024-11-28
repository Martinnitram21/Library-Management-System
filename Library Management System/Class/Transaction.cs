using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Class
{
    internal class Transaction
    {
        public int TransactionId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Nullable to handle when the book isn't returned yet
        public decimal Fine { get; set; }

        // Constructor
        public Transaction(int transactionId, int memberId, int bookId, DateTime borrowDate, DateTime dueDate, DateTime returnDate, decimal fine)
        {
            TransactionId = transactionId;
            MemberId = memberId;
            BookId = bookId;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            ReturnDate = null; // Will be assigned when the book is returned
            Fine = fine;
        }
    }
}
