using BusinessLogic.Models;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface that manager working with CustomerService
    /// </summary>
    public interface ICustomerService : IService
    {
        /// <summary>
        /// Finding customer by model
        /// </summary>
        /// <param name="model">Represent MVVC for SignIn</param>
        /// <returns> <c>Customer</c></returns>
        Customer Find(SignInViewModel model);

        /// <summary>
        /// Saving customer in database
        /// </summary>
        /// <param name="model">Represent MVVC for SignUp</param>
        /// <exception cref="Exceptions.CustomerNullException"
        void Add(SignUpViewModel model);

        UpdateCustomerViewModel Get(int? customerId);

        /// <summary>
        /// Update customer using model
        /// </summary>
        /// <param name="model">Represent MVVC for UpdateCustomer</param>
        void Update(UpdateCustomerViewModel model);

        /// <summary>
        /// Changing password using model
        /// </summary>
        /// <param name="model">Represent MVVC for ForgotPassword</param>
        void ChangePassword(ForgotPasswordViewModel model);

        /// <summary>
        /// Sending email link for authentication 
        /// </summary>
        /// <param name="email">Represent customer email as string</param>
        /// <param name="url">Represent link url</param>
        /// <exception cref="Exceptions.CustomerNullException"
        public void ResetPasswordLinkSend(string email, string url);

        /// <summary>
        /// When customer forget authentication code, he call system to send code again to the email
        /// </summary>
        /// <param name="email">Represent email needed to send verification code </param>
        public long SendCodeAgain(string email);

        /// <summary>
        /// Check if user enter valid code for authentication
        /// </summary>
        /// <param name="code">Code that customer enter as long</param>
        /// <param name="email">Customer email as string</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <term>True if code is correct</term>
        /// </item>
        /// <item>
        /// <term>False if code is not correct</term>
        /// </item>
        /// </list>
        /// </returns>
        public bool CheckCode(long code, string email);
    }
}