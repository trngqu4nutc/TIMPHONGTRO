namespace MODEL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Img")]
    public partial class Img
    {
        public int ImgId { get; set; }

        public int? NewsId { get; set; }

        [StringLength(100)]
        public string Picture { get; set; }

        public virtual News News { get; set; }
    }
}
