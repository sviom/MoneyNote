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
        public List<MoneyItem> GetAllMoney([FromBody]ApiRequest<User> user)
        {
            var moneyList = SqlLauncher.GetAll<MoneyItem>();
            return moneyList;
        }

        [HttpPost]
        public MoneyItem SaveMoney([FromBody]ApiRequest<MoneyItem> item)
        {
            var insertResult = SqlLauncher.Insert(item.Content);
            return insertResult;
        }

        [HttpPost]
        public MoneyItem UpdateMoney([FromBody]ApiRequest<MoneyItem> item)
        {
            var insertResult = SqlLauncher.Update(item.Content);
            return insertResult;
        }
    }
}