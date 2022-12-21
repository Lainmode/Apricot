using Apricot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Apricot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentPage = CurrentPage.Home;
            return View();
        }

        public IActionResult SignUp()
        {
            ViewBag.CurrentPage = CurrentPage.SignUp;
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.CurrentPage = CurrentPage.Login;
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Chat(int chatId)
        {
            return View();
        }

        public IActionResult Space(int spaceId)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.CurrentPage = CurrentPage.About;
            return View();
        }
        public IActionResult ContactSupport()
        {
            ViewBag.CurrentPage = CurrentPage.ContactSupport;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}