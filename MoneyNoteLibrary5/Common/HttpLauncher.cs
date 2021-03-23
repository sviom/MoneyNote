using MoneyNoteLibrary5.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary5.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary5.Common
{
    public static class HttpLauncher
    {
#if DEBUG
        public static string BaseUri = "https://localhost:50456/";
        //public static string BaseURL = "https://localhost:50456/api/";
        public static string BaseURL = "https://localhost:44356/api/";
#else
        public static string BaseUri = "https://moneynoteapi.azurewebsites.net/";
        public static string BaseURL = "https://moneynoteapi.azurewebsites.net/api/";
#endif

        //public static string basic = "https://moneynoteapi.azurewebsites.net/api/";

        public static async Task<ApiResult<U>> ApiLauncher<T, U>(this MoneyApi api, T item, ControllerEnum controllerEnum = ControllerEnum.money)
        {
            ApiResult<U> result;
            using (var client = new HttpClient())
            {
                var request = new ApiRequest<T>(item);
                var itemString = JsonConvert.SerializeObject(request);

                var content = new StringContent(itemString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(BaseURL + controllerEnum + "/" + api, content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<ApiResult<U>>(responseContent);
                result = apiResult;
            }
            return result;
        }

        /// <summary>
        /// API실행기
        /// </summary>
        /// <typeparam name="T">반환받고 싶은 형식</typeparam>
        /// <param name="api">API 종류(API 이름)</param>
        /// <param name="request">API에 전달하고자 하는 값</param>
        /// <param name="controllerEnum">어느 컨트롤러에서 실행시킬건지 정하기</param>
        /// <returns></returns>
        public static async Task<ApiResult<T>> ApiLauncher<T>(this MoneyApi api, IApiRequest request, ControllerEnum controllerEnum = ControllerEnum.money)
        {
            ApiResult<T> result;
            using (var client = new HttpClient())
            {
                var itemString = JsonConvert.SerializeObject(request);

                var content = new StringContent(itemString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(BaseURL + controllerEnum + "/" + api, content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<ApiResult<T>>(responseContent);
                result = apiResult;
            }
            return result;
        }
    }
}
