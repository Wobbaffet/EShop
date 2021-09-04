using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent Address class
    ///  </summary>
    /// <remarks>
    /// Address class is merged in database with Customer class
    /// <para>This is 1 to 1 relation</para>
    /// </remarks>

    public class Address
    {
        private string streetName;

        /// <value>Represent street name  as string</value>
        /// <exception cref="NullReferenceException">

        public string StreetName
        {
            get { return streetName; }
            set {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Street name cannot be empty or null");

                streetName = value; 
            }
        }

        private string streetNumber;

        /// <value>Represent street number  as string</value>
        /// <exception cref="NullReferenceException">

        public string StreetNumber
        {
            get { return streetNumber; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Street number cannot be empty or null");

                streetNumber = value;
            }
        }


        private string cityName;

        /// <value>Represent city name as string</value>
        /// <exception cref="NullReferenceException">

        public string CityName
        {
            get { return cityName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("City name cannot be empty or null");

                cityName = value;
            }
        }

        private int ptt;

        /// <value>Represent ptt as int</value>
        /// <exception cref="NullReferenceException">
        public int PTT
        {
            get { return ptt; }
            set
            {
                if (value==0 || value<0)
                    throw new NullReferenceException("Ptt cannot be empty,zero or negative value");

                ptt = value;
            }
        }

       
    }
}
