using MoneyNoteAPI.Services;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyNoteUnitTest.Helper
{
    public class TestHelper
    {
        public static User CreateTestAccount(MoneyContext context)
        {
            User user;
            var email = $"{Guid.NewGuid()}@raincome.net";
            var password = Guid.NewGuid().ToString();

            var newUser = new User()
            {
                Email = email,
                Password = password
            };

            var userService = new UserService(context);

            user = userService.SignUp(newUser);
            return user;
        }
    }
}
