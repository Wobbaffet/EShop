using EShop.Model;
using System;
using System.Collections.Generic;

namespace EShop.Data
{
    public abstract class Generic<T> where T : class, new()
    {
        protected ShopContext context;
        public Generic(ShopContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Add(entity);
        }
        public abstract List<T> GetAll(T entity);

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
