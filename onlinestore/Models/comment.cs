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
    
    public partial class comment
    {
        public int c_id { get; set; }
        public string c_message { get; set; }
        public Nullable<System.DateTime> c_date { get; set; }
        public Nullable<int> c_fk_log { get; set; }
        public Nullable<int> c_fk_pro { get; set; }
    
        public virtual product product { get; set; }
        public virtual login login { get; set; }
    }
}