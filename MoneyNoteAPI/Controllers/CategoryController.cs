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
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        public ApiResult<List<MainCategory>> GetMainCategories([FromBody]ApiRequest<User> user)
        {
            var result = new ApiResult<List<MainCategory>>();

            try
            {
                var service = new CategoryService();
                var categoryList = service.GetMainCategories(x => x.UserId == user.Content.Id);

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
        public ApiResult<List<SubCategory>> GetSubCategories([FromBody]ApiRequest<MainCategory> mainCategory)
        {
            var result = new ApiResult<List<SubCategory>>();
            try
            {
                var service = new CategoryService();
                var categoryList = service.GetSubCategories(x => x.MainCategoryId == mainCategory.Content.Id);

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