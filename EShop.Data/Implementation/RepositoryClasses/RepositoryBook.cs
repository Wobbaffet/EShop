using EShop.Model;
using EShop.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryBook : IRepositoryBook
    {
        private readonly ShopContext context;

        public RepositoryBook(ShopContext context)
        {
            this.context = context;
        }
        public void Add(Book entity)
        {
            context.Add(entity);
        }
        public Book Find(Predicate<Book> condition)
        {
            return context.Book.AsNoTracking().ToList().Find(condition);
        }

        public List<Book> GetAll()
        {
            List<Book> books = context.Book.ToList();
            return books;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
