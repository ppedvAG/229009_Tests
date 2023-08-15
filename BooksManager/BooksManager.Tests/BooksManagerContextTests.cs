using BooksManager.Data;
using BooksManager.Model;

namespace BooksManager.Tests
{
    public class BooksManagerContextTests
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=BooksManager_tests;Trusted_Connection=true";

        [Fact]
        [Trait("", "Integration")]
        public void Can_create_db()
        {
            var con = new BooksManagerContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
        }

        [Fact]
        [Trait("", "Integration")]
        public void Can_add_Book()
        {
            var book = new Book() { Title = "Testing is Fun", Description = "Fun with Unittests", Pages = 100 };
            var con = new BooksManagerContext(conString);
            con.Database.EnsureCreated();

            con.Books.Add(book);
            var result = con.SaveChanges();

            Assert.Equal(1, result);
        }

        [Fact]
        [Trait("", "Integration")]
        public void Can_read_Book()
        {
            var book = new Book() { Title = $"Testing is Fun_{Guid.NewGuid()}", Description = "Fun with Unittests", Pages = 100 };
            using (var con = new BooksManagerContext(conString))
            {
                con.Database.EnsureCreated();
                con.Books.Add(book);
                con.SaveChanges();
            }

            using (var con = new BooksManagerContext(conString))
            {

                var loaded = con.Books.Find(book.Id);

                Assert.Equal(loaded.Title, book.Title);
            }
        }

        [Fact]
        [Trait("", "Integration")]
        public void Can_udate_Book()
        {
            var book = new Book() { Title = $"Testing is Fun_{Guid.NewGuid()}", Description = "Fun with Unittests", Pages = 100 };
            var newTitle = $"Testing is pain_{Guid.NewGuid()}";
            using (var con = new BooksManagerContext(conString))
            {
                con.Database.EnsureCreated();
                con.Books.Add(book);
                con.SaveChanges();
            }

            using (var con = new BooksManagerContext(conString))
            {
                var loaded = con.Books.Find(book.Id);
                loaded.Title = newTitle;
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new BooksManagerContext(conString))
            {
                var loaded = con.Books.Find(book.Id);
                Assert.Equal(loaded.Title, newTitle);
            }
        }

        [Fact]
        [Trait("", "Integration")]
        public void Can_delete_Book()
        {
            var book = new Book() { Title = $"Testing is Fun_{Guid.NewGuid()}", Description = "Fun with Unittests", Pages = 100 };
            using (var con = new BooksManagerContext(conString))
            {
                con.Database.EnsureCreated();
                con.Books.Add(book);
                con.SaveChanges();
            }

            using (var con = new BooksManagerContext(conString))
            {
                var loaded = con.Books.Find(book.Id);
                con.Remove(loaded);
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new BooksManagerContext(conString))
            {
                var loaded = con.Books.Find(book.Id);
                Assert.Null(loaded);
            }
        }
    }
}