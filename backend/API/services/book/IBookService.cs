using backend.models.database;
using backend.persistence;
using Microsoft.AspNetCore.Mvc;

namespace backend.services {
    public interface IBookService {
        public Task<int> AddBookToDb(Book book);
        public Task<int> AddBookToUser(Book book, User user);
        public Task<int> UpdateBookDescription(string workId, string description);
        public Task<int> UpdateBookTakeaways(string workId, ICollection<Takeaway> takeaways);
        public Task<string> GetBookDescription(string title, string authorName, CancellationToken ct);
        public Task<string> GetBookTakeaways(int numberOfTakeaways, string title, string authorName, CancellationToken ct);
    }
}