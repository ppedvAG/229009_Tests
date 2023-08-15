using BooksManager.Data;
using BooksManager.Model;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BooksManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string conString = "Server=(localdb)\\mssqllocaldb;Database=BooksManager_tests;Trusted_Connection=true";
        IRepository repository;
        ObservableCollection<Book> bookList = new ObservableCollection<Book>();

        public MainWindow()
        {
            InitializeComponent();
            repository = new EfCoreRepository(conString);
            myGrid.ItemsSource = bookList;
        }

        private void LoadBooks(object sender, RoutedEventArgs e)
        {
            bookList.Clear();
            repository.GetAll<Book>().ToList().ForEach(x => bookList.Add(x));
        }

        private void SaveAll(object sender, RoutedEventArgs e)
        {
            repository.SaveAll();
        }

        private void AddNewBook(object sender, RoutedEventArgs e)
        {
            var newB = new Book() { Title = "NUE" };
            repository.Add(newB);
            bookList.Add(newB);
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            if( myGrid.SelectedItem is Book b)
            {
                repository.Delete(b);
                bookList.Remove(b);
            }
        }
    }
}