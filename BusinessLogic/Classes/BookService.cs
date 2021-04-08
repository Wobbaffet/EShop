using BusinessLogic.Interfaces;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IUnitOfWork uow { get; set; }
        public void Add(List<Book> books)
        {
            foreach (var item in books)
            {
              //  Book b = uow.RepositoryBook.FindWithoutInclude(b => b.Title == item.Title && b.Description == item.Description);
                Book b = uow.RepositoryBook.Find(b => b.Title == item.Title && b.Description == item.Description);
                if (b == null)
                {
                    for (int i = 0; i < item.Genres.Count; i++)
                    {
                      //  item.Genres[i] = uow.RepositoryGenre.FindWithoutInclude(g => g.Name == item.Genres[i].Name);
                        item.Genres[i] = uow.RepositoryGenre.Find(g => g.Name == item.Genres[i].Name);
                    }
                    for (int i = 0; i < item.Autors.Count; i++)
                    {
                       // Autor a = uow.RepositoryAutor.FindWithoutInclude(a => a.FirstName == item.Autors[i].FirstName && a.LastName == item.Autors[i].LastName);
                        Autor a = uow.RepositoryAutor.Find(a => a.FirstName == item.Autors[i].FirstName && a.LastName == item.Autors[i].LastName);
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
      //  public Book Find(int? bookId) => uow.RepositoryBook.FindWithInclude(b => b.BookId == bookId);
        public Book Find(int? bookId) => uow.RepositoryBook.Find(b => b.BookId == bookId);
        public List<Book> Search(string title) => uow.RepositoryBook.SearchByTitle(title);
        public List<Book> GetBooksByCondition(int pageNumber, string price, List<string> genres)
        {

            Func<Book, bool> func;
            var FirstSecondPrice = Price(price);
            if (genres.Count > 0)
            {
                func = (b => b.Supplies != 0 &&
                           b.Price >= FirstSecondPrice[0] && b.Price <= FirstSecondPrice[1] &&
                           b.Genres.Any(g => genres.Any(genre => genre == g.Name)));
            }
            else
            {
                func = (b => b.Supplies != 0 && b.Price >= FirstSecondPrice[0] && b.Price <= FirstSecondPrice[1]);
            }

            return uow.RepositoryBook.GetBooksByCondition(func, pageNumber);


        }
        private List<int> Price(string price)
        {
            List<int> list = new List<int>();
            int firstPrice = 0;
            int secondPrice = 0;
            if (price == null || price == "No filters")
            {
                secondPrice = int.MaxValue;
            }
            else if (price.Contains("Less"))
            {
                secondPrice = 500;
            }
            else if (price.Contains("More"))
            {
                firstPrice = 5000;
                secondPrice = int.MaxValue;
            }
            else
            {
                string[] prices = price.Split(" - ");
                firstPrice = int.Parse(prices[0]);
                secondPrice = int.Parse(prices[1]);
            }
            list.Add(firstPrice);
            list.Add(secondPrice);
            return list;
        }
        public int GetBooksNumberByCondition(string price, List<string> genres)
        {
            var FirstSecondPrice = Price(price);
            if (genres.Count == 0)
                return uow.RepositoryBook.GetTotalNumberOfBooksByCondition(b => b.Supplies != 0 &&
                b.Price >= FirstSecondPrice[0] && b.Price <= FirstSecondPrice[1]);
            else
            {
                return uow.RepositoryBook.GetTotalNumberOfBooksByCondition(b => b.Supplies != 0 &&
               b.Price >= FirstSecondPrice[0] && b.Price <= FirstSecondPrice[1]
                && b.Genres.Any(g => genres.Any(genre => genre == g.Name)));
            }

        }
    }
}
