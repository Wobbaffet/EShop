using BusinessLogic.Models;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
   public  interface ICustomer :IService
    {

        Customer Find(SignInViewModel model);

        void Add(SignUpViewModel model);
    }
}
