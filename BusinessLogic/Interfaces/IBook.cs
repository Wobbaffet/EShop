using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
   public  interface IBook:IService
    {

        Book Find(int? bookId);

        void Add(List<Book> books);

        List<Book> Search(string title);
    }
}
