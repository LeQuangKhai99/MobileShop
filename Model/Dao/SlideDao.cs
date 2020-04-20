using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SlideDao
    {
        MobileShopDbContext db = null;
        public SlideDao()
        {
            db = new MobileShopDbContext();
        }

        public List<Slide> GetListSlide()
        {
            return db.Slides.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool Insert(Slide slide)
        {
            try
            {
                db.Slides.Add(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public Slide GetSlideById(int id)
        {
            return db.Slides.Find(id);
        }

        public bool Update(Slide model)
        {
            try
            {
                var c = db.Slides.Find(model.ID);
                c.Link = model.Link;
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
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
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
