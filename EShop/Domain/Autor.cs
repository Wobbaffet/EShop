using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent Autor class
    /// </summary>
    public class Autor
    {
        /// <value>Represent Autor id as int</value>
        public int AutorId { get; set; }

        private string firstName;

        /// <value>Represent first name as string</value>
        /// <exception cref="NullReferenceException">

        public string FirstName
        {
            get { return firstName; }
            set {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("First name cannot be empty");
   
                firstName = value;
            }
        } 
        private string lastName;

        /// <value>Represent last name as string</value>
        /// <exception cref="NullReferenceException">

        public string LastName
        {
            get { return lastName; }
            set {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Last name cannot be empty");
   
                lastName = value;
            }
        }


        /// <value>Represent all book genres
        /// <para>Between autor and book is association class  BookAutor</para>
        /// </value>

        public List<Book> Books { get; set; }

        /// <summary>
        /// Override string method
        /// </summary>
        /// <returns>string in format <c>FirstName</c> <c>LastName</c></returns>
      
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
