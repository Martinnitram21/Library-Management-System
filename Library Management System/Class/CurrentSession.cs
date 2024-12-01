using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Class
{
    internal class CurrentSession
    {
        // Property to store the logged-in user's username
        public static string Username { get; set; } = null;

        // Property to store the logged-in user's role (Admin or Staff)
        public static string Role { get; set; } = null;
    }
}
