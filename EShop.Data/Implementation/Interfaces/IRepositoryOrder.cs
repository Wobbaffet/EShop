using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation.Interfaces
{
    /// <inheritdoc/>
    /// <summary>
    /// Represent IRepository for Order
    /// </summary>
    public interface IRepositoryOrder : IRepository<Order>
    {
        /// <summary>
        /// Represent method that return all orders by some condition
        /// </summary>
        /// <param name="condition">condition as predicate </param>
        /// <returns> list of orders</returns>
        List<Order> GetAllOrders(Predicate<Order> condition);

        /// <summary>
        /// Sorting orders by order status
        /// </summary>
        /// <returns> list of sorted orders</returns>
        List<Order> Sort();
    }
}
