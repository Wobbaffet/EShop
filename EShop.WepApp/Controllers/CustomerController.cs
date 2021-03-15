using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using EShop.WepApp.Models;
using EShop.WepApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

namespace EShop.WepApp.Controllers
{
    public class CustomerController : Controller
    {

        private IUnitOfWork uow;

        public EShopServices Services { get; }

        public CustomerController(IUnitOfWork uow, EShopServices services)
        {
            this.uow = uow;
            Services = services;
        }


        [LoggedInFillter]
        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }


        [LoggedInFillter]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.Email == model.Email && c.Password == model.Password);

            if (customer is null)
            {
                ModelState.AddModelError(string.Empty, "Wrong credentials");
                return View();
            }
            else
            {
                HttpContext.Session.SetInt32("customerId", customer.CustomerId);
                TempData["logged"] = true;
                TempData.Keep("logged");
                if (!customer.IsAdmin)
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index", "Admin");
            }

        }

        public ActionResult SignOut()
        {
            HttpContext.Session.Clear();
            TempData["logged"] = false;
            TempData.Keep("logged");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            Random r = new Random();
            Customer c = uow.RepostiryCustomer.Find(c => c.Email == email);
            string token = r.Next(10000, 100000).ToString();
            var passwordResetLink = Url.Action("ResetPassword", "Customer", new { Email = c.Email, Token = token }, Request.Scheme);
            SendEmail2(c, passwordResetLink);
            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public ActionResult ResetPassword(string Email, string Token)
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel
            {
                Email = Email,
                Token = Token
            };
            return View("ResetPassword", model);
        }

        [HttpPost]
        public ActionResult ResetPassword(ForgotPasswordViewModel model)
        {
            Customer c = uow.RepostiryCustomer.Find(c => c.Email == model.Email);
            c.Password = model.Password;
            uow.Commit();
            return View("SignIn");
        }

        [HttpPost]

        public ActionResult Create([FromForm] SignUpViewModel model)
        {
            Customer exist = uow.RepostiryCustomer.Find(c => c.Email == model.Email && c.Status == false);

            if (!(exist is null))
            {
                uow.RepostiryCustomer.Delete(exist);
            }

            Customer customer;
            if (model.CompanyName == null)
            {
                customer = new NaturalPerson()
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,

                    Address = new Address()
                    {

                        PTT = model.Address.PTT,
                        CityName = model.Address.CityName,
                        StreetName = model.Address.StreetName,
                        StreetNumber = model.Address.StreetNumber

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

                        PTT = model.Address.PTT,
                        CityName = model.Address.CityName,
                        StreetName = model.Address.StreetName,
                        StreetNumber = model.Address.StreetNumber
                    }
                };
            }

            SendEmail(customer);
            uow.RepostiryCustomer.Add(customer);
            uow.Commit();
            model.VerificationCode = customer.VerificationCode;
            return View("RegistrationVerification", model.Email);
        }


        public ActionResult Update()
        {
            int? id = HttpContext.Session.GetInt32("customerId");

            Customer customer = uow.RepostiryCustomer.Find(c => c.CustomerId == id);

            UpdateCustomerViewModel model;
            if (customer is NaturalPerson)
            {
                NaturalPerson np = customer as NaturalPerson;
                model = new UpdateCustomerViewModel()
                {
                    FirstName = np.FirstName,
                    LastName = np.LastName,
                    PhoneNumber = np.PhoneNumber,
                    CityName = np.Address.CityName,
                    PTT = np.Address.PTT,
                    StreetName = np.Address.StreetName,
                    StreetNumber = np.Address.StreetNumber,
                    Type = CustomerType.NaturalPerson,
                    CustomerId = np.CustomerId,
                    /*                    AddressId = np.AddressId,
                    */
                };
            }
            else
            {
                LegalEntity np = customer as LegalEntity;
                model = new UpdateCustomerViewModel()
                {
                    CompanyName = np.CompanyName,
                    TIN = np.TIN,
                    PhoneNumber = np.PhoneNumber,
                    CityName = np.Address.CityName,
                    PTT = np.Address.PTT,
                    StreetName = np.Address.StreetName,
                    StreetNumber = np.Address.StreetNumber,
                    Type = CustomerType.LegalEntity,
                    CustomerId = np.CustomerId,
                    /*AddressId = np.AddressId*/
                };
            }

            return View("Update", model);
        }

        [HttpPost]
        public ActionResult Update(UpdateCustomerViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.CustomerId == model.CustomerId);
            if (model.Type == CustomerType.NaturalPerson)
            {
                NaturalPerson np = customer as NaturalPerson;
                np.FirstName = model.FirstName;
                np.LastName = model.LastName;
                np.PhoneNumber = model.PhoneNumber;
                np.Address.CityName = model.CityName;
                np.Address.StreetName = model.StreetName;
                np.Address.PTT = model.PTT;
                np.Address.StreetNumber = model.StreetNumber;
            }
            else
            {
                LegalEntity lg = customer as LegalEntity;
                lg.CompanyName = model.CompanyName;
                lg.TIN = model.TIN;
                lg.PhoneNumber = model.PhoneNumber;
                lg.Address.CityName = model.CityName;
                lg.Address.StreetName = model.StreetName;
                lg.Address.PTT = model.PTT;
                lg.Address.StreetNumber = model.StreetNumber;
            }

            uow.Commit();

            return RedirectToAction("Index", "Book");
        }


        [HttpPost]
        public ActionResult Verification(long code, string email)
        {

            Customer c = uow.RepostiryCustomer.Find(c => c.Email == email);

            if (c.VerificationCode == code)
            {
                c.Status = true;
                c.VerificationCode = 1;
                uow.Commit();
                return SignIn();
            }
            else
            {

                return View("RegistrationVerification", email);
            }

        }


        [HttpPost]
        public void SendCodeAgain(string email)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.Email == email);

            SendEmail(customer);

            uow.Commit();


            //ovdje treba da ga obavestimo da mu je poslat code ! 

            /*return View("RegistrationVerification", model);*/
        }


        private void SendEmail(Customer customer)
        {

            Random generateCode = new Random();

            customer.VerificationCode = generateCode.Next(1000, 10000);

            SmtpClient smtp = new SmtpClient();



            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("dragojlo406@gmail.com", "pitajbabu406.");

            MailMessage message = new MailMessage();

            message.Subject = "Activation code to Verify Email Address";
            message.Body = $"Dear user, Your Activation Code is {customer.VerificationCode}";


            message.To.Add(customer.Email);
            message.From = new MailAddress("dragojlo406@gmail.com");

            try
            {
                smtp.Send(message);
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void SendEmail2(Customer customer, string url)
        {

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("dragojlo406@gmail.com", "pitajbabu406.");

            MailMessage message = new MailMessage();

            message.Subject = "Reset password link";
            message.Body = $"Reset password link: {url}";


            message.To.Add(customer.Email);
            message.From = new MailAddress("dragojlo406@gmail.com");

            try
            {
                smtp.Send(message);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
