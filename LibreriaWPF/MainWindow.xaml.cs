using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibreriaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            BooksBase.GetInstance();
            DataContext = this;

        }
        private void ViewBooks()
        {
            FilterBooks = new ObservableCollection<BookModel>(BooksBase.Books.Where(filterTask => filterTask.IsEBook == true && filterTask.Status == ""));
            Signal(nameof(FilterBooks));
        }

        public ObservableCollection<BookModel> FilterBooks { get; set; }
        public BookModel SelectedBook { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void NewBook_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.ShowDialog();
            ViewBooks();
        }

        private void BookHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryBooks history = new HistoryBooks();
            history.ShowDialog();
        }

        private void ComplateClick_Button(object sender, RoutedEventArgs e)
        {
            var selectedBook = SelectedBook;
            if (selectedBook != null)
            {
                selectedBook.Status = "Удалена";
                BooksBase.SaveBook();
                ViewBooks();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = SelectedBook;
            ChangeBook changeBook = new ChangeBook();
            changeBook.ShowDialog();
        }
    }
}