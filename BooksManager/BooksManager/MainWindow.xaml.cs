using BooksManager.Data;
using BooksManager.Model;
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

        public MainWindow()
        {
            InitializeComponent();
            repository = new EfCoreRepository(conString);
        }

        private void LoadBooks(object sender, RoutedEventArgs e)
        {
            myGrid.ItemsSource = repository.GetAll<Book>();
        }
    }
}