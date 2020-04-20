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
        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên sản phẩm")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public long? CategoryID { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        [Display(Name = "Giá gốc")]
        [Required(ErrorMessage = "Hãy nhập giá gốc của sản phẩm")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [Required(ErrorMessage = "Hãy nhập giá khuyến mãi của sản phẩm")]
        public decimal? PromotionPrice { get; set; }

        [StringLength(300)]
        [Display(Name = "Mô Tả Sản Phẩm")]
        [Required(ErrorMessage = "Hãy nhập mô tả của sản phẩm")]
        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
    }
}
