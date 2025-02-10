using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
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
using static System.Reflection.Metadata.BlobBuilder;

namespace LibreriaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string BookFormat { get; set; }

        public int CounReaders {  get; set; }
        public MainWindow()
        {
          
            InitializeComponent();
            BooksBase.GetInstance();
            BookReaders.GetInstance();
            BooksBase.Initialize();
            ViewBooks();
            DataContext = this;

        }

        public BookModel BookById(string bookGuid)
        {
            BookModel result = new();
            foreach (var book in BooksBase.Books)
            {
                if (book.GUID.Contains(bookGuid))
                    result = book;
            }
            return result;
        }
        private void ViewBooks()
        {
            FilterBooks = new ObservableCollection<BookModel>(BooksBase.Books.Where(filterTask => filterTask.Status == ""));

            List<BookModel> list = new();
            foreach(var b in BookReaders.BookReadersList.Keys)
            {
                if (BookReaders.BookReadersList[b].Count > 0)
                    list.Add(BookById(b));
            }

                Books = list.Where(s => s.Status != "Удалена").ToList();
                
            
            Signal(nameof(FilterBooks));
            Signal(nameof(Books));
        }

        public ObservableCollection<BookModel> FilterBooks { get; set; } 
        public BookModel SelectedBook { get; set; }
        public BookModel SelectedGiveBook {  get; set; }

        public List<BookModel> Books {  get; set; }


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
            ViewBooks();
        }

        private void ComplateClick_Button(object sender, RoutedEventArgs e)
        {
            var selectedBook = SelectedBook;
            if (MessageBox.Show("Вы уверены, что хотите удалить книгу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (selectedBook != null )
                {
                    if (!Books.Contains(selectedBook))
                    {
                        selectedBook.Status = "Удалена";
                        BooksBase.SaveBook();
                        ViewBooks();
                    }
                    else
                        MessageBox.Show("Сначала нужно, чтобы все книги вернули в библиотеку");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBook != null)
            {
                ChangeBook changeBook = new ChangeBook(SelectedBook);
                changeBook.ShowDialog();
                ViewBooks();
            }
            else
            {
                MessageBox.Show("Выберите книгу для редактирования!");
            }
        }

        private void GiveBookClick(object sender, RoutedEventArgs e)
        {
            if (SelectedBook != null)
            {
                if (SelectedBook.IsEBook == true)
                {
                    if (SelectedBook.Count > 0)
                    {
                        GiveBookWindow giveBookWindow = new GiveBookWindow(SelectedBook);
                        giveBookWindow.ShowDialog();
                        ViewBooks();
                    }
                    else
                    {
                        MessageBox.Show("Свободных книг нет");

                    }
                }
                else
                {
                    GiveBookWindow giveBookWindow = new GiveBookWindow(SelectedBook);
                    giveBookWindow.ShowDialog();
                    ViewBooks();
                }
                
            }
            else
            {
                MessageBox.Show("Выберите книгу для выдачи!");
            }
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void ReadersBookClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedGiveBook != null)
            {

                BookReadersWindow bookReadersWindow = new BookReadersWindow(SelectedGiveBook);
                bookReadersWindow.ShowDialog();
                ViewBooks();
            }
            else
            {
                MessageBox.Show("Двойным щелчком нажмите на книгу, чтобы увидеть список читателей!");
            }
        }
    }
}