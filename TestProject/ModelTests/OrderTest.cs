using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


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
        [DataRow(0)]
        [DataRow(-2)]
        public void Test_OrderTotalException(double total)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => order.Total =total);
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
