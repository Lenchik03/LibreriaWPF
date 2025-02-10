using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibreriaWPF
{
    /// <summary>
    /// Логика взаимодействия для NewBook.xaml
    /// </summary>
    public partial class NewBook : Window, INotifyPropertyChanged
    {
        public BookModel Book { get; set; } = new BookModel();
        private int id = 0;
        public NewBook()
        {
            InitializeComponent();
            BooksBase.GetInstance();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Book)));
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        
        private void AddClick_Button(object sender, RoutedEventArgs e)
        {
            BooksBase.GetInstance().NewBook(Book);
            Close();
        }
    }
}
