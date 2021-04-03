using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation.Interfaces
{
    public interface IRepositoryAutor : IRepository<Autor>
    {
        public List<Autor> GetAutorsByNames(string autors);
    }
}
