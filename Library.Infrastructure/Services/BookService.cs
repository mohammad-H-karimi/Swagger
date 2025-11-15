using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
namespace Library.Infrastructure.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _repo;
		public BookService(IBookRepository repo)
		{
			_repo = repo;
		}
		public async Task<int> CreateAsync(BookDto dto)
		{
			var book = new Book
			{
				Title = dto.Title,
				ISBN = dto.ISBN,
				PublishYear = dto.PublishYear,
				Price = dto.Price
			};
			await _repo.AddAsync(book);
			return book.BookId;
		}
		public async Task DeleteAsync(int id)
		{
			await _repo.DeleteAsync(id);
		}
		public async Task<List<BookDto>> GetAllAsync()
		{
			var list = await _repo.GetAllAsync();
			return list.Select(b => new BookDto
					{
					BookId = b.BookId,
					Title = b.Title,
					ISBN = b.ISBN,
					PublishYear = b.PublishYear,
					Price = b.Price
					}).ToList();
		}
		public async Task<BookDto> GetByIdAsync(int id)
		{
			var b = await _repo.GetByIdAsync(id);
			if (b == null) return null;
			return new BookDto
			{
				BookId = b.BookId,
				Title = b.Title,
				ISBN = b.ISBN,
				PublishYear = b.PublishYear,
				Price = b.Price
			};
		}
		public async Task UpdateAsync(int id, BookDto dto)
		{
			var existing = await _repo.GetByIdAsync(id);
			if (existing == null) return;
			existing.Title = dto.Title;
			existing.ISBN = dto.ISBN;
			existing.PublishYear = dto.PublishYear;
			existing.Price = dto.Price;
			await _repo.UpdateAsync(existing);
		}
	}
}

