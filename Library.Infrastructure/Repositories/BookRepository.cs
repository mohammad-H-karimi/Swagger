using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
namespace Library.Infrastructure.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly LibraryDbContext _context;
		public BookRepository(LibraryDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Book book)
		{
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Books.FindAsync(id);
			if (entity == null) return;
			_context.Books.Remove(entity);
			await _context.SaveChangesAsync();
		}
		public async Task<List<Book>> GetAllAsync()
		{
			return await _context.Books.Include(b => b.Authors).ToListAsync();
		}
		public async Task<Book> GetByIdAsync(int id)
		{
			return await _context.Books
				.Include(b => b.Authors)
				.FirstOrDefaultAsync(b => b.BookId == id);
		}
		public async Task UpdateAsync(Book book)
		{
			_context.Entry(book).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
