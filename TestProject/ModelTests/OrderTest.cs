using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ModelTests
{
    [TestClass]
   public  class OrderTest
    {

        private Order order;

        [TestInitialize]
        public void Initialize()
        {
            order= new Order();
        }

        [TestMethod]

        public void Test_OrderTotalException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => order.Total = -2);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => order.Total = 0);
        } 
        [TestMethod]
        public void Test_OrderItemException()
        {
            Assert.ThrowsException<NullReferenceException>(() => order.OrderItems=null);
        }   
        [TestMethod]
        public void Test_CustomerException()
        {
            Assert.ThrowsException<NullReferenceException>(() => order.Customer=null);
        }

    }
}
