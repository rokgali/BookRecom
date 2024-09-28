using backend.models.database;
using backend.persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorController : ControllerBase
    {
        private readonly BookRecomDbContext _context;
        public AuthorController(BookRecomDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostAuthor(string authorName)
        {
            var newAuthor = new Author(){Name = authorName};

            await _context.Authors.AddAsync(newAuthor);
            var result = await _context.SaveChangesAsync();

            if(result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to insert author to database" });

            return Ok("Inserted author to database succesfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableAuthors()
        {
            return Ok(await _context.Authors.ToListAsync());
        }
    }
}