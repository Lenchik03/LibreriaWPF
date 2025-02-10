using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWPF
{
    public class BookModel
    {
        //public int Id { get; set; }
        public string GUID {  get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public bool IsEBook { get; set; }
        public string BookFormat {  get; set; }
        public string Status { get; set; } = "";
        public int ?Count {  get; set; }
        //public string ReaderGuid { get; set; }
        //public ObservableCollection<User> BookUsers { get; set; } = new();
    }
}
