using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoCore.Api;
using ToDoCore.Models;

namespace ToDoCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //ViewBag.BaseApiUrl = "http://saas.pbmscore.com";
            ViewBag.BaseApiUrl = "http://localhost:5121";
            ViewBag.ApiKey = "QV0UV1JMXMT1L2S606ZI";
            ViewBag.CompanyId = 1;
            RestClient api = new RestClient();
            var vmodel = await api.GetToDoList(ViewBag.BaseApiUrl, ViewBag.ApiKey, ViewBag.CompanyId);
            return View(vmodel);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ToDos(int tenant)
        {
            //ViewBag.BaseApiUrl = "http://saas.pbmscore.com";
            ViewBag.BaseApiUrl = "http://localhost:5121";
            ViewBag.ApiKey = "QV0UV1JMXMT1L2S606ZI";
            ViewBag.CompanyId = 1;
            RestClient api = new RestClient();
            var vmodel = await api.GetToDoList(ViewBag.BaseApiUrl, ViewBag.ApiKey, tenant);
            return View("Index",vmodel);
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
    }
}
