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
        }
    }
}
