using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoCore.Helpers;
using ToDoCore.Models;
using ToDoCore.Services;

namespace ToDoCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IRestServices _restServices;
        private readonly IWebHostEnvironment _webHost;

        public ToDoController(IRestServices restServices,
            IWebHostEnvironment webHost)
        {
            _restServices = restServices;
            _webHost = webHost;
        }
        [HttpGet]
        public async Task<IActionResult> GetToDo()
        {
            var todos = await _restServices.GetToDoList(0);
            return Ok(todos);
        }
        [HttpPost("AddToDo")]
        public async Task<IActionResult> Insert(IFormCollection payload)
        {
            ToDo objForm = new ToDo();
            ToDo objNew = new ToDo();
            string fileNewNormalizedName = "gallery.png";
            if (payload.Files.Count > 0)
            {
                var formFile = payload.Files["file-1"];
                fileNewNormalizedName = "service_" + DateTime.Now.Year.ToString() + new Random().Next().ToString() + ".jpg";
                var fulPath = Path.Combine(_webHost.ContentRootPath, "wwwroot\\images\\issues\\" + fileNewNormalizedName);
                using (FileStream fs = System.IO.File.Create(fulPath))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }
            }
            if (payload.Keys.Count > 0)
            {
                objForm.CompanyId = Common.ToDecimalConvertObject(payload["TenantId"].ToString(), 1);
                objForm.ToDoSubject = payload["ToDoSubject"].ToString();
                if (objForm.TeamMember == null)
                    objForm.TeamMember = "Dev";
                if(objForm.CreationDate == null)
                    objForm.CreationDate = DateTime.Now;
                if (objForm.LastStatus == null)
                    objForm.LastStatus = "New";
                objNew = await _restServices.AddToDo(objForm);
            }

            return Ok(objNew);
        }

       
    }
}
