using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Domain
{

    /// <summary>
    /// Represent Genre class
    /// </summary>
    public class Genre
    {
        /// <value>Represent Genre id as int</value>
        public int GenreId { get; set; }
        /// <value>Represent name as string</value>

        public string Name { get; set; }
        /// <value>Represent all book genres
        /// <para>Between genre and book is association class  BookGenre</para>
        /// </value>
        
        public List<Book> Books { get; set; }

        /// <summary>
        /// Override string method
        /// </summary>
        /// <returns>string in format <c>Name</c></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
