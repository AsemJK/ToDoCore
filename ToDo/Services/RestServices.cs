using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoCore.Models;
using ToDoCore.Helper;
using Newtonsoft.Json;
using System.Text;
using ToDoCore.Helpers;
using System.Reflection;

namespace ToDoCore.Services
{
    public class RestServices : IRestServices
    {
        public async Task<List<ToDo>> GetToDoList(int tenantId)
        {
            using (var httpClient = new HttpClient())
            {
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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/Saas/Tenants?Key=" + Constants.TestUserApiKey))
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

        public async Task<bool> CheckLogin(string userName, string password)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/User/Login?userName=" + userName + "&password=" + password + "&key=" + Constants.TestUserApiKey))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public async Task<ToDo> AddToDo(ToDo todo)
        {
            decimal todono = 0;
            using (var httpClient = new HttpClient())
            {
                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(todo).ToString();
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(todo).ToString(), Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(Constants.ApiBaseUrl + "/ToDo/AddToDo?key=" + Constants.TestUserApiKey, content).Result;
                //todono = Common.ToDecimalConvertObject(result.Content.ReadAsStringAsync().Result,0);
                var objectModel = JsonConvert.DeserializeObject<ToDo>(result.Content.ReadAsStringAsync().Result);
                return objectModel;
            }
        }

        public async Task<ToDo> ToDoDetail(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/ToDo/Detail/"+ id.ToString() +"?key=" + Constants.TestUserApiKey))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ToDo>(apiResponse);
                    }
                    else
                    {
                        return new ToDo();
                    }
                }
            }
        }
    }
}
