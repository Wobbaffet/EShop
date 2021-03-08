using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation
{
  public   interface IRepositoryCustomer:IRepository<Customer>
    {
        void Update(Customer customer);
    }
}
