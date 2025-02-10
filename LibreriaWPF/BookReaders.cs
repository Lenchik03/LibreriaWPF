using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibreriaWPF
{
    public class BookReaders
    {
        public static Dictionary<string, List<string>> BookReadersList { get; set; } = new();
        static BookReaders instance;

        private BookReaders()
        {
            using (FileStream fs = new FileStream("book_readers.json", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                    BookReadersList = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(fs);
                else
                    BookReadersList = new Dictionary<string, List<string>>();
            }
        }
        public void AddBookReaders(BookModel book, User user)
        {
            if (BookReadersList.ContainsKey(book.GUID))
                BookReadersList[book.GUID].Add(user.GUID);
            else
            {
                BookReadersList.Add(book.GUID, new List<string> { user.GUID });
            }
            SaveBook();

        }
        public static void SaveBook()
        {
            using (FileStream fs = new FileStream("book_readers.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, BookReadersList);
            }
        }

        public void Delete()
        {
            //var test = "test_dutys";
            //foreach(var student in  studentRepository.Students)
            //{
            //string path = Path.Combine(Environment.CurrentDirectory, test, $"{student.Info}.json");
            //if (!File.Exists(path))
            //    continue;
            //else
            //{

            File.Delete("book_readers.json");
            var fs = File.Create("book_readers.json");
            fs.Close();
            //}
            //using (var fs = File.OpenRead(path))
            //    JsonSerializer.Serialize(fs, dutys);
            //}


            //dutys.Add(today);

        }
        public static BookReaders GetInstance()
        {
            {
                if (instance == null)
                    instance = new BookReaders();
                return instance;
            }
        }
    }
}
