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

        public void DeleteUser(User user)
        {

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
    }
}
