//using Microsoft.Azure.KeyVault;
//using Microsoft.Azure.KeyVault.Models;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault;
using Azure.Security.KeyVault.Secrets;
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

        public static string GetKeyVaultEndpoint() => "https://todaylunchkeyvault.vault.azure.net/";

        public static async Task<string> OnGetAsync(string secretName)
        {
            string secretValue;
            try
            {
                //var azureServiceTokenProvider = new AzureServiceTokenProvider();
                //var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                //var secret = await keyVaultClient.GetSecretAsync(GetKeyVaultEndpoint() + "/secrets/" + secretName).ConfigureAwait(false);
                //secretValue = secret.Value;

                var options = new SecretClientOptions()
                {
                    Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
                };
                var client = new SecretClient(new Uri(GetKeyVaultEndpoint()), new DefaultAzureCredential(), options);

                var secret = await client.GetSecretAsync(secretName);
                secretValue = secret.Value.Value;
            }
            //catch (KeyVaultErrorException keyVaultException)
            catch (Exception ex)
            {
                secretValue = ex.Message;
            }
            return secretValue;
        }

    }

    public enum KeyVaultName
    {
        MoneyNoteConnectionString,
        SaltPassword,
        MoneyNoteTestConnection,
        MainEmailKey
    }
}
