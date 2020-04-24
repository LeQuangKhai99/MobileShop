using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class FeedBackController : Controller
    {
        // GET: Admin/FeedBack
        public ActionResult Index()
        {
            var dao = new FeedBackDao();
            var model = dao.GetListFeedBack();
            return View(model);
        }

        public ActionResult Delete(int id =1)
        {
            var dao = new FeedBackDao();
            if (dao.Delete(id))
            {
                TempData["Success"] = "Xóa feedback hoàn tất";
            }
            else
            {
                TempData["Error"] = "Xóa feedback thất bại";
            }
            return RedirectToAction("Index");
        }
    }
}