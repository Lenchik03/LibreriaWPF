using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibreriaWPF
{
    /// <summary>
    /// Логика взаимодействия для ReaderVindow.xaml
    /// </summary>
    public partial class ReaderVindow : Window, INotifyPropertyChanged
    {
        public User User { get; set; }

        public BookModel SelectedBook { get; set; }
        public BookModel SelectedMyBook { get; set; }
        public ObservableCollection<BookModel> Books { get; set; }
        public ObservableCollection<BookModel> MyBooks { get; set; }
        public ObservableCollection<BookModel> HistoryBooks { get; set; }
        public ReaderVindow(User user)
        {
            InitializeComponent();
            User = user;
            BooksBase.GetInstance();
            BookReaders.GetInstance();
            UserHistoryBooks.GetInstance();
            BooksBase.Initialize();
            GetBooks();
            DataContext = this;
        }
        protected void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<BookModel> Search(string bookGuid)
        {
            ObservableCollection<BookModel> result = new();
            foreach (var book in BooksBase.Books)
            {
                if (book.GUID.Contains(bookGuid))
                    result.Add(book);
            }
            return result;
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
        public List<User> SearchUsers(string userGuid)
        {
            List<User> result = new();
            foreach (var user in UsersBase.Users)
            {
                if (user.GUID.Contains(userGuid))
                    result.Add(user);
            }
            return result;
        }
        public void GetBooks()
        {
            Books = new ObservableCollection<BookModel>(BooksBase.Books.Where(s => s.Status != "Удалена"));
            ObservableCollection<BookModel> myBooks = new();

                foreach (var book in BookReaders.BookReadersList.Keys)
                {
                    
                    if (BookReaders.BookReadersList[book].Contains(User.GUID))
                    {
                        var books = Search(book);
                        foreach (var b in books)
                            myBooks.Add(b);
                    }
                }
            MyBooks = myBooks;
            myBooks = new();
            //HistoryBooks = new ObservableCollection<BookModel>(BooksBase.Books);
            
            ObservableCollection<BookModel> hbooks = new();
            
            if (UserHistoryBooks.HistoryBooks.ContainsKey(User.GUID))
            {
                var list = UserHistoryBooks.HistoryBooks[User.GUID];
                foreach (var book in list)
                {
                    hbooks.Add(BookById(book));
                }
            }
                HistoryBooks = hbooks;
                hbooks = new();
            

            Signal(nameof(Books));
            Signal(nameof(MyBooks));
            Signal(nameof(HistoryBooks));
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        bool b = false;
        private void GiveMeBookClick(object sender, RoutedEventArgs e)
        {

            if (SelectedBook != null)
            {
                if (!MyBooks.Contains(SelectedBook))
                {
                        if (SelectedBook.IsEBook == false)
                        {
                            BookReaders.GetInstance().AddBookReaders(SelectedBook, User);
                        if (UserHistoryBooks.HistoryBooks.ContainsKey(User.GUID))
                        {
                            if (UserHistoryBooks.HistoryBooks[User.GUID].Contains(SelectedBook.GUID))
                            {
                                var list = UserHistoryBooks.HistoryBooks;
                                list[User.GUID].Remove(SelectedBook.GUID);
                                UserHistoryBooks.GetInstance().Delete();
                                UserHistoryBooks.HistoryBooks = list;
                                UserHistoryBooks.SaveBook();
                            }
                        }
                        MessageBox.Show("Вам выдана книга" +" "+ SelectedBook.Title);
                            GetBooks();
                        }
                        else
                        {
                            MessageBox.Show("Книгу в бумажном варианте можно взять только в библиотеке!");
                        }
                    
                }
                else
                    MessageBox.Show("Вы уже взяли эту книгу почитать!");
            }
            else
                MessageBox.Show("Выберите книгу, которую хотите взять почитать!");
        }


        private void HistoryBook(object sender, RoutedEventArgs e)
        {
            if (SelectedMyBook != null)
            {
                if (SelectedMyBook.IsEBook == false)
                {
                    var list = BookReaders.BookReadersList;
                    list[SelectedMyBook.GUID].Remove(User.GUID);
                    BookReaders.GetInstance().Delete();
                    BookReaders.BookReadersList = list;
                    BookReaders.SaveBook();
                    UserHistoryBooks.GetInstance().AddHistoryBook(User, SelectedMyBook);
                    MessageBox.Show("Книга отправлена в архив!");
                    GetBooks();
                }
                else
                {
                    MessageBox.Show("Книгу в бумажном варианте можно вернуть только в библиотеке!");
                }
            }
            else
                MessageBox.Show("Выберите книгу, которую хотите отправить в архив!");
        }

    }
}
