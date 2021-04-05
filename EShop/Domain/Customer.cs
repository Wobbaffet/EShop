using EShop.Model.Validation;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// <value>Represent customer password as string value</value>
        public string Password { get; set; }
        /// <value>Represent Customer email</value>
        public string Email { get; set; }
        /// <value>Represent phone number as string</value>
        public string PhoneNumber { get; set; }
        /// <value>Represent customer address 
        /// 
        /// <para>In database customer and address are merged into one table</para>
        /// <para>This is 1 to 1 relation</para>
        /// 
        /// </value>
        public Address Address { get; set; }
        /// <value>Represent customer orders</value>
        public List<Order> Orders { get; set; }
        /// <value>This is code send to user when sign up</value>
        public long VerificationCode{ get; set;}
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
