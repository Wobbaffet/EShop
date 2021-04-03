using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation.Interfaces
{
    public interface IRepositoryOrder : IRepository<Order>
    {
        List<Order> GetAllOrders(Predicate<Order> condition);

        List<Order> Sort();
    }
}
