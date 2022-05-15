using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoCore.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
        }
    }
}
