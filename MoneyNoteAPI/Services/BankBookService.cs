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
        public List<BankBook> GetBankBookList(Expression<Func<BankBook, bool>> expression)
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
    }
}
