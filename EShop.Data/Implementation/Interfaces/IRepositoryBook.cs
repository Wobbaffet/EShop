using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EShop.Data.Implementation.Interfaces
{
    public interface IRepositoryBook : IRepository<Book>
    {
        public int GetNumberOfBooksByGenre(List<string> genres);
        public int GetNumberOfBooksByGenreAndPrice(string price, List<string> genres);
        public List<Book> GetTwelveBooksByGenre(int totalNumberOfBooksByGenreAndPrice, int pagiNumber, List<string> genres);
        public List<Book> GetTwelveBooksByGenreAndPrice(int totalNumberOfBooksByGenreAndPrice, int pagiNumber, string price, List<string> genres);
        public List<Book> Search(string autor);
    }
}
