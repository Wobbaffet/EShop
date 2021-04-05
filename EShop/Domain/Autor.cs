using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent Autor class
    /// </summary>
    public class Autor
    {
        /// <value>Represent Autor id as int</value>
        public int AutorId { get; set; }
        /// <value>Represent first name as string</value>
        public string FirstName { get; set; }
        /// <value>Represent last name as string</value>

        public string LastName { get; set; }
        /// <value>Represent all book genres
        /// <para>Between autor and book is association class  BookAutor</para>
        /// </value>

        public List<Book> Books { get; set; }

        /// <summary>
        /// Override string method
        /// </summary>
        /// <returns>string in format <c>FirstName</c> <c>LastName</c></returns>
      
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
