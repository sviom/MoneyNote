using Microsoft.EntityFrameworkCore;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary5;
using MoneyNoteLibrary5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyNoteAPI.Services
{
    public class CategoryService
    {
        public List<MainCategory> GetCategories(Expression<Func<MainCategory, bool>> expression)
        {
            try
            {
                if (expression == null)
                    return null;
                using var context = new MoneyContext();
                return context.MainCategories.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubCategory> GetSubCategories(Expression<Func<SubCategory, bool>> expression)
        {
            try
            {
                if (expression == null)
                    return null;
                using var context = new MoneyContext();
                return context.SubCategories.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCategory(ICategory categoryItem)
        {
            try
            {
                var result = SqlLauncher.Delete(categoryItem);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICategory SaveCategory(ICategory inputObject)
        {
            try
            {
                var saveResult = SqlLauncher.Insert(inputObject);
                return saveResult;
            }
            catch (Exception ex)
            {
                throw;
            }
            return default;
        }

        public ICategory UpdateCategory(ICategory inputObject)
        {
            try
            {
                var result = SqlLauncher.Update(inputObject);
                return result;

            }
            catch (Exception ex)
            {
                throw;
            }
            return default;
        }

    }
}
