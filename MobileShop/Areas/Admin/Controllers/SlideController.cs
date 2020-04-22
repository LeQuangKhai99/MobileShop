using MobileShop.Common;
using MobileShop.Controllers;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index()
        {
            var slides = new SlideDao().GetListSlide();
            return View(slides);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Slide model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    var dao = new SlideDao();
                    string fileName = Path.GetFileName(image.FileName);
                    string[] tokens = fileName.Split('.');
                    if (tokens[tokens.Count() - 1] == "png" || tokens[tokens.Count() - 1] == "jpg" || tokens[tokens.Count() - 1] == "jpeg" || tokens[tokens.Count() - 1] == "gif")
                    {
                        string folderPath = Path.Combine(Server.MapPath("/Assets/client/images"), fileName);
                        image.SaveAs(folderPath);

                        model.Image = "/Assets/client/images/" + fileName;
                        model.CreatedDate = DateTime.Now;
                        var result = dao.Insert(model);
                        if (result)
                        {
                            TempData["Success"] = "Thêm sản phẩm thành công!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Error"] = "Thêm sản phẩm thất bại!";
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "File hình ảnh chưa phù hợp!");
                        return View();
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "Chưa chọn hình ảnh!");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Delete(int id)
        {
            var dao = new SlideDao();
            var result = dao.Delete(id);
            if (result)
            {
                TempData["Success"] = "Xóa Slide thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Success"] = "Xóa Slide thất bại!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Update(int id = 1)
        {
            var dao = new SlideDao();
            var slide = dao.GetSlideById(id);
            if(slide == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.image = slide.Image;
            return View(slide);
        }

        [HttpPost]
        public ActionResult Update(Slide model, HttpPostedFileBase image, int id)
        {
            var dao = new SlideDao();
            var c = dao.GetSlideById(id);
            ViewBag.image = c.Image;
            if (ModelState.IsValid)
            {
                // nếu model valid

                if (image != null)
                {
                    // nếu có thay đổi hình ảnh
                    string fileName = Path.GetFileName(image.FileName);
                    string[] tokens = fileName.Split('.');
                    if (tokens[tokens.Count() - 1] == "png" || tokens[tokens.Count() - 1] == "jpg" || tokens[tokens.Count() - 1] == "jpeg" || tokens[tokens.Count() - 1] == "gif")
                    {
                        // nếu hình ảnh hợp lệ
                        string folderPath = Path.Combine(Server.MapPath("/Assets/client/images"), fileName);
                        image.SaveAs(folderPath);
                        FuncUpdate(model, fileName, dao);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // nếu hình ảnh ko hợp lệ
                        ModelState.AddModelError("", "File hình ảnh chưa phù hợp!");
                        return View();
                    }
                }
                else
                {
                    // nếu ko thay đổi hình ảnh
                    string fileName = "";
                    FuncUpdate(model, fileName, dao);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View();
            }
        }

        public bool FuncUpdate(Slide model, string fileName, SlideDao dao)
        {
            model.Image = "/Assets/client/images/" + fileName;
            bool result = dao.Update(model);
            if (result)
            {
                TempData["Success"] = "Cập nhật slide thành công!";
                return true;
            }
            else
            {
                TempData["Error"] = "Cập nhật slide thất bại!";
                return false;
            }
        }
    }
}