using MobileShop.Common;
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
        public ActionResult Create(Category cate, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                var dao = new CateDao();
                cate.CreatedDate = DateTime.Now;
                cate.MetaTitle = Slug.ConvertToUnSign(cate.Name.ToLower());
                bool isIsset = dao.CheckIssetCate(cate.Name);
                if (isIsset)
                {
                    ModelState.AddModelError("", "Loại sản phẩm này đã tồn tại");
                    return View();
                }
                else
                {
                    if (image != null)
                    {
                        string fileName = Path.GetFileName(image.FileName);
                        string[] tokens = fileName.Split('.');
                        if (tokens[tokens.Count() - 1] == "png" || tokens[tokens.Count() - 1] == "jpg" || tokens[tokens.Count() - 1] == "jpeg" || tokens[tokens.Count() - 1] == "gif")
                        {
                            string folderPath = Path.Combine(Server.MapPath("/Assets/client/images"), fileName);
                            image.SaveAs(folderPath);
                            cate.MetaTitle = Slug.ConvertToUnSign(cate.Name);
                            cate.CreatedDate = DateTime.Now;
                            cate.Image = "/Assets/client/images/" + fileName;
                            dao.Insert(cate);
                            TempData["Success"] = "Thêm loại sản phẩm thành công!";
                            return RedirectToAction("Index");
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
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult Update(long id = 1)
        {
            var dao = new CateDao();
            Category cate = dao.GetCategoryById(id);
            if(cate == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.image = cate.Image;
            return View(cate);
        }

        [HttpPost]
        public ActionResult Update(Category cate, HttpPostedFileBase image)
        {
            var dao = new CateDao();
            var c = dao.GetCategoryById(cate.ID);
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
                        bool isntChange = dao.CheckChange(cate.ID, cate.Name);
                        if (isntChange)
                        {
                            // nếu tên ko thay đổi
                            string folderPath = Path.Combine(Server.MapPath("/Assets/client/images"), fileName);
                            image.SaveAs(folderPath);
                            FuncUpdate(cate, fileName, dao);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // nếu tên thay đổi
                            var isIsset = dao.CheckIssetCate(cate.Name);
                            if (isIsset)
                            {
                                // nếu tên đã tồn tại
                                ModelState.AddModelError("", "Loại sản phẩm này đã tồn tại !");
                            }
                            else
                            {
                                // nếu tên chưa tồn tại
                                string folderPath = Path.Combine(Server.MapPath("/Assets/client/images"), fileName);
                                image.SaveAs(folderPath);
                                FuncUpdate(cate, fileName, dao);
                                return RedirectToAction("Index");
                            }
                        }
                    }
                    else
                    {
                        // nếu hình ảnh ko hợp lệ
                        ModelState.AddModelError("", "File hình ảnh chưa phù hợp!");
                        
                    }
                }
                else
                {
                    // nếu ko thay đổi hình ảnh
                    string fileName = "";
                    bool isntChange = dao.CheckChange(cate.ID, cate.Name);
                    if (isntChange)
                    {
                        // nếu tên ko thay đổi
                        FuncUpdate(cate, fileName, dao);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // nếu tên thay đổi
                        var isIsset = dao.CheckIssetCate(cate.Name);
                        if (isIsset)
                        {
                            // nếu tên đã tồn tại
                            ModelState.AddModelError("", "Loại sản phẩm này đã tồn tại !");
                        }
                        else
                        {
                            // nếu tên chưa tồn tại
                            FuncUpdate(cate, fileName, dao);
                            return RedirectToAction("Index");
                        }
                    }
                }
                
            }
            
            return View();
        }

        public bool FuncUpdate(Category cate, string fileName, CateDao dao)
        {
            cate.Image = "/Assets/client/images/" + fileName;
            cate.MetaTitle = Slug.ConvertToUnSign(cate.Name);
            bool result = dao.Update(cate);
            if (result)
            {
                TempData["Success"] = "Cập nhật loại sản phẩm thành công!!";
                return true;
            }
            else
            {
                TempData["Error"] = "Cập nhật loại sản phẩm thất bại!";
                return false;
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool result = new CateDao().Delete(id);
            if (result)
            {
                TempData["Success"] = "Xóa loại sản phẩm thành công!";
            }
            else
            {
                TempData["Error"] = "Xóa loại sản phẩm thất bại!";
            }
            return RedirectToAction("Index");
        }
    }
}