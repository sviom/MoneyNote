﻿using System;
using System.Collections.Generic;
using System.Text;
using MoneyNoteAPI.Controllers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Models;
using Xunit;

namespace MoneyNoteUnitTest.ServiceTest
{
    public class UserServiceTest : IClassFixture<SharedDatabaseFixture>
    {
        public UserServiceTest(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public void SignUpUser()
        {
            // DB에서 조회            

            using (var context = Fixture.CreateContext())
            {
                var rff = new ApiRequest<string>();
                var sss = new User();
                var controller = new MoneyController(context);
                var items = controller.GetAllMoney(rff);

                //Assert.Equal(3, items.Count);
                //Assert.Equal("ItemOne", items[0].Name);
                //Assert.Equal("ItemThree", items[1].Name);
                //Assert.Equal("ItemTwo", items[2].Name);
            }

        }

    }
}
