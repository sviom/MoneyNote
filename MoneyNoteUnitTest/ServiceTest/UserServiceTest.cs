using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyNoteAPI.Services;

namespace MoneyNoteUnitTest.ServiceTest
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void SignUpUser()
        {
            // 회원 가입
            var test = new UserService();

            var result = test.SignUp(new MoneyNoteLibrary.Models.User());




            // DB에서 조회
            Assert.IsNotNull(result);
        }

    }
}
