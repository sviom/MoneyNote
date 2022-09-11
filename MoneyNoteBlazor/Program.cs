using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MoneyNoteBlazor;
using MoneyNoteLibrary5.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

namespace MoneyNoteBlazor
{
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

        public static async Task<T> GetItemInLocalStorage<T>(IJSRuntime JS, Keys key)
        {
            var result = await JS.InvokeAsync<T>("GetUserInfo", key.ToString());

            return result;
        }

        public static async Task SetItemInLocallStorage(IJSRuntime JS, Keys key, object value)
        {
            await JS.InvokeVoidAsync("SetToLocalStorage", key.ToString(), JsonConvert.SerializeObject(value));
        }

        public static async Task ClearLocalStorage(IJSRuntime JS)
        {
            await JS.InvokeVoidAsync("ClearLocalStorage");
        }

        public enum Keys
        {
            userKey,
            KeepLogin
        }
    }
}
