using System;
using System.Collections.Generic;

namespace EShop.Model.Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Supplies { get; set; }
        public string Description { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Autor> Autors { get; set; }
    }
}
