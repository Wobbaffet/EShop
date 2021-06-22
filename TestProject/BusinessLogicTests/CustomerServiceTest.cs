using Autofac.Extras.Moq;
using BusinessLogic.Classes;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestProject.BusinessLogicTests
{
    [TestClass]
    public class CustomerServiceTest
    {
        private Mock<IUnitOfWork> uow ;

        [TestInitialize]
        public void Initialize()
        {
            uow = MoqClass.Mocks.GetMockUnitOfWork();
        }
        [TestMethod]
        public void Test_FindMethod()
        {
            SignInViewModel model = new SignInViewModel()
            {
                Email = "markobabovic406@gmail.com",
                Password = "Markoni21@."
            };
            
                var service = new CustomerService(uow.Object);

                var expected = uow.Object.RepostiryCustomer.Find(c => c.Email == model.Email && c.Password == model.Password);
                var actual = service.Find(model);

                Assert.IsNotNull(actual);
                Assert.AreEqual(actual.CustomerId, expected.CustomerId);
        }
        [TestMethod]
        public void Test_FindMethod_ThrowCustomerNullException()
        {
            SignInViewModel model = new SignInViewModel();

            var service = new CustomerService(uow.Object);

            Assert.ThrowsException<CustomerNullException>(()=>service.Find(model));
        }


        //public void Test_AddNaturalPersonMethod()
        //{
        //       var servise = new CustomerService(uow.Object);
        //    SignUpViewModel model = new SignUpViewModel()
        //    {
        //        FirstName = "Stefan",
        //        LastName = "Stefanovic",
        //        Email = "acavicic@gmail.com",
        //        Password = "Stefan97@.",
        //        PhoneNumber="+381 1232131",
        //        Address = new Address()
        //        {
        //            CityName = "Belgrade",
        //            PTT = 10000,
        //            StreetName = "Bulevar umetnosti",
        //            StreetNumber = "143"
        //        }
        //    };

        //    servise.Add(model);

        //    SignInViewModel modelFind = new SignInViewModel()
        //    {
        //        Email = "acavicic@gmail.com",
        //        Password = "Stefan97@."
        //    };
        //    var expected = servise.Find(modelFind);

        //    Customer customer = new NaturalPerson()
        //    {
        //        FirstName = "Stefan",
        //        LastName = "Stefanovic",
        //        Email = "acavicic@gmail.com",
        //        Password = "Stefan97@.",
        //        PhoneNumber = "+381 1232131",
        //        Address = new Address()
        //        {
        //            CityName = "Belgrade",
        //            PTT = 10000,
        //            StreetName = "Bulevar umetnosti",
        //            StreetNumber = "143"
        //        }
        //    };
        //    uow.Object.RepostiryCustomer.Add(customer);
        //    var actual = uow.Object.RepostiryCustomer.Find(p=>p.Email==customer.Email && p.Password==customer.Password);

        //    Assert.AreEqual(expected.CustomerId, actual.CustomerId);
        //    Assert.AreEqual(expected.Email, actual.Email);
        //}
        [TestMethod]

        public void Test_GetMethod()
        {
            var service = new CustomerService(uow.Object);

            var expected = service.Get(1);
            var actual = uow.Object.RepostiryCustomer.Find(c => c.CustomerId == 1);

            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.CustomerId,actual.CustomerId);
        }

    }
}
