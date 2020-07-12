using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Models;
using MoneyNoteUnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MoneyNoteUnitTest.ServiceTest
{
    public class MoneyServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public MoneyServiceTest(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void Test()
        {
            using var context = Fixture.CreateContext();
            var testAccount = TestHelper.CreateTestAccount(context);
            var service = new MoneyService(context);

            var newItem = new MoneyItem();

            newItem.UserId = testAccount.Id;

            var savedItem = service.SaveMoney(newItem);

            var resultItem = service.GetMoney(x => x.Id == savedItem.Id);

            Assert.NotNull(resultItem);
        }
    }
}
