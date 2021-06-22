using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Classes
{
    /// <inheritdoc/>
    /// <summary>
    ///  Represent class for business logic with Orders
    /// </summary>
    public class OrderService:IOrderService
    {
        public IUnitOfWork uow { get; set ; }

        public OrderService(IUnitOfWork uow)
        {
            this.uow = uow;
            //uow= new EShopUnitOfWork(new ShopContext());
        }

        public List<Order> GetAll(int? customerId)
        {
          return uow.RepositoryOrder.GetAllOrders(o => o.Customer.CustomerId == customerId);
        }

        public Order GetOrderItems(int orderId)
        {
            return  uow.RepositoryOrder.Find(o => o.OrderId == orderId);
        }

        public void UpdateOrders(List<Order> orders)
        {
            orders.ForEach(o =>
            {
                Order order = uow.RepositoryOrder.Find(or => or.OrderId == o.OrderId);
                order.OrderStatus = o.OrderStatus;
                uow.Commit();
            });
        }

        public List<Order> SortOrders(bool condition)
        {
            List<Order> orders;
            if (condition)
                orders = uow.RepositoryOrder.Sort();
            else
                orders = uow.RepositoryOrder.GetAll();

            return orders;
        }

        public void PurchaseBooks(Order order, int? customerId)
        {
            if (customerId == null || uow.RepostiryCustomer.Find(c => c.CustomerId == customerId) == null)
                throw new CustomerNullException("Customer id is null or customer doesn't exist");

            if (order.OrderItems==null ||order.OrderItems.Count == 0)
                throw new OrderException("Order must has at least one order item ! ");

            if (order.Total != order.OrderItems.Sum(ot => ot.Quantity * ot.Book.Price))
                throw new OrderException("Total price doesn't  have good value !");


            order.OrderItems.ForEach(oi => oi.Book = uow.RepositoryBook.Find(b => b.BookId == oi.Book.BookId));

            order.Date = DateTime.Now;

            order.Customer = uow.RepostiryCustomer.Find(c => c.CustomerId == customerId);

            foreach (var item in order.OrderItems)
            {
                item.Book.Supplies -= item.Quantity;
            }
            uow.RepositoryOrder.Add(order);
            uow.Commit();
        }
    }
}
