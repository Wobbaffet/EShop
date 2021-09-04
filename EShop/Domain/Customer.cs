using EShop.Model.Exceptions;
using EShop.Model.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EShop.Model.Domain
{
   
    /// <summary>
    /// Represent abstact class for Customer
    /// <para>Contains all properties both for LegalEntity and NaturalPerson</para>
    /// </summary>
    public abstract class Customer
    {
        /// <value>Represent CustomerId</value>
        public int CustomerId { get; set; }

        private string password;

        /// <value>Represent customer password as string value</value>
        /// <exception cref="NullReferenceException">
        /// <exception cref="PasswordException">
        public string Password
        {
            get { return password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Email cannot be empty or null");

                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{6,})");
                Match match = regex.Match(value);
                if (!match.Success)
                    throw new PasswordException("Wrong password format!");

                password = value;
            }
        }

        private string email;

        /// <value>Represent Customer email</value>
        /// <exception cref="NullReferenceException">
        /// <exception cref="EmailException">
        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Email cannot be empty or null");
                ///^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value);
                if (!match.Success)
                    throw new EmailException("Wrong email format!");

                email = value;
            }
        }

        private string phoneNumber;

        /// <value>Represent phone number as string</value>
        /// <exception cref="NullReferenceException">
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Phone number cannot be empty or null");

                phoneNumber = value;
            }
        }
        /// <value>Represent customer address 
        /// 
        /// <para>In database customer and address are merged into one table</para>
        /// <para>This is 1 to 1 relation</para>
        /// 
        /// </value>
        public Address Address { get; set; }
        /// <value>Represent customer orders</value>
        public List<Order> Orders { get; set; }

        private long verificationCode;

        /// <value>This is code send to user when sign up</value>
        /// <exception cref="ArgumentOutOfRangeException"
        public long VerificationCode
        {
            get { return verificationCode; }
            set {
                if (value <= 1000 || value >= 9999)
                    throw new ArgumentOutOfRangeException("Verification code must be 4 digit number");

                verificationCode = value; 
            }
        }

        /// <value>Represent customer status when sign up 
        /// <return>
        /// <list type="bullet">
        /// <item> <description>True if customer verified code</description></item>
        /// <item> <description>False if customer haven't verified code yet</description></item>
        /// </list>
        /// </return>
        /// </value>
        public bool Status { get; set; }
        /// <value>Represent if user is admin or customer</value>
        public bool IsAdmin { get; set; }
    }
}
