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
            m.last_name AS 'Name',
            m.Email AS 'Email',
            m.Phone AS 'Phone',
            m.Membership_Date AS 'Membership Date',
            m.Member_Status AS 'Status',
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
            m.last_Name LIKE @Name
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

                        // Clear existing labels or fields
                        ClearFields();

                        if (reader.Read())
                        {
                            // Fill Student Details
                            lblStudentID.Text = reader["Student ID"].ToString();
                            lblName.Text = reader["Name"].ToString();
                            lblEmail.Text = reader["Email"].ToString();
                            lblPhone.Text = reader["Phone"].ToString();
                            lblMembershipDate.Text = Convert.ToDateTime(reader["Membership Date"]).ToShortDateString();
                            lblStatus.Text = reader["Status"].ToString();

                            // Fill Issued Book Details
                            lblTransactionID.Text = reader["Borrow ID"].ToString();
                            lblBookID.Text = reader["Book ID"].ToString();
                            lblBookTitle.Text = reader["Book Title"].ToString();
                            lblBorrowDate.Text = reader["Borrow Date"].ToString();
                            lblDueDate.Text = reader["Due Date"].ToString();
                            lblBookStatus.Text = reader["Book Status"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No records found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            lblMembershipDate.Text = "?";
            lblStatus.Text = "?";

            lblTransactionID.Text = "?";
            lblBookID.Text = "?";
            lblBookTitle.Text = "?";
            lblBorrowDate.Text = "?";
            lblDueDate.Text = "?";
            lblBookStatus.Text = "?";
        }
    }
}
