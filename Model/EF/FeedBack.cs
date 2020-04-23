namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        public long ID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bạn chưa nhập tên!")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại!")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bạn chưa nhập email!")]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ!")]
        public string Address { get; set; }

        [StringLength(250)]
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn chưa nhập nội dung!")]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }
    }
}
