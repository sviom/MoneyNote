using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MoneyNoteLibrary5.Common;

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
                //if (context.HostingEnvironment.IsProduction())
                //{
                //}

                //config.AddAzureAppConfiguration(options =>
                //{
                //    options.Connect(settings["ConnectionStrings:AppConfig"])
                //            .ConfigureKeyVault(kv =>
                //            {
                //                var cert = new ClientSecretCredential("<tenant id>", "client id", "client secret");
                //                kv.SetCredential(cert);
                //            });
                //});

                //var keyVaultEndpoint = AzureKeyVault.GetKeyVaultEndpoint();
                //if (!string.IsNullOrEmpty(keyVaultEndpoint))
                //{
                //    var azureServiceTokenProvider = new AzureServiceTokenProvider();
                //    var keyVaultClient = new KeyVaultClient(
                //        new KeyVaultClient.AuthenticationCallback(
                //            azureServiceTokenProvider.KeyVaultTokenCallback));
                //    config.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                //}
            })

            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
