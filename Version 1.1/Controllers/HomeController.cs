using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROG_PART2.Models;

namespace PROG_PART2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Create_Claim()
        {
            return View();
        }

        public IActionResult View_Claims_Lecturer()
        {
            return View();
        }

        public IActionResult View_Claims_General() 
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
