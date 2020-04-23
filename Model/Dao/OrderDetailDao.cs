using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        MobileShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new MobileShopDbContext();
        }


        public bool Insert(OrderDetail model)
        {
            try
            {
                db.OrderDetails.Add(model);
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
