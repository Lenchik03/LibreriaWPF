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
        public static ObservableCollection<BookModel> Books { get; set; } = new ObservableCollection<BookModel>();
        static BooksBase instance;
        int id = 0;

        private BooksBase()
        {
           
        }
        public static void Initialize()
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

        public void NewBook(BookModel book)
        {
            if (book.IsEBook == false)
                book.BookFormat = "Электронная";
            else
                book.BookFormat = "Бумажная";
            if (book.IsEBook == false)
                book.Count = 0;
            book.GUID = Guid.NewGuid().ToString();
            Books.Add(book);
            SaveBook();
        }
        public static BooksBase GetInstance()
        {
            
                if (instance == null)
                    instance = new BooksBase();
                return instance;
            
        }
    }
}
