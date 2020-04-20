namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Bạn chưa nhập tên")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string Phone { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Admin")]
        public bool Level { set; get; }

        public DateTime? CreatedDate { get; set; }
    }
}
