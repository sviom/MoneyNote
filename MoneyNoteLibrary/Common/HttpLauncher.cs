using MoneyNoteLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNoteLibrary.Common
{
    public class HttpLauncher
    {
#if DEBUG
        public static string basic = "http://localhost:50456/api/money/";
#else
        public static string basic = "https://moneynoteapi.azurewebsites.net/api/";
#endif

        public static async Task GetAll<T>(T item) where T : class
        {
            using (var client = new HttpClient())
            {
                var itemString = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(basic + "GetAllMoney", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<MoneyItem>(responseContent);
            }
        }

        public static async Task Insert()
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent("");
                var response = await client.PostAsync("", content);
            }
        }
    }
}
