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

    /// <inheritdoc/>
    /// <summary>
    /// Represent class for business logic with Customers
    /// </summary>
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Constructor that initialize UnitOfWork
        /// </summary>
        public CustomerService()
        {
            uow = new EShopUnitOfWork(new ShopContext());
        }

        public IUnitOfWork uow { get; set; }

        public Customer Find(SignInViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.Email == model.Email && c.Password == model.Password);

            if (customer is null)
                throw new CustomerNullException("Wrong credentials");

            return customer;
        }
        public void Add(SignUpViewModel model)
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

            Random generateCode = new Random();
            customer.VerificationCode = generateCode.Next(1000, 10000);
            SendEmail(customer.Email, "Activation code", $"Dear user, Your Activation Code is {customer.VerificationCode}");
            uow.RepostiryCustomer.Add(customer);
            uow.Commit();


        }
        public UpdateCustomerViewModel Get(int? customerId)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.CustomerId == customerId);

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
            return model;

        }
        public void Update(UpdateCustomerViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.CustomerId == model.CustomerId);
            if (customer is null)
                throw new CustomerNullException("Customer doesn't exist! ");
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
        }
        public void ChangePassword(ForgotPasswordViewModel model)
        {
            Customer c = uow.RepostiryCustomer.Find(c => c.Email == model.Email);
            c.Password = model.Password;
            uow.Commit();
        }
        public void ResetPasswordLinkSend(string email,string url)
        {
            Random r = new Random();
            Customer c = uow.RepostiryCustomer.Find(c => c.Email == email);
            if (c is null)
                throw new CustomerNullException("Customer doesn't exist");
           
            SendEmail(email, "Reset password link", $"Reset password link: {url}");
        }
        public long SendCodeAgain(string email)
        {
            Customer customer = uow.RepostiryCustomer.Find(c => c.Email == email);
            Random generateCode = new Random();
            customer.VerificationCode = generateCode.Next(1000, 10000);
            SendEmail(customer.Email, "Activation code", $"Dear user, Your Activation Code is {customer.VerificationCode}");
            uow.Commit();
            return customer.VerificationCode;
        }
        public bool CheckCode(long code,string email)
        {
            Customer c = uow.RepostiryCustomer.Find(c => c.Email == email);

            if (c.VerificationCode == code)
            {
                c.Status = true;
                c.VerificationCode = 1;
                uow.Commit();
                return true;


            }
            else
                return false;
        }

        /// <summary>
        /// Sending email to customer
        /// </summary>
        /// <param name="email">Represent customer email as string</param>
        /// <param name="messageSubject">Represent email Subject as string</param>
        /// <param name="messageBody">Represent message body for email as string</param>
        private void SendEmail(string email,string messageSubject,string messageBody)
        {
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("dragojlo406@gmail.com", "pitajbabu406.");

            MailMessage message = new MailMessage();

            message.Subject = messageSubject;
            message.Body = messageBody;


            message.To.Add(email);
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

