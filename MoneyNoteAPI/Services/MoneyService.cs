using Microsoft.EntityFrameworkCore;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary5;
using MoneyNoteLibrary5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static MoneyNoteLibrary5.Enums.MoneyEnum;

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
                var result = SqlLauncher.Insert(moneyItem);
                if (result == null) return null;

                var bankBookResult = UpdateBankBookWithMoney(moneyItem);
                if (!bankBookResult) return null;

                return moneyItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MoneyItem UpdateMoney(double oldMoney, MoneyItem moneyItem)
        {
            try
            {
                var result = SqlLauncher.Update(moneyItem);
                if (result == null) return null;

                var money = oldMoney - moneyItem.Money;
                var changeMoneyItem = new MoneyItem()
                {
                    Money = Math.Abs(money),
                    BankBook = moneyItem.BankBook,
                    BankBookId = moneyItem.BankBookId,
                    Division = moneyItem.Division
                };


                var bankBookResult = UpdateBankBookWithMoney(changeMoneyItem);
                if (!bankBookResult)
                    return null;

                return moneyItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMoney(MoneyItem moneyItem)
        {
            try
            {
                var result = SqlLauncher.Delete(moneyItem);
                if (!result) return false;

                moneyItem.Money = -moneyItem.Money;

                var bankBookResult = UpdateBankBookWithMoney(moneyItem);
                if (!bankBookResult) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region MoneyItem과 연관된 내용의 설정

        public bool UpdateBankBookWithMoney(MoneyItem moneyItem)
        {
            using var db = new MoneyContext();
            var bankService = new BankBookService();
            var nowBankBook = db.BankBooks.Where(y => y.Id == moneyItem.BankBookId).FirstOrDefault();
            if (nowBankBook == null)
                return false;

            if (moneyItem.Division == MoneyCategory.Expense)
                nowBankBook.Assets -= moneyItem.Money;
            else
                nowBankBook.Assets += moneyItem.Money;

            db.BankBooks.Update(nowBankBook);
            db.SaveChanges();
            return true;
        }

        #endregion
    }
}
