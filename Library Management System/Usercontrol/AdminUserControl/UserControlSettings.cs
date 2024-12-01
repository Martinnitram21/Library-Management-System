using Library_Management_System.Class;
using Library_Management_System.UI;
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

namespace Library_Management_System.Usercontrol
{
    public partial class UserControlSettings : UserControl
    {
        public UserControlSettings()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }
        private void Logout()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Clear session data (optional)
                CurrentSession.Username = null;
                CurrentSession.Role = null;

                // Redirect to login
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();

                // Close this form
                this.Hide();
            }
        }
    }
}
