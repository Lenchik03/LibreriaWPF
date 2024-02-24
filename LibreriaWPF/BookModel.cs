using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWPF
{
    public class BookModel
    {
        public string Title {  get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public bool IsEBook {  get; set; } = false;
        public string Status { get; set; } = "";
    }
}
