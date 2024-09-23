using backend.models.database;
using backend.models.dto.get;
using backend.persistence;
using Microsoft.AspNetCore.Mvc;

namespace backend.services {
    public interface IBookService {
        public Task<int> AddBookToDb(Book book);
        public Task<int> AddBookToUser(Book book, User user);
        public Task<int> UpdateBookDescription(string workId, string description);
        public Task<int> UpdateBookTakeaways(string workId, string takeaways);
        public Task<string> GetBookDescription(GeminiBookDataDTO geminiBookDataDTO, CancellationToken ct);
        public Task<string> GetBookTakeaways(GeminiBookDataDTO geminiBookDataDTO, CancellationToken ct);
    }
}