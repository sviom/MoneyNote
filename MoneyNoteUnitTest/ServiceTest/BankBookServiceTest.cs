using Microsoft.CodeAnalysis.CSharp.Syntax;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary5.Models;
using MoneyNoteUnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace MoneyNoteUnitTest.ServiceTest
{
    public class BankBookServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public TestHelper Helper = new TestHelper();

        public BankBookServiceTest(SharedDatabaseFixture fixture)
        {
            Fixture = fixture;
        }

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void SaveBankBook()
        {
            using var context = Fixture.CreateContext();
            var testAccount = Helper.CreateTestAccount();

            var service = new BankBookService();

            var testTitle = Guid.NewGuid().ToString();
            var newItem = new BankBook
            {
                Name = testTitle,
                User = testAccount,
                UserId = testAccount.Id
            };

            var result = service.SaveBankBook(newItem);

            var getResult = context.BankBooks.Where(x => x.Name == testTitle).FirstOrDefault();

            Assert.NotNull(getResult);
            Assert.Equal(result.Name, getResult.Name);
            Assert.Equal(result.Id, getResult.Id);
        }

        [Fact]
        public void DeleteBankBook()
        {
            using var context = Fixture.CreateContext();
            var testAccount = Helper.CreateTestAccount();

            var service = new BankBookService();

            var testTitle = Guid.NewGuid().ToString();
            var newItem = new BankBook
            {
                Name = testTitle,
                User = testAccount,
                UserId = testAccount.Id
            };

            var saveResult = service.SaveBankBook(newItem);

            var deleteResult = service.DeleteBankBook(saveResult);
            var getResult = context.BankBooks.Where(x => x.Name == testTitle).FirstOrDefault();

            Assert.Null(getResult);
            Assert.True(deleteResult);
        }

        [Fact]
        public void UpdateBankBook()
        {
            using var context = Fixture.CreateContext();
            var testAccount = Helper.CreateTestAccount();

            var service = new BankBookService();

            var testTitle = Guid.NewGuid().ToString();
            var newItem = new BankBook
            {
                Name = testTitle,
                User = testAccount,
                UserId = testAccount.Id
            };

            var saveResult = service.SaveBankBook(newItem);

            var newName = Guid.NewGuid().ToString();
            saveResult.Name = newName;

            var updateResult = service.UpdateBankBook(saveResult);
            var getResult = context.BankBooks.Where(x => x.Name == newName).FirstOrDefault();

            Assert.NotNull(getResult);
            Assert.Equal(updateResult.Name, newName);
        }
    }
}
