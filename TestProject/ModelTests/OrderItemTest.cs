using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ModelTests
{
    [TestClass]
   public  class OrderItemTest
    {

        private OrderItem oi;

        [TestInitialize]
        public void Initialize()
        {
            oi= new OrderItem();
        }

        [TestMethod]
        public void Test_QuantityException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => oi.Quantity = 0);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => oi.Quantity = -2);
        } 

        [TestMethod]
        public void Test_BookException()
        {
            Assert.ThrowsException<NullReferenceException>(() => oi.Book= null);
        }

    }
}
