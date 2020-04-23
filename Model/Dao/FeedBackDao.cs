using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedBackDao
    {
        MobileShopDbContext db = null;

        public FeedBackDao()
        {
            db = new MobileShopDbContext();
        }

        public bool Insert(FeedBack model)
        {
            try
            {
                db.FeedBacks.Add(model);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
