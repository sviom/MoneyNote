using MoneyNoteAPI.Controllers;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary5.Models;
using MoneyNoteUnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xunit;

namespace MoneyNoteUnitTest.ServiceTest
{
    public class UserServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public TestHelper Helper = new TestHelper();

        public UserServiceTest(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void SignUpUser()
        {
            var email = $"{Guid.NewGuid()}@raincome.net";
            var password = Guid.NewGuid().ToString();

            var newUser = new User()
            {
                Email = email,
                Password = password
            };

            var userService = new UserService();

            var signUpedUser = userService.SignUp(newUser);

            Assert.NotNull(signUpedUser);
            Assert.Equal(email, signUpedUser.Email);
            Assert.Equal(password, signUpedUser.Password);
            Assert.False(signUpedUser.IsApproved);
        }

        [Fact]
        public void ApproveUser()
        {
            var testUser = Helper.CreateTestAccount();
            var userService = new UserService();

            var approveResult = userService.ApproveUser(testUser);

            Assert.True(approveResult);
            Assert.True(testUser.IsApproved);
        }

        [Fact]
        public void LogInWithNotApproved()
        {
            var testUser = Helper.CreateTestAccount();
            var userService = new UserService();

            (var user, var result, var isApproved) = userService.LogIn(testUser);

            Assert.True(result);
            Assert.False(isApproved);
            Assert.NotNull(user);
        }

        [Fact]
        public void LogInWithApproved()
        {
            var testUser = Helper.CreateTestAccount();
            var userService = new UserService();

            var approveResult = userService.ApproveUser(testUser);

            (var user, var result, var isApproved) = userService.LogIn(testUser);

            Assert.True(result);
            Assert.True(isApproved);
            Assert.NotNull(user);
        }

        [Fact]
        public void DeleteUser()
        {
            (var testAccount, var bankbook, var category) = Helper.CreateSeed();

            var userService = new UserService();

            var deleteResult = userService.DeleteUser(testAccount);

            Assert.True(deleteResult);
            var context = Fixture.CreateContext();

            var userResult = context.Users.Where(x => x.Id == testAccount.Id).ToList();
            var moneyResult = context.MoneyItems.Where(x => x.UserId == testAccount.Id).ToList();
            var bankbookResult = context.BankBooks.Where(x => x.UserId == testAccount.Id).ToList();
            var categoryResult = context.MainCategories.Where(x => x.UserId == testAccount.Id).ToList();

            Assert.Empty(userResult);
            Assert.Empty(moneyResult);
            Assert.Empty(bankbookResult);
            Assert.Empty(categoryResult);
        }
    }
}
