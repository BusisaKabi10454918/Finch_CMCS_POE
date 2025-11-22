using Microsoft.AspNetCore.Mvc;

namespace PROG.Controllers
{
    public class ManagerController : Controller
    {
        [HttpGet]
        public IActionResult ManagerDash() => View();

        public IActionResult ReviewEscalatedClaims() => View();
    }
}
