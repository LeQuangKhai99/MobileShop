using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CateDao
    {
        MobileShopDbContext db = null;

        public CateDao()
        {
            db = new MobileShopDbContext();
        }

        public List<Category> GetListCate()
        {
            return db.Categories.ToList();
        }

        public bool Insert(Category cate)
        {
            try
            {
                
                db.Categories.Add(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Category GetCategoryById(long id)
        {
            return db.Categories.Find(id);
        }

        public bool CheckIssetCate(string name)
        {
            bool result = db.Categories.Count(x => x.Name == name) > 0;
            return result;
        }

        public bool CheckChange(long id, string name)
        {
            return db.Categories.Count(x => x.Name == name && x.ID == id) > 0;
        }

        public bool Update(Category cate)
        {
            try
            {
                var c = db.Categories.Find(cate.ID);
                c.Name = cate.Name;
                c.MetaTitle = cate.MetaTitle;
                if(cate.Image == "/Assets/client/images/")
                {
                    c.Image = c.Image;
                }
                else
                {
                    c.Image = cate.Image;
                }
                c.Status = cate.Status;
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
                var cate = db.Categories.Find(id);
                db.Categories.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

