using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface that manager working with BookService
    /// </summary>
   public  interface IBookService:IService
    {

        /// <summary>
        /// Finds book by bookId
        /// </summary>
        /// <param name="bookId">Book id as int</param>
        /// <returns>Book</returns>
        Book Find(int? bookId);

        /// <summary>
        /// Adds book in database
        /// </summary>
        /// <param name="books">List of books that need to be saved in database</param>
        /// <returns>Book</returns>
        void Add(List<Book> books);

        /// <summary>
        /// Searches books by title
        /// </summary>
        /// <param name="books">Represent title of book</param>
        /// <returns> List<Book> or null if there are no book in database</returns>

        List<Book> Search(string title);
    }
}
