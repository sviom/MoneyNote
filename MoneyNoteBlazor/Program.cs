using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MoneyNoteLibrary5.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNoteBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            if (builder.HostEnvironment.IsDevelopment())
            {

            }

            await builder.Build().RunAsync();
        }
    }

    public static class SharedClass
    {
        public static User SharedUser { get; set; }

        public static async Task<User> GetUserInfo(IJSRuntime JS)
        {
            var result = await JS.InvokeAsync<string>("GetUserInfo", "userKey");
            if (string.IsNullOrEmpty(result))
                return null;

            return JsonConvert.DeserializeObject<User>(result);
        }

        public static async Task SetItemInLocallStorage(IJSRuntime JS, string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                return;

            await JS.InvokeAsync<string>(key, value);
        }
    }
}
