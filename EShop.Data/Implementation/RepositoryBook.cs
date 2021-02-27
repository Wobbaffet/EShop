using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation
{
    public class RepositoryBook : Generic<Book>
    {
        public RepositoryBook(ShopContext context) : base(context)
        {

        }
        public override List<Book> GetAll(Book entity)
        {
            return context.Book.ToList();
        }
    }
}
