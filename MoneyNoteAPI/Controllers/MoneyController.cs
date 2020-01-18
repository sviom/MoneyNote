using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        [HttpPost]
        public List<MoneyItem> GetAllMoney([FromBody]ApiRequest<string> user)
        {
            var baseId = user.Content;
            UtilityLauncher.DecryptAES256(baseId, AzureKeyVault.SaltPassword);
            var moneyList = SqlLauncher.GetAll<MoneyItem>(x => x.UserId.ToString() == baseId);
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