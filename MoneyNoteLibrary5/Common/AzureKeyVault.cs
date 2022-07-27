using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault;
//using Microsoft.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNoteLibrary5.Common
{
    public static class AzureKeyVault
    {
        public static string SaltPassword => OnGetAsync(KeyVaultName.SaltPassword).Result;

        public static string GetKeyVaultEndpoint() => "https://todaylunchkeyvault.vault.azure.net/";

        private static string KeyVaultEndPoint => "https://moneynote.vault.azure.net/";

        public static async Task<string> OnGetAsync(KeyVaultName secretName)
        {
            string secretValue;
            try
            {
                switch (secretName)
                {
                    case KeyVaultName.MoneyNoteConnectionString:
                        secretValue = "";
                        break;
                    case KeyVaultName.SaltPassword:
                        secretValue = "";
                        break;
                    case KeyVaultName.MoneyNoteTestConnection:
                        secretValue = "";
                        break;
                    case KeyVaultName.MainEmailKey:
                        secretValue = "";
                        break;
                    default:
                        secretValue = string.Empty;
                        break;
                }
                return secretValue;
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

#if DEBUG
                var sss = new DefaultAzureCredential(includeInteractiveCredentials: true);
#else
                var sss = new DefaultAzureCredential();
#endif
                //var fff = new ClientSecretCredential('', "", "");
                

                var client = new SecretClient(new Uri(GetKeyVaultEndpoint()), sss);
                var secret = await client.GetSecretAsync(secretName.ToString());
                secretValue = secret.Value.Value;
            }
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
