namespace WebApplication_UN.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }

        public MembershipCard MembershipCard { get; set; }
        public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
    }
}
