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
    
    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            this.Warranties = new HashSet<Warranty>();
        }
    
        public long ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public Nullable<long> RoleID { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Image { get; set; }
    
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warranty> Warranties { get; set; }
    }
}