using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EShop.Data.Implementation.Interfaces
{
    public interface IRepositoryBook : IRepository<Book>
    {
     
        public List<Book> SearchByTitle(string title);

        List<Book> GetBooksByCondition(Func<Book, bool> condition, int pageNumber);

        public int GetTotalNumberOfBooksByCondition(Func<Book, bool> condition);
        
    }
}
