using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MoneyNoteAPI.Context;
using MoneyNoteAPI.Models;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary5;
using MoneyNoteLibrary5.Common;
using MoneyNoteLibrary5.Models;
//using MoneyNoteLibrary5.Models;
using Newtonsoft.Json;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<ApiActionResult<User>> SignUp([FromBody] ApiRequest<User> item)
        {
            var result = new ApiActionResult<User>();
            try
            {
                var service = new UserService();

                if (item == null)
                {
                    result.Result = false;
                    result.ResultMessage = "에러가 발생했습니다.";
                    result.Content = item.Content;
                    return result;
                }

                var duplicateCheck = service.CheckExist(item.Content, x => x.Email == item.Content.Email);

                if (duplicateCheck)
                {
                    result.Content = item.Content;
                    result.ResultMessage = "중복된 이메일이 존재합니다.";
                    result.Result = false;
                    return result;
                }

                var insertResult = service.SignUp(item.Content);
                if (insertResult == null)
                    return result;

                if (string.IsNullOrEmpty(insertResult.Email))
                    return result;

                var sendEmailResult = await EmailLauncher.SendConfirmEmail(insertResult.Email, insertResult);
                if (sendEmailResult)
                {
                    result.Content = insertResult;
                    result.Result = true;
                }
                else
                {
                    result.ResultMessage = "이메일 인증에 문제가 발생했습니다.";
                }
            }
            catch
            {
                result.Result = false;
                result.ResultMessage = "에러가 발생했습니다.";
                result.Content = item.Content;
            }

            var ss = StatusCode(500);

            result.StatusCode(200);

            return result;
        }

        [HttpPost]
        public ApiResult<User> LogIn([FromBody] ApiRequest<User> item)
        {
            var result = new ApiResult<User>();
            try
            {
                //var ss = UtilityLauncher.EncryptAES256(userResult.Id.ToString(), AzureKeyVault.SaltPassword);
                var service = new UserService();
                var user = item.Content;

                (var userResult, var logInResult, var isApproved) = service.LogIn(user);

                if (logInResult && isApproved)
                {
                    result.Content = userResult;
                    result.Result = logInResult;
                }
                else if (logInResult && !isApproved)
                {
                    result.ResultMessage = "승인을 대기중인 계정입니다.";
                    result.Content = user;
                    result.Result = false;
                    return result;
                }
                else
                {
                    result.Content = user;
                    result.ResultMessage = "이메일 또는 패스워드가 잘못되었습니다.";
                    result.Result = false;
                }
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<List<User>> GetUsers([FromBody] ApiRequest<bool> item)
        {
            var result = new ApiResult<List<User>>();
            try
            {
                var service = new UserService();

                var userList = service.GetUserList(item.Content);
                if (userList != null)
                {
                    result.Content = userList;
                    result.Result = true;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<User> ApproveUser([FromBody] ApiRequest<User> item)
        {
            var result = new ApiResult<User>();
            try
            {
                //var ss = UtilityLauncher.EncryptAES256(userResult.Id.ToString(), AzureKeyVault.SaltPassword);
                var service = new UserService();
                var user = item.Content;
                var countResult = service.ApproveUser(user);
                result.Content = user;
                result.Result = countResult;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpGet]
        public bool ConfirmEmail(string confirmMessage)
        {
            try
            {
                var emailMessage = UtilityLauncher.DecryptAES256(confirmMessage, AzureKeyVault.SaltPassword);
                var needConfirmedUser = JsonConvert.DeserializeObject<User>(emailMessage);

                var service = new UserService();
                var user = service.GetUser(needConfirmedUser.Id.ToString());
                user.IsApproved = true;
                var result = service.UpdateUser(user);
                return result;
            }
            catch
            {
            }
            return false;
        }

        [HttpPost]
        public ApiResult<bool> DeleteUser([FromBody] ApiRequest<User> request)
        {
            var result = new ApiResult<bool>();

            try
            {
                var service = new UserService();
                var deleteResult = service.DeleteUser(request.Content);
                if (deleteResult)
                {
                    result.Content = true;
                    result.Result = true;
                }
            }
            catch
            {
            }
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<bool>> ClearUser([FromBody] ApiRequest<User> request)
        {
            var result = new ApiResult<bool>();
            try
            {
                var service = new UserService();
                var clearResult = await service.ClearUserData(request.Content);
                if (clearResult)
                {
                    result.Content = true;
                    result.Result = true;
                }
            }
            catch
            {
            }
            return result;
        }


        [HttpPost]
        public ApiResult<bool> ChangePassword([FromBody] ApiRequest<User> request)
        {
            var result = new ApiResult<bool>();
            try
            {
                var service = new UserService();
                var clearResult = service.UpdateUser(request.Content);
                if (clearResult)
                {
                    result.Content = true;
                    result.Result = true;
                }
            }
            catch
            {
            }
            return result;
        }
    }
}