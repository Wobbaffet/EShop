using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{

    ///<inheritdoc/>
    ///
    /// <summary>
    ///Represent LegalEntity class
    ///<remarks>
    ///Contains properites <c>TIN</c> and <c>CompanyName</c>
    /// </remarks>
    /// </summary>
    public class LegalEntity : Customer
    {
        /// <value>Represent TIN <b>(taxpayer identification number)</b> as int</value>
        public int TIN { get; set; }
        /// <value>Represent company name as string</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Override string method
        /// </summary>
        /// <returns>string in format <c>CompanyName</c></returns>
        public override string ToString()
        {
            return $"{CompanyName}";
        }
    }
}
