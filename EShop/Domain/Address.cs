using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent Address class
    /// <remarks>
    /// Address class is merged in database with Customer class
    /// <para>This is 1 to 1 relation</para>
    /// </remarks>
    /// </summary>
    public class Address
    {
        /// <value>Represent street name  as string</value>
        public string StreetName { get; set; }
        /// <value>Represent street number  as string</value>

        public string StreetNumber { get; set; }
        /// <value>Represent city name as string</value>

        public string CityName { get; set; }
        /// <value>Represent ptt as int</value>

        public int PTT { get; set; }
    }
}
