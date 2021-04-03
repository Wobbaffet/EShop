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

        public Book FindWithoutInclude(Predicate<Book> condition)
        {
            return context.Book.ToList().Find(condition);
        }

        public Book FindWithInclude(Predicate<Book> condition)
        {

            return context.Book.Include(a => a.Autors).Include(a => a.Genres).ToList().Find(condition);
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

        public int GetNumberOfBooksByGenre(List<string> genres)
        {
            if (genres.Count == 0)
                return context.Book.Include(a => a.Genres).Where(b => b.Supplies != 0).Count();
            return context.Book.Include(a => a.Genres)
                .Where(b => b.Supplies != 0 && b.Genres.Any(g => genres.Any(genre => genre == g.Name))).Count();
        }

        private int PrivateGetNumberOfBooksByGenreAndPrice(int firstPrice, int secondPrice, List<string> genres)
        {
            if (genres.Count > 0)
                return context.Book.Include(a => a.Genres)
                    .Where(
                        b => b.Supplies != 0 &&
                        b.Price >= firstPrice && b.Price <= secondPrice &&
                        b.Genres.Any(g => genres.Any(genre => genre == g.Name))
                    ).Count();
            else
                return context.Book.Include(a => a.Genres)
                   .Where(
                       b => b.Supplies != 0 &&
                       b.Price >= firstPrice && b.Price <= secondPrice
                   ).Count();
        }

        public int GetNumberOfBooksByGenreAndPrice(string price, List<string> genres)
        {
            int firstPrice = 0;
            int secondPrice = 0;
            if (price == null || price == "No filters") { }
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

            if (price == "No filters")
            {
                return GetNumberOfBooksByGenre(genres);
            }
            else
            {
                return PrivateGetNumberOfBooksByGenreAndPrice(firstPrice, secondPrice, genres);
            }
        }

        public List<Book> GetTwelveBooksByGenre(int totalNumberOfBooksByGenreAndPrice, int pagiNumber, List<string> genres)
        {
            try
            {
                if (genres.Count == 0)
                {
                    if (pagiNumber * 12 > totalNumberOfBooksByGenreAndPrice)
                        return context.Book.Include(a => a.Genres).Where(b => b.Supplies != 0)
                            .Skip((pagiNumber - 1) * 12).Take(12 - pagiNumber * 12 + totalNumberOfBooksByGenreAndPrice).ToList();
                    else
                        return context.Book.Include(a => a.Genres).Where(b => b.Supplies != 0)
                            .Skip((pagiNumber - 1) * 12).Take(12).ToList();
                }
                else
                {
                    if (pagiNumber * 12 > totalNumberOfBooksByGenreAndPrice)
                        return context.Book.Include(a => a.Genres)
                                .Where(b => b.Supplies != 0 && b.Genres.Any(g => genres.Any(genre => genre == g.Name)))
                                .Skip((pagiNumber - 1) * 12).Take(12 - pagiNumber * 12 + totalNumberOfBooksByGenreAndPrice).ToList();
                    else
                        return context.Book.Include(a => a.Genres)
                                .Where(b => b.Supplies != 0 && b.Genres.Any(g => genres.Any(genre => genre == g.Name)))
                                .Skip((pagiNumber - 1) * 12).Take(12).ToList();
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                return new List<Book>();
            }
        }

        private List<Book> PrivateGetTwelveBooksByGenreAndPrice(int totalNumberOfBooksByGenreAndPrice, int pagiNumber, int firstPrice, int secondPrice, List<string> genres)
        {
            try
            {
                if (genres.Count > 0)
                {
                    if (pagiNumber * 12 > totalNumberOfBooksByGenreAndPrice)
                    {
                        return context.Book.Include(a => a.Genres)
                            .Where(
                                b => b.Supplies != 0 &&
                                b.Price >= firstPrice && b.Price <= secondPrice &&
                                b.Genres.Any(g => genres.Any(genre => genre == g.Name))
                            ).Skip((pagiNumber - 1) * 12).Take(12 - pagiNumber * 12 + totalNumberOfBooksByGenreAndPrice).ToList();
                    }
                    else
                        return context.Book.Include(a => a.Genres)
                            .Where(
                                b => b.Supplies != 0 &&
                                b.Price >= firstPrice && b.Price <= secondPrice &&
                                b.Genres.Any(g => genres.Any(genre => genre == g.Name))).Skip((pagiNumber - 1) * 12).Take(12).ToList();
                }
                else
                {
                    if (pagiNumber * 12 > totalNumberOfBooksByGenreAndPrice)
                        return context.Book.Include(a => a.Genres)
                                .Where(
                                    b => b.Supplies != 0 &&
                                    b.Price >= firstPrice && b.Price <= secondPrice
                                ).Skip((pagiNumber - 1) * 12).Take(12 - pagiNumber * 12 + totalNumberOfBooksByGenreAndPrice).ToList();
                    else
                        return context.Book.Include(a => a.Genres)
                                .Where(
                                    b => b.Supplies != 0 &&
                                    b.Price >= firstPrice && b.Price <= secondPrice
                                ).Skip((pagiNumber - 1) * 12).Take(12).ToList();
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                return new List<Book>();
            }
        }

        public List<Book> GetTwelveBooksByGenreAndPrice(int totalNumberOfBooksByGenreAndPrice, int pagiNumber, string price, List<string> genres)
        {
            int firstPrice = 0;
            int secondPrice = 0;
            if (price == null || price == "No filters") { }
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

            if (price == "No filters")
            {
                return GetTwelveBooksByGenre(totalNumberOfBooksByGenreAndPrice, pagiNumber, genres);
            }
            else
            {
                return PrivateGetTwelveBooksByGenreAndPrice(totalNumberOfBooksByGenreAndPrice, pagiNumber, firstPrice, secondPrice, genres);
            }
        }
    }

}
