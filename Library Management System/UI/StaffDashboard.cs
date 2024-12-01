using Library_Management_System.Usercontrol.StaffUserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private readonly StaffDashBoardUserControl staffDashBoardUserControl = new StaffDashBoardUserControl();

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            MovePanel(btnDashboard);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(staffDashBoardUserControl);
        }

        private void btnBorrowReturn_Click(object sender, EventArgs e)
        {

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
    }
}
