using Microsoft.EntityFrameworkCore;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyNoteAPI.Services
{
    public class UserService
    {
        public UserService()
        {
        }

        public UserService(string eee)
        {
        }

        public User SignUp(User user)
        {
            try
            {
                using var db = new MoneyContext();
                db.Entry(user).State = EntityState.Added;
                var set = db.Set<User>();
                set.Add(user);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    return user;

                //var result = SqlLauncher.Insert(user);
                //return result;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return null;
        }

        public bool DeleteUser(User user)
        {
            var returnValue = false;
            try
            {
                if (CheckExist(user, x => x.Id == user.Id))
                {
                    using var db = new MoneyContext();
                    db.Users.Remove(user);

                    db.SaveChanges();
                    returnValue = true;
                }
            }
            catch
            {
            }

            return returnValue;
        }

        public bool CheckExist(User user, Expression<Func<User, bool>> expression)
        {
            if (user == null)
                return false;

            try
            {
                using var db = new MoneyContext();
                var userCount = db.Users.Where(expression).Count();
                return userCount == 1;
            }
            catch
            {
            }
            return false;
        }

        public bool NeedApprovedUser(User user)
        {
            if (user == null)
                return false;

            try
            {
                using var db = new MoneyContext();
                var userCount = db.Users.Where(x => x.Email == user.Email && x.Password == user.Password && x.IsApproved == false).Count();
                return userCount == 1;
            }
            catch
            {
            }
            return false;
        }

        public (User, bool) LogIn(User user, Expression<Func<User, bool>> expression)
        {
            try
            {
                using var db = new MoneyContext();
                var dbSet = db.Set<User>();
                if (expression != null)
                {
                    var userResult = dbSet.Where(expression).FirstOrDefault();
                    if (userResult != null)
                        return (userResult, true);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return (null, false);
        }

        public List<User> GetUserList()
        {
            try
            {
                using var db = new MoneyContext();

                return db.Users.ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool ApproveUser(User item)
        {
            if (item == null)
                return false;

            try
            {
                if (CheckExist(item, x => x.Id == item.Id))
                {
                    using var db = new MoneyContext();
                    if (item != null && !item.IsApproved)
                    {
                        item.IsApproved = true;

                        db.Users.Update(item);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
