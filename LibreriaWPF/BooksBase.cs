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
    public class BooksBase
    {
        public static ObservableCollection<BookModel> Books { get; set; }
        static BooksBase instance;

        private BooksBase()
        {
            using (FileStream fs = new FileStream("books.json", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                    Books = JsonSerializer.Deserialize<ObservableCollection<BookModel>>(fs);
                else
                    Books = new ObservableCollection<BookModel>();
            }
        }

        public static void SaveBook()
        {
            using (FileStream fs = new FileStream("books.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, Books);
            }
        }
        public static BooksBase GetInstance()
        {
            {
                if (instance == null)
                    instance = new BooksBase();
                return instance;
            }
        }
    }
}
