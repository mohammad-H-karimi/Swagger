using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_UN.Data;
using WebApplication_UN.Models;

namespace WebApplication_UN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipCardsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public MembershipCardsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembershipCards()
        {
            var cards = await _context.MembershipCards.Include(c => c.Member).ToListAsync();
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMembershipCard(int id)
        {
            var card = await _context.MembershipCards
                .Include(c => c.Member)
                .FirstOrDefaultAsync(c => c.MembershipCardId == id);

            if (card == null) return NotFound();
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMembershipCard(MembershipCard card)
        {
            _context.MembershipCards.Add(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMembershipCard), new { id = card.MembershipCardId }, card);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMembershipCard(int id, MembershipCard card)
        {
            if (id != card.MembershipCardId) return BadRequest();
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembershipCard(int id)
        {
            var card = await _context.MembershipCards.FindAsync(id);
            if (card == null) return NotFound();

            _context.MembershipCards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

