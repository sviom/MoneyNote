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

        public static BankBook CreateBankBook(MoneyContext context, User user)
        {
            var service = new BankBookService(context);

            var testTitle = Guid.NewGuid().ToString();
            var newItem = new BankBook
            {
                Name = testTitle,
                User = user,
                UserId = user.Id
            };

            var result = service.SaveBankBook(newItem);

            return result;
        }

        public static MainCategory CreateCategory(MoneyContext context, User user)
        {
            var testTitle = Guid.NewGuid().ToString();
            var service = new CategoryService(context);
            var newCategory = new MainCategory();
            newCategory.User = user;
            newCategory.UserId = user.Id;
            newCategory.Title = testTitle;

            var saveResult = service.SaveCategory(newCategory);

            return (MainCategory)saveResult;
        }

        public static (User user, BankBook bankbook, MainCategory category) CreateSeed(MoneyContext context)
        {
            // user
            var user = CreateTestAccount(context);
            // bankbook
            var bankbook = CreateBankBook(context, user);
            // category
            var category = CreateCategory(context, user);

            return (user, bankbook, category);
        }
    }
}
