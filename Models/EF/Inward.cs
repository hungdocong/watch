//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Watch.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inward
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inward()
        {
            this.Inward_Detail = new HashSet<Inward_Detail>();
        }
    
        public long ID { get; set; }
        public Nullable<long> TotalQuantity { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public string RecivedInfo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inward_Detail> Inward_Detail { get; set; }
    }
}
