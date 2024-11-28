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
        private void LoadMembersData()
        {
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
        }

        private void UserControlMembers_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
