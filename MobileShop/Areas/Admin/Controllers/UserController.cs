using Common;
using MobileShop.Controllers;
using Model.Dao;
using Model.EF;
using MyShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var dao = new UserDao();
            var users = dao.GetListUser();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            var dao = new UserDao();
            int check = 0;
            if (ModelState.IsValid)
            {
                if(model.Email == null)
                {
                    check = 1;
                    ModelState.AddModelError("", "Bạn chưa nhập email!");
                }

                if (model.Password == null)
                {
                    check = 1;
                    ModelState.AddModelError("", "Bạn chưa nhập mật khẩu!");
                }

                if(check == 0)
                {
                    if (dao.CheckIssetEmail(model.Email))
                    {
                        ModelState.AddModelError("", "Email đã đc đăng kí!");
                    }
                    else
                    {

                        model.CreatedDate = DateTime.Now;
                        string pass = model.Password;
                        model.Password = Encryptor.MD5Hash(model.Password);
                        if (dao.Insert(model))
                        {
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/CreateUser.html"));

                            content = content.Replace("{{Phone}}", model.Phone);
                            content = content.Replace("{{Email}}", model.Email);
                            content = content.Replace("{{Address}}", model.Address);
                            content = content.Replace("{{Password}}", pass);

                            new MailHelper().SendMail(model.Email, "Tạo tài khoản thành công!", content);

                            TempData["Success"] = "Thêm user thành công!";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            TempData["Success"] = "Thêm user thất bại!";
                            return RedirectToAction("Create");
                        }
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            if (dao.Delete(id))
            {
                TempData["Success"] = "Xóa user thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Xóa user thất bại!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Update(int id = 1)
        {
            var dao = new UserDao();
            var user = dao.GetUserByID(id);
            if(user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Update(User model)
        {
            var dao = new UserDao();
            if (ModelState.IsValid)
            {
                if (dao.Update(model))
                {
                    TempData["Success"] = "Cập nhật user thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Cập nhật user thất bại!";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}