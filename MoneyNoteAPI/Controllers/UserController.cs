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
    public class UserController : ControllerBase
    {
        [HttpPost]
        public User SignUp([FromBody]ApiRequest<User> item)
        {
            var insertResult = SqlLauncher.Insert(item.Content);
            return insertResult;
        }

        [HttpPost]
        public ApiResult<User> LogIn([FromBody]ApiRequest<User> item)
        {
            var user = item.Content;
            var countResult = SqlLauncher.Count<User>(x => x.Email == user.Email && x.Password == user.Password);
            var userResult = SqlLauncher.Get<User>(x => x.Email == user.Email && x.Password == user.Password);
            return new ApiResult<User>() { Result = countResult > 0, Content = userResult };
        }
    }
}