using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyNoteAPI
{
    public class KeyVault
    {
        public static async Task<string> OnGetAsync(string secretName)
        {
            var secretValue = "Your application description page.";
            try
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = await keyVaultClient.GetSecretAsync(Program.GetKeyVaultEndpoint() + "/secrets/" + secretName).ConfigureAwait(false);
                secretValue = secret.Value;
            }
            catch (KeyVaultErrorException keyVaultException)
            {
                secretValue = keyVaultException.Message;
            }
            return secretValue;
        }
    }
}
