//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class RentalData
    {
        public System.Guid RentalDataRef { get; set; }
        public string CustomerName { get; set; }
        public System.DateTime DateRented { get; set; }
        public System.DateTime DateReturned { get; set; }
        public decimal Cost { get; set; }
        public System.Guid CarTypeRef { get; set; }
    
        public virtual CarType CarType { get; set; }
    }
}
