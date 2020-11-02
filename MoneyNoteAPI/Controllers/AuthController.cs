using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using Newtonsoft.Json;

namespace MoneyNoteAPI.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Index(string query)
        {
            ViewBag.IsSuccessApprove = false;
            try
            {
                ViewBag.IsSuccessApprove = false;
                var safeString = UtilityLauncher.ConvertSafeToOriginalString(query);
                var userGuid = UtilityLauncher.DecryptAES256(safeString, AzureKeyVault.SaltPassword);

                var service = new UserService();
                var user = service.GetUser(userGuid);

                if (user == null) return View();

                var countResult = service.ApproveUser(user);
                ViewBag.IsSuccessApprove = countResult;
            }
            catch
            {
            }

            return View();
        }
    }
}
