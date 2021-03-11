using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Models
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; }
        public List<Autor> Autors { get; set; }
    }
}
