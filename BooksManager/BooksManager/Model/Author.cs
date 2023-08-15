
namespace BooksManager.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        
        public string MyProperty { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
