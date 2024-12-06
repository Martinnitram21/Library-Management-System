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
    public partial class IssueReturnUserControl : UserControl
    {
        public IssueReturnUserControl()
        {
            InitializeComponent();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            FormIssueBook formIssueBook = new FormIssueBook();
            formIssueBook.ShowDialog();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormReturnBook formReturnBook  = new FormReturnBook();
            formReturnBook.ShowDialog();
        }
    }
}
