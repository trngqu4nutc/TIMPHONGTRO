namespace MODEL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            Imgs = new HashSet<Img>();
        }

        public int NewsId { get; set; }

        public int AccountId { get; set; }

        public int CategoryId { get; set; }

        public int ProvincialId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public int StreetId { get; set; }

        [StringLength(50)]
        public string HomeNum { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        [StringLength(500)]
        public string SortContent { get; set; }

        public decimal Price { get; set; }

        [StringLength(20)]
        public string Area { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ActiveFlag { get; set; }

        public virtual Account Account { get; set; }

        public virtual Category Category { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Img> Imgs { get; set; }

        public virtual Provincial Provincial { get; set; }

        public virtual Street Street { get; set; }

        public virtual Ward Ward { get; set; }
    }
}
