using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Hãy nhập email!")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu!")]
        public string Password { set; get; }
    }
}