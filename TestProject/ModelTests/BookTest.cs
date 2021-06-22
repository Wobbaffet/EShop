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
        [DataRow(0)]
        [DataRow(-5)]
        public void Test_BookPriceException(double price)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => book.Price = price);
        }
        [TestMethod]

        public void Test_BookSuppliesException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => book.Supplies = -1);
        }


    }
}
