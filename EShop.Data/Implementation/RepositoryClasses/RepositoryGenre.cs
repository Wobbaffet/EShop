using EShop.Data.Implementation.Interfaces;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{

    /// <inheritdoc/>
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryGenre : IRepositoryGenre
    {
        private readonly ShopContext context;
        public RepositoryGenre(ShopContext context) => this.context = context;
        public void Add(Genre entity) => throw new NotImplementedException();
        public Genre Find(Predicate<Genre> p) => context.Genre.ToList().Find(p);
        public List<Genre> GetAll() => context.Genre.ToList();
    }
}
