using EShop.Data.Implementation;
using EShop.Data.Implementation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.UnitOfWork
{
    /// <summary>
    /// Represent interface UnitOfWork pattern
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Represent repository for Book
        /// </summary>
        public IRepositoryBook RepositoryBook { get; set; }
        /// <summary>
        /// Represent repository for Customer
        /// </summary>
        public IRepositoryCustomer RepostiryCustomer { get; set; }
        /// <summary>
        /// Represent repository for Order
        /// </summary>
        public IRepositoryOrder RepositoryOrder { get; set; }
        /// <summary>
        /// Represent repository for Genre
        /// </summary>
        public IRepositoryGenre RepositoryGenre { get; set; }
        /// <summary>
        /// Represent repository for Autor
        /// </summary>
        public IRepositoryAutor RepositoryAutor { get; set; }
        /// <summary>
        /// Metod that do transaction in database
        /// </summary>
        void Commit();
    }
}
