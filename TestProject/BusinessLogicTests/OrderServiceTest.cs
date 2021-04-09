using BusinessLogic.Classes;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestProject.BusinessLogicTests
{
    [TestClass]
    public class OrderServiceTest
    {

        OrderService os;
        EShopUnitOfWork uow;

        [TestInitialize]
        public void Initialize()
        {
            os = new OrderService();
            uow = new EShopUnitOfWork(new EShop.Model.ShopContext());
        }
        

        [TestMethod]
        public void Test_GetAllMethod()
        {
          var orders=  os.GetAll(1126);

            Assert.IsNotNull(orders);
        }

        [TestMethod]
        public void Test_GetOrderItemsMethod()
        {
            var order = os.GetOrderItems(19);

            Assert.IsNotNull(order);
        }

        //True kada je sortiranje po statusima
        [TestMethod]
        public void Test_SortOrdersMethodTrue()
        {
            var order = os.SortOrders(true);

            for (int i = 0; i < order.Count-1; i++)
            {
                Assert.IsTrue(order[i].OrderStatus <= order[i+1].OrderStatus);
            }
        }

        //False kada je sortiranje po datumu
        [TestMethod]
        public void Test_SortOrdersMethodFalse()
        {
            var order = os.SortOrders(false);

            for (int i = 0; i < order.Count - 1; i++)
            {
                Assert.IsTrue(order[i].Date < order[i + 1].Date);
            }
        }

       




    }
}
