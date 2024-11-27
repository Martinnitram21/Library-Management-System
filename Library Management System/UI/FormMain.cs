using Library_Management_System.Usercontrol;
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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void MovePanel(Control btn)
        {
            panelSlide.Top = btn.Top;
            panelSlide.Height = btn.Height;
        }
        UserControlDashboard ucdash = new UserControlDashboard();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MovePanel(btnDashboard);
            mainPanel.Controls.Clear();      // Clear existing controls in the panel
            ucdash.Dock = DockStyle.Fill;  // Make it fill the panel
            mainPanel.Controls.Add(ucdash); // Add the user control to the panel
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            MovePanel(btnBooks);
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            MovePanel(btnMembers);
        }

        private void btnBorrowReturn_Click(object sender, EventArgs e)
        {
            MovePanel(btnBorrowReturn);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MovePanel(btnSearch);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MovePanel(btnReports);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            MovePanel(btnSettings);
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
    }
}
