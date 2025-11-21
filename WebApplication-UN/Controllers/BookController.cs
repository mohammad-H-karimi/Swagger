using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Application.Services;
using Library.Application.DTOs;

namespace WebApplication_UN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService service, ILogger<BooksController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Request: GET all books");

            var list = await _service.GetAllAsync();

            _logger.LogInformation("Response: {Count} books returned", list.Count);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Request: GET book {Id}", id);

            var b = await _service.GetByIdAsync(id);
            if (b == null)
            {
                _logger.LogWarning("Book {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Book {Id} found", id);
            return Ok(b);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDto dto)
        {
            _logger.LogInformation("Request: POST create book with Title={Title}", dto.Title);

            var id = await _service.CreateAsync(dto);

            _logger.LogInformation("Book created with Id={Id}", id);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookDto dto)
        {
            _logger.LogInformation("Request: PUT update book {Id}", id);

            await _service.UpdateAsync(id, dto);

            _logger.LogInformation("Book {Id} updated (if existed)", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Request: DELETE book {Id}", id);

            await _service.DeleteAsync(id);

            _logger.LogInformation("Book {Id} deleted (if existed)", id);
            return NoContent();
        }
    }
}
