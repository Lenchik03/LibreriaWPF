using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibreriaWPF
{
    public class UsersBase
    {
        public static ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        static UsersBase instance;
        int id = 0;

        private UsersBase()
        {

        }
        public static void Initialize()
        {
            using (FileStream fs = new FileStream("users.json", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                    Users = JsonSerializer.Deserialize<ObservableCollection<User>>(fs);
                else
                    Users = new ObservableCollection<User>();
            }
        }

        public void NewUser(User user)
        {
            var pass = user.Password;
            user.GUID = Guid.NewGuid().ToString();
            user.RoleId = 2;
            user.Password = Md5.HashPassword(pass);
            Users.Add(user);
            SaveUser();
        }
        public static void SaveUser()
        {
            using (FileStream fs = new FileStream("users.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, Users);
            }
        }
        public static UsersBase GetInstance()
        {

            if (instance == null)
                instance = new UsersBase();
            return instance;

        }
    }
}
