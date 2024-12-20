using System.Text;
using System.Text.Json;
using backend.models.database;
using backend.models.gemini.response;
using backend.persistence;
using backend.services.gemini;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.services.book {
    public class BookService : IBookService
    {
        private readonly BookRecomDbContext _context;
        private readonly IAiClientManager _aiClientManager;

        public BookService(BookRecomDbContext context, IAiClientManager aiClientManager)
        {
            _context = context;
            _aiClientManager = aiClientManager;
        }

        public async Task<int> AddBookToDb(Book book)
        {
            await _context.Books.AddAsync(book);
            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> AddBookToUser(Book book, User user)
        {
            UserBook newUserBook = new UserBook {
                FK_User_Id = user.Id,
                User = user,
                FK_Book_Id = book.Id,
                Book = book,
                BookStatus = BookStatus.InLibrary
            };

            await _context.UserBooks.AddAsync(newUserBook);
            int result = await _context.SaveChangesAsync();

            return result;

        }

        public async Task<string> GetBookDescription(string title, string authorName, CancellationToken ct)
        {
            string initialPrompt = "Hello, please give me a short summary of this book." +
                            "The title of the book is " + title + 
                            " and the name of the author is " + authorName;

            string response = await _aiClientManager.GenerateContentAsync(AiClients.Gemini, initialPrompt, "models/gemini-1.5-flash-latest:generateContent", ct);

            GeminiResponse geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(response, 
            new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            return geminiResponse.Candidates[0].Content.Parts[0].Text;
        }

        public async Task<string> GetBookTakeaways(int numberOfTakeaways, string title, string authorName, CancellationToken ct)
        {
            if(numberOfTakeaways < 1 || numberOfTakeaways > 5)
                throw new ArgumentOutOfRangeException("number of takeaways must be between 1 and 5");

            string initialPrompt = $"Give me {numberOfTakeaways} key takeaways from '{title}' by {authorName}. Do not add any other text besides the json.";

            string response = await _aiClientManager.GenerateContentAsync(AiClients.Gemini, initialPrompt, "tunedModels/main-book-takeaways-au2dj9bfx11d:generateContent", ct);

            GeminiResponse geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(response, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            return geminiResponse?.Candidates.FirstOrDefault()?.Content?.Parts.FirstOrDefault()?.Text ?? string.Empty;
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

        public async Task<int> UpdateBookTakeaways(string workId, ICollection<Takeaway> takeaways)
        {
            var foundBook = await _context.Books.FirstOrDefaultAsync(b => b.WorkId == workId);

            if(foundBook == null)
                throw new Exception("Specified book does not exist");

            foundBook.Takeaways = takeaways;
            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}