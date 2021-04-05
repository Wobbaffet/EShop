using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent enum for Order status
    ///  </summary>
    /// <remarks>
    /// Contains status:
    /// 
    /// <list type="bullet">
    /// <item>
    /// <term>Uncompleted</term>
    /// </item>
    ///  <item>
    /// <term>InProgress</term>
    /// </item>
    ///  <item>
    /// <term>Shipped</term>
    /// </item>  
    /// <item>
    /// <term>Completed</term>
    /// </item>
    /// 
    /// </list>
    /// 
    /// </remarks>

    public enum OrderStatus
    {
       
        
        Uncompleted,
        InProgress,
        Shipped,
        Completed
    }
}
