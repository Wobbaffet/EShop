using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation
{
    /// <inheritdoc/>
    /// <summary>
    /// Represent IRepository for Customer
    /// </summary>
    public interface IRepositoryCustomer:IRepository<Customer>
    {
        /// <summary>
        /// Update customer in database
        /// </summary>
        /// <param name="customer">customer that need to be updated</param>
        void Update(Customer customer);

        /// <summary>
        /// Delete customer in database
        /// </summary>
        /// <param name="customer">customer that need to be deleted</param>
        void Delete(Customer customer);
    }
}
