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
        /// <summary>
        /// Last status : "All","New","Done"
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="lastStatus"></param>
        /// <returns></returns>
        public async Task<List<ToDo>> GetToDoList(int tenantId,string lastStatus)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/ToDo?q="+ lastStatus + "&Key=" + Constants.UserApiKey + "&tenant=" + tenantId.ToString()))
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
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/Saas/Tenants?Key=" + Constants.UserApiKey))
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

        public async Task<User> CheckLogin(string userName, string password)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/User/Login?userName=" + userName + "&password=" + password + "&key=" + Constants.UserApiKey))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                    else
                    {
                        return new User();
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
                var result = httpClient.PostAsync(Constants.ApiBaseUrl + "/ToDo/AddToDo?key=" + Constants.UserApiKey, content).Result;
                //todono = Common.ToDecimalConvertObject(result.Content.ReadAsStringAsync().Result,0);
                var objectModel = JsonConvert.DeserializeObject<ToDo>(result.Content.ReadAsStringAsync().Result);
                return objectModel;
            }
        }

        public async Task<ToDo> ToDoDetail(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/ToDo/Detail/"+ id.ToString() +"?key=" + Constants.UserApiKey))
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
        public async Task<ToDo> UpdateToDo(ToDo todo)
        {
            decimal todono = 0;
            using (var httpClient = new HttpClient())
            {
                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(todo).ToString();
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(todo).ToString(), Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(Constants.ApiBaseUrl + "/ToDo/UpdateToDo?key=" + Constants.UserApiKey, content).Result;
                //todono = Common.ToDecimalConvertObject(result.Content.ReadAsStringAsync().Result,0);
                var objectModel = JsonConvert.DeserializeObject<ToDo>(result.Content.ReadAsStringAsync().Result);
                return objectModel;
            }
        }

        public async Task<ToDoDetail> AddToDoComment(ToDoDetail todoDetails)
        {
            decimal todono = 0;
            using (var httpClient = new HttpClient())
            {
                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(todoDetails).ToString();
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(todoDetails).ToString(), Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(Constants.ApiBaseUrl + "/ToDo/ToDoAddComment?key=" + Constants.UserApiKey, content).Result;
                var objectModel = JsonConvert.DeserializeObject<ToDoDetail>(result.Content.ReadAsStringAsync().Result);
                return objectModel;
            }
        }
        public async Task<List<ToDoDetail>> GetToDoHistoryList(int ticketId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "/ToDo/ToDoDetails?Key=" + Constants.UserApiKey + "&ticketId=" + ticketId.ToString()))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<ToDoDetail>>(apiResponse);
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
