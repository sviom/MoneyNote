using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly MoneyContext _context;

        public MoneyController(MoneyContext context) => _context = context;

        [HttpPost]
        public ApiResult<List<MoneyItem>> GetAllMoney([FromBody]ApiRequest<string> user)
        {
            var result = new ApiResult<List<MoneyItem>>();

            try
            {
                var baseId = user.Content;
                //UtilityLauncher.DecryptAES256(baseId, AzureKeyVault.SaltPassword);
                var service = new MoneyService(_context);
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
                var service = new MoneyService(_context);
                var insertResult = service.SaveMoney(item.Content);
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
                var service = new MoneyService(_context);

                var oldMoneyItem = service.GetMoney(x => x.Id == item.Content.Id);
                var updateResult = service.UpdateMoney(oldMoneyItem, item.Content);

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

        [HttpPost]
        public ApiResult<bool> DeleteMoney([FromBody]ApiRequest<MoneyItem> item)
        {
            var result = new ApiResult<bool>();
            try
            {
                var service = new MoneyService(_context);
                var updateResult = service.DeleteMoney(item.Content);

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