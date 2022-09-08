using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyNoteLibrary5.Common;

namespace MoneyNoteAPI
{

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();

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
