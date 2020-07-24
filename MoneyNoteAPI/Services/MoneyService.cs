﻿using Microsoft.EntityFrameworkCore;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static MoneyNoteLibrary.Enums.MoneyEnum;

namespace MoneyNoteAPI.Services
{
    public class MoneyService
    {
        public List<MoneyItem> GetMoneyList(Expression<Func<MoneyItem, bool>> expression = null)
        {
            List<MoneyItem> returnList = new List<MoneyItem>();
            try
            {
                using var context = new MoneyContext();

                if (expression == null)
                    returnList = context.MoneyItems
                        .Include(x => x.MainCategory)
                        .ThenInclude(main => main.SubCategories)
                        .Include(z => z.BankBook)
                        .OrderByDescending(x => x.CreatedTime)
                        .ToList();//.Include(y => y.SubCategory).ToList();
                else
                    returnList = context.MoneyItems
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

        public MoneyItem GetMoney(Expression<Func<MoneyItem, bool>> expression = null)
        {
            if (expression == null)
                return null;

            var returnItem = new MoneyItem();
            try
            {
                using var context = new MoneyContext();

                returnItem = context.MoneyItems
                          .Include(x => x.MainCategory)
                          .ThenInclude(main => main.SubCategories)
                          .Include(z => z.BankBook)
                          .OrderByDescending(x => x.CreatedTime)
                          .Where(expression).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnItem;
        }

        public MoneyItem SaveMoney(MoneyItem moneyItem)
        {
            if (moneyItem == null)
                return null;
            try
            {
                //using var context = new MoneyContext();

                //context.MoneyItems.Add(moneyItem);


                using var db = new MoneyContext();
                db.Entry(moneyItem).State = EntityState.Added;
                var set = db.Set<MoneyItem>();
                set.Add(moneyItem);

                int saveResult = db.SaveChanges();
                if (saveResult > 0)
                {
                    UpdateBankBookWithMoney(db, moneyItem);
                    return moneyItem;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public MoneyItem UpdateMoney(double oldMoney, MoneyItem moneyItem)
        {
            try
            {
                //using var context = new MoneyContext();
                using var db = new MoneyContext();
                db.Entry(moneyItem).State = EntityState.Modified;
                var set = db.Set<MoneyItem>();


                var money = oldMoney - moneyItem.Money;
                var changeMoneyItem = new MoneyItem()
                {
                    Money = Math.Abs(money),
                    BankBook = moneyItem.BankBook,
                    BankBookId = moneyItem.BankBookId,
                    Division = moneyItem.Division
                };

                //context.MoneyItems.Update(moneyItem);
                //int saveResult = context.SaveChanges();
                //if (saveResult <= 0)
                //    return null;

                set.Update(moneyItem);
                int saveResult = db.SaveChanges();

                var bankBookResult = UpdateBankBookWithMoney(db, changeMoneyItem);
                //var bankBookResult = UpdateBankBookWithMoney(context, changeMoneyItem);
                if (!bankBookResult)
                    return null;

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
                //using var context = new MoneyContext();

                //context.MoneyItems.Remove(moneyItem);

                //moneyItem.Money = -moneyItem.Money;

                using var db = new MoneyContext();
                db.Entry(moneyItem).State = EntityState.Deleted;
                var set = db.Set<MoneyItem>();
                set.Remove(moneyItem);


                UpdateBankBookWithMoney(db, moneyItem);

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

        #region MoneyItem과 연관된 내용의 설정

        public bool UpdateBankBookWithMoney(MoneyContext refContext, MoneyItem moneyItem)
        {
            var bankService = new BankBookService();
            var nowBankBook = refContext.BankBooks.Where(y => y.Id == moneyItem.BankBookId).FirstOrDefault();
            if (nowBankBook == null)
                return false;

            if (moneyItem.Division == MoneyCategory.Expense)
                nowBankBook.Assets -= moneyItem.Money;
            else
                nowBankBook.Assets += moneyItem.Money;

            refContext.BankBooks.Update(nowBankBook);
            return true;
        }

        #endregion
    }
}
