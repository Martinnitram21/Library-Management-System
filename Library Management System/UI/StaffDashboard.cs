using Library_Management_System.Usercontrol;
using Library_Management_System.Usercontrol.StaffUserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System.UI
{
    public partial class StaffDashboard : Form
    {
        public StaffDashboard()
        {
            InitializeComponent();
        }

        //private readonly StaffDashBoardUserControl staffDashBoardUserControl = new StaffDashBoardUserControl();
        private readonly UserControlDashboard userControlDashboard = new UserControlDashboard();
        private readonly FindBookUserControl findBookUserControl = new FindBookUserControl();
        private readonly FindStudentUserControl findStudentUserControl = new FindStudentUserControl();
        private readonly IssueReturnUserControl issueReturnUserControl = new IssueReturnUserControl();
        private readonly ViewLogUserControl viewLogUserControl = new ViewLogUserControl();

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            MovePanel(btnDashboard);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(userControlDashboard);
        }

        private void btnBorrowReturn_Click(object sender, EventArgs e)
        {
            MovePanel(btnBorrowReturn);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(issueReturnUserControl);
        }
        private void MovePanel(Control btn)
        {
            panelSlide.Top = btn.Top;
            panelSlide.Height = btn.Height;
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

        private void btnBooks_Click(object sender, EventArgs e)
        {
            MovePanel(btnBooks);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(findBookUserControl);
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            MovePanel(btnMembers);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(findStudentUserControl);
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

        private void btnReports_Click(object sender, EventArgs e)
        {
            MovePanel(btnReports);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(viewLogUserControl);
        }
    }
}
