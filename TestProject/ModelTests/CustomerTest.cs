using EShop.Model.Domain;
using EShop.Model.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


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
        [DataRow(2)]
        [DataRow(23)]
        [DataRow(231)]
        [DataRow(23123)]
        public void Test_VerificationCodeException(long verificationCode)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => customer.VerificationCode = verificationCode);
        }
        [TestMethod]
        [DataRow("marko@gmail")]
        [DataRow("marko@gmail.c")]
        [DataRow("marko")]
        public void Test_EmailExcepion(string email)
        {
            Assert.ThrowsException<EmailException>(() => customer.Email = email);
        }
        [TestMethod]
        [DataRow("Ana3@")]
        [DataRow("Ana32121")]
        [DataRow("12312313qaas@")]
        [DataRow("MARKO12312@@")]
        public void Test_PasswordExcepion(string password)
        {
            Assert.ThrowsException<PasswordException>(() => customer.Password = password);
        }

        [TestMethod]
        [DataRow("markobabovic406@gmail.com")]
        [DataRow("markobabovic406@student.fon.bg.ac.rs")]
        public void Test_EmailRegex(string email)
        {
            customer.Email = email;
            Assert.AreEqual(customer.Email, email);
        }
        
        [TestMethod]
        [DataRow("Nikola97@.")]
        [DataRow("Nikola97!.")]
        public void Test_PasswordRegex(string password)
        {
            customer.Password = password;
            Assert.AreEqual(customer.Password, password);
        }

    }
}
