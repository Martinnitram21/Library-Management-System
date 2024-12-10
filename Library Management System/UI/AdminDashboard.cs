using Library_Management_System.Class;
using Library_Management_System.Usercontrol;
using Library_Management_System.Usercontrol.AdminUserControl;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Library_Management_System.UI
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }
        private void MovePanel(Control btn)
        {
            panelSlide.Top = btn.Top;
            panelSlide.Height = btn.Height;
        }
        readonly UserControlDashboard ucDash = new UserControlDashboard();
        readonly UserControlBooks ucBook = new UserControlBooks();
        readonly UserControlMembers ucMember = new UserControlMembers();
        readonly UserControlBorrowReturn ucBorrow = new UserControlBorrowReturn();
        readonly UserControlReports ucReport = new UserControlReports();
        readonly UserControlSettings ucSetting = new UserControlSettings();
        readonly UserControlAuthPub ucAuthPub = new UserControlAuthPub();
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblWelcome.Text = $"Welcome, {CurrentSession.Username}!"; // Example: Display username
            //lblRole.Text = $"Role: {CurrentSession.Role}";            // Example: Display role
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MovePanel(btnDashboard);
            mainPanel.Controls.Clear();      // Clear existing controls in the panel
            mainPanel.Controls.Add(ucDash); // Add the user control to the panel
            //ucDash.Dock = DockStyle.Fill;  // Make it fill the panel
            //userControlBooks1.Hide();
            //userControlDashboard1.Show();

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            MovePanel(btnBooks);
            mainPanel.Controls.Clear();      // Clear existing controls in the panel
            mainPanel.Controls.Add(ucBook); // Add the user control to the panel
            //ucBook.Dock = DockStyle.Fill;  // Make it fill the panel
            //userControlDashboard1.Hide();
            //userControlBooks1.Show();
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            MovePanel(btnMembers);
            mainPanel.Controls.Clear();      // Clear existing controls in the panel
            mainPanel.Controls.Add(ucMember); // Add the user control to the panel
        }

        private void btnBorrowReturn_Click(object sender, EventArgs e)
        {
            MovePanel(btnBorrowReturn);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ucBorrow);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MovePanel(btnReports);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ucReport);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            MovePanel(btnSettings);
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.ExitThread(); // Terminate current thread
                System.Diagnostics.Process.Start(Application.ExecutablePath); // Restart the application
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                timer1.Stop();
                Application.Exit();
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }

        private void userControlBooks1_Load(object sender, EventArgs e)
        {

        }

        private void btnAuthPub_Click(object sender, EventArgs e)
        {
            MovePanel(btnAuthPub);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ucAuthPub);
        }
    }
}
