using AutoMapper;
using backend.models.database;
using backend.models.dto.RequestArgs;
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
        private readonly IMapper _mapper;
        public AuthorController(BookRecomDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorDTO authorDTO)
        {
            var authorWithKeyExists = _context.Authors.Any(a => a.Key == authorDTO.Key);

            if(authorWithKeyExists)
                return Conflict("An author with this key already exists");

            var newAuthor = _mapper.Map<Author>(authorDTO);

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