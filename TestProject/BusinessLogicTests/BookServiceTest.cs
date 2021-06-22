using Autofac.Extras.Moq;
using BusinessLogic.Classes;
using EShop.Data.Implementation;
using EShop.Data.Implementation.Interfaces;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assert = NUnit.Framework.Assert;

namespace TestProject.BusinessLogicTests
{
    [TestClass]
    public class BookServiceTest
    {
       private Mock<IUnitOfWork> uow;
       private BookService service;
        [TestInitialize]
        public void Initialize()
        {
            uow = MoqClass.Mocks.GetMockUnitOfWork();
            service = new BookService(uow.Object);
        }

        [TestMethod]
        public void Test_SearchMethod()
        {
            string title = "1";

            var expected = service.Search(title);

            var actual = uow.Object.RepositoryBook.SearchByTitle("1");

            Assert.IsTrue(actual != null);
            Assert.IsTrue(expected != null);
            Assert.AreEqual(actual.Count, expected.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(actual[i].Title == expected[i].Title);
            }
        } 
        
        [TestMethod]

        public void Test_FindMethod()
        {
            var expected = service.Find(1);

            var actual = uow.Object.RepositoryBook.Find(b=>b.BookId==1);

            Assert.IsTrue(expected != null);
            Assert.AreEqual(expected.BookId, actual.BookId);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.Supplies, actual.Supplies);
        }

       
    }
}
