using BusinessLogic.Interfaces;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Classes
{
    public class OrderService:IOrder
    {
        public IUnitOfWork uow { get; set ; }

        public OrderService()
        {
            uow= new EShopUnitOfWork(new ShopContext());
        }

        public List<Order> GetAll(int? customerId)
        {
          return uow.RepositoryOrder.GetAllOrders(o => o.Customer.CustomerId == customerId);

        }

        public Order GetOrderItems(int orderId)
        {
            return  uow.RepositoryOrder.FindWithInclude(o => o.OrderId == orderId);
        }

        public void UpdateOrders(List<Order> orders)
        {
            orders.ForEach(o =>
            {
                Order order = uow.RepositoryOrder.FindWithoutInclude(or => or.OrderId == o.OrderId);
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
    }
}
