using MobileShop.Common;
using MobileShop.Models;
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

        [HttpGet]
        public ActionResult Search(string search = "", int page = 1, int pageSize = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }
            ViewBag.search = search;

            int total = 0;
            ViewBag.page = page;
            ViewBag.url = "?search=" + search + "&";
            var product = new ProductDao().GetListProductBySearch(search, ref total, page, pageSize);
            ViewBag.totalPage = (int)Math.Ceiling(((double)total / (double)pageSize));
            if(ViewBag.totalPage == 0)
            {
                ViewBag.totalPage = 1;
            }
            return View(product);
        }

        [ChildActionOnly]
        public ActionResult _Show()
        {
            var cart = (List<CartItem>)Session[Constant.CartSession];
            int quantity = 0;
            decimal total = 0;
            decimal price = 0;
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    price = item.product.PromotionPrice != null ? item.product.PromotionPrice : item.product.Price;
                    quantity += item.Quantity;
                    total += (item.Quantity * price);
                }

            }
            ViewBag.total = total;
            ViewBag.quantity = quantity;
            return View();
        }
    }

}