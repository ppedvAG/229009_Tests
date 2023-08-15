namespace BooksManager.Model
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;

        T GetById<T>(int id) where T : class;

        IEnumerable<T> GetAll<T>() where T : class;

        void SaveAll();
    }
}
