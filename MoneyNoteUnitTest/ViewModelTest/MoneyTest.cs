using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Models;

namespace MoneyNoteUnitTest.ViewModelTest
{
    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void GetAllMoney()
        {
            var service = new MoneyService();
            // ID : 5e47fc8f-7e33-473e-cf5b-08d7ab71b01c
            // 
            var moneyItem = new MoneyItem();

            service.SaveMoney(moneyItem);


            
            var moneyList = service.GetMoneyList();
        }
    }
}
