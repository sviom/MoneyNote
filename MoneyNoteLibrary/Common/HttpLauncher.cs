using MoneyNoteLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary.Common
{
    public static class HttpLauncher
    {
#if DEBUG
        public static string basic = "http://localhost:50456/api/money/";
#else
        public static string basic = "https://moneynoteapi.azurewebsites.net/api/money/";
#endif

        public static async Task<List<U>> GetAll<T, U>(T item) where T : class
        {
            List<U> items = new List<U>();
            using (var client = new HttpClient())
            {
                var request = new ApiRequest<T>(item);
                var itemString = JsonConvert.SerializeObject(request);

                var content = new StringContent(itemString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(basic + "GetAllMoney", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<U>>(responseContent);
            }

            return items;
        }

        public static async Task Insert<T>(T item) where T : class
        {
            using (var client = new HttpClient())
            {
                var request = new ApiRequest<T>(item);
                var itemString = JsonConvert.SerializeObject(request);

                var content = new StringContent(itemString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(basic + "SaveMoney", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var ss = JsonConvert.DeserializeObject<T>(responseContent);
            }
        }

        public static async Task<T> Test<T>(this MoneyApi api, T item)
        {
            T result;
            using (var client = new HttpClient())
            {
                var request = new ApiRequest<T>(item);
                var itemString = JsonConvert.SerializeObject(request);

                var content = new StringContent(itemString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(basic + api, content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var ss = JsonConvert.DeserializeObject<T>(responseContent);
                result = ss;
            }
            return result;
        }
    }
}
