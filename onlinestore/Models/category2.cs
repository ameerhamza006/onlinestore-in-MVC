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
    
    public partial class category2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public category2()
        {
            this.products = new HashSet<product>();
        }
    
        public int cat2_id { get; set; }
        public Nullable<int> cat2_fk_cat1 { get; set; }
        public string cat2_name { get; set; }
    
        public virtual category1 category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}