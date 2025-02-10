using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для ChangeBook.xaml
    /// </summary>
    public partial class ChangeBook : Window, INotifyPropertyChanged
    {
        //private BookModel selectedBook;

        public event PropertyChangedEventHandler? PropertyChanged;

        //public BookModel SelectedBook
        //{
        //    get => selectedBook;
        //    set
        //    {
        //        selectedBook = value;
        //        PropertyChanged?.Invoke(this,
        //            new PropertyChangedEventArgs(nameof(SelectedBook)));

        //    }
        //}


        public BookModel SelectedBook {  get; set; }
        public ChangeBook(BookModel bookModel)
        {
            InitializeComponent();
            SelectedBook = bookModel;
            BooksBase.GetInstance();
            //BooksBase.Initialize();
            DataContext = this;
        }

        private void ChangeClick_Button(object sender, RoutedEventArgs e)
        {
            if (SelectedBook != null)
            {
                var book = BooksBase.Books.FirstOrDefault(s => s.GUID == SelectedBook.GUID);

                //book.Title = SelectedBook.Title;
                //book.Author = SelectedBook.Author;
                //book.Description = SelectedBook.Description;
                //book.Status = SelectedBook.Status;
                //book.BookFormat = SelectedBook.BookFormat;
                //book.IsEBook = SelectedBook.IsEBook;
                //book.Count = SelectedBook.Count;
                //book.ReaderId = SelectedBook.ReaderId;
                
                if (BooksBase.Books.Contains(book))
                {
                    if (SelectedBook.IsEBook == true)
                        SelectedBook.BookFormat = "Бумажная";
                    else
                        SelectedBook.BookFormat = "Электронная";
                    if(SelectedBook.IsEBook == false)
                        SelectedBook.Count = 0;
                    BooksBase.Books[BooksBase.Books.IndexOf(SelectedBook)] = SelectedBook;
                    BooksBase.SaveBook();
                    //MainWindow mainWindow = new MainWindow();
                    PropertyChanged?.Invoke(this,
                            new PropertyChangedEventArgs(nameof(SelectedBook)));
                    //BooksBase.SaveBook();
                    Close();
                }
                else
                    MessageBox.Show("Ошибка!");
                    
            }
        }
    }
}
