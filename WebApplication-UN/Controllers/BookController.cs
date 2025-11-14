using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_UN.Data;
using WebApplication_UN.Models;

namespace WebApplication_UN.Controllers
{
    namespace LibraryApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BooksController : ControllerBase
        {
            private readonly LibraryContext _context;

            public BooksController(LibraryContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetBooks()
            {
                var books = await _context.Books.Include(b => b.Authors).ToListAsync();
                return Ok(books);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetBook(int id)
            {
                var book = await _context.Books
                    .Include(b => b.Authors)
                    .FirstOrDefaultAsync(b => b.BookId == id);

                if (book == null) return NotFound();
                return Ok(book);
            }

            [HttpPost]
            public async Task<IActionResult> CreateBook(Book book)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateBook(int id, Book book)
            {
                if (id != book.BookId) return BadRequest();
                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteBook(int id)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null) return NotFound();

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}

