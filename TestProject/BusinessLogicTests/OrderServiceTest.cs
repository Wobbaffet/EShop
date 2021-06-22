using BusinessLogic.Classes;
using BusinessLogic.Exceptions;
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
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestProject.BusinessLogicTests
{


    [TestClass]
    public class OrderServiceTest
    {

        Mock<IUnitOfWork> uow;
        OrderService service;
        [TestInitialize]
        public void Initialize()
        {
            uow = MoqClass.Mocks.GetMockUnitOfWork() ;
            service = new OrderService(uow.Object);
        }

        [TestMethod]
        public void Test_GetAllMethod()
        {
            var expected = service.GetAll(1).OrderBy(o=>o.OrderId).ToList();
            var actual = uow.Object.RepositoryOrder.GetAllOrders(o => o.Customer.CustomerId == 1).OrderBy(o=>o.OrderId).ToList();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Customer.CustomerId , actual[i].Customer.CustomerId);
            }
        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Test_SortOrdersMethodTrue(bool condition)
        {
            var expected = service.SortOrders(condition);

            var actual=new List<Order>();
            if (condition)
            {
             actual = uow.Object.RepositoryOrder.Sort();
            }
            else
            {
                 actual = uow.Object.RepositoryOrder.GetAll();
            }

            Assert.AreEqual(actual.Count, expected.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(expected[i].OrderId == actual[i].OrderId);
            }
        }

        [TestMethod]
        public void Test_PurchaseBooks()
        {
            Order order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Quantity=3,
                        Book=new Book(){BookId=1,Title="Title 1",Image="img1",Price=10,Supplies=10}
                    },
                    new OrderItem()
                    {
                        Quantity=5,
                        Book=new Book(){BookId=2,Title="Title 2",Image="img2",Price=15,Supplies=10}
                    },

                },
                
            };

            order.Total = order.OrderItems.Sum(oi => oi.Quantity * oi.Book.Price);

            service.PurchaseBooks(order, 1);

            var customer = uow.Object.RepostiryCustomer.Find(c => c.CustomerId == 1);
            var actual = uow.Object.RepositoryOrder.Find(o => o.OrderId == 50);
            Assert.IsNotNull(actual);
            Assert.IsTrue(order.OrderItems[0].Book.Supplies == 7);
            Assert.IsTrue(order.OrderItems[1].Book.Supplies == 5);
            

        }

        [TestMethod]
        public void Test_PurchaseBooksOrderItemsZero()
        {
            Order order = new Order();
            Assert.ThrowsException<OrderException>(() => service.PurchaseBooks(order, 1));
        }  
        
        [TestMethod]
        public void Test_PurchaseBooksTotalError()
        {
             Order order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Quantity=3,
                        Book=new Book(){BookId=1,Title="Title 1",Image="img1",Price=10,Supplies=10}
                    },
                    new OrderItem()
                    {
                        Quantity=5,
                        Book=new Book(){BookId=2,Title="Title 2",Image="img2",Price=15,Supplies=10}
                    },

                },

            };

            order.Total = order.OrderItems.Sum(oi => oi.Quantity * oi.Book.Price)+10;
            Assert.ThrowsException<OrderException>(() => service.PurchaseBooks(order, 1));
        }

        [TestMethod]
        public void Test_PurchaseBooksCustomerException()
        {
            Assert.ThrowsException<CustomerNullException>(() => service.PurchaseBooks(null, null));
            Assert.ThrowsException<CustomerNullException>(() => service.PurchaseBooks(null, 1213));
        }

    }
}
