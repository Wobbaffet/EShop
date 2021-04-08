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
    /// <inheritdoc/>
    /// <summary>
    ///Represent class for business logic with Books

    /// </summary>
    public class BookService : IBook
    {
        /// <summary>
        /// Constructor that initialize UnitOfWork
        /// </summary>
        public BookService()
        {

                uow = new EShopUnitOfWork(new ShopContext());
            
        }
        public IUnitOfWork uow { get ; set; }

        public void Add(List<Book> books)
        {
            foreach (var item in books)
            {
                Book b = uow.RepositoryBook.FindWithoutInclude(b => b.Title == item.Title && b.Description == item.Description);
                if (b == null)
                {
                    for (int i = 0; i < item.Genres.Count; i++)
                    {
                        item.Genres[i] = uow.RepositoryGenre.FindWithoutInclude(g => g.Name == item.Genres[i].Name);
                    }
                    for (int i = 0; i < item.Autors.Count; i++)
                    {
                        Autor a = uow.RepositoryAutor.FindWithoutInclude(a => a.FirstName == item.Autors[i].FirstName && a.LastName == item.Autors[i].LastName);
                        if (a != null)
                        {
                            item.Autors[i] = a;
                        }
                    }
                    uow.RepositoryBook.Add(item);
                }
                else
                {
                    b.Supplies += item.Supplies;
                }
                uow.Commit();
            }
        }

        public Book Find(int? bookId) => uow.RepositoryBook.FindWithInclude(b => b.BookId == bookId);

        public List<Book> Search(string title) => uow.RepositoryBook.SearchByTitle(title);

    }
}
