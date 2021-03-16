using EShop.Model.Domain;
using EShop.Model.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Models
{
    public class SignUpViewModel
    {


        public Address Address { get; set; }
        public long VerificationCode { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        [Unique(ErrorMessage ="Email already exist")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public int TIN { get; set; }


    }
}
