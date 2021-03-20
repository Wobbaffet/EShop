using EShop.Data.Implementation.Interfaces;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryGenre : IRepositoryGenre
    {
        private readonly ShopContext context;

        public RepositoryGenre(ShopContext context)
        {
            this.context = context;
        }
        public void Add(Genre entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Genre FindWithInclude(Predicate<Genre> condition)
        {
            throw new NotImplementedException();
        }

        public Genre FindWithoutInclude(Predicate<Genre> condition)
        {
            return context.Genre.ToList().Find(condition);
        }

        public List<Genre> GetAll()
        {
            return context.Genre.ToList();
        }
    }
}
