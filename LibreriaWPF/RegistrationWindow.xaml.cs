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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window, INotifyPropertyChanged
    {
        private User user;
        private string repeatPass;
        private string password;
        private string userName;

        public User User
        {
            get => user;
            set
            {
                user = value;
                Signal();
            }
        }
        public ObservableCollection<User> Users { get; set; }
        public string RepeatPass 
        { get => repeatPass;
            set { repeatPass = value;
                Signal();
            }
        }
        public string Password
        {
            get => password;
            set { password = value;
                Signal();
            }
        }
        public string UserName
        {
            get => userName;
            set { userName = value;
                Signal();
            }
        }
        public RegistrationWindow()
        {
            InitializeComponent();
            User = new();
            UsersBase.GetInstance();
            UsersBase.Initialize();
            GetUsers();
            DataContext = this;
        }
        protected void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void GetUsers()
        {
            Users = new ObservableCollection<User>(UsersBase.Users);
        }


        private void RefistrationClick(object sender, RoutedEventArgs e)
        {
            if (User.UserName != null && User.Password !=null && User.FIO != null)
            {
                var user = UsersBase.Users.FirstOrDefault(s => s.UserName == User.UserName);
                if (user != null)
                {
                    MessageBox.Show("Пользователь с таким именем пользователя уже существует!");
                }
                else
                {
                    if (RepeatPass == User.Password)
                    {
                        UsersBase.GetInstance().NewUser(User);
                        MessageBox.Show("Вы успешно зарегистрировались");
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        Close();
                    }
                    else
                        MessageBox.Show("Пароли не совпадают!");
                }
            }
            else
            {
                MessageBox.Show("Заполните ВСЕ поля!");
            }


        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
