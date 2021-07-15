using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
//using MoneyNoteAPI.Context;
using MoneyNoteLibrary5.Context;
using MoneyNoteLibrary5;
using MoneyNoteLibrary5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//using System.Runtime.InteropServices.WindowsRuntime;
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
                var deleteResult = SqlLauncher.Delete(bankbook);
                return deleteResult;
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
                var insertResult = SqlLauncher.Insert(bankbook);

                if (insertResult != null)
                    return insertResult;
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
                var updateResult = SqlLauncher.Update(bankBook);
                if (updateResult != null)
                    return updateResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
