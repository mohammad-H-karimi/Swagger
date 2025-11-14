namespace WebApplication_UN.Models
{
    public class BookLoan
    {
        public int BookLoanId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Member Member { get; set; }
        public Book Book { get; set; }
    }
}
