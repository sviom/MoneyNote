using MoneyNoteAPI.Context;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return result;
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
    }
}
