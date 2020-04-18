using MobileShop.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class CateController : Controller
    {
        // GET: Admin/Cate
        public ActionResult Index()
        {
            List<Category> list = new List<Category>();
            var dao = new CateDao();
            list = dao.GetListCate();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cate)
        {
            var dao = new CateDao();
            cate.CreatedDate = DateTime.Now;
            cate.MetaTitle = Slug.ConvertToUnSign(cate.Name.ToLower());
            bool isIsset = dao.CheckIssetCate(cate.Name);

            if (ModelState.IsValid)
            {
                if (isIsset)
                {
                    ModelState.AddModelError("", "Loại sản phẩm này đã tồn tại");
                }
                else
                {
                    bool result = dao.Insert(cate);
                    if (result)
                    {
                        TempData["Success"] = "Thêm loại sản phẩm thành công!";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        TempData["Error"] = "Thêm loại sản phẩm thất bại!";
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Update(long id)
        {
            var dao = new CateDao();
            Category cate = dao.GetCategoryById(id);
            return View(cate);
        }

        [HttpPost]
        public ActionResult Update(Category cate)
        {
            if (ModelState.IsValid)
            {
                var dao = new CateDao();
                bool isChagne = dao.CheckChange(cate.ID, cate.Name);
                if (isChagne != true)
                {
                    bool isIsset = dao.CheckIssetCate(cate.Name);
                    if (isIsset)
                    {
                        ModelState.AddModelError("", "Loại sản phẩm này đã tồn tại");
                    }
                    else
                    {
                        bool result = dao.Update(cate);
                        if (result)
                        {
                            TempData["Success"] = "Cập nhật loại sản phẩm thành công!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Error"] = "Cập nhật loại sản phẩm thất bại!";
                        }
                    }
                }
                else
                {
                    cate.MetaTitle = Slug.ConvertToUnSign(cate.Name);
                    bool result = dao.Update(cate);
                    if (result)
                    {
                        TempData["Success"] = "Cập nhật loại sản phẩm thành công!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Cập nhật loại sản phẩm thất bại!";
                    }
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            new CateDao().Delete(id);
            TempData["Success"] = "Xóa loại sản phẩm thành công!";
            return RedirectToAction("Index");
            

        }
    }
}