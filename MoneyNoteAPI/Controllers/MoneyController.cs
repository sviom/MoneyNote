using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        [HttpPost]
        public ApiResult<List<MoneyItem>> GetAllMoney([FromBody]ApiRequest<string> user)
        {
            var result = new ApiResult<List<MoneyItem>>();

            try
            {
                var baseId = user.Content;
                //UtilityLauncher.DecryptAES256(baseId, AzureKeyVault.SaltPassword);
                var service = new MoneyService();
                var moneyList = service.GetMoneyList(x => x.UserId.ToString() == baseId);
                result.Content = moneyList;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<MoneyItem> SaveMoney([FromBody]ApiRequest<MoneyItem> item)
        {
            var result = new ApiResult<MoneyItem>();
            try
            {
                var service = new MoneyService();
                var insertResult = service.SaveMoney(item.Content);

                //var insertResult = SqlLauncher.Insert(item.Content);
                result.Content = insertResult;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<MoneyItem> UpdateMoney([FromBody]ApiRequest<MoneyItem> item)
        {
            var result = new ApiResult<MoneyItem>();
            try
            {
                var service = new MoneyService();
                var updateResult = service.UpdateMoney(item.Content);

                //var updateResult = SqlLauncher.Update(item.Content);
                result.Content = updateResult;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }
    }
}