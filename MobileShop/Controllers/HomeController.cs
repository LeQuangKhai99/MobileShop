using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var dao = new ProductDao();
            ViewBag.products = dao.ListLatestProduct(10);
            ViewBag.promotion_product = dao.Get3PromotionProduct(3);
            ViewBag.cheap_product = dao.Get3CheapProduct(3);
            ViewBag.new_product = dao.Get3NewProduct(3);
            return View();
        }

        [ChildActionOnly]
        public ActionResult _MainMenu()
        {
            var cate = new CateDao().GetListCategoryShow();
            return View(cate);
        }

        [ChildActionOnly]
        public ActionResult _MenuTop()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _Slide()
        {
            var slide = new SlideDao().GetListSlideShow();
            return View(slide);
        }

        [ChildActionOnly]
        public ActionResult _Brand()
        {
            var cate = new CateDao().GetListCategoryShow();
            return View(cate);
        }
    }
}