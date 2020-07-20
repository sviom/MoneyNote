using Microsoft.EntityFrameworkCore.Internal;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Models;
using MoneyNoteUnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MoneyNoteUnitTest.ServiceTest
{
    public class MoneyServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public MoneyServiceTest(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void SaveMoney()
        {
            using var context = Fixture.CreateContext();
            (var testAccount, var bankbook, var category) = TestHelper.CreateSeed(context);
            var service = new MoneyService(context);

            var testMoney = 100000;
            var testTitle = Guid.NewGuid().ToString();
            var newItem = new MoneyItem();
            newItem.Money = testMoney;
            newItem.Title = testTitle;
            newItem.UserId = testAccount.Id;
            newItem.User = testAccount;
            newItem.MainCategory = category;
            newItem.BankBook = bankbook;

            var savedItem = service.SaveMoney(newItem);

            var resultItem = service.GetMoney(x => x.Id == savedItem.Id);
            var resultBank = context.BankBooks.Where(x => x.Id == bankbook.Id).FirstOrDefault();

            Assert.NotNull(resultItem);
            Assert.Equal(testMoney, resultItem.Money);
            Assert.Equal(testTitle, resultItem.Title);
            Assert.Equal(-testMoney, resultBank.Assets);
        }

        [Fact]
        public void UpdateMoney()
        {
            var context = Fixture.CreateContext();
            (var testAccount, var bankbook, var category) = TestHelper.CreateSeed(context);
            var service = new MoneyService(context);

            var testMoney = 100000;
            var testTitle = Guid.NewGuid().ToString();
            var newItem = new MoneyItem();
            newItem.Money = testMoney;
            newItem.Title = testTitle;
            newItem.UserId = testAccount.Id;
            newItem.User = testAccount;
            newItem.MainCategory = category;
            newItem.BankBook = bankbook;

            var savedItem = service.SaveMoney(newItem);

            context.Dispose();
            context = Fixture.CreateContext();
            service = new MoneyService(context);

            var money = savedItem.Money;
            var newMoney = 200000;
            var updateItem = savedItem;//.ShallowCopy();
            updateItem.Money = newMoney;

            var updatedItem = service.UpdateMoney(money, updateItem);

            var resultBank = context.BankBooks.Where(x => x.Id == bankbook.Id).FirstOrDefault();

            context.Dispose();

            Assert.NotNull(updatedItem);
            Assert.Equal(newMoney, updatedItem.Money);
            Assert.Equal(testTitle, updatedItem.Title);
            Assert.Equal(-newMoney, resultBank.Assets);
        }
    }
}
