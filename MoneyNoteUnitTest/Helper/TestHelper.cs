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
        public static User CreateTestAccount()
        {
            User user;
            var email = $"{Guid.NewGuid()}@raincome.net";
            var password = Guid.NewGuid().ToString();

            var newUser = new User()
            {
                Email = email,
                Password = password
            };

            var userService = new UserService();

            user = userService.SignUp(newUser);
            return user;
        }

        public static BankBook CreateBankBook(User user, double defaultAssets = 0)
        {
            var service = new BankBookService();

            var testTitle = Guid.NewGuid().ToString();
            var newItem = new BankBook
            {
                Name = testTitle,
                User = user,
                UserId = user.Id,
                Assets = defaultAssets
            };

            var result = service.SaveBankBook(newItem);

            return result;
        }

        public static MainCategory CreateCategory(User user)
        {
            var testTitle = Guid.NewGuid().ToString();
            var service = new CategoryService();
            var newCategory = new MainCategory();
            newCategory.User = user;
            newCategory.UserId = user.Id;
            newCategory.Title = testTitle;

            var saveResult = service.SaveCategory(newCategory);

            return (MainCategory)saveResult;
        }

        public static (User user, BankBook bankbook, MainCategory category) CreateSeed(double defaultAssets = 0)
        {
            // user
            var user = CreateTestAccount();
            // bankbook
            var bankbook = CreateBankBook(user, defaultAssets);
            // category
            var category = CreateCategory(user);

            return (user, bankbook, category);
        }
    }
}
