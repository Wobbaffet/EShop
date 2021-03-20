using EShop.Data.Implementation.Interfaces;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryAutor : IRepositoryAutor
    {

        private readonly ShopContext context;

        public RepositoryAutor(ShopContext context)
        {
            this.context = context;
        }

        public void Add(Autor entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Autor FindWithInclude(Predicate<Autor> condition)
        {
            throw new NotImplementedException();
        }

        public Autor FindWithoutInclude(Predicate<Autor> condition)
        {
            return context.Autor.ToList().Find(condition);
        }

        public List<Autor> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
