using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoCore.Services;
using ToDoCore.Helper;
using ToDoCore.Models;
using Microsoft.AspNetCore.Http;

namespace ToDoCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestServices _restServices;

        public HomeController(ILogger<HomeController> logger,
            IRestServices restServices)
        {
            _logger = logger;
            _restServices = restServices;
        }

        public async Task<IActionResult> Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            {
                ViewBag.User = HttpContext.Session.GetString("user");
                ToDoViewModel model = new ToDoViewModel();
                model.Tenants = await _restServices.GetTenantsList();
                return View(model);
            }
            else
                return RedirectToAction("Login","Account");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> PartialToDos(int tenant)
        {
            var vmodel = await _restServices.GetToDoList(tenant);
            return PartialView("_ToDoListPartial", vmodel);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ToDoAdd(ToDo todo)
        {
            return View(todo);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ToDo(int id)
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            var todoObj = await _restServices.ToDoDetail(id);
            return View("ToDoDetail", todoObj);
        }
    }
}
