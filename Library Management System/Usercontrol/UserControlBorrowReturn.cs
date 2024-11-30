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

namespace Library_Management_System.Usercontrol
{
    public partial class UserControlBorrowReturn : UserControl
    {
        public UserControlBorrowReturn()
        {
            InitializeComponent();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            FormBorrowBook borrowForm = new FormBorrowBook();
            borrowForm.ShowDialog(); // Opens the borrow form
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormReturnBook returnForm = new FormReturnBook();
            returnForm.ShowDialog(); // Opens the return form
        }
    }
}
