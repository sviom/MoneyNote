using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary5;
using MoneyNoteLibrary5.Common;
using MoneyNoteLibrary5.Models;

namespace MoneyNoteAPI.Controllers
{
    // /[action]
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {

        [HttpGet]
        public ApiResult<List<MoneyItem>> Money()
        {
            var result = new ApiResult<List<MoneyItem>>();

            try
            {
                // userid는 jwt 값으로 해결해야함
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

        [HttpGet]
        public ApiResult<List<MoneyItem>> Money(DateTimeOffset date)
        {
            var result = new ApiResult<List<MoneyItem>>();

            try
            {
                // 사용자는 추후 쿠키나 세션 등에서 토큰으로 처리하도록 해야 함
                var userInfo = new User(); // request.Content;
                var standardDate = date;

                //UtilityLauncher.DecryptAES256(baseId, AzureKeyVault.SaltPassword);
                var service = new MoneyService();
                var moneyList = service.GetMoneyList(x => x.UserId.ToString() == userInfo.Id.ToString() && x.CreatedTime.Year == standardDate.Year && x.CreatedTime.Month == standardDate.Month);
                result.Content = moneyList;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpGet]
        public ApiResult<MoneyItem> Money(string guid)
        {
            var result = new ApiResult<MoneyItem>();

            try
            {
                //var moneyItemId = new Guid(guid);

                var tryResult = Guid.TryParse(guid, out Guid moneyItemId);
                if (!tryResult)
                    throw new Exception();

                var service = new MoneyService();
                var moneyItem = service.GetMoney(x => x.Id == moneyItemId);
                result.Content = moneyItem;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<MoneyItem> SaveMoney([FromBody] ApiRequest<MoneyItem> item)
        {
            var result = new ApiResult<MoneyItem>();
            try
            {
                var service = new MoneyService();
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

        [HttpPatch]
        public ApiResult<MoneyItem> UpdateMoney([FromBody] ApiRequest<MoneyItem> item)
        {
            var result = new ApiResult<MoneyItem>();
            try
            {
                var service = new MoneyService();
                var oldMoneyItem = service.GetMoney(x => x.Id == item.Content.Id);
                var updateResult = service.UpdateMoney(oldMoneyItem.Money, item.Content);

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

        [HttpDelete]
        public ApiResult<bool> Money()
        {
            var result = new ApiResult<bool>();
            try
            {
                var service = new MoneyService();
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