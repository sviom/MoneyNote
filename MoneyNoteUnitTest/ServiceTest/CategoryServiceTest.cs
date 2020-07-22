using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Models;
using MoneyNoteUnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void UpdateCategory()
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

            var updateCategory = (MainCategory)saveResult;
            var newTitle = Guid.NewGuid().ToString();
            updateCategory.Title = newTitle;

            var updateResult = service.UpdateCategory(updateCategory);

            Assert.NotNull(updateResult);
            Assert.Equal(updateResult.Title, newTitle);
            Assert.True(updateResult is MainCategory);
        }

        [Fact]
        public void DeleteCategory()
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
            Assert.True(saveResult is MainCategory);

            var item = (MainCategory)saveResult;
            var deleteResult = service.DeleteCategory((MainCategory)saveResult);

            var getItem = context.MainCategories.Where(x => x.Id == item.Id).FirstOrDefault();

            Assert.True(deleteResult);
            Assert.Null(getItem);
        }

        [Fact]
        public void DeleteCategoryWithSubCategory()
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
            var item = (MainCategory)saveResult;

            var subIdList = new List<Guid>();
            for (int i = 0; i < 3; i++)
            {
                var subTestTitle = Guid.NewGuid().ToString();
                var subCategory = new SubCategory();
                subCategory.MainCategoryId = item.Id;
                subCategory.Title = subTestTitle;
                var subResult = service.SaveCategory(subCategory) as SubCategory;
                subIdList.Add(subResult.Id);
            }

            var deleteResult = service.DeleteCategory(item);

            var getItem = context.MainCategories.Where(x => x.Id == item.Id).FirstOrDefault();

            Assert.True(deleteResult);
            Assert.Null(getItem);

            for (int i = 0; i < subIdList.Count; i++)
            {
                var subItem = context.SubCategories.Where(x => x.Id == subIdList[i]).FirstOrDefault();
                Assert.Null(subItem);
            }
        }
    }
}
