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
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        public ApiResult<List<MainCategory>> GetMainCategories([FromBody]ApiRequest<User> user)
        {
            var result = new ApiResult<List<MainCategory>>();

            try
            {
                //UtilityLauncher.DecryptAES256(baseId, AzureKeyVault.SaltPassword);
                var categoryList = SqlLauncher.GetAll<MainCategory>(x => x.UserId == user.Content.Id);

                result.Content = categoryList;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<MainCategory> SaveMainCategory([FromBody]ApiRequest<MainCategory> item)
        {
            var result = new ApiResult<MainCategory>();
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
        public ApiResult<SubCategory> SaveSubCategory([FromBody]ApiRequest<SubCategory> item)
        {
            var result = new ApiResult<SubCategory>();
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
        public ApiResult<MainCategory> UpdateMainCategory([FromBody]ApiRequest<MainCategory> item)
        {
            var result = new ApiResult<MainCategory>();
            try
            {
                var insertResult = SqlLauncher.Update(item.Content);
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
        public ApiResult<SubCategory> UpdateSubCategory([FromBody]ApiRequest<SubCategory> item)
        {
            var result = new ApiResult<SubCategory>();
            try
            {
                var insertResult = SqlLauncher.Update(item.Content);
                result.Content = insertResult;
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