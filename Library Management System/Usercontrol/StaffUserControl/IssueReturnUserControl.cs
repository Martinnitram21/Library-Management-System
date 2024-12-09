using Library_Management_System.UI;
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
            /*
            StaffFormIssueBook formIssueBook = new StaffFormIssueBook();
            formIssueBook.ShowDialog();
            */

            FormBorrowBook borrowForm = new FormBorrowBook();
            borrowForm.ShowDialog(); // Opens the borrow form
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            /*
            StaffFormReturnBook formReturnBook  = new StaffFormReturnBook();
            formReturnBook.ShowDialog();
            */

            FormReturnBook returnForm = new FormReturnBook();
            returnForm.ShowDialog(); // Opens the return form
        }
    }
}
