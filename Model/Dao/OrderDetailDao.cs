using Model.EF;
using Model.ViewModel;
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

        public List<OrderDetailViewModel> GetOrderDetail()
        {
            var model = from a in db.OrderDetails
                        join b in db.Products
                        on a.ProductID equals b.ID
                        select new OrderDetailViewModel()
                        {
                            ProductName = b.Name,
                            OrderID = a.OrderID,
                            Quantity = a.Quantity,
                            Price = a.Price 
                        };
            return model.ToList();
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


        public bool DeleteAllByOrderId(long id = 1)
        {
            try
            {
                var orderDetails = db.OrderDetails.Where(x => x.OrderID == id).ToList();
                db.OrderDetails.RemoveRange(orderDetails);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteAllByProductId(long id = 1)
        {
            try
            {
                var orderDetails = db.OrderDetails.Where(x => x.ProductID == id).ToList();
                db.OrderDetails.RemoveRange(orderDetails);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<OrderDetail> GetOrderDetailByIdOrder(int id =1)
        {
            return db.OrderDetails.Where(x => x.OrderID == id).ToList();
        }
    }
}
