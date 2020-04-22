using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileShop.Models
{
    public class ChangePassModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string Pass { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu mới")]
        public string NewPass { get; set; }
        [Required(ErrorMessage = "Bạn chưa xác nhận mật khẩu mới")]
        public string ConfirmPass { get; set; }
    }
}