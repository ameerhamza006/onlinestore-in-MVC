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
    
    public partial class login
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public login()
        {
            this.billings = new HashSet<billing>();
            this.carts = new HashSet<cart>();
            this.comments = new HashSet<comment>();
            this.billinggs = new HashSet<billingg>();
        }
    
        public int log_id { get; set; }
        public string log_name { get; set; }
        public string log_email { get; set; }
        public string log_number { get; set; }
        public string log_city { get; set; }
        public string log_address { get; set; }
        public string log_password { get; set; }
        public string emp_img { get; set; }
        public string emp_client { get; set; }
        public string log_role { get; set; }
        public Nullable<bool> log_status { get; set; }
        public Nullable<int> fk_job { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<billing> billings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comments { get; set; }
        public virtual job job { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<billingg> billinggs { get; set; }
    }
}
