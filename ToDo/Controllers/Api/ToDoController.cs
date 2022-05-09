using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoCore.Services;

namespace ToDoCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IRestServices _restServices;

        public ToDoController(IRestServices restServices)
        {
            _restServices = restServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetToDo()
        {
            var todos = await _restServices.GetToDoList(0);
            return Ok(todos);
        }
    }
}
