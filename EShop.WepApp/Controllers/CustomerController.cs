using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpGet]
          public ActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.Email == model.Email && c.Password == model.Password);

            if(customer is null)
            {
                ModelState.AddModelError(string.Empty, "Wrong credentials");
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            
        }


        [HttpPost]
       
        public ActionResult Create([FromForm]SignUpViewModel model)
        {
            Customer customer;
            if (model.CompanyName == null)
            {

             customer = new NaturalPerson()
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
            }
            else
            {
                customer = new LegalEntity
                {
                    Email = model.Email,
                    Password = model.Password,
                    CompanyName = model.CompanyName,
                    TIN = model.TIN,
                    PhoneNumber = model.PhoneNumber,

                    Address = new Address()
                    {

                        PTT = model.PTT,
                        CityName = model.CityName,
                        StreetName = model.StreetName,
                        StreetNumber = model.StreetNumber

                    }
                };
            }

            SendEmail(customer);
            uow.RepostiryCustomer.Add(customer);
            uow.Commit();
            return View("RegistrationVerification",model);
        }

       
       
       
        [HttpPost]
        public ActionResult Verification(long code,SignUpViewModel model)
        {

          Customer c = uow.RepostiryCustomer.Find(c => c.Email==model.Email && c.VerificationCode==code);

            if (c is null)
            {
                //neka poruka da nije dobar vcode ! 
                
                return View("RegistrationVerification",model);
            }
            c.Status = true;
            c.VerificationCode =0;
            uow.Commit();

            return SignIn();
        }

       
        public ActionResult SendCodeAgain(SignUpViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.Find(c=>c.Email==model.Email);

            SendEmail(customer);

            uow.Commit();

          
           //ovdje treba da ga obavestimo da mu je poslat code ! 

            return View("RegistrationVerification",model);
        }


        private void SendEmail(Customer customer) {

            Random generateCode = new Random();
            customer.VerificationCode = generateCode.Next(1000, 10000);
            SmtpClient smtp = new SmtpClient();

            //izbrisati ovo
            Console.WriteLine(customer.VerificationCode);

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("dragojlo406@gmail.com","pitajbabu406.");

            MailMessage message = new MailMessage();

            message.Subject = "Activation code to Verify Email Address";
            message.Body = $"Dear user, Your Activation Code is {customer.VerificationCode}";


            message.To.Add(customer.Email);
            message.From = new MailAddress("dragojlo406@gmail.com");

            try
            {
                smtp.Send(message);

            }
            catch (Exception )
            {

                throw;
            }


        }

    }
}
