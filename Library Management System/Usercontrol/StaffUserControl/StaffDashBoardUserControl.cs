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
    public partial class StaffDashBoardUserControl : UserControl
    {
        public StaffDashBoardUserControl()
        {
            InitializeComponent();
        }

        private void StaffDashBoard_Load(object sender, EventArgs e)
        {

        }

        private void btnFindBook_Click(object sender, EventArgs e)
        {
            FindBookUserControl findBookUserControl = new FindBookUserControl();
            LoadDynamicContent(findBookUserControl);
        }

        private void btnFindIssueBook_Click(object sender, EventArgs e)
        {
            FindIssueBookUserControl findIssueBookUserControl = new FindIssueBookUserControl();
            LoadDynamicContent(findIssueBookUserControl);
        }
        private void LoadDynamicContent(UserControl control)
        {
            // Clear existing content
            pnlDynamicContent.Controls.Clear();

            // Add the selected UserControl
            control.Dock = DockStyle.Fill;
            pnlDynamicContent.Controls.Add(control);
        }

        private void btnFindMember_Click(object sender, EventArgs e)
        {
            FindStudentUserControl findStudentControl = new FindStudentUserControl();
            LoadDynamicContent(findStudentControl);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
