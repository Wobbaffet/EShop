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
        /// <exception cref="NullReferenceException">Throws when url is null or empty</exception>

        private string image;

        public string Image
        {
            get { return image; }
            set {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Image url cannot be empty or null!");

                image = value; 
            }
        }

        /// <value>Represent book price as double
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Throws when price is zero or negative value !</exception>
        private double price;

        public double Price
        {
            get { return price; }
            set {

                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Price cannot be zero or negative !");

                price = value;
            }
        }

        /// <value>Represent book supplies in storage 
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Throws when value is negative!</exception>
        private int supplies;

        public int Supplies
        {
            get { return supplies; }
            set {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Supplies cannot be negative !");
                
                supplies = value; 
            }
        }

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
