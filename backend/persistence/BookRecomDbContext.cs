using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.persistence 
{
    public class BookRecomDbContext : IdentityDbContext
    {
        public BookRecomDbContext(DbContextOptions<BookRecomDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}