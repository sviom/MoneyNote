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
                //using (var db = new MoneyContext())
                //{
                //    db.Entry(deleteObject).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                //    var set = db.Set<T>();
                //    set.Remove(deleteObject);
                //    int saveResult = db.SaveChanges();
                //    if (saveResult > 0)
                //        result = true;
                //}

                using var context = new MoneyContext();
                context.Entry(categoryItem).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                switch (categoryItem)
                {
                    case MainCategory mainCategory:
                        var set = context.Set<MainCategory>();
                        set.Remove(mainCategory);

                        //context.MainCategories.Remove(mainCategory);
                        break;
                    case SubCategory subCategory:
                        var subset = context.Set<SubCategory>();
                        subset.Remove(subCategory);

                        //context.SubCategories.Remove(subCategory);
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
                using var db = new MoneyContext();
                db.Entry(inputObject).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                

                //using var context = new MoneyContext();
                switch (inputObject)
                {
                    case MainCategory mainCategory:
                        var set = db.Set<MainCategory>();
                        set.Add(mainCategory);
                        //context.MainCategories.Add(mainCategory);
                        break;
                    case SubCategory subCategory:
                        var subset = db.Set<SubCategory>();
                        subset.Add(subCategory);
                        //context.SubCategories.Add(subCategory);
                        break;
                    default:
                        return default;
                }

                int saveResult = db.SaveChanges();
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
                using var db = new MoneyContext();
                db.Entry(inputObject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
                //int saveResult = db.SaveChanges();
                //if (saveResult > 0)
                //    addedObject = updateObject;

                using var context = new MoneyContext();
                switch (inputObject)
                {
                    case MainCategory mainCategory:
                        var set = db.Set<MainCategory>();
                        set.Update(mainCategory);

                        //context.MainCategories.Update(mainCategory);
                        break;
                    case SubCategory subCategory:
                        var subset = db.Set<SubCategory>();
                        subset.Update(subCategory);

                        //context.SubCategories.Update(subCategory);
                        break;
                    default:
                        return default;
                }

                int saveResult = db.SaveChanges();
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
