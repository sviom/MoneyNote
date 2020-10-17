using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNoteLibrary.Common
{
    public static class AzureKeyVault
    {
        public static string SaltPassword => OnGetAsync("SaltPassword").Result;

        public static string GetKeyVaultEndpoint() => "https://todaylunchkeyvault.vault.azure.net";

        public static async Task<string> OnGetAsync(string secretName)
        {
            var secretValue = "Your application description page.";
            try
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = await keyVaultClient.GetSecretAsync(GetKeyVaultEndpoint() + "/secrets/" + secretName).ConfigureAwait(false);
                secretValue = secret.Value;
            }
            catch (KeyVaultErrorException keyVaultException)
            {
                secretValue = keyVaultException.Message;
            }
            return secretValue;
        }
    }

    public enum KeyVaultName
    {
        MoneyNoteConnectionString,
        SaltPassword,
        MoneyNoteTestConnection,
        SendGridKey
    }
}
