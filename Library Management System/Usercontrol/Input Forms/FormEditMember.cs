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

namespace Library_Management_System.Usercontrol
{
    public partial class FormEditMember : Form
    {
        private readonly int _memberId;
        private readonly string _connectionString = "Server=localhost;Database=librarydb;uid=root;Pwd=martinjericho22@2002;";
        public FormEditMember(int memberId)
        {
            InitializeComponent();
            _memberId = memberId;
            LoadMemberData();
        }
        private void LoadMemberData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT Name, Email, Phone, Membership_Date, Member_Status FROM Members_tbl WHERE Member_Id = @MemberId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberId", _memberId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtPhone.Text = reader["Phone"].ToString();
                                dtpMembershipDate.Value = Convert.ToDateTime(reader["Membership_Date"]);
                                comboStatus.Text = reader["Member_Status"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Members_tbl 
                                 SET Name = @Name, Email = @Email, Phone = @Phone, Membership_Date = @MembershipDate, Member_Status = @Status 
                                 WHERE Member_Id = @MemberId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@MembershipDate", dtpMembershipDate.Value);
                        cmd.Parameters.AddWithValue("@Status", comboStatus.Text);
                        cmd.Parameters.AddWithValue("@MemberId", _memberId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Member details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving member details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
