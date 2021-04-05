using System;
using System.Collections.Generic;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent Book class
    /// </summary>
    public class Book
    {
        /// <value>Represent Book id as int</value>
        public int BookId { get; set; }
        /// <value>Represent Book title as string</value>
        public string Title { get; set; }
        /// <value>Represent image url
        /// </value>
        public string Image { get; set; }
        /// <value>Represent book price as double
        /// </value>
        public double Price { get; set; }
        /// <value>Represent book supplies in storage 
        /// </value>
        public int Supplies { get; set; }
        /// <value>Represent book description
        /// </value>
        public string Description { get; set; }
        /// <value>Represent all book genres
        /// <para>Between genre and book is association class  BookGenre</para>
        /// </value>
        public List<Genre> Genres { get; set; }
        /// <value>Represent all book autors
        /// <p>Between autor and book is association class BookAutor</p>
        /// </value>
        public List<Autor> Autors { get; set; }
    }
}
