using BooksManager.Model;

namespace BooksManager.Data
{
    internal class EfCoreRepository : IRepository
    {
        private readonly BooksManagerContext context;

        public EfCoreRepository(string conString)
        {
            context = new BooksManagerContext(conString);
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : class
        {
            return context.Set<T>().Find(id);
        }

        public void SaveAll()
        {
            context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }
    }
}
