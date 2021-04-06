using BusinessLogic.Interfaces;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Classes
{
    public class BookService : IBook
    {

        public BookService()
        {

                uow = new EShopUnitOfWork(new ShopContext());
            
        }
        public IUnitOfWork uow { get ; set; }

        public Book Find(int? bookId) => uow.RepositoryBook.FindWithInclude(b => b.BookId == bookId);
       
    }
}
