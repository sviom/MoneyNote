using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    public class BankBookServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public BankBookServiceTest(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void SaveBankBookTest()
        {
            using var context = Fixture.CreateContext();
            var testAccount = TestHelper.CreateTestAccount(context);

            var service = new BankBookService(context);

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
    }
}
