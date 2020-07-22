using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankBookController : ControllerBase
    {
        private readonly MoneyContext _context;

        public BankBookController(MoneyContext context) => _context = context;

        [HttpPost]
        public ApiResult<List<BankBook>> GetBankBooks([FromBody]ApiRequest<User> user)
        {
            var result = new ApiResult<List<BankBook>>();
            try
            {
                var service = new BankBookService(_context);
                var bankbookList = service.GetBankBooks(user.Content);

                result.Content = bankbookList;
                result.Result = bankbookList != null;
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
                var service = new BankBookService(_context);
                var insertResult = service.SaveBankBook(item.Content);
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
        public ApiResult<BankBook> ModifyBankBook([FromBody]ApiRequest<BankBook> item)
        {
            var result = new ApiResult<BankBook>();
            try
            {
                var service = new BankBookService(_context);
                var insertResult = service.UpdateBankBook(item.Content);
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
        public ApiResult<bool> DeleteBankBook([FromBody]ApiRequest<BankBook> item)
        {
            var result = new ApiResult<bool>();
            try
            {
                var service = new BankBookService(_context);
                var updateResult = service.DeleteBankBook(item.Content);

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