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
    /// Логика взаимодействия для HistoryBooks.xaml
    /// </summary>
    public partial class HistoryBooks : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<BookModel> Books { get; set; } = BooksBase.Books;
        public HistoryBooks()
        {
            InitializeComponent();
            BooksBase.GetInstance();
            BooksBase.Initialize();
            DataContext = this;
        }
       
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
