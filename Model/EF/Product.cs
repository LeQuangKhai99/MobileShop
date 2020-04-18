namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
    }
}
