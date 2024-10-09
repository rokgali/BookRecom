using System.Net.Security;
using System.Text.Json;
using AutoMapper;
using backend.models.database;
using backend.models.dto.Create;
using backend.models.dto.Response;
using backend.persistence;
using backend.services;
using backend.services.gemini;
using backend.services.jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

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
        private readonly Kernel _kernel;
        private readonly ILogger<BookController> _logger;
        public BookController(IGeminiClient geminiClient, BookRecomDbContext context, 
                              IMapper mapper, IBookService bookService, IBackgroundTaskQueue backgroundTaskQueue,
                              ILogger<BookController> logger, Kernel kernel)
        {
            _geminiClient = geminiClient;
            _context = context;
            _mapper = mapper;
            _bookService = bookService;
            _backgroundTaskQueue = backgroundTaskQueue;
            _logger = logger;
            _kernel = kernel;
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
            IEnumerable<TakeawayResponseDTO> takeawaysResponse;

            if(numberOfTakeaways < 0 || numberOfTakeaways > 5)
                return BadRequest("The number of takeaways must be between 0 and 5");

            var foundBook = await _context.Books.Include(b => b.Takeaways).FirstOrDefaultAsync(b => b.WorkId == workId);

            if(foundBook == null)
            {
                var generatedTakeaways = await _bookService.GetBookTakeaways(numberOfTakeaways, title, authorName, ct);

                _logger.Log(LogLevel.Information, generatedTakeaways);

                using var document = JsonDocument.Parse(generatedTakeaways);

                var takeawaysElement = document.RootElement.GetProperty("takeaways");
                // var takeawaysHeadingElement = document.RootElement.GetProperty("heading");
                var takeawaysJson = takeawaysElement.GetRawText();

                takeawaysResponse = JsonSerializer.Deserialize<IEnumerable<TakeawayResponseDTO>>(takeawaysJson, 
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                return Ok(takeawaysResponse);
            }

            var takeawaysDTO = _mapper.Map<ICollection<TakeawayResponseDTO>>(foundBook.Takeaways);

            return Ok(takeawaysDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDTO bookDTO)
        {
            var bookInLibraryExists = await _context.Books.Where(b => b.WorkId == bookDTO.WorkId).AnyAsync();

            if(bookInLibraryExists)
                return Ok(new { message = "A book with this WorkId already exists in the database" });

            var author = await _context.Authors.FirstOrDefaultAsync(author => author.Key == bookDTO.AuthorKey);

            if(author == null)
                return BadRequest("Author with the provided key doesn't exist");

            if(bookDTO.Takeaways == null)
                return BadRequest("Takeaways can't be empty");

            Book newBook = _mapper.Map<Book>(bookDTO);
            newBook.Author = author;

            int addBookToDbResult = await _bookService.AddBookToDb(newBook);

            if(addBookToDbResult == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to add book to the database" });


            return Ok("Book succesfully added to");
        }

        [HttpGet]
        public async Task<IActionResult> GetCachedBooks(int offset, int pageSize)
        {
            IQueryable<Book> books = _context.Books.Include(b => b.Takeaways).Include(backend => backend.Author).Skip((offset - 1) * pageSize).Take(pageSize);
            var booksResponse = _mapper.Map<IEnumerable<BookResponseDTO>>(books);

            return Ok(booksResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookTakeawaysSemanticKernel(int numberOfTakeaways, string title, string authorName, CancellationToken ct)
        {
            string initialPrompt = $"Give me {numberOfTakeaways} key " +
                                    $"takeaways from '{title}' " + 
                                    $"by {authorName}. Do not add any other text besides the json.";

            var executionSettings = new PromptExecutionSettings();
            #pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            // executionSettings.ServiceId = "takeaways";
            executionSettings.ServiceId = "description";
            #pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            var request = _kernel.CreateFunctionFromPrompt(initialPrompt, executionSettings);
            var result = await _kernel.InvokeAsync(request);

            return Ok(result.ToString());
        }
    }
}