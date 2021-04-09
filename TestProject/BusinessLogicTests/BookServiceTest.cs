using BusinessLogic.Classes;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Assert = NUnit.Framework.Assert;

namespace TestProject.BusinessLogicTests
{
    [TestClass]
  public   class BookServiceTest
    {


        BookService bs;
        EShopUnitOfWork uow;

        [TestInitialize]
        public void Initialize()
        {
            bs = new BookService();
            uow = new EShopUnitOfWork(new EShop.Model.ShopContext());
        }


        [TestMethod]

        public void Test_FindMethod()
        {
          Book b=  bs.Find(3);
            Book exp = new Book()
            {
                Title = "Women"
            };

            Assert.AreEqual(b.Title, exp.Title);
        }

       [TestMethod]

        public void Test_SearchMethod()
        {
           var books= bs.Search("sex");

            books.ForEach(b =>
            {
                Assert.IsTrue(b.Title.ToLower().Contains("sex"));
            });
            
        }



    }
}
