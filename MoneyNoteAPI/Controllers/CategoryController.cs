using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Context;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary5;
using MoneyNoteLibrary5.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        public ApiResult<List<MainCategory>> GetMainCategories([FromBody] ApiRequest<User> user)
        {
            var result = new ApiResult<List<MainCategory>>();

            try
            {
                var service = new CategoryService();
                var categoryList = service.GetCategories(x => x.UserId == user.Content.Id);

                result.Content = categoryList;
                result.Result = categoryList != null;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        //[HttpPost]
        [HttpGet]
        public ApiResult<List<SubCategory>> GetSubCategories(string guid)
        {
            var result = new ApiResult<List<SubCategory>>();
            try
            {
                var service = new CategoryService();
                var categoryList = service.GetSubCategories(x => x.MainCategoryId == new Guid(guid));

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
        public ApiResult<MainCategory> SaveMainCategory([FromBody] ApiRequest<MainCategory> item)
        {
            var result = new ApiResult<MainCategory>();
            try
            {
                var service = new CategoryService();

                var saveResult = service.SaveCategory<MainCategory>(item.Content);

                result.Content = (MainCategory)saveResult;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<SubCategory> SaveSubCategory([FromBody] ApiRequest<SubCategory> item)
        {
            var result = new ApiResult<SubCategory>();
            try
            {
                var service = new CategoryService();

                var saveResult = service.SaveCategory<SubCategory>(item.Content);

                result.Content = (SubCategory)saveResult;
                result.Result = true;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<MainCategory> UpdateMainCategory([FromBody] ApiRequest<MainCategory> item)
        {
            var result = new ApiResult<MainCategory>();
            try
            {
                var service = new CategoryService();
                var insertResult = service.UpdateCategory<MainCategory>(item.Content);

                if (insertResult is MainCategory mainCategory)
                {
                    result.Content = mainCategory;
                    result.Result = true;
                }
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<SubCategory> UpdateSubCategory([FromBody] ApiRequest<SubCategory> item)
        {
            var result = new ApiResult<SubCategory>();
            try
            {
                var service = new CategoryService();
                var insertResult = service.UpdateCategory<SubCategory>(item.Content);

                if (insertResult is SubCategory subCategory)
                {
                    result.Content = subCategory;
                    result.Result = true;
                }
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<bool> DeleteMainCategory([FromBody] ApiRequest<MainCategory> item)
        {
            var result = new ApiResult<bool>();

            try
            {
                var service = new CategoryService();
                var deleteResult = service.DeleteCategory(item.Content);

                result.Content = true;
                result.Result = deleteResult;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }

        [HttpPost]
        public ApiResult<bool> DeleteSubCategory([FromBody] ApiRequest<SubCategory> item)
        {
            var result = new ApiResult<bool>();

            try
            {
                var service = new CategoryService();
                var deleteResult = service.DeleteCategory(item.Content);

                result.Content = deleteResult;
                result.Result = deleteResult;
            }
            catch
            {
                result.Result = false;
            }
            return result;
        }
    }
}