using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для BookReadersWindow.xaml
    /// </summary>
    public partial class BookReadersWindow : Window, INotifyPropertyChanged
    {
        public List<User> Readers {  get; set; } 
        public User SelectedReader {  get; set; }
        public BookModel Book {  get; set; }
        public BookReadersWindow(BookModel book)
        {
            InitializeComponent();
            BookReaders.GetInstance();
            BooksBase.GetInstance();
            Book = book;
            GetLists();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public User UserById(string userGuid)
        {
            User result = new();
            foreach (var user in UsersBase.Users)
            {
                if (user.GUID.Contains(userGuid))
                    result = user;
            }
            return result;
        }

        public void GetLists()
        {
            var list = new List<User>();
                foreach (var user in BookReaders.BookReadersList[Book.GUID])
                {

                    list.Add(UserById(user));
                }
            

                
            Readers = list;
            Signal(nameof(Readers));
        }
        private void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReader != null)
            {
                var list = BookReaders.BookReadersList;
                list[Book.GUID].Remove(SelectedReader.GUID);
                BookReaders.GetInstance().Delete();
                BookReaders.BookReadersList = list;
                BookReaders.SaveBook();
                UserHistoryBooks.GetInstance().AddHistoryBook(SelectedReader, Book);
                GetLists();
                if(Book.IsEBook == true)
                {
                    Book.Count++;
                    BooksBase.SaveBook();
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя, который хочет вернуть книгу");
            }
        }
    }
}
