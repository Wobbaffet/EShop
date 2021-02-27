using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
