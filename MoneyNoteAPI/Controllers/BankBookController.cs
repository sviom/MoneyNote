using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankBookController : ControllerBase
    {
        [HttpPost]
        public ApiResult<List<BankBook>> GetBankBookList([FromBody]ApiRequest<User> user)
        {
            var result = new ApiResult<List<BankBook>>();
            try
            {
                var service = new BankBookService();
                var categoryList = service.GetBankBookList(x => x.UserId == user.Content.Id);

                result.Content = categoryList;
                result.Result = categoryList != null;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<BankBook> SaveBankBook([FromBody]ApiRequest<BankBook> item)
        {
            var result = new ApiResult<BankBook>();
            try
            {
                var insertResult = SqlLauncher.Insert(item.Content);
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
        public void ModifyBankBook()
        {
        }

        [HttpPost]
        public void DeleteBankBook()
        {

        }
    }
}