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

        public int Login(string email, string pass)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.Status == false)
                {
                    return 1;
                }
                else
                {
                    if(result.Password != pass)
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
            }

        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return db.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
        }

        public string ResetPassword(string email)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == email);
            return user == null ? "" : user.Password;
        }

        public bool ChangePass(string email, string pass)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.Email == email);
                user.Password = pass;
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
