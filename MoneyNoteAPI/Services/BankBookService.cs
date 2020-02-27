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
    public class BankBookService
    {
        public List<BankBook> GetBankBooks(Expression<Func<BankBook, bool>> expression)
        {
            try
            {
                if (expression == null)
                    return null;

                using var db = new MoneyContext();

                var dbSet = db.Set<BankBook>();

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

        public bool DeleteBankBook(BankBook bankbook)
        {
            try
            {
                using var db = new MoneyContext();
                db.Entry(bankbook).State = EntityState.Deleted;
                var set = db.Set<BankBook>();
                set.Remove(bankbook);
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
