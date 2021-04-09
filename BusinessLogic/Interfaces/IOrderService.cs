using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface that manager working with OrderService
    /// </summary>
    public interface IOrderService:IService
    {
        /// <summary>
        /// Returning all orders for customer id 
        /// </summary>
        /// <param name="customerId">Represent cusotmer id  as int</param>
        /// <returns>List<Order> or null if there is no orders for customerId</returns>
        List<Order> GetAll(int ? customerId);

        /// <summary>
        /// Returning all orders and all order items
        /// </summary>
        /// <param name="orderId">Represent order id as int</param>
        /// <returns>Return Order with all order items</returns>
        Order GetOrderItems(int orderId);
        /// <summary>
        /// Updates all orders status
        /// </summary>
        /// <param name="orders">Represent List<Order> that need to be updated</param>
        void UpdateOrders(List<Order> orders);

        /// <summary>
        /// Sort all order items by condition <br></br>if condition is true, sorting by status, otherwise sorting by date
        /// </summary>
        /// <param name="condition">Represent condition of sorting as bool type</param>
        /// <returns>List<Order> that is sorted by some condition</returns>
        List<Order> SortOrders(bool condition);

        /// <summary>
        /// Metod save orders for current customerId
        /// </summary>
        /// <param name="order">Represent order as type Order</param>
        /// <param name="customerId">Represent customer id as int</param>
        void PurchaseBooks(Order order, int? customerId);
    }
}
