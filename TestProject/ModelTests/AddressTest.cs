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
        [DataRow(0)]
        [DataRow(-2)]
        public void Test_AddressPTTException(int ptt)
        {
            Assert.ThrowsException<NullReferenceException>(() => address.PTT =ptt);
        }
        [TestMethod]
        [DataRow("Vienna")]
        [DataRow("Belgrade")]
        public void Test_AddressCityName(string cityName)
        {
            address.CityName = cityName;
            Assert.AreEqual(address.CityName, cityName);
        } 
        [TestMethod]
        [DataRow("Sarajevska")]
        [DataRow("Kneza Milosa")]
        public void Test_AddressStreetName(string streetName)
        {
            address.StreetName = streetName;
            Assert.AreEqual(address.StreetName, streetName);
           
        }


    }
}
