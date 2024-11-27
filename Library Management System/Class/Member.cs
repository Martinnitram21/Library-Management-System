using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_Management_System.Class
{
    internal class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime MembershipDate { get; set; }
        public string Status { get; set; }

        // Constructor
        public Member(int memberId, string fullName, string email, string phone, DateTime membershipDate, string status)
        {
            MemberId = memberId;
            FullName = fullName;
            Email = email;
            Phone = phone;
            MembershipDate = membershipDate;
            Status = status;
        }

        public bool CanBorrow(Member member)
        {
            return member.Status == "Active";
        }
    }
}
