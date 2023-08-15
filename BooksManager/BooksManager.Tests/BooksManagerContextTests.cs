using AutoFixture;
using AutoFixture.Kernel;
using BooksManager.Data;
using BooksManager.Model;
using FluentAssertions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooksManager.Tests
{
    public class BooksManagerContextTests
    {
        readonly string conString = "Server=(localdb)\\mssqllocaldb;Database=BooksManager_tests;Trusted_Connection=true";

        [Fact]
        [Trait("", "Integration")]
        public void Can_create_db()
        {
            var con = new BooksManagerContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            result.Should().BeTrue();
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

            result.Should().Be(1);
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
                con.Books.Find(book.Id).Title.Should().Be(book.Title);
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
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new BooksManagerContext(conString))
            {
                con.Books.Find(book.Id).Title.Should().Be(newTitle);
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
                var rows = con.SaveChanges().Should().Be(1);
            }

            using (var con = new BooksManagerContext(conString))
            {
                var loaded = con.Books.Find(book.Id);
                loaded.Should().BeNull();
            }
        }


        [Fact]
        [Trait("", "Integration")]
        public void Can_create_Book_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter("Id"));
            var book = fix.Create<Book>();

            using (var con = new BooksManagerContext(conString))
            {
                con.Database.EnsureCreated();
                con.Books.AddRange(book);
                con.SaveChanges();
            }

            using (var con = new BooksManagerContext(conString))
            {
                //var loaded = con.Books.Include(x => x.Authors).FirstOrDefault(x => x.Id == book.Id);// eager Loading
                var loaded = con.Books.FirstOrDefault(x => x.Id == book.Id);// lazy Loading

                loaded.Should().BeEquivalentTo(book, x => x.IgnoringCyclicReferences());
            }
        }
    }
    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }

}