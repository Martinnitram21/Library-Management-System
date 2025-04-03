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

namespace Library_Management_System.Usercontrol.StaffUserControl
{
    public partial class FindMemberUserControl : UserControl
    {
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
        public FindMemberUserControl()
        {
            InitializeComponent();
            LoadAllMembers(); // Load all members on control load
        }
        private void LoadAllMembers()
        {
            SearchStudent(""); // Pass empty string to load all members
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string studentName = txtSearchMember.Text.Trim();

            if (!string.IsNullOrEmpty(studentName))
            {
                SearchStudent(studentName);
            }
            else
            {
                MessageBox.Show("Please enter a student name to search.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void SearchStudent(string studentName)
        {
            string query = @"
    SELECT 
        m.Member_Id AS 'Student ID',
        m.first_name AS 'First Name',
        m.last_name AS 'Last Name',
        m.Email AS 'Email',
        m.Phone AS 'Phone',
        m.profile_pic AS 'Profile Pic'
    FROM 
        Members_tbl m
    WHERE 
        @Name = '' OR m.last_name LIKE @Name OR CONCAT(m.first_name, ' ', m.last_name) LIKE @Name
    ORDER BY 
        m.Member_Id";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", "%" + studentName + "%");
                        MySqlDataReader reader = cmd.ExecuteReader();

                        flowLayoutPanelMembers.Controls.Clear(); // Clear existing controls

                        while (reader.Read())
                        {
                            // Create a panel for each member
                            int memberId = Convert.ToInt32(reader["Student ID"]);
                            string fullName = $"{reader["First Name"]} {reader["Last Name"]}";
                            string email = reader["Email"].ToString();
                            string phone = reader["Phone"].ToString();
                            string profilePic = reader["Profile Pic"].ToString();

                            Panel memberPanel = CreateMemberPanel(memberId, fullName, email, phone, profilePic);
                            flowLayoutPanelMembers.Controls.Add(memberPanel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
            MemberRepository memberRepository = new MemberRepository(connectionString);
            FormAddMember formAddMember = new FormAddMember(memberRepository);
            formAddMember.ShowDialog();
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            Button btnEdit = sender as Button;
            int memberId = (int)btnEdit.Tag;

            FormEditMember formEditMember = new FormEditMember(memberId);
            formEditMember.ShowDialog();

            // Refresh the FlowLayoutPanel after editing
            SearchStudent("");
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            Button btnDelete = sender as Button;
            int memberId = (int)btnDelete.Tag;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this member?",
                                                  "Confirm Delete",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteMemberFromDatabase(memberId);
                SearchStudent(""); // Refresh FlowLayoutPanel
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
        private Panel CreateMemberPanel(int memberId, string fullName, string email, string phone, string profilePic)
        {
            Panel panel = new Panel
            {
                Size = new Size(350, 120),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5)
            };

            // Profile Picture
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            if (!string.IsNullOrEmpty(profilePic) && System.IO.File.Exists(profilePic))
            {
                pictureBox.Image = Image.FromFile(profilePic);
            }

            panel.Controls.Add(pictureBox);

            // Member Details
            Label lblName = new Label
            {
                Text = $"Name: {fullName}",
                AutoSize = true,
                Location = new Point(100, 10)
            };
            panel.Controls.Add(lblName);

            Label lblEmail = new Label
            {
                Text = $"Email: {email}",
                AutoSize = true,
                Location = new Point(100, 35)
            };
            panel.Controls.Add(lblEmail);

            Label lblPhone = new Label
            {
                Text = $"Phone: {phone}",
                AutoSize = true,
                Location = new Point(100, 60)
            };
            panel.Controls.Add(lblPhone);

            // Edit Button
            Button btnEdit = new Button
            {
                Text = "Edit",
                Size = new Size(80, 30),
                Location = new Point(190, 85),
                BackColor = Color.Green,
                Tag = memberId // Store memberId in Tag for reference
            };
            btnEdit.Click += btnEditMember_Click;
            panel.Controls.Add(btnEdit);

            // Delete Button
            Button btnDelete = new Button
            {
                Text = "Delete",
                Size = new Size(80, 30),
                Location = new Point(270, 85),
                BackColor = Color.Red,
                Tag = memberId // Store memberId in Tag for reference
            };
            btnDelete.Click += btnDeleteMember_Click;
            panel.Controls.Add(btnDelete);

            return panel;
        }
    }
}
