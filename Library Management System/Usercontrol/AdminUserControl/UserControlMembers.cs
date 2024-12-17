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
        private readonly MemberRepository memberRepository;
        public UserControlMembers()
        {
            InitializeComponent();
            memberRepository = new MemberRepository("Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;");
            LoadMembersData();
        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;uid=root;Pwd=martinjericho22@2002;";
        private void LoadMembersData(string searchQuery = "")
        {
            try
            {
                List<Member> members = memberRepository.GetMembers(searchQuery);

                dgvMembers.DataSource = members.Select(m => new
                {
                    m.MemberId,
                    m.FirstName,
                    m.LastName,
                    m.Email,
                    m.Phone,
                    m.MemberType,
                    m.MembershipDate,
                    m.Status
                }).ToList();

                // Hide the BookId column
                dgvMembers.Columns["MemberId"].Visible = false;
                dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControlMembers_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAddMember formAddMember = new FormAddMember(memberRepository);
            if (formAddMember.ShowDialog() == DialogResult.OK)
            {
                LoadMembersData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count > 0)
            {
                int memberId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MemberId"].Value);
                FormEditMember formEditMember = new FormEditMember(memberId);
                if (formEditMember.ShowDialog() == DialogResult.OK)
                {
                    LoadMembersData();
                }
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
                int memberId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MemberId"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (memberRepository.DeleteMember(memberId))
                        {
                            MessageBox.Show("Member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMembersData();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void dgvMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvMembers.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
                {
                    string status = e.Value.ToString();
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // Apply background color based on the status value
                    if (status == "Active")
                    {
                        e.CellStyle.BackColor = Color.LightGreen; // Green for 'Available'
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else if (status == "Inactive")
                    {
                        e.CellStyle.BackColor = Color.LightCoral; // Red for 'Borrowed'
                        e.CellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error formatting cells: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
