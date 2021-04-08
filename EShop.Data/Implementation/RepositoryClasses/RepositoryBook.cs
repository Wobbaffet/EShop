using EShop.Data.Implementation.Interfaces;
using EShop.Model;
using EShop.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public List<Book> GetAll()
        {
            List<Book> books = context.Book.Include(a => a.Autors).Include(a => a.Genres).ToList();

            List<Book> booksWithSupplies = new List<Book>();
            books.ForEach(b =>
            {
                if (b.Supplies != 0)
                    booksWithSupplies.Add(b);
            });
            return booksWithSupplies;
        }
        public List<Book> SearchByTitle(string title)
        {
            return context.Book.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();
        }
        public List<Book> GetBooksByCondition(Func<Book,bool> condition,int pageNumber)
        {
            return context.Book.Include(b => b.Genres).Where(condition).Skip((pageNumber-1)*12).Take(12).ToList();
        }
        public int  GetTotalNumberOfBooksByCondition(Func<Book, bool> condition)
        {
            return context.Book.Include(b => b.Genres).Where(condition).ToList().Count();
        }
        public Book Find(Predicate<Book> p)
        {
            return context.Book.Include(a => a.Autors).Include(a => a.Genres).ToList().Find(p);
        }
    }

}
