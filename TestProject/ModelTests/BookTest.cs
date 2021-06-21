using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ModelTests
{
    [TestClass]
    public class BookTest
    {
        private Book book;

        [TestInitialize]
        public void Initialize()
        {
            book = new Book();
        }

        [TestMethod]

        public void Test_BookImageException()
        {
        Assert.ThrowsException<NullReferenceException>(() => book.Image = "");
        Assert.ThrowsException<NullReferenceException>(() => book.Image = null);
        }

        [TestMethod]

        public void Test_BookPriceException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => book.Price = 0);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => book.Price = -5);
        }
        [TestMethod]

        public void Test_BookSuppliesException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => book.Supplies = -1);
        }


    }
}
