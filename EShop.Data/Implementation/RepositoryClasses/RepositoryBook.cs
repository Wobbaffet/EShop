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
        public Book Find(Predicate<Book> condition)
        {
           
            return context.Book.ToList().Find(condition);
        }

        public List<Book> GetAll()
        {
            List<Book> books = context.Book.Include(a => a.Autors).Include(a => a.Genres).ToList();
            return books;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public List<Book> Search(string autor)
        {
            List<Book> books = GetAll();
            if (autor == "all")
                return books;
            List<Book> booksWithThatAutor = new List<Book>();
            foreach (var item in books)
            {
                if (item.Autors.Any(a => (a.FirstName + " " + a.LastName).ToLower() == autor))
                    booksWithThatAutor.Add(item);
            }
            return booksWithThatAutor;
        }
    }
}
