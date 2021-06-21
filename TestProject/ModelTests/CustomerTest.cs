using EShop.Model.Domain;
using EShop.Model.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ModelTests
{
    [TestClass]
    public class CustomerTest
    {
        private Customer customer;

        [TestInitialize]
        public void Initialize()
        {
            customer = new LegalEntity();
        }
        [TestMethod]
        public void Test_PasswordNullReferenceException()
        {
            Assert.ThrowsException<NullReferenceException>(() => customer.Password = "");
            Assert.ThrowsException<NullReferenceException>(() => customer.Password =null);
        }
        [TestMethod]
        public void Test_EmailNullReferenceException()
        {
            Assert.ThrowsException<NullReferenceException>(() => customer.Email = "");
            Assert.ThrowsException<NullReferenceException>(() => customer.Email=null);
        }

        [TestMethod]
        public void Test_PhoneNumberNullReferenceException()
        {
            Assert.ThrowsException<NullReferenceException>(() => customer.PhoneNumber = "");
            Assert.ThrowsException<NullReferenceException>(() => customer.PhoneNumber = null);
        }
        [TestMethod]
        public void Test_VerificationCodeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => customer.VerificationCode = 32);
        }
        [TestMethod]
        public void Test_EmailExcepion()
        {
            Assert.ThrowsException<EmailException>(() => customer.Email = "123@");
        }
        [TestMethod]
        public void Test_PasswordExcepion()
        {
            Assert.ThrowsException<PasswordException>(() => customer.Password = "Stefan.");
        }


    }
}
