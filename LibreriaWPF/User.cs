using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWPF
{
    public class User
    {
        public string GUID { get; set; }
        public string FIO { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        //public string BookGuid { get; set; }
        //public ObservableCollection<BookModel> HistoryBook { get; set; } = new(); 
    }
}
