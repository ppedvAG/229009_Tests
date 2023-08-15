using BooksManager.Logic;
using BooksManager.Model;
using FluentAssertions;
using NSubstitute;

namespace BooksManager.Tests
{
    public class BooksServiceTests
    {
        [Fact]
        public void GetAuthorWithMostPages_Wilma_has_most_books()
        {
            var bs = new BooksService(new TestRepo());

            var result = bs.GetAuthorWithMostPages();

            result.Name.Should().Be("Wilma");
        }

        [Fact]
        public void GetAuthorWithMostPages_Wilma_has_most_books_NSubstitute()
        {
            var a1 = new Author() { Name = "Fred" };
            a1.Books.Add(new Book() { Pages = 200 });

            var a2 = new Author() { Name = "Wilma" };
            a2.Books.Add(new Book() { Pages = 100 });
            a2.Books.Add(new Book() { Pages = 120 });

            var a3 = new Author() { Name = "Barney" };
            a3.Books.Add(new Book() { Pages = 20 });

            var repo = Substitute.For<IRepository>();
            repo.GetAll<Author>().Returns(new[] { a1, a2, a3 });

            var bs = new BooksService(repo);

            var result = bs.GetAuthorWithMostPages();

            result.Name.Should().Be(a2.Name);
        }

        [Fact]
        public void GetAuthorWithMostPages_empty_db_should_return_null()
        {
            var repo = Substitute.For<IRepository>();
            repo.GetAll<Author>().Returns(new List<Author>());
            var bs = new BooksService(repo);

            var result = bs.GetAuthorWithMostPages();

            result.Should().BeNull();
        }

    }

    class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            if (typeof(T) == typeof(Author))
            {
                var a1 = new Author() { Name = "Fred" };
                a1.Books.Add(new Book() { Pages = 200 });

                var a2 = new Author() { Name = "Wilma" };
                a2.Books.Add(new Book() { Pages = 100 });
                a2.Books.Add(new Book() { Pages = 120 });

                var a3 = new Author() { Name = "Barney" };
                a3.Books.Add(new Book() { Pages = 20 });

                return new[] { a1, a2, a3 }.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
