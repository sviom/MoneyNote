using Microsoft.EntityFrameworkCore;
using MoneyNoteAPI.Context;
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
        public List<MainCategory> GetMainCategories(Expression<Func<MainCategory, bool>> expression)
        {
            try
            {
                if (expression == null)
                    return null;

                using var db = new MoneyContext();

                var dbSet = db.Set<MainCategory>();

                if (expression != null)
                {
                    return dbSet.Where(expression).ToList();
                }
                else
                    return dbSet.ToList();
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

                using var db = new MoneyContext();

                var dbSet = db.Set<SubCategory>();

                if (expression != null)
                {
                    return dbSet.Where(expression).ToList();
                }
                else
                    return dbSet.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMainCategory(MainCategory mainCategory)
        {
            try
            {
                using var db = new MoneyContext();
                db.MainCategories.Remove(mainCategory);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteSubCategory(SubCategory subCategory)
        {
            try
            {
                using var db = new MoneyContext();
                db.SubCategories.Remove(subCategory);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
