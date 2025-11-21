using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.Extensions.Logging;
using Library.Infrastructure.Repositories;
using static Library.Infrastructure.Services.BookService;

namespace Library.Infrastructure.Services
{
    public class BookService : IBookService
    {
        public class BookService : IBookService
        {
            private readonly IBookRepository _repo;
            private readonly ILogger<BookService> _logger;

            public BookService(IBookRepository repo, ILogger<BookService> logger)
            {
                _repo = repo;
                _logger = logger;
            }

            public async Task<int> CreateAsync(BookDto dto)
            {
                _logger.LogInformation("Creating book. Title={Title}", dto.Title);

                var book = new Book
                {
                    Title = dto.Title,
                    ISBN = dto.ISBN,
                    PublishYear = dto.PublishYear,
                    Price = dto.Price
                };

                await _repo.AddAsync(book);

                _logger.LogInformation("Book created in repository with Id={Id}", book.BookId);
                return book.BookId;
            }

            public async Task DeleteAsync(int id)
            {
                _logger.LogInformation("Deleting book Id={Id}", id);
                await _repo.DeleteAsync(id);
                _logger.LogInformation("Delete operation finished for Id={Id}", id);
            }

            public async Task<List<BookDto>> GetAllAsync()
            {
                _logger.LogDebug("Fetching all books from repository");

                var list = await _repo.GetAllAsync();

                var dtoList = list.Select(b => new BookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    PublishYear = b.PublishYear,
                    Price = b.Price
                }).ToList();

                _logger.LogDebug("Fetched {Count} books from repository", dtoList.Count);
                return dtoList;
            }

            public async Task<BookDto> GetByIdAsync(int id)
            {
                _logger.LogDebug("Fetching book by Id={Id}", id);

                var b = await _repo.GetByIdAsync(id);
                if (b == null)
                {
                    _logger.LogWarning("Book with Id={Id} not found in repository", id);
                    return null;
                }

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
                _logger.LogInformation("Updating book Id={Id}", id);

                var existing = await _repo.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning("Update requested for non-existing book Id={Id}", id);

                    // throw new KeyNotFoundException($"Book with id {id} not found.");

                    return;
                }

                existing.Title = dto.Title;
                existing.ISBN = dto.ISBN;
                existing.PublishYear = dto.PublishYear;
                existing.Price = dto.Price;

                await _repo.UpdateAsync(existing);

                _logger.LogInformation("Book Id={Id} successfully updated", id);
            }
        }
    }
}
