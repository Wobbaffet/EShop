using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ModelTests
{
    [TestClass]
  public class  AddressTest
    {
        private Address address;

        [TestInitialize]
        public void Initialize()
        {
            address = new Address();
        }

        [TestMethod]

        public void Test_AddressStreetNameException()
        {
            Assert.ThrowsException<NullReferenceException>(() =>address.StreetName= "");
            Assert.ThrowsException<NullReferenceException>(() =>address.StreetName= null);
        }

        [TestMethod]
        public void Test_AddressStreetNumberException()
        {
            Assert.ThrowsException<NullReferenceException>(() => address.StreetNumber = "");
            Assert.ThrowsException<NullReferenceException>(() => address.StreetNumber = null);
        }

        [TestMethod]
        public void Test_AddressCityNameException()
        {
            Assert.ThrowsException<NullReferenceException>(() => address.CityName = "");
            Assert.ThrowsException<NullReferenceException>(() => address.CityName = null);
        }
        [TestMethod]
        public void Test_AddressPTTException()
        {
            Assert.ThrowsException<NullReferenceException>(() => address.PTT =0);
            Assert.ThrowsException<NullReferenceException>(() => address.PTT =-2);
        }




    }
}
