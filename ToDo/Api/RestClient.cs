using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoCore.Models;
using ToDoCore.Helper;
using Newtonsoft.Json;

namespace ToDoCore.Api
{
    public class RestClient
    {
        public async Task<List<ToDo>> GetToDoList(string baseApiUrl, string userApiKey, int tenantId)
        {
            //var authHeader = new AuthenticationHeaderValue("bearer", SystemConstant.AccessToken);
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Authorization = authHeader;
                using (var response = await httpClient.GetAsync(baseApiUrl + "/ToDo?Key=" + userApiKey + "&tenant=" + tenantId.ToString()))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<ToDo>>(apiResponse);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
