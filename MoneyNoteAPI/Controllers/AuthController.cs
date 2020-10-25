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
                var base64Text = UtilityLauncher.ConvertSaftToOriginalString(query);
                byte[] orgBytes = Convert.FromBase64String(base64Text);
                string orgStr = Encoding.UTF8.GetString(orgBytes);

                var sss = UtilityLauncher.DecryptAES256(orgStr, AzureKeyVault.SaltPassword);


                var fff = JsonConvert.DeserializeObject<User>(sss);

                var service = new UserService();
                var countResult = service.ApproveUser(fff);
                ViewBag.IsSuccessApprove = countResult;
            }
            catch
            {
            }

            return View();
        }
    }
}
