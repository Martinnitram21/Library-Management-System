using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Class
{
    public class MemberRepository
    {
        private readonly string connectionString;

        public MemberRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        // Load members data from the database
        public List<Member> GetMembers(string searchQuery = "")
        {
            List<Member> members = new List<Member>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT Member_Id, First_Name, Last_Name, Email, Phone, Member_Type, Membership_Date, Member_Status 
                                     FROM Members_tbl";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += @" WHERE Member_Id LIKE @Search 
                                  OR First_Name LIKE @Search 
                                  OR Last_Name LIKE @Search 
                                  OR Email LIKE @Search 
                                  OR Phone LIKE @Search 
                                  OR Member_Type LIKE @Search 
                                  OR Member_Status LIKE @Search";
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
                                members.Add(new Member
                                {
                                    MemberId = Convert.ToInt32(reader["Member_Id"]),
                                    FirstName = reader["First_Name"].ToString(),
                                    LastName = reader["Last_Name"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    MemberType = reader["Member_Type"].ToString(),
                                    MembershipDate = Convert.ToDateTime(reader["Membership_Date"]),
                                    Status = reader["Member_Status"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading members: {ex.Message}");
            }

            return members;
        }

        public bool DeleteMember(int memberId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Members_tbl WHERE Member_Id = @MemberId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberId", memberId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting member: {ex.Message}");
            }
        }

        public void AddOrUpdateMember(Member member, bool isUpdate = false)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = isUpdate
                        ? @"UPDATE Members_tbl 
                            SET First_Name = @FirstName, Last_Name = @LastName, Email = @Email, Phone = @Phone, 
                            Member_Type = @MemberType, Membership_Date = @MembershipDate, Member_Status = @Status 
                            WHERE Member_Id = @MemberId"
                        : @"INSERT INTO Members_tbl (First_Name, Last_Name, Email, Phone, Member_Type, Membership_Date, Member_Status) 
                            VALUES (@FirstName, @LastName, @Email, @Phone, @MemberType, @MembershipDate, @Status)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (isUpdate)
                        {
                            cmd.Parameters.AddWithValue("@MemberId", member.MemberId); // Only include MemberId if updating
                        }
                        cmd.Parameters.AddWithValue("@FirstName", member.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", member.LastName);
                        cmd.Parameters.AddWithValue("@Email", member.Email);
                        cmd.Parameters.AddWithValue("@Phone", member.Phone);
                        cmd.Parameters.AddWithValue("@MemberType", member.MemberType);
                        cmd.Parameters.AddWithValue("@MembershipDate", member.MembershipDate);
                        cmd.Parameters.AddWithValue("@Status", member.Status);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (use a logging framework here)
                Console.WriteLine($"Error saving member: {ex.Message}");
                throw new Exception("An error occurred while saving the member. Please contact support.");
            }
        }
    }
}
