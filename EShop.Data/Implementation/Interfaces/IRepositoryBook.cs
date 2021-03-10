using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EShop.Data.Implementation.Interfaces
{
    public interface IRepositoryBook : IRepository<Book>
    {
        public List<Book> Search(string autor);
    }
}
