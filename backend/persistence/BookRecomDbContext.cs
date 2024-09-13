using backend.models.database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.persistence 
{
    public class BookRecomDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Book> Books {get; set;}
        public DbSet<UserBook> UserBooks {get;set;}
        public BookRecomDbContext(DbContextOptions<BookRecomDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Many to many relationship User - Book

            // -------------------------------------

            builder.Entity<UserBook>()
                   .HasKey(ub => new { ub.BookId, ub.UserId });

            builder.Entity<UserBook>()
                   .HasOne(ub => ub.User)
                   .WithMany(u => u.UserBooks)
                   .HasForeignKey(ub => ub.UserId);

            builder.Entity<UserBook>()
                   .HasOne(ub => ub.Book)
                   .WithMany(b => b.UserBooks)
                   .HasForeignKey(ub => ub.BookId);

            // -------------------------------------
        }
    }
}