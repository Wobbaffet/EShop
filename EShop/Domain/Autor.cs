using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Book> Books { get; set; }
    }
}
