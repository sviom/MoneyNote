﻿using System;
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
        public static string Key => AzureKeyVault.OnGetAsync(KeyVaultName.MainEmailKey.ToString()).Result;

        public static async Task<bool> SendConfirmEmail(string emailAddress, User user)
        {
            var apiKey = Key;// _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var mailFrom = new EmailAddress("kanghanstar@raincome.net", "MoneyNote");

            var mailTo = new EmailAddress(emailAddress);

            var subject = "[MoneyNote] 메일 주소를 인증해주세요.";

            var userString = JsonConvert.SerializeObject(user);
            var encryptedUser = UtilityLauncher.EncryptAES256(userString, AzureKeyVault.SaltPassword);
            var userInfo = UtilityLauncher.ConvertSafeString(encryptedUser);

            var url = HttpLauncher.BaseUri + "Auth?query=" + userInfo;

            var htmlContent = @"
<strong>우측의 링크를 클릭해주세요.</strong>
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