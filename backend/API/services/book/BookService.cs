using System.Text;
using System.Text.Json;
using backend.models.database;
using backend.models.dto.get;
using backend.models.gemini.response;
using backend.persistence;
using backend.services.gemini;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.services.book {
    public class BookService : IBookService
    {
        private readonly BookRecomDbContext _context;
        private readonly IGeminiClient _geminiClient;

        public BookService(BookRecomDbContext context, IGeminiClient geminiClient)
        {
            _context = context;
            _geminiClient = geminiClient;
        }

        public async Task<int> AddBookToDb(Book book)
        {
            try {

                await _context.Books.AddAsync(book);
                int result = await _context.SaveChangesAsync();

                return result;

            } catch {

                throw;

            }
        }

        public async Task<int> AddBookToUser(Book book, User user)
        {
            UserBook newUserBook = new UserBook {
                UserId = user.Id,
                User = user,
                BookId = book.Id,
                Book = book,
                BookStatus = BookStatus.InLibrary
            };

            try {

                await _context.UserBooks.AddAsync(newUserBook);
                int result = await _context.SaveChangesAsync();

                return result;

            } catch {

                throw;

            }
        }

        public async Task<string> GetBookDescription(GeminiBookDataDTO geminiBookDataDTO, CancellationToken ct)
        {
            string initialPrompt = "Hello, please give me a short summary of this book." +
                            "The title of the book is " + geminiBookDataDTO.bookDTO.Title + 
                            " and the name of the author is " + geminiBookDataDTO.bookDTO.AuthorName;

            try {
                string description = await _geminiClient.GenerateContentAsync(initialPrompt, "models/gemini-1.5-flash-latest:generateContent", ct);

                return description;
            } catch {
                throw;
            }
        }

        public async Task<string> GetBookTakeaways(GeminiBookDataDTO geminiBookDataDTO, CancellationToken ct)
        {
            if(geminiBookDataDTO.NumberOfTakeaways is null)
                throw new ArgumentNullException("Number of takeaways can not be null");

            string initialPrompt = $"Give me {geminiBookDataDTO.NumberOfTakeaways} key " +
                                    $"takeaways from '{geminiBookDataDTO.bookDTO.Title}' " + 
                                    $"by {geminiBookDataDTO.bookDTO.AuthorName}";

            try {
                string description = await _geminiClient.GenerateContentAsync(initialPrompt, "tunedModels/main-book-takeaways-au2dj9bfx11d:generateContent", ct);

                return description;
            } catch {
                throw;
            }
        }

        public async Task<int> UpdateBookDescription(string workId, string description)
        {
            var foundBook = await _context.Books.FirstOrDefaultAsync(b => b.WorkId == workId);

            if(foundBook == null)
                throw new Exception("Specified book does not exist");

            foundBook.Description = description;
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> UpdateBookTakeaways(string workId, string takeaways)
        {
            var foundBook = await _context.Books.FirstOrDefaultAsync(b => b.WorkId == workId);

            if(foundBook == null)
                throw new Exception("Specified book does not exist");

            Takeaways content = JsonSerializer.Deserialize<Takeaways>(takeaways, 
            new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            content.Book = foundBook;

            foundBook.TakeAways = content;
            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}