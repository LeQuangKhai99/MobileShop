using Common;
using MobileShop.Common;
using MobileShop.Models;
using Model.Dao;
using Model.EF;
using MyShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                model.Password = Encryptor.MD5Hash(model.Password);
                var result = dao.Login(model.Email, model.Password);
                switch (result)
                {
                    case 0:
                        ModelState.AddModelError("", "Email chưa được đăng kí!");
                        break;
                    case 1:
                        ModelState.AddModelError("", "Tài khoản bị khóa!");
                        break;
                    case 2:
                        ModelState.AddModelError("", "Mật khẩu không chính xác!");
                        break;
                    case 3:
                        var user = new User();
                        user = dao.GetUserByEmailAndPassword(model.Email, model.Password);
                        Session.Add(Constant.USER_SESSION, user);
                        return Redirect("/");
                }
                
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckIssetEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email này đã đc đăng kí!");
                }
                else
                {

                    if(model.Password == model.PassConfirm)
                    {
                        var user = new User();
                        user.Name = model.Name;
                        user.Email = model.Email;
                        user.Password = Encryptor.MD5Hash(model.Password);
                        user.Phone = model.Phone;
                        user.Address = model.Address;
                        user.Status = true;
                        user.Level = false;
                        user.CreatedDate = DateTime.Now;
                        if (dao.Insert(user))
                        {
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/CreateUser.html"));

                            content = content.Replace("{{Phone}}", model.Phone);
                            content = content.Replace("{{Email}}", model.Email);
                            content = content.Replace("{{Address}}", model.Address);
                            content = content.Replace("{{Password}}", model.Password);

                            new MailHelper().SendMail(model.Email, "Tạo tài khoản thành công!", content);

                            TempData["Success"] = "tạo tài khoản thành công!";
                            return Redirect("/dang-nhap");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tạo tài khoản thất bại!");
                            ModelState.AddModelError("", "Hãy thử tạo lại!");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Xác nhận mật khẩu không khớp!");
                        return View();
                    }

                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forgot(string email)
        {
            if(email == "")
            {
                ModelState.AddModelError("", "Chưa nhập email");
            }
            else
            {
                var dao = new UserDao();
                var pass = RandomString.Text(5);
                if (dao.ChangePass(email, Encryptor.MD5Hash(pass)))
                {
                    if (dao.CheckIssetEmail(email))
                    {
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/GetPass.html"));

                        content = content.Replace("{{Password}}", pass);

                        new MailHelper().SendMail(email, "Reset mật khẩu thành công!", content);

                        TempData["Forgot"] = "Mật khẩu mới đã được gửi tới gmail của bạn!";
                        return Redirect("/dang-nhap");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email chưa được đăng kí!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Reset mật khẩu không thành công!");
                }
                
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePass(ChangePassModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var pass = Encryptor.MD5Hash(model.Pass);
                if (dao.Login(model.Email, pass) == 3)
                {
                    if(model.NewPass == model.ConfirmPass)
                    {
                        if (dao.ChangePass(model.Email, Encryptor.MD5Hash(model.NewPass)))
                        {
                            TempData["Change"] = "";
                            return Redirect("/dang-nhap");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đổi mật khẩu không thành công!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Xác nhận mật khẩu mới ko khớp!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu!");
                }
            }
            return View();
        }
    }
}