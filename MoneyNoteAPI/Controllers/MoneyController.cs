using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteLibrary.Models;

namespace MoneyNoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        [HttpPost]
        public List<MoneyItem> GetAllMoney()
        {
            return new List<MoneyItem>();
        }

        public bool SaveMoney()
        {
            return true;
        }
    }
}