//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TruText_Service.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderItem
    {
        public int OrderItemID { get; set; }
        public int BookID { get; set; }
        public int OrderQty { get; set; }
        public decimal Price { get; set; }
        public int OrderID { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}
