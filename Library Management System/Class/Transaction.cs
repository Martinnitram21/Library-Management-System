using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public void AddTransactionToDatabase(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Transactions (MemberId, BookId, BorrowDate, DueDate, ReturnDate, Fine) " +
                                   "VALUES (@MemberId, @BookId, @BorrowDate, @DueDate, @ReturnDate, @Fine)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberId", MemberId);
                        cmd.Parameters.AddWithValue("@BookId", BookId);
                        cmd.Parameters.AddWithValue("@BorrowDate", BorrowDate);
                        cmd.Parameters.AddWithValue("@DueDate", DueDate);
                        cmd.Parameters.AddWithValue("@ReturnDate", (object)ReturnDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Fine", Fine);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Transaction added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error adding transaction.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        /*
        public static Transaction GetTransactionFromDatabase(int transactionId, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT TransactionId, MemberId, BookId, BorrowDate, DueDate, ReturnDate, Fine " +
                                   "FROM Transactions WHERE TransactionId = @TransactionId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionId", transactionId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                return new Transaction(
                                    reader.GetInt32(0), // TransactionId
                                    reader.GetInt32(1), // MemberId
                                    reader.GetInt32(2), // BookId
                                    reader.GetDateTime(3), // BorrowDate
                                    reader.GetDateTime(4), // DueDate
                                    //reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5), // ReturnDate (nullable)
                                    reader.GetDecimal(6) // Fine
                                );
                            }
                            else
                            {
                                Console.WriteLine("Transaction not found.");
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
        */
        public void UpdateReturnDateAndFine(int transactionId, DateTime returnDate, decimal fine, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Transactions SET ReturnDate = @ReturnDate, Fine = @Fine WHERE TransactionId = @TransactionId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                        cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                        cmd.Parameters.AddWithValue("@Fine", fine);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Transaction updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error updating transaction.");
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
