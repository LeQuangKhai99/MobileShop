namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public long ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Hãy điền tiên loại sản phẩm")]
        public string Name { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        public string Image { set; get; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
    }
}
