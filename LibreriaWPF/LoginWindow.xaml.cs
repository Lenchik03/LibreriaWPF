using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {

        public User User { get; set; } = new();
        public ObservableCollection<User> Users { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            UsersBase.GetInstance();
            UsersBase.Initialize();
            GetUsers();
            DataContext = this;
        }

        public void GetUsers()
        {
            Users = new ObservableCollection<User>(UsersBase.Users);
        }
        protected void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RegistrationWindowClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            Close();
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            if(User.UserName == "123" && User.Password == "123")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
                return;
            }

            var user = Users.FirstOrDefault(s=> s.UserName == User.UserName && User.Password == User.Password);
            if(user != null)
            {
                ReaderVindow readerVindow = new ReaderVindow(user);
                readerVindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
                
            }
        }
    }
}
