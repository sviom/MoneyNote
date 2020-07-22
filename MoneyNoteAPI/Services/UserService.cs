using Microsoft.EntityFrameworkCore;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary;
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
        private readonly MoneyContext context;

        public UserService(MoneyContext _context) => this.context = _context;

        public User SignUp(User user)
        {
            if (context == null)
                return null;

            try
            {
                var db = context;
                db.Entry(user).State = EntityState.Added;
                var set = db.Set<User>();
                set.Add(user);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    return user;
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
                    var db = context;
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
                var userCount = context.Users.Where(expression).Count();
                return userCount == 1;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public (User user, bool result, bool isApproved) LogIn(User user)
        {
            try
            {
                var userResult = context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (userResult != null)
                    return (userResult, true, userResult.IsApproved);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return (null, false, false);
        }

        public List<User> GetUserList(bool isApproved = false)
        {
            try
            {
                var db = context;

                var isNotApprovedUsers = db.Users.Where(x => x.IsApproved == isApproved);

                return isNotApprovedUsers.ToList();
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
                    if (item != null && !item.IsApproved)
                    {
                        item.IsApproved = true;

                        context.Users.Update(item);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}
