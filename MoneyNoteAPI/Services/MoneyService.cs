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
                        .Include(z => z.BankBook)
                        .OrderByDescending(x => x.CreatedTime)
                        .ToList();//.Include(y => y.SubCategory).ToList();
                else
                    returnList = db.MoneyItems
                        .Include(x => x.MainCategory)
                        .ThenInclude(main => main.SubCategories)
                        .Include(z => z.BankBook)
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
            if (moneyItem == null)
                return null;

            try
            {
                using var db = new MoneyContext();
                db.Entry(moneyItem).State = EntityState.Added;
                var set = db.Set<MoneyItem>();
                set.Add(moneyItem);
                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                {
                    // 자산도 추가
                    var bankService = new BankBookService();
                    var nowBankBook = db.BankBooks.Where(y => y.Id == moneyItem.BankBookId).FirstOrDefault();
                    if (nowBankBook != null)
                    {
                        if (moneyItem.Division == MoneyNoteLibrary.Enums.MoneyEnum.MoneyCategory.Expense)
                            nowBankBook.Assets -= moneyItem.Money;
                        else
                            nowBankBook.Assets += moneyItem.Money;

                        bankService.UpdateBankBook(nowBankBook);
                    }
                    return moneyItem;
                }
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

        public bool DeleteMoney(MoneyItem moneyItem)
        {
            try
            {
                using var db = new MoneyContext();
                db.Entry(moneyItem).State = EntityState.Deleted;
                var set = db.Set<MoneyItem>();
                set.Remove(moneyItem);
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
