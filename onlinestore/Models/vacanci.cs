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
    
    public partial class vacanci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vacanci()
        {
            this.jobs = new HashSet<job>();
        }
    
        public int v_id { get; set; }
        public string v_name { get; set; }
        public string v_discrip { get; set; }
        public Nullable<System.DateTime> v_last_date { get; set; }
        public string v_img { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<job> jobs { get; set; }
    }
}