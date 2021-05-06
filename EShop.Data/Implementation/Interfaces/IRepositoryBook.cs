using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EShop.Data.Implementation.Interfaces
{
    /// <inheritdoc/>
    /// <summary>
    /// Represent IRepository for Book
    /// </summary>
    public interface IRepositoryBook : IRepository<Book>
    {
     /// <summary>
     /// Method searcing for books by title
     /// </summary>
     /// <param name="title">title as string</param>
     /// <returns>List<Book></returns>
        public List<Book> SearchByTitle(string title);

        /// <summary>
        /// Returning books by some condition
        /// <br>
        /// <para>Using for pagination, filtering by some condition</para>
        /// </summary>
        /// <param name="condition">condition as function delegate</param>
        /// <param name="pageNumber"> represent page number in pagination</param>
        /// <returns> list of <c>Book</c></returns>
        List<Book> GetBooksByCondition(Func<Book, bool> condition, int pageNumber);

        /// <summary>
        /// Returning number of books needed to for pagination
        /// </summary>
        /// <param name="condition">condition as function</param>
        /// <returns> total number of books by some condition in database</returns>
        public int GetTotalNumberOfBooksByCondition(Func<Book, bool> condition);
        
    }
}
