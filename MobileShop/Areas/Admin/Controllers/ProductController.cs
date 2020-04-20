using MobileShop.Common;
using Model.Dao;
using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            List<ProductViewModel> list = new ProductDao().ListProduct();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            DropDownCate();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase image)
        {
            DropDownCate();
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var isIsset = dao.CheckIsset(model.Name);
                if (isIsset)
                {
                    ModelState.AddModelError("", "Tên sản phẩm này đã tồn tại!");
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
                            model.Image = "/Assets/client/images/" + fileName;
                            model.MetaTitle = Slug.ConvertToUnSign(model.Name);
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
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "File hình ảnh chưa phù hợp!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Chưa chọn hình ảnh!");
                    }

                }
            }
            return View();
        }

        public void DropDownCate(long? id = null)
        {
            var dao = new CateDao();
            ViewBag.CategoryID = new SelectList(dao.GetListCate(), "ID", "Name", id);
        }

        public bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool result = new ProductDao().Delete(id);
            if (result)
            {
                TempData["Success"] = "Xóa sản phẩm thành công!";
            }
            else
            {
                TempData["Error"] = "Xóa sản phẩm thất bại!";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id = 1)
        {
            DropDownCate(id);
            Product product = new ProductDao().GetProductById(id);
            if(product == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.image = product.Image;
            return View(product);
        }

        [HttpPost]
        public ActionResult Update(Product model, HttpPostedFileBase image, int id)
        {
            DropDownCate();
            var dao = new ProductDao();
            var c = dao.GetProductById(id);
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
                        bool isntChange = dao.CheckChange(model.ID, model.Name);
                        if (isntChange)
                        {
                            // nếu tên ko thay đổi
                            string folderPath = Path.Combine(Server.MapPath("/Assets/client/images"), fileName);
                            image.SaveAs(folderPath);
                            FuncUpdate(model, fileName, dao);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // nếu tên thay đổi
                            var isIsset = dao.CheckIsset(model.Name);
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
                                FuncUpdate(model, fileName, dao);
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
                    bool isntChange = dao.CheckChange(model.ID, model.Name);
                    if (isntChange)
                    {
                        // nếu tên ko thay đổi
                        FuncUpdate(model, fileName, dao);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // nếu tên thay đổi
                        var isIsset = dao.CheckIsset(model.Name);
                        if (isIsset)
                        {
                            // nếu tên đã tồn tại
                            ModelState.AddModelError("", "Sản phẩm này đã tồn tại !");
                        }
                        else
                        {
                            // nếu tên chưa tồn tại
                            FuncUpdate(model, fileName, dao);
                            return RedirectToAction("Index");
                        }
                    }
                }

            }
            return View();
        }

        public bool FuncUpdate(Product model, string fileName, ProductDao dao)
        {
            model.Image = "/Assets/client/images/" + fileName;
            model.MetaTitle = Slug.ConvertToUnSign(model.Name);
            bool result = dao.Update(model);
            if (result)
            {
                TempData["Success"] = "Cập nhật sản phẩm thành công!";
                return true;
            }
            else
            {
                TempData["Error"] = "Cập nhật sản phẩm thất bại!";
                return false;
            }
        }
    }
}