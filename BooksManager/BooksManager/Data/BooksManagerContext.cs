using BooksManager.Model;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Data
{
    public class BooksManagerContext : DbContext
    {
        private readonly string conString;

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public BooksManagerContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString)
                          .UseLazyLoadingProxies();
        }

    }
}
