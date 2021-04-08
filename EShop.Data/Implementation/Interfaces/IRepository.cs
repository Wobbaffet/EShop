using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        List<T> GetAll();
        T FindWithoutInclude(Predicate<T> condition);
        T FindWithInclude(Predicate<T> condition);
        T Find(Predicate<T> p);
    }
}
