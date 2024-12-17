﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
                    string query = "SELECT first_Name, last_name, Email, Phone, member_type, Membership_Date, Member_Status, profile_pic FROM Members_tbl WHERE Member_Id = @MemberId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberId", _memberId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFirstName.Text = reader["first_Name"].ToString();
                                txtLastName.Text = reader["last_Name"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtPhone.Text = reader["Phone"].ToString();
                                comboMemberType.Text = reader["Member_type"].ToString();
                                dtpMembershipDate.Value = Convert.ToDateTime(reader["Membership_Date"]);
                                comboStatus.Text = reader["Member_Status"].ToString();

                                // Check if profile_pic is NULL or empty
                                var profilePicPath = reader["profile_pic"]?.ToString();
                                if (!string.IsNullOrEmpty(profilePicPath) && System.IO.File.Exists(profilePicPath))
                                {
                                    // Load the image if the path is valid
                                    profilePictureBox.Image = Image.FromFile(profilePicPath);
                                    profilePictureBox.Tag = profilePicPath; // Store the file path in the Tag
                                }
                                else
                                {
                                    profilePictureBox.Image = null; // No profile image found
                                }
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
                                 SET first_Name = @FirstName, last_name = @LastName, Email = @Email, Phone = @Phone, member_type = @MemberType, Membership_Date = @MembershipDate, Member_Status = @Status, profile_pic = @ProfilePic 
                                 WHERE Member_Id = @MemberId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@MemberType", comboMemberType.Text);
                        cmd.Parameters.AddWithValue("@MembershipDate", dtpMembershipDate.Value);
                        cmd.Parameters.AddWithValue("@Status", comboStatus.Text);
                        cmd.Parameters.AddWithValue("@MemberId", _memberId);
                        cmd.Parameters.AddWithValue("@ProfilePic", profilePictureBox.Tag != null
                        ? profilePictureBox.Tag.ToString()
                        : (object)DBNull.Value);

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

        private void FormEditMember_Load(object sender, EventArgs e)
        {

        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    profilePictureBox.Image = Image.FromFile(filePath);
                    profilePictureBox.Tag = filePath; // Temporarily store the file path
                }
            }
        }
    }
}
