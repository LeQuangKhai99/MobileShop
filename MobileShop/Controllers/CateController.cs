using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class CateController : Controller
    {
        public ActionResult Type(string metatitle, int id = -1, int page = 1, int pageSize = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }
            var cate = new CateDao();
            var cateDetail = cate.GetCateByIdAndMetatitle(metatitle, id);
            if(cateDetail == null)
            {
                return Redirect("/");
            }
            ViewBag.cate = cateDetail.Name;

            string url = "/san-pham/" + cateDetail.MetaTitle + "-" + cateDetail.ID;
            ViewBag.url = url;

            int total = 0;
            ViewBag.page = page;

            var product = new ProductDao().GetListProductByCateID(id, ref total, page, pageSize);
            ViewBag.totalPage = (int)Math.Ceiling(((double)total / (double)pageSize));
            return View(product);
        }
    }
}