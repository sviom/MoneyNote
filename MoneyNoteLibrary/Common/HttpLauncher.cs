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
        public static string basic = "http://localhost:50456/api/";
#else
        public static string basic = "https://moneynoteapi.azurewebsites.net/api/";
#endif

        public static async Task<ApiResult<U>> ApiLauncher<T, U>(this MoneyApi api, T item, ControllerEnum controllerEnum = ControllerEnum.money)
        {
            ApiResult<U> result;
            using (var client = new HttpClient())
            {
                var request = new ApiRequest<T>(item);
                var itemString = JsonConvert.SerializeObject(request);

                var content = new StringContent(itemString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(basic + controllerEnum + "/" + api, content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<ApiResult<U>>(responseContent);
                result = apiResult;
            }
            return result;
        }
    }
}
