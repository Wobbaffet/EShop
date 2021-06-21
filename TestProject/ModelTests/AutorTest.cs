using EShop.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ModelTests
{
    [TestClass]
   public  class AutorTest
    {

        private Autor autor;


        [TestInitialize]
        public void Initialize()
        {
            autor= new Autor();
        }


        [TestMethod]
        public void Test_AutorFirstNameException()
        {
            Assert.ThrowsException<NullReferenceException>(() => autor.FirstName = "");
            Assert.ThrowsException<NullReferenceException>(() => autor.FirstName = null);
        }

        [TestMethod]
        public void Test_AutorLastNameException()
        {
            Assert.ThrowsException<NullReferenceException>(() =>autor.LastName = "");
            Assert.ThrowsException<NullReferenceException>(() =>autor.LastName = null);
        }

    }

}
