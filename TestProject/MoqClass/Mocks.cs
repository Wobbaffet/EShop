using EShop.Data.Implementation;
using EShop.Data.Implementation.Interfaces;
using EShop.Data.UnitOfWork;
using EShop.Model;
using EShop.Model.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject.MoqClass
{
   public  class Mocks
    {

        public static Mock<IRepositoryCustomer> GetMockCustomerRepository()
        {
            var customers = new List<Customer>()
            {
                new NaturalPerson()
                {
                    CustomerId=1,
                    Address =new Address()
                    {
                        CityName="Belgrade",
                        PTT=11000,
                        StreetName="Tosin Bunar",
                        StreetNumber="157A"
                    },
                    Email="markobabovic406@gmail.com",
                    FirstName="Marko",
                    LastName="Babovic",
                    Password="Markoni21@.",
                    PhoneNumber="0655214799",
                    VerificationCode=1232,
                },
                new LegalEntity()
                {
                     CustomerId=2,
                    Address =new Address()
                    {
                        CityName="Belgrade",
                        PTT=11000,
                        StreetName="Tosin Bunar",
                        StreetNumber="157B"
                    },
                    Email="nikola406@gmail.com",
                    CompanyName="Google",
                    TIN=1222,
                    Password="Markoni21@.",
                    PhoneNumber="0655214799",
                    VerificationCode=1232,
                    
                }
            
            };

             var mockCustomerRepo = new Mock<IRepositoryCustomer>();

             mockCustomerRepo.Setup(r => r.Find(It.IsAny<Predicate<Customer>>())).Returns((Predicate<Customer> p)=>
              {
                  return customers.Find(p);
              });

             mockCustomerRepo.Setup(r => r.GetAll()).Returns(customers);
          
             mockCustomerRepo.Setup(r => r.Add(It.IsAny<Customer>())).Callback((Customer c)=>
               {
                c.CustomerId = 20;
                customers.Add(c);
              }).Verifiable();  
            
             mockCustomerRepo.Setup(r => r.Delete(It.IsAny<Customer>())).Callback((Customer c)=>
              {
                var cus=  customers.Find(customer => customer.CustomerId == c.CustomerId);
                customers.Remove(cus);
              }).Verifiable();

            return mockCustomerRepo;
        }
        public static Mock<IRepositoryBook> GetMockBookRepository()
        {
            var mockBookRepository = new Mock<IRepositoryBook>();

            mockBookRepository.Setup(x => x.SearchByTitle(It.IsAny<string>())).Returns((string title) =>
            {
                return Books().FindAll(b => b.Title.Contains(title));
            });  
            
            mockBookRepository.Setup(x => x.Find(It.IsAny<Predicate<Book>>())).Returns((Predicate<Book> p) =>
            {
                return Books().Find(p);
            });

            return mockBookRepository;
        }
        public static Mock<IRepositoryOrder> GetMockOrderRepository()
        {
            var orders = new List<Order>() {

                new Order()
                {
                    OrderId=1,
                    Date=DateTime.Now,
                    OrderStatus=OrderStatus.Completed,
                    Customer =new NaturalPerson()
                    {
                        CustomerId=1
                    },
                    OrderItems=new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            OrderItemId=55,
                            Quantity=10,
                        }, 
                        new OrderItem()
                        {
                            OrderItemId=56,
                            Quantity=12,
                        },

                    },
                    Total=10,
                }
            };

            var mockOrderRepo = new Mock<IRepositoryOrder>();

            mockOrderRepo.Setup(repo => repo.GetAll()).Returns(orders);

            mockOrderRepo.Setup(repo => repo.Sort()).Returns(() =>
            {
                return orders.OrderBy(o => o.OrderStatus).ToList();
            });

             mockOrderRepo.Setup(repo => repo.GetAllOrders(It.IsAny<Predicate<Order>>())).Returns((Predicate<Order> p) =>
            {
                return orders.FindAll(p).ToList();
            }); 
            
            mockOrderRepo.Setup(repo => repo.Find(It.IsAny<Predicate<Order>>())).Returns((Predicate<Order> p) =>
            {
                return orders.Find(p);
            });

            mockOrderRepo.Setup(repo => repo.Add(It.IsAny<Order>())).Callback((Order o) =>
            {
                o.OrderId = 50;
                for (int i = 0; i < o.OrderItems.Count; i++)
                {
                    o.OrderItems[i].OrderItemId = i+20;
                }
                orders.Add(o);
            }).Verifiable();


            return mockOrderRepo;
        }
        public static Mock<IUnitOfWork> GetMockUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(uow => uow.RepostiryCustomer).Returns(GetMockCustomerRepository().Object);
            mockUnitOfWork.Setup(uow => uow.RepositoryBook).Returns(GetMockBookRepository().Object);
            mockUnitOfWork.Setup(uow => uow.RepositoryOrder).Returns(GetMockOrderRepository().Object);

            mockUnitOfWork.Setup(uow => uow.Commit()).Verifiable();
            
            return mockUnitOfWork;
        }
        private static List<Book> Books()
        {
            List<Book> books = new List<Book>()
            {
                new Book()
                {
                    BookId=1,
                    Title="Title 1",
                    Price=10,
                    Supplies=10,
                    Image="img1",
                    Description="dasdasda",
                    Autors=new List<Autor>()
                    {
                        new Autor(){LastName="Marko",FirstName="Markovic"}
                    }
                },
                new Book()
                {
                    BookId=2,
                    Title="Title 2",
                    Price=15,
                    Supplies=10,
                    Image="img2",
                    Description="dasdasda",
                     Autors=new List<Autor>()
                    {
                        new Autor(){LastName="Marko",FirstName="Markovic"}
                    },
                },
                new Book()
                {
                    BookId=3,
                    Title="Title 3",
                    Price=11,
                    Supplies=32,
                    Image="img3s",
                    Description="dasdasda",
                     Autors=new List<Autor>()
                    {
                        new Autor(){LastName="Marko",FirstName="Markovic"}
                    }
                }

            };

            return books;
        }
    }
}
