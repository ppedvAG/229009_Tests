using BooksManager.Model;
using System.Data;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BooksManager.Tests")]

namespace BooksManager.Logic
{
    internal class BooksService
    {
        private readonly IRepository repository;

        public BooksService(IRepository repository)
        {
            this.repository = repository;
        }

        public Author? GetAuthorWithMostPages()
        {
            return repository.GetAll<Author>()
                             .OrderByDescending(x => x.Books.Sum(y => y.Pages))
                             .ThenBy(x => x.Name)
                             .FirstOrDefault();
        }
    }
}
