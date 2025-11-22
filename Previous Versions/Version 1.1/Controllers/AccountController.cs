using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROG_PART2.Data;
using PROG_PART2.Models;

namespace PROG_PART2.Controllers
{
    public class AccountController : Controller
    {
        private readonly FinchSystemDbContext _context;

        public AccountController(FinchSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Account/Login")]
        public IActionResult Login() => View();



        [HttpGet]
        [Route("Account/Register")]
        public IActionResult Register() => View();

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            // TODO: set session/cookie here
            return RedirectToAction("Index", "Home");
        }

    }
}
