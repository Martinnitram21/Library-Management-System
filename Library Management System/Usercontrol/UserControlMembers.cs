using Library_Management_System.Class;
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
    public partial class UserControlMembers : UserControl
    {
        public UserControlMembers()
        {
            InitializeComponent();
            LoadMembersData();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;uid=root;Pwd=martinjericho22@2002;";
        private void LoadMembersData(string searchQuery = "")
        {
            /*
            string query = "SELECT member_id, name, email, phone, membership_date, member_status FROM members_tbl";

            try
            {
                // Open the database connection and fetch data
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable booksTable = new DataTable();
                    adapter.Fill(booksTable);

                    dgvMembers.DataSource = booksTable; // Bind data to DataGridView
                }
            }
            catch (Exception ex)
            {
                // Show error message
                MessageBox.Show($"Error loading books data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    Member_Id AS 'Member_ID', 
                    Name AS 'Name', 
                    Email AS 'Email', 
                    Phone AS 'Phone', 
                    Membership_Date AS 'Membership_Date',
                    member_Status AS 'member_Status'
                FROM Members_tbl";

                    // Add search filter
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE Member_id LIKE @Search OR Name LIKE @Search OR Email LIKE @Search OR Phone LIKE @Search OR member_status LIKE @Search";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@Search", $"%{searchQuery}%");
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvMembers.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControlMembers_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAddMember formAddMember = new FormAddMember();
            formAddMember.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count > 0)
            {
                // Get the selected member's ID
                int memberId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["Member_ID"].Value);

                // Open the edit form and pass the member ID
                FormEditMember formEditMember = new FormEditMember(memberId);
                formEditMember.ShowDialog();

                // Refresh data after editing
                LoadMembersData();
            }
            else
            {
                MessageBox.Show("Please select a member to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count > 0)
            {
                // Get the selected member's ID
                int memberId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["Member_ID"].Value);

                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteMemberFromDatabase(memberId);
                }
            }
            else
            {
                MessageBox.Show("Please select a member to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteMemberFromDatabase(int memberId)
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
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMembersData(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the member. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchMembers_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchMembers.Text.Trim();
            LoadMembersData(searchQuery);
        }
    }
}
