//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onlinestore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class billing
    {
        public int bil_id { get; set; }
        public Nullable<int> bil_fk_login { get; set; }
        public string bil_firstname { get; set; }
        public string bil_lastname { get; set; }
        public string bil_country { get; set; }
        public string bil_street { get; set; }
        public string bil_city { get; set; }
        public string bil_postcode { get; set; }
        public string bil_number { get; set; }
        public string bil_email { get; set; }
        public string bill_address { get; set; }
        public Nullable<System.DateTime> bil_date { get; set; }
        public Nullable<long> bil_ordernumber { get; set; }
        public string bil_payment { get; set; }
    
        public virtual login login { get; set; }
    }
}
