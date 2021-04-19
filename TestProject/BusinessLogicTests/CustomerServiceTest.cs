using BusinessLogic.Classes;
using BusinessLogic.Models;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.BusinessLogicTests
{
    [TestClass]
    public class CustomerServiceTest
    {
        CustomerService cs;
        EShopUnitOfWork uow;

        [TestInitialize]
        public void Initialize()
        {
            cs = new CustomerService();
            uow = new EShopUnitOfWork(new EShop.Model.ShopContext());
        }
        [TestMethod]
        public void Test_FindMethod()
        {
            SignInViewModel signInViewModel = new SignInViewModel()
            {
                Email = "acavicic@gmail.com",
                Password = "Aca97@"
            };

            //actual
            var customer = cs.Find(signInViewModel);

            Assert.AreEqual(customer.Email, signInViewModel.Email);

        }
        [TestMethod]
        public void Test_FindMethod_ThrowCustomerNullException()
        {
            //U bazi ovaj objekat ne postoji
            SignInViewModel signInViewModel = new SignInViewModel()
            {
                Email = "markobabovic406@gmail.comm",
                Password = "1"
            };
            Assert.ThrowsException<BusinessLogic.Exceptions.CustomerNullException>(() => cs.Find(signInViewModel));
        }

        [TestMethod]

        public void Test_AddMethod()
        {
            SignUpViewModel addCustomer = new SignUpViewModel()
            {
                Email = "acavicic@gmail.com",
                FirstName = "Aleksandar",
                LastName = "Vicic",
                Address = new Address()
                {
                    CityName = "Krusevac",
                    PTT = 100,
                    StreetName = "Gornji Katun",
                    StreetNumber = "12A"
                },
                Password = "Aca97@",
                PhoneNumber = "0655214799"
            };

            cs.Add(addCustomer);

            SignInViewModel signIn = new SignInViewModel()
            {
                Email = "acavicic@gmail.com",
                Password = "Aca97@"
            };
            var customer = cs.Find(signIn);

            Assert.AreEqual(customer.Email, addCustomer.Email);


        }


        [TestMethod]

        public void Test_GetMethod()
        {
            int? customerId = 1128;

            UpdateCustomerViewModel model = cs.Get(customerId);

            Assert.AreEqual(model.CustomerId, customerId);
        }


        [TestMethod]
        public void Test_UpdateMethod()
        {
            int customerId = 1128;

            UpdateCustomerViewModel model = new UpdateCustomerViewModel()
            {
                CustomerId = customerId,
                FirstName = "Stefan",
                LastName = "Stefanovic",
                PhoneNumber = "+38268135801",
                StreetName = "Nemanjina",
                CityName = "Beograd",
                StreetNumber = "50",
                PTT = 11000,
                Type = CustomerType.NaturalPerson
            };

            cs.Update(model);

            SignInViewModel signIn = new SignInViewModel()
            {
                Email = "acavicic@gmail.com",
                Password = "Aca97@"
            };
            NaturalPerson customer = (NaturalPerson)cs.Find(signIn);

            NaturalPerson person = new NaturalPerson()
            {
                CustomerId = customerId,
                FirstName = "Stefan",
                LastName = "Stefanovic",
                PhoneNumber = "+38268135801",
                Address = new Address()
                {
                    StreetName = "Nemanjina",
                    CityName = "Beograd",
                    StreetNumber = "50",
                    PTT = 11000,

                },
                Password = "Aca97@",
                Email = "acavicic@gmail.com",

            };


            Assert.AreEqual(customer.CustomerId, person.CustomerId);
            Assert.AreEqual(customer.FirstName, person.FirstName);
            Assert.AreEqual(customer.LastName, person.LastName);
            Assert.AreEqual(customer.PhoneNumber, person.PhoneNumber);
            Assert.AreEqual(customer.Password, person.Password);
            Assert.AreEqual(customer.Email, person.Email);
            Assert.AreEqual(customer.Address.StreetName, person.Address.StreetName);
            Assert.AreEqual(customer.Address.CityName, person.Address.CityName);
            Assert.AreEqual(customer.Address.StreetNumber, person.Address.StreetNumber);
            Assert.AreEqual(customer.Address.PTT, person.Address.PTT);


        }

        [TestMethod]

        public void Test_ChangePasswordMethod()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel()
            {
                Email = "acavicic@gmail.com",
                Password = "Aca97@"
            };

            cs.ChangePassword(model);

            var customer = uow.RepostiryCustomer.Find(c => c.Email == "acavicic@gmail.com");

            Assert.AreEqual(customer.Password, model.Password);


        }

        [TestMethod]

        public void Test_SendCodeAgainMethod()
        {
            var customer = uow.RepostiryCustomer.Find(c=>c.Email=="acavicic@gmail.com");

            long code = customer.VerificationCode;
            long newCode = cs.SendCodeAgain("acavicic@gmail.com");
            Assert.AreNotEqual(code,newCode);

        }

        [TestMethod]

        public void Test_CheckCodeMethod()
        {
            var customer = uow.RepostiryCustomer.Find(c => c.Email == "acavicic@gmail.com");

            Assert.IsTrue(cs.CheckCode(customer.VerificationCode, "acavicic@gmail.com"));

        }













    }
}
