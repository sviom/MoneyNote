using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpdateHistoryController : ControllerBase
    {
        public List<UpdateHistory> GetBankBooks(UpdateHistory user)
        {
            try
            {
                using var context = new MoneyContext();
                return null;// context.BankBooks.Where(x => x.UserId == user.Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
