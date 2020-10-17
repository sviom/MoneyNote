using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoneyNoteLibrary.Models;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MoneyNoteLibrary.Common
{
    public class EmailService
    {
#if DEBUG
        public static string basic = "http://localhost:50456/api/";
#else
        public static string basic = "https://moneynoteapi.azurewebsites.net/api/";
#endif

        public static string Key => "";

        public static async Task<bool> SendEmail(string emailAddress, User user)
        {
            var apiKey = Key;// _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var mailFrom = new EmailAddress("kanghanstar@raincome.net", "MoneyNote");

            var mailTo = new EmailAddress(emailAddress);

            var subject = "[MoneyNote] 메일 주소를 인증해주세요.";

            var userString = JsonConvert.SerializeObject(user);
            var encryptedUser = UtilityLauncher.EncryptAES256(userString, AzureKeyVault.SaltPassword);
            var htmlContent = @"
<strong>Hello world with HTML content</strong>
<a href='" + basic + encryptedUser + "' >이메일 인증 링크</a>";
            //var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
            var msg = MailHelper.CreateSingleEmail(mailFrom, mailTo, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

    }
}
