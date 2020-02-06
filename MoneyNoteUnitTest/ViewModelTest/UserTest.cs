using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyNoteAPI.Services;

namespace MoneyNoteUnitTest.ViewModelTest
{
    [TestClass]
    public class UserTest
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

        [TestMethod]
        public void DeleteUser()
        {
            // 사용자 삭제
            // 사용자 삭제 시 MoneyItems, Category가 전부 삭제되어야 함
        }
    }
}
