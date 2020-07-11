namespace MODEL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            News = new HashSet<News>();
        }

        public int AccountId { get; set; }

        [StringLength(30)]
        public string Fullname { get; set; }

        [StringLength(12)]
        public string PhoneNum { get; set; }

        public string Password { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public int RoleId { get; set; }

        public decimal Money { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
    }
}
