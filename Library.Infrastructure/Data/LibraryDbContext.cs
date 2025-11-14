using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
namespace Library.Infrastructure.Data
{
	public class LibraryDbContext : DbContext
	{
		public LibraryDbContext(DbContextOptions<LibraryDbContext> options) :
			base(options) { }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<MembershipCard> MembershipCards { get; set; }
		public DbSet<BookLoan> BookLoans { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasMany(b => b.Authors)
				.WithMany(a => a.Books)
				.UsingEntity(j => j.ToTable("BookAuthors"));
			modelBuilder.Entity<Member>()
				.HasOne(m => m.MembershipCard)
				.WithOne(c => c.Member)
				.HasForeignKey<MembershipCard>(c => c.MemberId);
			base.OnModelCreating(modelBuilder);
		}
	}
}
