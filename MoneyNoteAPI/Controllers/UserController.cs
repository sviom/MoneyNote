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
    public class UserController : ControllerBase
    {
        [HttpPost]
        public ApiResult<User> SignUp([FromBody]ApiRequest<User> item)
        {
            var result = new ApiResult<User>();
            try
            {
                var service = new UserService();
                var insertResult = service.SignUp(item.Content);

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
        public ApiResult<User> LogIn([FromBody]ApiRequest<User> item)
        {
            var result = new ApiResult<User>();
            try
            {
                //var ss = UtilityLauncher.EncryptAES256(userResult.Id.ToString(), AzureKeyVault.SaltPassword);
                var user = item.Content;
                var countResult = SqlLauncher.Count<User>(x => x.Email == user.Email && x.Password == user.Password);
                var userResult = SqlLauncher.Get<User>(x => x.Email == user.Email && x.Password == user.Password);

                result.Content = userResult;
                result.Result = countResult > 0;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }
    }
}