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
    public class MoneyService
    {
        public static MoneyContext context = new MoneyContext();

        public List<MoneyItem> GetMoneyList(Expression<Func<MoneyItem, bool>> expression = null)
        {
            List<MoneyItem> returnList = new List<MoneyItem>();
            try
            {
                using var db = new MoneyContext();

                DbSet<MoneyItem> dbSet = db.Set<MoneyItem>();

                if (expression == null)
                    returnList = db.MoneyItems
                        .Include(x => x.MainCategory)
                        .ThenInclude(main => main.SubCategories)
                        .OrderByDescending(x => x.CreatedTime)
                        .ToList();//.Include(y => y.SubCategory).ToList();
                else
                    returnList = db.MoneyItems
                        .Include(x => x.MainCategory)
                        .ThenInclude(main => main.SubCategories)
                        .OrderByDescending(x => x.CreatedTime)
                        .Where(expression).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnList;
        }

        public MoneyItem SaveMoney(MoneyItem moneyItem)
        {
            try
            {
                using var db = new MoneyContext();
                db.Entry(moneyItem).State = EntityState.Added;
                var set = db.Set<MoneyItem>();
                set.Add(moneyItem);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    return moneyItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public MoneyItem UpdateMoney(MoneyItem moneyItem)
        {
            try
            {
                using var db = new MoneyContext();
                db.Entry(moneyItem).State = EntityState.Modified;
                var set = db.Set<MoneyItem>();
                set.Update(moneyItem);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                    return moneyItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
