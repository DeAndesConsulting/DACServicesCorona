//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DACServices.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPaymentDetail
    {
        public int pde_id { get; set; }
        public int pay_id { get; set; }
        public string pde_operacion { get; set; }
        public string pde_vendor_response_id { get; set; }
        public string pde_vendor_response_status { get; set; }
        public Nullable<System.DateTime> pde_fecha { get; set; }
    
        public virtual tbPayment tbPayment { get; set; }
        public virtual tbRequestPaymentVendor tbRequestPaymentVendor { get; set; }
    }
}