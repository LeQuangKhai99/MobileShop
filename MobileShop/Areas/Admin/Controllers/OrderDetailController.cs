using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: Admin/OrderDetail
        public ActionResult Index()
        {
            var orderDetaiDao = new OrderDetailDao();
            var orderDetail = orderDetaiDao.GetOrderDetail();
            return View(orderDetail);
        }
    }
}