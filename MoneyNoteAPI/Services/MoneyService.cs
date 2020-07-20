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
    public class MoneyService
    {
        private readonly MoneyContext context;

        public MoneyService(MoneyContext _context) => this.context = _context;

        public List<MoneyItem> GetMoneyList(Expression<Func<MoneyItem, bool>> expression = null)
        {
            List<MoneyItem> returnList = new List<MoneyItem>();
            try
            {
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
                context.MoneyItems.Add(moneyItem);

                int saveResult = context.SaveChanges();
                if (saveResult > 0)
                {
                    UpdateBankBookWithMoney(context, moneyItem);
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
                var money = oldMoney - moneyItem.Money;
                var changeMoneyItem = new MoneyItem()
                {
                    Money = money,
                    BankBook = moneyItem.BankBook,
                    BankBookId = moneyItem.BankBookId
                };

                context.MoneyItems.Update(moneyItem);
                int saveResult = context.SaveChanges();
                if (saveResult <= 0)
                    return null;

                var bankBookResult = UpdateBankBookWithMoney(context, changeMoneyItem, false);
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
                context.MoneyItems.Remove(moneyItem);

                UpdateBankBookWithMoney(context, moneyItem, false);

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

        #region MoneyItem과 연관된 내용의 설정

        public bool UpdateBankBookWithMoney(MoneyContext refContext, MoneyItem moneyItem, bool isSave = true)
        {
            var bankService = new BankBookService(refContext);
            var nowBankBook = refContext.BankBooks.Where(y => y.Id == moneyItem.BankBookId).FirstOrDefault();
            if (nowBankBook != null)
            {
                if (isSave)
                {
                    if (moneyItem.Division == MoneyNoteLibrary.Enums.MoneyEnum.MoneyCategory.Expense)
                        nowBankBook.Assets -= moneyItem.Money;
                    else
                        nowBankBook.Assets += moneyItem.Money;
                }
                else
                {
                    nowBankBook.Assets += moneyItem.Money;
                }

                refContext.BankBooks.Update(nowBankBook);
                return true;
                //var result = bankService.UpdateBankBook(nowBankBook);
                //return result != null;
            }

            return false;
        }

        #endregion
    }
}
