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
        public ChangeBook()
        {
            InitializeComponent();
            
            BooksBase.GetInstance();
            DataContext = this;
        }

        private void ChangeClick_Button(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            var selectedBook = mainWindow.SelectedBook;
            PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(selectedBook)));
            Close();
        }
    }
}
