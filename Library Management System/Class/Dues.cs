using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Class
{
    internal class Dues
    {
        public int DueId { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // E.g., "Pending", "Paid"
    }
}
