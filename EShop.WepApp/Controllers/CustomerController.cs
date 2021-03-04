using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Controllers
{
    public class CustomerController : Controller
    {
        private IUnitOfWork uow;
        public CustomerController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ActionResult SignUp()
        {
            return View("SignUp");
        }


        [HttpPost]
        public ActionResult Create(SignUpViewModel model)
        {
          
         
            NaturalPerson naturalPerson = new NaturalPerson()
            {

                Email=model.Email,
                Password=model.Password,
                FirstName=model.FirstName,
                LastName=model.LastName,
                PhoneNumber=model.PhoneNumber,

                Address=new Address()
                {

                    PTT=model.PTT,
                    CityName=model.CityName,
                    StreetName=model.StreetName,
                    StreetNumber=model.StreetNumber
                    
                }
            };

            uow.RepostiryCustomer.Add(naturalPerson);
            uow.Commit();
            return null;
        }
       
    }
}
