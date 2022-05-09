using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoCore.Models;
using ToDoCore.Helper;
using Newtonsoft.Json;

namespace ToDoCore.Services
{
    public class RestServices : IRestServices
    {
        public async Task<List<ToDo>> GetToDoList(int tenantId)
        {
            //var authHeader = new AuthenticationHeaderValue("bearer", SystemConstant.AccessToken);
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Authorization = authHeader;
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/ToDo?Key=" + Constants.TestUserApiKey + "&tenant=" + tenantId.ToString()))
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
        public async Task<List<Tenant>> GetTenantsList()
        {
            //var authHeader = new AuthenticationHeaderValue("bearer", SystemConstant.AccessToken);
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Authorization = authHeader;
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/Saas/Tenants?Key=" + Constants.TestUserApiKey ))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Tenant>>(apiResponse);
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
