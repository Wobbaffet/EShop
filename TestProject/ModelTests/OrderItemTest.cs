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
        [DataRow(0)]
        [DataRow(-2)]
        public void Test_QuantityException(int quantity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => oi.Quantity = quantity);
        } 

        [TestMethod]
        public void Test_BookException()
        {
            Assert.ThrowsException<NullReferenceException>(() => oi.Book= null);
        }

    }
}
