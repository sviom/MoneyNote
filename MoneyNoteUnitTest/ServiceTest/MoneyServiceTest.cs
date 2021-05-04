using Microsoft.EntityFrameworkCore.Internal;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary5.Models;
using MoneyNoteUnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static MoneyNoteLibrary5.Enums.MoneyEnum;

namespace MoneyNoteUnitTest.ServiceTest
{
    public class MoneyServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public TestHelper Helper = new TestHelper();

        public MoneyServiceTest(SharedDatabaseFixture fixture)
        {
            Fixture = fixture;
            Helper = new TestHelper();
        }

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void SaveMoney()
        {
            var context = Fixture.CreateContext();
            (var testAccount, var bankbook, var category) = Helper.CreateSeed();
            context.Dispose();

            //(var options, var connString) = Fixture.CreateOptionsString();            
            var service = new MoneyService();

            var testMoney = 100000;
            var testTitle = Guid.NewGuid().ToString();
            var newItem = new MoneyItem();
            newItem.Money = testMoney;
            newItem.Title = testTitle;
            //newItem.UserId = testAccount.Id;
            newItem.User = testAccount;
            newItem.MainCategory = category;
            newItem.BankBook = bankbook;

            var savedItem = service.SaveMoney(newItem);

            var resultItem = service.GetMoney(x => x.Id == savedItem.Id);

            context = Fixture.CreateContext();
            var resultBank = context.BankBooks.Where(x => x.Id == bankbook.Id).FirstOrDefault();
            context.Dispose();

            Assert.NotNull(resultItem);
            Assert.Equal(testMoney, resultItem.Money);
            Assert.Equal(testTitle, resultItem.Title);
            Assert.Equal(-testMoney, resultBank.Assets);
        }

        [Theory]
        [InlineData(500, 400, 100, MoneyCategory.Expense)]
        [InlineData(500, 300, 800, MoneyCategory.Income)]
        public void UpdateMoney(double defaultAssets, double newMoney, double expectedAssets, MoneyCategory moneyCategory)
        {
            var context = Fixture.CreateContext();
            (var testAccount, var bankbook, var category) = Helper.CreateSeed(defaultAssets);
            var service = new MoneyService();

            var testMoney = 100;
            var testTitle = Guid.NewGuid().ToString();
            var newItem = new MoneyItem();
            newItem.Money = testMoney;
            newItem.Title = testTitle;
            newItem.UserId = testAccount.Id;
            newItem.User = testAccount;
            newItem.MainCategory = category;
            newItem.BankBook = bankbook;
            newItem.Division = moneyCategory;

            var savedItem = service.SaveMoney(newItem);

            context.Dispose();
            context = Fixture.CreateContext();
            service = new MoneyService();

            var money = savedItem.Money;
            //var newMoney = 200000;
            var updateItem = savedItem;//.ShallowCopy();
            updateItem.Money = newMoney;

            var updatedItem = service.UpdateMoney(money, updateItem);

            var resultBank = context.BankBooks.Where(x => x.Id == bankbook.Id).FirstOrDefault();

            context.Dispose();

            Assert.NotNull(updatedItem);
            Assert.Equal(newMoney, updatedItem.Money);
            Assert.Equal(testTitle, updatedItem.Title);
            Assert.Equal(expectedAssets, resultBank.Assets);
        }

        [Theory]
        [InlineData(500, 500, MoneyCategory.Expense)]
        [InlineData(500, 500, MoneyCategory.Income)]
        public void DeleteMoney(double defaultAssets, double expectedAssets, MoneyCategory moneyCategory)
        {
            var context = Fixture.CreateContext();
            (var testAccount, var bankbook, var category) = Helper.CreateSeed(defaultAssets);
            var service = new MoneyService();

            var testMoney = 100;
            var testTitle = Guid.NewGuid().ToString();
            var newItem = new MoneyItem();
            newItem.Money = testMoney;
            newItem.Title = testTitle;
            newItem.UserId = testAccount.Id;
            newItem.User = testAccount;
            newItem.MainCategory = category;
            newItem.BankBook = bankbook;
            newItem.Division = moneyCategory;

            var savedItem = service.SaveMoney(newItem);

            context.Dispose();
            context = Fixture.CreateContext();
            service = new MoneyService();

            var deleteItem = savedItem;//.ShallowCopy();

            var deleteResult = service.DeleteMoney(deleteItem);

            var deletedItem = context.MoneyItems.Where(x => x.Id == deleteItem.Id).FirstOrDefault();
            var resultBank = context.BankBooks.Where(x => x.Id == bankbook.Id).FirstOrDefault();

            context.Dispose();

            Assert.Null(deletedItem);
            Assert.True(deleteResult);
            Assert.Equal(expectedAssets, resultBank.Assets);
        }
    }
}
