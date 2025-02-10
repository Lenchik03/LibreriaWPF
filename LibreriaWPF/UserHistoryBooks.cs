using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibreriaWPF
{
    public class UserHistoryBooks
    {
        public static Dictionary<string, List<string>> HistoryBooks { get; set; } = new();
        static UserHistoryBooks instance;

        private UserHistoryBooks()
        {
            using (FileStream fs = new FileStream("history_books.json", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                    HistoryBooks = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(fs);
                else
                    HistoryBooks = new Dictionary<string, List<string>>();
            }
        }
        public void AddHistoryBook(User user, BookModel book)
        {
            if (HistoryBooks.ContainsKey(user.GUID))
                HistoryBooks[user.GUID].Add(book.GUID);
            else
            {
                HistoryBooks.Add(user.GUID, new List<string> { book.GUID });
            }
            SaveBook();

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

            File.Delete("history_books.json");
            var fs = File.Create("history_books.json");
            fs.Close();
            //}
            //using (var fs = File.OpenRead(path))
            //    JsonSerializer.Serialize(fs, dutys);
            //}


            //dutys.Add(today);

        }
        public static void SaveBook()
        {
            using (FileStream fs = new FileStream("history_books.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, HistoryBooks);
            }
        }
        public static UserHistoryBooks GetInstance()
        {
            {
                if (instance == null)
                    instance = new UserHistoryBooks();
                return instance;
            }
        }
    }
}
