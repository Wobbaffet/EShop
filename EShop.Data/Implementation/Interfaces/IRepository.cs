using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation
{
    /// <summary>
    /// Represent generic repository
    /// </summary>
    /// <typeparam name="T">In T will be some class (Autor,Book,Customer...)</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Method adding entity in database
        /// </summary>
        /// <param name="entity">some entity needed to be added in database</param>
        void Add(T entity);
        /// <summary>
        /// Finding all objects in database
        /// </summary>
        /// <returns>List of classes</returns>
        List<T> GetAll();
        /// <summary>
        /// Finding object in database
        /// </summary>
        /// <param name="p">p as predicate delegate</param>
        /// <returns>Some class</returns>
        T Find(Predicate<T> p);
    }
}
