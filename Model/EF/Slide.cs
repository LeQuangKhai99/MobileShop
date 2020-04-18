namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
    }
}
