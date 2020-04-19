using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        MobileShopDbContext db = null;
        public ProductDao()
        {
            db = new MobileShopDbContext();
        }

        public List<ProductViewModel> ListProduct()
        {
            var model = from a in db.Products
                        join b in db.Categories
                        on a.CategoryID equals b.ID
                        select new ProductViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            MetaTitle = a.MetaTitle,
                            CateName = b.Name,
                            CateMetaTitle = b.MetaTitle,
                            CreatedDate = a.CreatedDate,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            Status = a.Status,
                            Description = a.Description
                        };
            return model.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool Insert(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CheckIsset(string name)
        {
            return db.Products.Where(x => x.Name == name).Count() > 0;
        }

        public bool Update(Product model)
        {
            try
            {
                var c = db.Products.Find(model.ID);
                c.Price = model.Price;
                c.PromotionPrice = model.PromotionPrice;
                c.Description = model.Description;
                c.CategoryID = model.CategoryID;
                c.Name = model.Name;
                c.MetaTitle = model.MetaTitle;
                c.CreatedDate = c.CreatedDate;
                if (model.Image == "/Assets/client/images/")
                {
                    c.Image = c.Image;
                }
                else
                {
                    c.Image = model.Image;
                }
                c.Status = model.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Product GetProductById(long id)
        {
            return db.Products.Find(id);
        }

        public bool CheckChange(long id, string name)
        {
            return db.Products.Where(x => x.ID == id && x.Name == name).Count() > 0;
        }
    }
}
