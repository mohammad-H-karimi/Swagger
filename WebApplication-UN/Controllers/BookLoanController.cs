using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_UN.Data;
using WebApplication_UN.Models;

namespace WebApplication_UN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookLoansController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookLoans()
        {
            var loans = await _context.BookLoans
                .Include(l => l.Member)
                .Include(l => l.Book)
                .ToListAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookLoan(int id)
        {
            var loan = await _context.BookLoans
                .Include(l => l.Member)
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.BookLoanId == id);

            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookLoan(BookLoan loan)
        {
            _context.BookLoans.Add(loan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookLoan), new { id = loan.BookLoanId }, loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookLoan(int id, BookLoan loan)
        {
            if (id != loan.BookLoanId) return BadRequest();
            _context.Entry(loan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookLoan(int id)
        {
            var loan = await _context.BookLoans.FindAsync(id);
            if (loan == null) return NotFound();

            _context.BookLoans.Remove(loan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}