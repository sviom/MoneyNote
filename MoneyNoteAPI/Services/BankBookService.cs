using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MoneyNoteAPI.Context;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace MoneyNoteAPI.Services
{
    public class BankBookService
    {        
        public List<BankBook> GetBankBooks(User user)
        {
            try
            {
                using var context = new MoneyContext();
                return context.BankBooks.Where(x => x.UserId == user.Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBankBook(BankBook bankbook)
        {
            if (bankbook == null)
                return false;

            try
            {
                //using var db = new MoneyContext();
                using var context = new MoneyContext();
                context.Entry(bankbook).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                var set = context.Set<BankBook>();
                set.Remove(bankbook);

                //context.BankBooks.Remove(bankbook);
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

        public BankBook SaveBankBook(BankBook bankbook)
        {
            if (bankbook == null)
                return null;

            try
            {
                using var context = new MoneyContext();
                context.Entry(bankbook).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                var set = context.Set<BankBook>();
                set.Add(bankbook);


                //context.BankBooks.Add(bankbook);
                int saveResult = context.SaveChanges();

                if (saveResult > 0)
                    return bankbook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public BankBook UpdateBankBook(BankBook bankBook)
        {
            if (bankBook == null)
                return null;

            try
            {
                //using var context = new MoneyContext();
                //context.BankBooks.Update(bankBook);
                using var context = new MoneyContext();
                context.Entry(bankBook).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                var set = context.Set<BankBook>();
                set.Update(bankBook);

                int saveResult = context.SaveChanges();
                if (saveResult > 0)
                    return bankBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
