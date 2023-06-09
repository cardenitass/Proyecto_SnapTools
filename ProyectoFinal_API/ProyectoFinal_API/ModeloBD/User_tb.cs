//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinal_API.ModeloBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_tb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_tb()
        {
            this.Cart = new HashSet<Cart>();
            this.Errors = new HashSet<Errors>();
            this.Invoice = new HashSet<Invoice>();
        }
    
        public int id_user { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool active { get; set; }
        public int id_role { get; set; }
        public string token_recovery { get; set; }
        public string identification { get; set; }
        public int id_province { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Errors> Errors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual Province Province { get; set; }
        public virtual Role Role { get; set; }
    }
}
