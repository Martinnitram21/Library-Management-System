using Google.Protobuf.WellKnownTypes;
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
    public partial class FindStudentUserControl : UserControl
    {
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

        public FindStudentUserControl()
        {
            InitializeComponent();
        }
        private int studentId; // Class-level variable to store Student ID
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
        m.member_type AS 'Type',
        m.Membership_Date AS 'Membership Date',
        m.Member_Status AS 'Status',
        m.profile_pic AS 'Profile Pic',
        br.Borrower_Id AS 'Borrow ID',
        bk.Book_Id AS 'Book ID',
        bk.Title AS 'Book Title',
        br.Borrow_Date AS 'Borrow Date',
        br.Due_Date AS 'Due Date',
        bk.Book_Status AS 'Book Status'
    FROM 
        Members_tbl m
    LEFT JOIN 
        Borrowers_tbl br ON m.Member_Id = br.Member_Id
    LEFT JOIN 
        Books_tbl bk ON br.Book_Id = bk.Book_Id
    WHERE 
        m.last_name LIKE @Name OR CONCAT(m.first_name, ' ', m.last_name) LIKE @Name
    ORDER BY 
        br.Borrow_Date DESC";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", "%" + studentName + "%");
                        MySqlDataReader reader = cmd.ExecuteReader();

                        // Dictionary to store all matching students and their details
                        Dictionary<int, Dictionary<string, string>> studentDetails = new Dictionary<int, Dictionary<string, string>>();
                        cmbSearchResults.Items.Clear(); // Clear any previous items

                        while (reader.Read())
                        {
                            int currentStudentId = Convert.ToInt32(reader["Student ID"]);
                            string displayName = $"{reader["First Name"]} {reader["Last Name"]}";

                            cmbSearchResults.Items.Add(displayName);

                            // Store details for this student
                            studentDetails[currentStudentId] = new Dictionary<string, string>
                    {
                        { "Student ID", currentStudentId.ToString() },
                        { "Name", displayName },
                        { "Email", reader["Email"].ToString() },
                        { "Phone", reader["Phone"].ToString() },
                        { "Type", reader["Type"].ToString() },
                        { "Membership Date", Convert.ToDateTime(reader["Membership Date"]).ToShortDateString() },
                        { "Status", reader["Status"].ToString() },
                        { "Borrow ID", reader["Borrow ID"].ToString() },
                        { "Book ID", reader["Book ID"].ToString() },
                        { "Book Title", reader["Book Title"].ToString() },
                        { "Borrow Date", reader["Borrow Date"].ToString() },
                        { "Due Date", reader["Due Date"].ToString() },
                        { "Book Status", reader["Book Status"].ToString() },
                        { "Profile Pic", reader["Profile Pic"].ToString() }
                    };
                        }

                        if (cmbSearchResults.Items.Count == 0)
                        {
                            MessageBox.Show("No records found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (cmbSearchResults.Items.Count == 1)
                        {
                            // If only one result, auto-select and populate fields
                            PopulateFields(studentDetails.First().Value);
                        }
                        else
                        {
                            // Display ComboBox and allow user to select
                            cmbSearchResults.Visible = true;
                            cmbSearchResults.Tag = studentDetails; // Store details in the ComboBox's Tag property
                            MessageBox.Show("Multiple records found. Please select a student from the list.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFields()
        {
            lblStudentID.Text = "?";
            lblName.Text = "?";
            lblEmail.Text = "?";
            lblPhone.Text = "?";
            lblMemberType.Text = "?";
            lblMembershipDate.Text = "?";
            lblStatus.Text = "?";

            lblTransactionID.Text = "?";
            lblBookID.Text = "?";
            lblBookTitle.Text = "?";
            lblBorrowDate.Text = "?";
            lblDueDate.Text = "?";
            lblBookStatus.Text = "?";
        }

        private void PopulateFields(Dictionary<string, string> details)
        {
            lblStudentID.Text = details["Student ID"];
            lblName.Text = details["Name"];
            lblEmail.Text = details["Email"];
            lblPhone.Text = details["Phone"];
            lblMemberType.Text = details["Type"];
            lblMembershipDate.Text = details["Membership Date"];
            lblStatus.Text = details["Status"];
            lblTransactionID.Text = details["Borrow ID"];
            lblBookID.Text = details["Book ID"];
            lblBookTitle.Text = details["Book Title"];
            lblBorrowDate.Text = details["Borrow Date"];
            lblDueDate.Text = details["Due Date"];
            lblBookStatus.Text = details["Book Status"];

            // Display the profile image
            string profilePicPath = details["Profile Pic"];
            if (!string.IsNullOrEmpty(profilePicPath) && System.IO.File.Exists(profilePicPath))
            {
                pictureBoxProfile.Image = Image.FromFile(profilePicPath); // Assuming you have a PictureBox control for the profile image
            }
            else
            {
                pictureBoxProfile.Image = null; // No image found, set to null or default image
            }
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            if (studentId != 0)
            {

                // Open the edit form and pass the member ID
                FormEditMember formEditMember = new FormEditMember(studentId);
                formEditMember.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please select a member to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
            MemberRepository memberRepository = new MemberRepository(connectionString);
            FormAddMember formAddMember = new FormAddMember(memberRepository);
            formAddMember.ShowDialog();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (studentId != 0)
            {

                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteMemberFromDatabase(studentId);
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

        private void cmbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearchResults.SelectedIndex >= 0)
            {
                // Retrieve the selected student's details from the ComboBox's Tag
                var studentDetails = (Dictionary<int, Dictionary<string, string>>)cmbSearchResults.Tag;

                // Find the selected student's name
                string selectedName = cmbSearchResults.SelectedItem.ToString();

                // Find the matching student details
                var selectedStudent = studentDetails.FirstOrDefault(
                    kvp => kvp.Value["Name"] == selectedName
                );

                if (selectedStudent.Value != null)
                {
                    PopulateFields(selectedStudent.Value);
                    cmbSearchResults.Visible = false; // Hide ComboBox after selection
                }
            }
        }

        private void FindStudentUserControl_Load(object sender, EventArgs e)
        {

        }
    }
}
