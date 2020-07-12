using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Models;
using MoneyNoteUnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static MoneyNoteLibrary.Enums.MoneyEnum;

namespace MoneyNoteUnitTest.ServiceTest
{
    public class CategoryServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public CategoryServiceTest(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void SaveMainCategory()
        {
            using var context = Fixture.CreateContext();
            var testAccount = TestHelper.CreateTestAccount(context);

            var testTitle = Guid.NewGuid().ToString();
            var service = new CategoryService(context);
            var newCategory = new MainCategory();
            newCategory.User = testAccount;
            newCategory.UserId = testAccount.Id;
            newCategory.Title = testTitle;

            var saveResult = service.SaveCategory(newCategory);

            Assert.NotNull(saveResult);
            Assert.Equal(saveResult.Title, testTitle);
            Assert.Equal(MoneyCategory.Expense, saveResult.Division);
        }

        [Fact]
        public void SaveSubCategory()
        {
            using var context = Fixture.CreateContext();
            var testAccount = TestHelper.CreateTestAccount(context);

            var testTitle = Guid.NewGuid().ToString();
            var service = new CategoryService(context);
            var newCategory = new MainCategory();
            newCategory.User = testAccount;
            newCategory.UserId = testAccount.Id;
            newCategory.Title = testTitle;

            var saveResult = service.SaveCategory(newCategory);
            Assert.NotNull(saveResult);
            var main = (MainCategory)saveResult;

            var subTestTitle = Guid.NewGuid().ToString();
            var subCategory = new SubCategory();
            subCategory.MainCategoryId = main.Id;
            subCategory.Title = subTestTitle;

            var subResult = service.SaveCategory(subCategory);
            var sub = (SubCategory)subResult;

            Assert.Equal(sub.Title, subTestTitle);
            Assert.Equal(MoneyCategory.Expense, sub.Division);
            Assert.Equal(sub.MainCategoryId, main.Id);
            
        }
    }
}
