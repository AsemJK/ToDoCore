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
        public async Task<IActionResult> GetToDo(string q)
        {
            var todos = await _restServices.GetToDoList(0,q);
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
                fileNewNormalizedName = "task_" + DateTime.Now.Year.ToString() + new Random().Next().ToString() + ".jpg";
                var fulPath = Path.Combine(_webHost.WebRootPath, "images/issues/" + fileNewNormalizedName);
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
                objForm.ImageFileName = fileNewNormalizedName;
                if (objForm.TeamMember == null)
                    objForm.TeamMember = HttpContext.Session.GetString("user");
                if (objForm.CreationDate == null)
                    objForm.CreationDate = DateTime.Now;
                if (objForm.LastStatus == null)
                    objForm.LastStatus = "New";
                objNew = await _restServices.AddToDo(objForm);
            }

            return Ok(objNew);
        }
        [HttpPost("UpdateToDo")]
        public async Task<IActionResult> Update(IFormCollection payload)
        {
            //to update status only

            ToDo objForm = new ToDo();

            if (payload.Keys.Count > 0)
            {
                int todoId = Common.ToIntConvertObject(payload["TicketId"].ToString(),0);
                objForm = _restServices.GetToDoList(0,"New").Result.Find(e => e.Id.Equals(todoId));
                objForm.LastStatus = payload["ToDoNewStatus"].ToString();
                await _restServices.UpdateToDo(objForm);
            }

            return Ok(objForm);
        }

        [HttpPost("AddToDoComment")]
        public async Task<IActionResult> InsertToDoComment(IFormCollection payload)
        {
            ToDoDetail objForm = new ToDoDetail();
            ToDoDetail objNew = new ToDoDetail();
           
            if (payload.Keys.Count > 0)
            {
                objForm.ToDoId = Common.ToIntConvertObject(payload["TicketId"].ToString(), 0);
                objForm.Notes = payload["ToDoNewComment"].ToString();
                objForm.TeamMember = HttpContext.Session.GetString("user");
                objNew = await _restServices.AddToDoComment(objForm);
            }

            return Ok(objNew);
        }

        [HttpGet("ToDoHistory")]
        public async Task<IActionResult> GetToDoHistory(int ticketNo)
        {
            var todoHistory = await _restServices.GetToDoHistoryList(ticketNo);
            return Ok(todoHistory);
        }
    }
}
