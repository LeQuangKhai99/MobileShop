using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FeedBack model)
        {
            if (ModelState.IsValid)
            {
                var dao = new FeedBackDao();
                model.CreatedDate = DateTime.Now;
                model.Status = true;
                if (dao.Insert(model))
                {
                    TempData["Success"] = "Cảm ơn bạn đã liên hệ!";
                    return Redirect("/");
                }
                else
                {
                    TempData["Error"] = "Thông tin bị lỗi, xin vui lòng gửi lại!";
                }
            }
            return View(model);
        }
    }
}