using Library_Management_System.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        // Connection string to the MySQL database
        private string connectionString = "server=localhost;database=librarydb;uid=root;pwd=martinjericho22@2002;";

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
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
                // Connect to the database
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
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            FormSignup signUpForm = new FormSignup();
            signUpForm.ShowDialog(); // Open the Sign-Up Form as a dialog
        }
    }
}
