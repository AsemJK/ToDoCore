using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoCore.Services;

namespace ToDoCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRestServices _restServices;

        public UserController(IRestServices restServices)
        {
            _restServices = restServices;
        }
        [HttpPost("[action]")]
        public async Task<bool> Login(IFormCollection payLoad) 
        {
            if (payLoad.Keys.Count > 0)
            {
                string uname = payLoad["UserName"].ToString();
                string pass = payLoad["Password"].ToString();
                
                var res = _restServices.CheckLogin(uname, pass).Result;
                if (res)
                {
                    HttpContext.Session.SetString("user", "test");
                    return true;
                }
                else
                    return false;
            }
            else
                return false;

        }
    }
}
