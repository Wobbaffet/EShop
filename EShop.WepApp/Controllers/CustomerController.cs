using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using EShop.WepApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using BusinessLogic.Exceptions;
using BusinessLogic.Classes;
using BusinessLogic.Models;

namespace EShop.WepApp.Controllers
{
    [LoggedUserFillter]
    [ForbiddenForAdminFillter]
    [AddToCartFillter]
    public class CustomerController : Controller
    {

        private IUnitOfWork uow;
        private CustomerService service;

        public EShopServices Services { get; }

        public CustomerController(IUnitOfWork uow, EShopServices services)
        {
            this.uow = uow;
            Services = services;
            service = new CustomerService();
        }

        [ForbiddenForLoggedUserFillter]
        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        [ForbiddenForLoggedUserFillter]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View("SignIn");
        }

        #region AddedBuisnessLogic

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {

            try
            {
              
                Customer customer = service.Find(model);


                if (!customer.IsAdmin)
                {
                    HttpContext.Session.SetInt32("customerId", customer.CustomerId);
                    if (customer is LegalEntity)
                        HttpContext.Session.SetString("companyName", ((LegalEntity)customer).CompanyName);
                    else if (customer is NaturalPerson)
                        HttpContext.Session.SetString("customerName", ((NaturalPerson)customer).FirstName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetInt32("adminId", customer.CustomerId);
                    return RedirectToAction("Index", "Admin");
                }

            }
            catch (SignInException se)
            {

                ModelState.AddModelError(string.Empty,se.Message);
                return View();
            }


        }


        [HttpPost]
        public ActionResult SignUp([FromForm] SignUpViewModel model)
        {

            if (ModelState.ErrorCount > 1 || (ModelState.ErrorCount == 1 && model.TIN != 0))
            {

                ModelState.AddModelError(string.Empty, "Email already exist");
                return SignUp();
            }
            service.Add(model);

            var redirectUrl = Url.Action("Create", "Customer", new { email = model.Email }, Request.Scheme);
            return Redirect(redirectUrl);
        }
        #endregion

        public ActionResult SignOut()
        {
            HttpContext.Session.Clear();
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
            Customer c = uow.RepostiryCustomer.FindWithoutInclude(c => c.Email == email);
            if (c is null)
                return ForgotPassword();
            string token = r.Next(10000, 100000).ToString();
            var passwordResetLink = Url.Action("ResetPassword", "Customer", new { Email = c.Email, Token = token }, Request.Scheme);
            SendEmail2(c, passwordResetLink);
            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public ActionResult ResetPassword(string Email, string Token)
        {
            if (Email is null || Token is null)
                return NotFound();

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
            Customer c = uow.RepostiryCustomer.FindWithoutInclude(c => c.Email == model.Email);
            c.Password = model.Password;
            uow.Commit();
            return View("SignIn");
        }

     

        public ActionResult Update()
        {
            int? id = HttpContext.Session.GetInt32("customerId");

            Customer customer = uow.RepostiryCustomer.FindWithoutInclude(c => c.CustomerId == id);

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
                    CustomerId = np.CustomerId
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
                    CustomerId = np.CustomerId
                };
            }

            return View("Update", model);
        }

        [HttpPost]
        public ActionResult Update(UpdateCustomerViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.FindWithoutInclude(c => c.CustomerId == model.CustomerId);
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

        public ActionResult Verification(long code, string email)
        {
            if (code == 0 || email == null)
                return NotFound();
            Customer c = uow.RepostiryCustomer.FindWithoutInclude(c => c.Email == email);

            if (c.VerificationCode == code)
            {
                c.Status = true;
                c.VerificationCode = 1;
                uow.Commit();
                return Json(new { redirectUrl = Url.Action("SignIn", "Customer") });

            }
            else
            {

                return Json(new { redirectUrl = Url.Action("Create", "Customer", new { email = email }) });
            }
        }

        public ActionResult Create(string email)
        {
            return View("RegistrationVerification2", email);
        }

        [HttpPost]
        public void SendCodeAgain(string email)
        {

            service.SendCodeAgain(email);
           
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
