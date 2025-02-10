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
    /// Логика взаимодействия для GiveBookWindow.xaml
    /// </summary>
    public partial class GiveBookWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<User> readers;

        public BookModel Book {  get; set; }
        public ObservableCollection<User> Readers
        {
            get => readers;
            set { readers = value;
                Signal(nameof(readers));
            }
        }
        public User SelectedReader { get; set; }
        public GiveBookWindow(BookModel bookModel)
        {
            InitializeComponent();
            Book = bookModel;
            UsersBase.GetInstance();
            BookReaders.GetInstance();
            BooksBase.GetInstance();
            GetLists();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public void GetLists()
        {
            Readers = new ObservableCollection<User>(UsersBase.Users);
            Signal(nameof(Readers));
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {

                if (SelectedReader != null)
                {
                    if (BookReaders.BookReadersList.ContainsKey(Book.GUID) && BookReaders.BookReadersList[Book.GUID].Contains(SelectedReader.GUID))
                    {
                        
                            MessageBox.Show("Эта книга уже выдана читателю!");
                        
                    }

                    else
                    {
                        BookReaders.GetInstance().AddBookReaders(Book, SelectedReader);
                        if (UserHistoryBooks.HistoryBooks.ContainsKey(SelectedReader.GUID))
                        {
                            if (UserHistoryBooks.HistoryBooks[SelectedReader.GUID].Contains(Book.GUID))
                            {
                                var list = UserHistoryBooks.HistoryBooks;
                                list[SelectedReader.GUID].Remove(Book.GUID);
                                UserHistoryBooks.GetInstance().Delete();
                                UserHistoryBooks.HistoryBooks = list;
                                UserHistoryBooks.SaveBook();
                            }
                        }
                        if (Book.IsEBook == true)
                        {
                            Book.Count--;
                            BooksBase.SaveBook();
                        }
                        MessageBox.Show("Книга успешно выдана!");
                        Close();
                    }
                    
                }
                
            
            else
            {
                MessageBox.Show("Выберите читателя!");
            }
        }
    }
}
