using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            var dao = new OrderDao();
            var order = dao.GetListOrder();
            return View(order);
        }

        public ActionResult Delete(int id = 1)
        {
            var dao = new OrderDao();
            var orderDetailDao = new OrderDetailDao();
            orderDetailDao.DeleteAllByOrderId(id);
            if (dao.Delete(id))
            {
                TempData["Success"] = "Xóa đơn hàng hoàn tất";
            }
            else
            {
                TempData["Error"] = "Xóa đơn hàng thất bại";
            }
            return RedirectToAction("Index");
        }
    }
}