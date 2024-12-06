using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_Management_System.Class
{
    internal class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime MembershipDate { get; set; }
        public string Status { get; set; }

        // Constructor
        public Member(int memberId, string fullName, string email, string phone, DateTime membershipDate, string status)
        {
            MemberId = memberId;
            FullName = fullName;
            Email = email;
            Phone = phone;
            MembershipDate = membershipDate;
            Status = status;
        }

        public bool CanBorrow(Member member)
        {
            return member.Status == "Active";
        }
        public void AddMemberToDatabase(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Members (FullName, Email, Phone, MembershipDate, Status) VALUES (@FullName, @Email, @Phone, @MembershipDate, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", FullName);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@MembershipDate", MembershipDate);
                        cmd.Parameters.AddWithValue("@Status", Status);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Member added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error adding member.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public static Member GetMemberFromDatabase(int memberId, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT MemberId, FullName, Email, Phone, MembershipDate, Status FROM Members WHERE MemberId = @MemberId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberId", memberId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Member(
                                    reader.GetInt32(0), // MemberId
                                    reader.GetString(1), // FullName
                                    reader.GetString(2), // Email
                                    reader.GetString(3), // Phone
                                    reader.GetDateTime(4), // MembershipDate
                                    reader.GetString(5)  // Status
                                );
                            }
                            else
                            {
                                Console.WriteLine("Member not found.");
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
    }
}
