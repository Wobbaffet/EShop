using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using EShop.Model.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
namespace BusinessLogic.Classes
{


    public class CustomerService : ICustomer
    {

       
        public CustomerService()
        {

            uow = new EShopUnitOfWork(new ShopContext());
        }

        public IUnitOfWork uow { get ; set ; }

        public void  Add(SignUpViewModel model)
        {

            Customer exist = uow.RepostiryCustomer.FindWithoutInclude(c => c.Email == model.Email && c.Status == false);

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
        
            
        }

        public Customer Find(SignInViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.FindWithoutInclude(c => c.Email == model.Email && c.Password == model.Password);

            if (customer is null)
                throw new SignInException("Wrong credentials");

            return customer;
        }



        public void SendCodeAgain(string email)
        {
            Customer customer = uow.RepostiryCustomer.FindWithoutInclude(c => c.Email == email);
            SendEmail(customer);
            uow.Commit();
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


    }
}

