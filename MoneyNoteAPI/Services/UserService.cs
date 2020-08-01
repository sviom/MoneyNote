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
        public User SignUp(User user)
        {
            try
            {
                var result = SqlLauncher.Insert(user);
                if (result == null) return null;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUser(User user)
        {
            var returnValue = false;
            try
            {
                if (CheckExist(user, x => x.Id == user.Id))
                {
                    var deleteResult = SqlLauncher.Delete(user);
                    returnValue = deleteResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public bool CheckExist(User user, Expression<Func<User, bool>> expression)
        {
            if (user == null)
                return false;

            try
            {
                //using var context = new MoneyContext();
                var userCount = SqlLauncher.Count(expression); //context.Users.Where(expression).Count();
                return userCount == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (User user, bool result, bool isApproved) LogIn(User user)
        {
            try
            {
                //using var context = new MoneyContext();
                var userResult = SqlLauncher.Get<User>(x => x.Email == user.Email && x.Password == user.Password);
                // context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (userResult != null)
                    return (userResult, true, userResult.IsApproved);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (null, false, false);
        }

        public List<User> GetUserList(bool isApproved = false)
        {
            try
            {
                //using var context = new MoneyContext();
                var isNotApprovedUsers = SqlLauncher.GetAll<User>(x => x.IsApproved == isApproved);
                // context.Users.Where(x => x.IsApproved == isApproved);

                return isNotApprovedUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ApproveUser(User item)
        {
            if (item == null)
                return false;

            try
            {
                if (CheckExist(item, x => x.Id == item.Id) && !item.IsApproved)
                {
                    //using var context = new MoneyContext();
                    item.IsApproved = true;
                    var result = SqlLauncher.Update(item);
                    if (result == null) return false;

                    //context.Users.Update(item);
                    //context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }
}
