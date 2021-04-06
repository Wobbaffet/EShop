using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
  public   interface IOrder:IService
    {

        List<Order> GetAll(int ? customerId);

        Order GetOrderItems(int orderId);

        void UpdateOrders(List<Order> orders);

        List<Order> SortOrders(bool condition);
    }
}
