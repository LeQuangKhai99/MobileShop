namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About
    {
        public long ID { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
    }
}
