namespace WebApplication_UN.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
