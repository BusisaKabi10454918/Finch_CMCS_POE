using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROG.Data;
using PROG.Models;

namespace PROG.Controllers
{
    public class GeneralController : Controller
    {
                private readonly FinchSystemDbContext _context;

        public GeneralController(FinchSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LoginView() => View("/Views/General/LoginView.cshtml");


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();   // clears all session data
            return RedirectToAction("LoginView", "General"); 
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //Check lecturers
            var lecturerCheck = await _context.IndependentLecturers
                .FirstOrDefaultAsync(l => l.Username == username);

            if (lecturerCheck != null && BCrypt.Net.BCrypt.Verify(password, lecturerCheck.PasswordHash))
            {
                // Store the logged-in lecturer’s ID in session
                HttpContext.Session.SetString("LecturerId", lecturerCheck.LecturerID.ToString());
                return RedirectToAction("LecturerDash", "Lecturer");
            }

            //Not a lecturer, check coordinators
            var coordinatorCheck = await _context.ProgrammeCoordinators
                .FirstOrDefaultAsync(c => c.Username == username);

            if (coordinatorCheck != null && BCrypt.Net.BCrypt.Verify(password, coordinatorCheck.PasswordHash))
            {
                // Store the logged-in coordinator’s ID in session
                HttpContext.Session.SetString("CoordinatorId", coordinatorCheck.CoordinatorID.ToString());
                return RedirectToAction("CoordinatorDash", "Coordinator");
            }

            //Not a coordinator, check managers
            var managerCheck = await _context.AcademicManagers
                .FirstOrDefaultAsync(m => m.Username == username);

            if (managerCheck != null && BCrypt.Net.BCrypt.Verify(password, managerCheck.PasswordHash))
            {
                // Store the logged-in manager’s ID in session
                HttpContext.Session.SetString("ManagerId", managerCheck.ManagerID.ToString());
                return RedirectToAction("ManagerDash", "Manager");
            }

            //If no matches found, return to login with error
            return FailedLogin();
        }

        public IActionResult FailedLogin()
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        [HttpGet]
        public IActionResult RegisterView() => View("/Views/General/RegisterView.cshtml");

        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string username, string password, string role)
        {
            Console.WriteLine($"Registering user: {firstName} {lastName}, Username: {username}, Role: {role}");

            if (role == "lecturer")
            {
                // Register the lecturer
                var lecturer = new Models.Lecturer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
                };
                _context.IndependentLecturers.Add(lecturer);
                _context.SaveChanges();

                return RedirectToAction("LoginView", "General");
            }
            else if(role == "coordinator")
            {
                // Register the coordinator
                var coordinator = new Models.Coordinator
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
                };
                _context.ProgrammeCoordinators.Add(coordinator);
                _context.SaveChanges();

                return RedirectToAction("LoginView", "General");
            }
            else if(role == "manager")
            {
                // Register the manager
                var manager = new Models.Manager
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
                };
                _context.AcademicManagers.Add(manager);
                _context.SaveChanges();

                return RedirectToAction("LoginView", "General");
            }

            // Save all changes to the database
            _context.SaveChanges();

            // Redirect to the login page after successful registration
            return RedirectToAction("LoginView", "General");
        }

        [HttpGet]
        public IActionResult FeatureUnderDevelopment() => View("/Views/General/FeatureUnderDevView.cshtml");

        [HttpGet]
        public IActionResult CreateClaim() => View();
    }
}
