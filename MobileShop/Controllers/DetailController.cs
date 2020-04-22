using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Detail(string metatitle,int id = 1)
        {
            var dao = new ProductDao();
            var product = dao.GetProductByIdAndMetatitle(metatitle,id);
            if(product == null)
            {
                return Redirect("/");
            }

            var cateDao = new CateDao();
            ViewBag.cate = cateDao.GetCategoryById(product.CategoryID).Name;

            ViewBag.NewProduct = dao.Get3NewProduct(3);
            ViewBag.RelateProduct = dao.GetListRelateProduct(product.ID, 5);

            ViewBag.NewProduct = dao.Get3NewProduct(3);
            return View(product);
        }
    }
}