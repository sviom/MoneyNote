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
        public ApiResult<bool> LogIn([FromBody]ApiRequest<User> item)
        {
            var user = item.Content;
            var countResult = SqlLauncher.Count<User>(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));
            return new ApiResult<bool>() { Result = countResult > 0 };
        }
    }
}