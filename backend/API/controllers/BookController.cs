using System.Text.Json;
using AutoMapper;
using backend.models.database;
using backend.models.dto.create;
using backend.models.dto.get;
using backend.models.dto.Return;
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
        public BookController(IGeminiClient geminiClient, BookRecomDbContext context, 
                              IMapper mapper, IBookService bookService, IBackgroundTaskQueue backgroundTaskQueue)
        {
            _geminiClient = geminiClient;
            _context = context;
            _mapper = mapper;
            _bookService = bookService;
            _backgroundTaskQueue = backgroundTaskQueue;
        }

        [HttpPost]
        public async Task<IActionResult> PostBookToLibraryAsync([FromBody]BookDTO bookDTO)
        {
            var bookInLibraryExists = await _context.Books.Where(b => b.WorkId == bookDTO.WorkId).AnyAsync();

            if(bookInLibraryExists)
                return Conflict(new { message = "A book with this WorkId already exists in users library" });

            Book newBook = _mapper.Map<Book>(bookDTO);

            int addBookToDbResult = await _bookService.AddBookToDb(newBook);

            if(addBookToDbResult == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to add book to the database" });

            // int addBookToUser = await _bookService.AddBookToUser(newBook, )

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetBookDescriptionAndTakeaways([FromBody]GeminiBookDataDTO geminiBookDataDTO, CancellationToken ct)
        {
            var foundBook = _context.Books.FirstOrDefault(b => b.WorkId == geminiBookDataDTO.bookDTO.WorkId);
            BookDescriptionAndTakeawaysDTO bookDescriptionAndTakeawaysDTO = new(){
                Description = "",
                Takeaways = new TakeawaysDTO(){Heading = "", TakeAways=[]}
            };

            if(foundBook == null)
            {
                var generatedBookDescription = await _bookService.GetBookDescription(geminiBookDataDTO, ct);
                var generatedBookTakeaways = await _bookService.GetBookTakeaways(geminiBookDataDTO, ct);

                TakeawaysDTO takeaways = JsonSerializer.Deserialize<TakeawaysDTO>(generatedBookTakeaways, 
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
                
                bookDescriptionAndTakeawaysDTO = new BookDescriptionAndTakeawaysDTO(){
                    Description = generatedBookDescription,
                    Takeaways = takeaways
                };

                return Ok(bookDescriptionAndTakeawaysDTO);
            }

            // TODO: tikriausiai neveiks mapping'as, nes takeawaydto reikalauja takeaways objekto
            TakeawaysDTO takeawaysDTO = _mapper.Map<TakeawaysDTO>(foundBook.TakeAways);

            bookDescriptionAndTakeawaysDTO = new BookDescriptionAndTakeawaysDTO(){
                Description = foundBook.Description,
                Takeaways = takeawaysDTO
            };

            return Ok(bookDescriptionAndTakeawaysDTO);
        }
    }
}