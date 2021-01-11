using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoneyNoteLibrary.Common;

namespace MoneyNoteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                //var keyVaultEndpoint = AzureKeyVault.GetKeyVaultEndpoint();
                //if (!string.IsNullOrEmpty(keyVaultEndpoint))
                //{
                //    var azureServiceTokenProvider = new AzureServiceTokenProvider();
                //    var keyVaultClient = new KeyVaultClient(
                //        new KeyVaultClient.AuthenticationCallback(
                //            azureServiceTokenProvider.KeyVaultTokenCallback));
                //    config.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                //}

                if (context.HostingEnvironment.IsProduction())
                {
              

                }

                var builtConfig = config.Build();
                var secretClient = new SecretClient(new Uri(AzureKeyVault.GetKeyVaultEndpoint()), new DefaultAzureCredential());
                config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

            })

            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
