using Microsoft.VisualBasic;

namespace LibraryManagementSystemAPI.Data
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MembershipDate { get; set; }
    }

}
