using backend.models.database;
using backend.persistence;
using Microsoft.AspNetCore.Mvc;

namespace backend.services {
    public interface IBookService {
        public Task<int> AddBookToDb(Book book, BookRecomDbContext context);
        public Task<int> AddBookToUser(Book book, User user, BookRecomDbContext context);
    }
}