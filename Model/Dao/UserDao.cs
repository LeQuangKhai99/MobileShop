using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        MobileShopDbContext db = null;
        public UserDao()
        {
            db = new MobileShopDbContext();
        }


        public List<User> GetListUser()
        {
            return db.Users.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool CheckIssetEmail(string email)
        {
            return db.Users.Where(x => x.Email == email).Count() > 0;
        }

        public bool CheckIssetPhone(string phone)
        {
            return db.Users.Where(x => x.Phone == phone).Count() > 0;
        }

        public bool Insert(User model)
        {
            try
            {
                db.Users.Add(model);
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
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public User GetUserByID(int id)
        {
            return db.Users.Find(id);
        }

        public bool IsChangeEmail(long id,string email)
        {
            return db.Users.Where(x => x.ID == id && x.Email == email).Count() <= 0;
        }

        public bool Update(User model)
        {
            
            try
            {
                var user = db.Users.Find(model.ID);
                user.Name = model.Name;
                user.Phone = model.Phone;
                user.Status = model.Status;
                user.Level = model.Level;
                user.Address = model.Address;
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
