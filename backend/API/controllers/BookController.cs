using System.Net.Security;
using System.Text.Json;
using AutoMapper;
using backend.models.database;
using backend.models.dto.RequestArgs;
using backend.models.dto.ResponseArgs;
using backend.persistence;
using backend.services;
using backend.services.gemini;
using backend.services.jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers
{
    [ApiController]
    // [Authorize]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IGeminiClient _geminiClient;
        private readonly BookRecomDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly ILogger<BookController> _logger;
        public BookController(IGeminiClient geminiClient, BookRecomDbContext context, 
                              IMapper mapper, IBookService bookService, IBackgroundTaskQueue backgroundTaskQueue,
                              ILogger<BookController> logger)
        {
            _geminiClient = geminiClient;
            _context = context;
            _mapper = mapper;
            _bookService = bookService;
            _backgroundTaskQueue = backgroundTaskQueue;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostBookToLibrary([FromBody]BookDTO bookDTO)
        {
            var bookInLibraryExists = await _context.Books.Where(b => b.WorkId == bookDTO.WorkId).AnyAsync();

            if(bookInLibraryExists)
                return Conflict(new { message = "A book with this WorkId already exists in users library" });

            Book newBook = _mapper.Map<Book>(bookDTO);
            Author newAuthor = _mapper.Map<Author>(bookDTO.Author);
            newBook.Author = newAuthor;

            int addBookToDbResult = await _bookService.AddBookToDb(newBook);

            if(addBookToDbResult == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to add book to the database" });

            // int addBookToUser = await _bookService.AddBookToUser(newBook, )

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBookDescription(string workId, string title, string authorName, CancellationToken ct)
        {
            var foundBook = await _context.Books.FirstOrDefaultAsync(b => b.WorkId == workId);

            if(foundBook == null)
            {
                var generatedBookDescription = await _bookService.GetBookDescription(title, authorName, ct);

                return Ok(generatedBookDescription);
            }

            return Ok(foundBook.Description);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookTakeaways(int numberOfTakeaways, string workId, string title, string authorName, CancellationToken ct)
        {
            if(numberOfTakeaways < 0 || numberOfTakeaways > 5)
                return BadRequest("The number of takeaways must be between 0 and 5");

            var foundBook = await _context.Books.FirstOrDefaultAsync(b => b.WorkId == workId);

            if(foundBook == null)
            {
                var generatedTakeaways = await _bookService.GetBookTakeaways(numberOfTakeaways, title, authorName, ct);

                IList<TakeawayDTO> takeaways = JsonSerializer.Deserialize<IList<TakeawayDTO>>(generatedTakeaways, 
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                return Ok(takeaways);
            }

            return Ok(foundBook.Takeaways);
        }

        [HttpPost]
        public async Task<IActionResult> PostBook(BookDTO bookDTO)
        {
            var foundBook = await _context.Books.AnyAsync(b => b.WorkId == bookDTO.WorkId);

            if(foundBook)
            {
                return BadRequest("Book with this work id already exists in the database");
            }

            try {
                    var newBook = _mapper.Map<Book>(bookDTO);
                    var newAuthor = _mapper.Map<Author>(bookDTO.Author);

                    await _context.Books.AddAsync(newBook);

                    var result = await _context.SaveChangesAsync();

                    if(result == 0)
                        return StatusCode(500, new {message = "Failed to save book to database"});

                    return Ok("Book succesfully saved");
            } catch {
                throw;
            }
        }
    }
}