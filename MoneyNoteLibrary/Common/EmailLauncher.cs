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
    public class EmailLauncher
    {
        public static string Key => AzureKeyVault.OnGetAsync(KeyVaultName.MainEmailKey).Result;

        public static async Task<bool> SendConfirmEmail(string emailAddress, User user)
        {
            var apiKey = Key;// _configuration.GetSection("SENDGRID_API_KEY").Value;

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(emailAddress))
                return false;

            var client = new SendGridClient(apiKey);
            var mailFrom = new EmailAddress("kanghanstar@raincome.net", "MoneyNote");

            var mailTo = new EmailAddress(emailAddress);

            var subject = "[MoneyNote] 메일 주소를 인증해주세요.";

            //var userString = JsonConvert.SerializeObject(user);
            var encryptedUser = UtilityLauncher.EncryptAES256(user.Id.ToString(), AzureKeyVault.SaltPassword);
            var userGuid = UtilityLauncher.ConvertSafeString(encryptedUser);

            var url = HttpLauncher.BaseUri + "Auth?query=" + userGuid;

            var htmlContent = @"
<strong>아래의 링크를 클릭해주세요.</strong>
<br />
<a href='" + url + "' >이메일 인증 링크</a>";
            //var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
            var msg = MailHelper.CreateSingleEmail(mailFrom, mailTo, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                return true;

            return false;
        }

    }
}
