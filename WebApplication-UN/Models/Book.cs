namespace WebApplication_UN.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishYear { get; set; }
        public decimal Price { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
