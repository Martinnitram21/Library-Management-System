using Library_Management_System.Class;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System.Usercontrol
{
    public partial class FormAddMember : Form
    {
        private readonly MemberRepository _memberRepository;
        public FormAddMember(MemberRepository memberRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Display formatted date
            _memberRepository = memberRepository;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMemberToDatabase();
        }
        private void AddMemberToDatabase()
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string memberType = comboMemberType.Text.Trim();
            DateTime membershipDate = DateTime.Now;
            string status = "Active";
            string profilePicPath = profilePictureBox.Tag?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(memberType))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var member = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    MemberType = memberType,
                    MembershipDate = membershipDate,
                    Status = status,
                    ProfilePic = profilePicPath // Add the profile picture path
                };

                _memberRepository.AddOrUpdateMember(member);

                MessageBox.Show("Member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            comboMemberType.SelectedIndex = -1;
        }

        private void FormAddMember_Load(object sender, EventArgs e)
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

        private void profilePictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
