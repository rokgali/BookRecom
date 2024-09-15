using backend.models.database;
using backend.persistence;
using Microsoft.AspNetCore.Mvc;

namespace backend.services.book {
    public class BookService : IBookService
    {
        public async Task<int> AddBookToDb(Book book, BookRecomDbContext context)
        {
            try {

                await context.Books.AddAsync(book);
                int result = await context.SaveChangesAsync();

                return result;

            } catch {

                throw;

            }
        }

        public async Task<int> AddBookToUser(Book book, User user, BookRecomDbContext context)
        {
            UserBook newUserBook = new UserBook {
                UserId = user.Id,
                User = user,
                BookId = book.Id,
                Book = book,
                BookStatus = BookStatus.InLibrary
            };

            try {

                await context.UserBooks.AddAsync(newUserBook);
                int result = await context.SaveChangesAsync();

                return result;

            } catch {

                throw;

            }
        }
    }
}