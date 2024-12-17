using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_Management_System.Class
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberType { get; set; }
        public DateTime MembershipDate { get; set; }
        public string Status { get; set; }
        public string ProfilePic { get; set; } // Path to the profile picture


        // Default constructor
        public Member() { }

        // Constructor
        public Member(int memberId, string firstname, string lastname, string email, string phone, string membertype, DateTime membershipDate, string status)
        {
            MemberId = memberId;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Phone = phone;
            MemberType = membertype;
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

                    string query = "INSERT INTO Members (FirstName, LastName, Email, Phone, MembershipDate, Status) VALUES (@Name, @Email, @Phone, @MembershipDate, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
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
                                    reader.GetString(1), // First Name
                                    reader.GetString(2), // Last Name
                                    reader.GetString(3), // Email
                                    reader.GetString(4), // Phone
                                    reader.GetString(5), // Member Type
                                    reader.GetDateTime(6), // MembershipDate
                                    reader.GetString(7)  // Status
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
