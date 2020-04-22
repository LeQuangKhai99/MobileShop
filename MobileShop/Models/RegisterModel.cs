using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileShop.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập tên!")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập email!")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        public string Password { set; get; }
        [Required(ErrorMessage = "Bạn chưa xác nhận mật khẩu")]
        public string PassConfirm { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string Address { get; set; }

    }
}