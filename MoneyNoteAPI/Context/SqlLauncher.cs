using Microsoft.EntityFrameworkCore;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyNoteAPI.Context
{
    public class SqlLauncher
    {
        public static T Insert<T>(T inputObject) where T : class
        {
            T addedObject = null;
            try
            {
                using var db = new MoneyContext();
                db.Entry(inputObject).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var set = db.Set<T>();
                set.Add(inputObject);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    addedObject = inputObject;
            }
            catch (Exception ex)
            {
                throw;
            }
            return addedObject;
        }

        public static List<T> InsertList<T>(List<T> inputList) where T : class
        {
            List<T> resultList = new List<T>();
            try
            {
                using var db = new MoneyContext();
                foreach (var item in inputList)
                {
                    var set = db.Set<T>();
                    set.Add(item);
                }

                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    resultList = inputList;
            }
            catch (Exception ex) { }

            return resultList;
        }

        public static List<T> GetAll<T>(Expression<Func<T, bool>> expression = null) where T : class, ICommon
        {
            List<T> returnList = new List<T>();
            try
            {
                using var db = new MoneyContext();
                //DbSet<T> dbSet;// = null;

                DbSet<T> dbSet = db.Set<T>();

                if (typeof(T) == typeof(MoneyItem))
                {
                    var ff = db.MoneyItems.Include(x => x.MainCategory);
                    var ssdbSet = db.Set<MoneyItem>();
                }
                else
                    dbSet = db.Set<T>();

                if (expression != null)
                {
                    returnList = dbSet.Where(expression).ToList();
                }
                else
                {
                    returnList = dbSet.ToList();
                }
            }
            catch (Exception ex)
            {
                //Insert(new Log() { Message = ex.Message, StackTrace = ex.StackTrace });
            }
            return returnList;
        }

        public static T Get<T>(Expression<Func<T, bool>> expression = null) where T : class
        {
            T returnObject;
            try
            {
                using var db = new MoneyContext();
                var dbSet = db.Set<T>();
                if (expression != null)
                {
                    returnObject = dbSet.Where(expression).FirstOrDefault();
                }
                else
                {
                    returnObject = dbSet.FirstOrDefault();
                }
                return returnObject;
            }
            catch (Exception ex)
            {
                //Log.Report(ex);
            }
            return null;
        }

        /// <summary>
        /// Object 개수 Counting, 조건 없으면 전체 개수 가져오기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static int Count<T>(Expression<Func<T, bool>> expression = null) where T : class
        {
            int count = 0;
            try
            {
                using var db = new MoneyContext();
                var dbSet = db.Set<T>();
                if (expression != null)
                    count = dbSet.Where(expression).ToList().Count;
                else
                    count = dbSet.ToList().Count;
            }
            catch (Exception ex)
            {
                throw;
                //Insert(new Log() { Message = ex.Message, StackTrace = ex.StackTrace });
            }
            return count;
        }

        public static bool Delete<T>(T deleteObject) where T : class
        {
            bool result = false;
            try
            {
                //if (deleteObject == null || deleteObject.Id == Guid.Empty)
                //    return false;

                using (var db = new MoneyContext())
                {
                    db.Entry(deleteObject).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    var set = db.Set<T>();
                    set.Remove(deleteObject);
                    int saveResult = db.SaveChanges();
                    if (saveResult > 0)
                        result = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static T Update<T>(T updateObject) where T : class
        {
            T addedObject = null;
            try
            {
                using var db = new MoneyContext();
                db.Entry(updateObject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var set = db.Set<T>();
                set.Update(updateObject);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    addedObject = updateObject;
            }
            catch (Exception ex)
            {

            }

            return addedObject;
        }
    }
}
