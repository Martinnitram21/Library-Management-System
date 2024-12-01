using Library_Management_System.Class;
using Library_Management_System.UI;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class FormLogin : Form
    {

        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";

        private void btnLogin_Click(object sender, EventArgs e)
        {
            /*
            // Get user inputs
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Connect to the database using DatabaseHelper
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to check credentials
                    string query = "SELECT COUNT(*) FROM login_auth WHERE username = @Username AND password_hash = MD5(@Password)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userCount > 0)
                        {
                            // Login successful
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide(); // Hide login form
                            FormMain mainForm = new FormMain(); // Load the main application
                            mainForm.Show();
                        }
                        else
                        {
                            // Invalid credentials
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */

            // Get user inputs
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string selectedRole = rbtnAdmin.Checked ? "Admin" : "Staff"; // Get the selected role

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and Password are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Connect to the database
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to validate the user credentials and role
                    string query = "SELECT COUNT(*) FROM users_tbl WHERE username = @Username AND password = SHA2(@Password, 256) AND role = @Role";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Role", selectedRole);

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        // Check if a matching user exists
                        if (userCount > 0)
                        {
                            // Success message and redirect based on role
                            MessageBox.Show($"Welcome {selectedRole}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CurrentSession.Username = txtUsername.Text; // Store the username
                            CurrentSession.Role = selectedRole;         // Store the role

                            if (selectedRole == "Admin")
                            {
                               AdminDashboard adminDashboard = new AdminDashboard();
                               adminDashboard.Show();
                            }
                            else if (selectedRole == "Staff")
                            {
                               StaffDashboard staffDashboard = new StaffDashboard();
                               staffDashboard.Show();
                            }
                            this.Hide(); // Close the login form
                        }
                        else
                        {
                            MessageBox.Show("Invalid credentials or incorrect role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred while connecting to the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            FormSignup signUpForm = new FormSignup();
            signUpForm.ShowDialog(); // Open the Sign-Up Form as a dialog
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
