using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PROG.Data;
using PROG.Models;

namespace PROG.Controllers
{
    public class ManagerController : Controller
    {
        private readonly FinchSystemDbContext _context;
        public ManagerController(FinchSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ManagerDash() => View();

        [HttpGet]
        public IActionResult ViewModules()
        {
            var modules = _context.Modules.ToList();
            return View(modules);
        }  

        [HttpGet]
        public IActionResult CreateNewModule() => View("/Views/Manager/CreateNewModule.cshtml");

        [HttpPost]
        public IActionResult NewModule(string moduleCode, string moduleName, int moduleRate)
        {
            // Create a new Module object
            Module newModule = new Module(moduleCode, moduleName, moduleRate);
            // Add the new module to the database
            _context.Modules.Add(newModule);
            _context.SaveChanges();
            // Redirect to the ViewModules page after creation
            return RedirectToAction("ViewModules", "Manager");
        }


        [HttpGet]
        public IActionResult ReviewEscalatedClaims()
        {
            var UncheckedClaims = _context.Claims
                .Where(c => c.ClaimStatus == Models.Claim.Status.Escalated.ToString())
                .Include(c => c.Lecturer)
                .ToList();

            return View(UncheckedClaims);
        }

        [HttpPost]
        public IActionResult RejectClaim(String claimReadID, string reason)
        {
            //Identify coordinator from session, isolate their name for logging
            var managerIdString = HttpContext.Session.GetString("managerID");

            var managerId = Guid.Parse(managerIdString);

            var managerName = _context.ProgrammeCoordinators.Find(managerId)?.FirstName;
            var managerLastName = _context.ProgrammeCoordinators.Find(managerId)?.LastName;

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimReadID == claimReadID);
            if (claim == null)
            {
                return NotFound();
            }

            claim.ClaimStatus = Models.Claim.Status.Rejected.ToString();
            claim.AdminComments = $"Reason:{reason}\n Reviewed by: Academic Manager {managerName} {managerLastName}";

            _context.SaveChanges();

            return RedirectToAction("ReviewEscalatedClaims", "Manager");
        }

        [HttpPost]
        public IActionResult ApproveClaim(String claimReadID)
        {
            //Debugging line
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("ClaimReadID received and trimmed.");
            Console.ResetColor();

            claimReadID = claimReadID.Trim();

            //Identify coordinator from session, isolate their name for logging
            var managerIdString = HttpContext.Session.GetString("managerID");

            var managerId = Guid.Parse(managerIdString);

            var managerName = _context.ProgrammeCoordinators.Find(managerId)?.FirstName;
            var managerLastName = _context.ProgrammeCoordinators.Find(managerId)?.LastName;

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimReadID == claimReadID);
            if (claim == null)
            {
                //Debugging line
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("ClaimNotFound");
                Console.ResetColor();
                return NotFound();
            }

            //Debugging line
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Claim was approved. Coordinator name appended");
            Console.ResetColor();

            claim.ClaimStatus = Models.Claim.Status.Approved.ToString();
            claim.AdminComments = $"Approved by: {managerName} {managerLastName}";

            //Debugging line
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Approval Processed");
            Console.ResetColor();

            _context.SaveChanges();

            //Debugging line
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Claim was approved. method returning");
            Console.ResetColor();

            return RedirectToAction("ReviewEscalatedClaims", "Manager");
        }
    }
}
