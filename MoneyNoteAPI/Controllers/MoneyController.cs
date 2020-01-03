using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        [HttpPost]
        public List<MoneyItem> GetAllMoney([FromBody]ApiRequest<string> id)
        {
            var moneyList = SqlLauncher.GetAll<MoneyItem>();
            return moneyList;
        }

        [HttpPost]
        public bool SaveMoney(MoneyItem moneyItem)
        {
            bool result = false;
            var item = SqlLauncher.Insert(moneyItem);
            if (item != null)
                result = true;

            return result;
        }
    }
}