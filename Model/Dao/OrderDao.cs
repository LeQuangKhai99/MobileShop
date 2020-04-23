using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        MobileShopDbContext db = null;
        public OrderDao()
        {
            db = new MobileShopDbContext();
        }


        public long Insert(Order model)
        {
            try
            {
                db.Orders.Add(model);
                db.SaveChanges();
                return model.ID;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
    }
}
