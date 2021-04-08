using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using EShop.WepApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

       
        private CustomerService service;

        public EShopServices Services { get; }

        public CustomerController( EShopServices services)
        {
            
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
            catch (CustomerNullException se)
            {

                ModelState.AddModelError(string.Empty, se.Message);
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

        [HttpPost]
        public void SendCodeAgain(string email)
        {

            service.SendCodeAgain(email);

        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {

            try
            {
                Random r = new Random();

                string token = r.Next(10000, 100000).ToString();
                var passwordResetLink = Url.Action("ResetPassword", "Customer", new { Email = email, Token = token }, Request.Scheme);

                service.ResetPasswordLinkSend(email, passwordResetLink);
                return View("ForgotPasswordConfirmation");
            }
            catch (CustomerNullException ex)
            {

                return ForgotPassword();
            }
        }

        [HttpPost]
        public ActionResult Update(UpdateCustomerViewModel model)
        {

            service.Update(model);
            return RedirectToAction("Index", "Book");
        }


        [HttpPost]
        public ActionResult ResetPassword(ForgotPasswordViewModel model)
        {

            service.ChangePassword(model);

            return View("SignIn");
        }

        public ActionResult GetCustomer()
        {
            try
            {
                int? id = HttpContext.Session.GetInt32("customerId");


                return View("Update", service.Get(id));
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        public ActionResult CheckCode(long code, string email)
        {
            if (code == 0 || email == null)
                return NotFound();

            if (service.CheckCode(code, email))
            {

                return Json(new { redirectUrl = Url.Action("SignIn", "Customer") });

            }
            else
            {

                return Json(new { redirectUrl = Url.Action("Create", "Customer", new { email = email }) });
            }
        }


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

        public ActionResult Create(string email)
        {
            return View("SignUpVerification", email);
        }


    }
}
