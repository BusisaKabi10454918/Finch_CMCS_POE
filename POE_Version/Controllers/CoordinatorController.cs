using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG.Data;
using PROG.Models;

namespace PROG.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly FinchSystemDbContext _context;

        public CoordinatorController(FinchSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CoordinatorDash() => View();

        [HttpGet]
        public IActionResult ReviewClaims()
        {
            var UncheckedClaims = _context.Claims
                .Where(c => c.ClaimStatus == Models.Claim.Status.Pending.ToString())
                .Include(c => c.Lecturer)
                .ToList();

            return View(UncheckedClaims);
        }

        [HttpPost]
        public IActionResult RejectClaim(String claimReadID, string reason)
        {
            //Identify coordinator from session, isolate their name for logging
            var coordinatorIdString = HttpContext.Session.GetString("CoordinatorID");

            var coordinatorId = Guid.Parse(coordinatorIdString);

            var coordinatorName = _context.ProgrammeCoordinators.Find(coordinatorId)?.FirstName;
            var coordinatorLastName = _context.ProgrammeCoordinators.Find(coordinatorId)?.LastName;

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimReadID == claimReadID);
            if (claim == null)
            {
                return NotFound();
            }

            claim.ClaimStatus = Models.Claim.Status.Rejected.ToString();
            claim.AdminComments = $"Reason:{reason}\n Reviewed by: Programme Coordinator {coordinatorName} {coordinatorLastName}";

            _context.SaveChanges();

            return RedirectToAction("ReviewClaims", "Coordinator");
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
            var coordinatorIdString = HttpContext.Session.GetString("CoordinatorID");

            
                var coordinatorId = Guid.Parse(HttpContext.Session.GetString(coordinatorIdString));

                var coordinatorName = _context.ProgrammeCoordinators.Find(coordinatorId).FirstName;
                var coordinatorLastName = _context.ProgrammeCoordinators.Find(coordinatorId).LastName;

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
                claim.AdminComments = $"Approved by: {coordinatorName} {coordinatorLastName}";

                //Debugging line
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Approval Processed");
                Console.ResetColor();

                _context.SaveChanges();

                //Debugging line
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Claim was approved. method returning");
                Console.ResetColor();

                return RedirectToAction("ReviewClaims", "Coordinator");
        }

        [HttpPost]
        public IActionResult EscalateClaim(String claimReadID, string reason)
        {
            //Identify coordinator from session, isolate their name for logging
            var coordinatorIdString = HttpContext.Session.GetString("CoordinatorID");

            var coordinatorId = Guid.Parse(coordinatorIdString);

            var coordinatorName = _context.ProgrammeCoordinators.Find(coordinatorId).FirstName;
            var coordinatorLastName = _context.ProgrammeCoordinators.Find(coordinatorId).LastName;

            //reason is optional, may be empty
            if (string.IsNullOrEmpty(reason))
            {
                reason = "No reason provided.";
            }
    
            reason = reason.Trim();

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimReadID == claimReadID);
            if (claim == null)
            {
                return NotFound();
            }
            claim.ClaimStatus = Models.Claim.Status.Escalated.ToString();
            claim.AdminComments += $"Escalated by: {coordinatorName} {coordinatorLastName}\nEscalation Reason: {reason}";
            
            _context.SaveChanges();
            return RedirectToAction("ReviewClaims", "Coordinator");
        }
    }
}
