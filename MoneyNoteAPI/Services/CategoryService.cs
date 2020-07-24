using Microsoft.EntityFrameworkCore;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Models;
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
                using var context = new MoneyContext();
                switch (categoryItem)
                {
                    case MainCategory mainCategory:
                        context.MainCategories.Remove(mainCategory);
                        break;
                    case SubCategory subCategory:
                        context.SubCategories.Remove(subCategory);
                        break;
                    default:
                        return default;
                }

                int saveResult = context.SaveChanges();
                if (saveResult > 0)
                    return true;

                return false;
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
                using var context = new MoneyContext();
                switch (inputObject)
                {
                    case MainCategory mainCategory:
                        context.MainCategories.Add(mainCategory);
                        break;
                    case SubCategory subCategory:
                        context.SubCategories.Add(subCategory);
                        break;
                    default:
                        return default;
                }

                int saveResult = context.SaveChanges();
                if (saveResult > 0)
                    return inputObject;
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
                using var context = new MoneyContext();
                switch (inputObject)
                {
                    case MainCategory mainCategory:
                        context.MainCategories.Update(mainCategory);
                        break;
                    case SubCategory subCategory:
                        context.SubCategories.Update(subCategory);
                        break;
                    default:
                        return default;
                }

                int saveResult = context.SaveChanges();
                if (saveResult > 0)
                    return inputObject;
            }
            catch (Exception ex)
            {
                throw;
            }
            return default;
        }

    }
}
