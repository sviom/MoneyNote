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
    public class CategoryController : ControllerBase
    {
        private readonly MoneyContext _context;

        public CategoryController(MoneyContext context) => _context = context;

        [HttpPost]
        public ApiResult<List<MainCategory>> GetMainCategories([FromBody] ApiRequest<User> user)
        {
            var result = new ApiResult<List<MainCategory>>();

            try
            {
                var service = new CategoryService(_context);
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

        [HttpPost]
        public ApiResult<List<SubCategory>> GetSubCategories([FromBody] ApiRequest<MainCategory> mainCategory)
        {
            var result = new ApiResult<List<SubCategory>>();
            try
            {
                var service = new CategoryService(_context);
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
        public ApiResult<MainCategory> SaveMainCategory([FromBody] ApiRequest<MainCategory> item)
        {
            var result = new ApiResult<MainCategory>();
            try
            {
                var service = new CategoryService(_context);

                var saveResult = service.SaveCategory(item.Content);

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
                var service = new CategoryService(_context);

                var saveResult = service.SaveCategory(item.Content);

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
                var service = new CategoryService(_context);
                var insertResult = service.UpdateCategory(item.Content);

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
                var service = new CategoryService(_context);
                var insertResult = service.UpdateCategory(item.Content);

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
                var service = new CategoryService(_context);
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
                var service = new CategoryService(_context);
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