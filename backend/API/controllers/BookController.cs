using AutoMapper;
using backend.models.database;
using backend.models.dto.create;
using backend.persistence;
using backend.services;
using backend.services.gemini;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IGeminiClient _geminiClient;
        private readonly BookRecomDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        public BookController(IGeminiClient geminiClient, BookRecomDbContext context, IMapper mapper, IBookService bookService)
        {
            _geminiClient = geminiClient;
            _context = context;
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookToLibraryAsync(BookDTO bookDTO)
        {
            var bookInLibraryExists = await _context.Books.Where(b => b.WorkId == bookDTO.WorkId).AnyAsync();

            if(bookInLibraryExists)
                return Conflict(new { message = "A book with this WorkId already exists in users library" });

            Book newBook = _mapper.Map<Book>(bookDTO);

            int addBookToDbResult = await _bookService.AddBookToDb(newBook, _context);

            if(addBookToDbResult == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to add book to the database" });

            // int addBookToUser = await _bookService.AddBookToUser(newBook, )

            return Ok();
        }
    }
}