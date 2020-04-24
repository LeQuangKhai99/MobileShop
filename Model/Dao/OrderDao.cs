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

        public List<Order> GetListOrder()
        {
            return db.Orders.OrderBy(x => x.CreatedDate).ToList();
        }

        public bool Delete(int id = 1)
        {
            try
            {
                var order = db.Orders.Find(id);
                db.Orders.Remove(order);
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
