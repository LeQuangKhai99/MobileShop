using MobileShop.Common;
using MobileShop.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class CartController : CheckController
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddCart(string metatitle = "", int id = 1)
        {
            var Product = new ProductDao().GetProductByIdAndMetatitle(metatitle, id);
            if(Product == null)
            {
                return Redirect("/");
            }
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                // Nếu trong danh sách đã tồn tại sản phẩm thì tăng số lượng
                if (list.Exists(x => x.product.ID == id && x.product.MetaTitle == metatitle))
                {
                    foreach (var item in list)
                    {
                        if (item.product.ID == id)
                        {
                            item.Quantity += 1;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.product = Product;
                    item.Quantity = 1;
                    list.Add(item);
                }
                Session[CartSession] = list;

            }
            else
            {
                // Tạo mới đối tượng
                var item = new CartItem();
                item.product = Product;
                item.Quantity = 1;
                var list = new List<CartItem>();
                list.Add(item);
                // gán vào session
                Session[CartSession] = list;
            }
            TempData["Cart"] = "Thêm sản phẩm vào giỏ hàng thành công!";
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Update(FormCollection f)
        {
            string[] quantity = f.GetValues("quantity");
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        int num = Convert.ToInt32(quantity[i]);
                        if (num < 1) num = 1;
                        if (num > 10) num = 10;
                        list[i].Quantity = num;
                    }
                    catch (Exception e)
                    {
                        return Redirect("/gio-hang");
                    }
                }
                Session[CartSession] = list;
            }
            TempData["Cart"] = "Cập nhật giỏ hàng thành công!";
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Delete(int id = 1)
        {
            var cart = (List<CartItem>)Session[Constant.CartSession]; 
            if(cart != null)
            {
                var pre = cart.Count();
                cart = cart.Where(x => x.product.ID != id).ToList();
                Session[Constant.CartSession] = cart;
                var next = cart.Count();
                if(next < pre)
                {
                    TempData["Cart"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
                }
                else
                {
                    TempData["Cart"] = "Sản phẩm bạn muốn xóa không tồn tại trong giỏ hàng!";
                }
            }

            return Redirect("/gio-hang");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = (List<CartItem>)Session[Constant.CartSession];
            if(cart != null)
            {
                var orderDao = new OrderDao();
                var order = new Order();
                var user = (Model.EF.User)Session[Constant.USER_SESSION];

                order.IDCustomer = user.ID;
                order.ShipName = user.Name;
                order.ShipPhone = user.Phone;
                order.ShipAddress = user.Address;
                order.Status = false;
                order.CreatedDate = DateTime.Now;
                long orderId = orderDao.Insert(order);
                if (orderId != -1)
                {
                   foreach(var item in cart)
                    {
                        var orderDetailDao = new OrderDetailDao();
                        var orderDetail = new OrderDetail();
                        decimal price = item.product.PromotionPrice != null ? item.product.PromotionPrice : item.product.Price;

                        orderDetail.ProductID = item.product.ID;
                        orderDetail.OrderID = orderId;
                        orderDetail.Price = price;
                        orderDetail.Quantity = item.Quantity;
                        if (orderDetailDao.Insert(orderDetail) != true)
                        {
                            TempData["ErrPay"] = "";
                            return Redirect("/gio-hang");
                        } 
                    }
                }
                else
                {
                    TempData["ErrPay"] = "";
                    return Redirect("/gio-hang");
                }
                Session[Constant.CartSession] = null;
                TempData["PayOke"] = "";
                return Redirect("/");
            }
            else
            {
                TempData["NullCart"] = "";
                return Redirect("/gio-hang");
            }
            
        }

    }
}