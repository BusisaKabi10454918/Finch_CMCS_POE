using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG.Data;

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
                .Where(c => c.ClaimStatus == Models.Claim.Status.Pending)
                .Include(c => c.Lecturer)
                .ToList();

            return View(UncheckedClaims);
        }

        [HttpPost]
        public IActionResult RejectClaim(Guid claimId, string reason)
        {
            //Identify coordinator from session, isolate their name for logging
            var coordinatorId = HttpContext.Session.GetString("CoordinatorID");

            var coordinatorName = _context.ProgrammeCoordinators.Find(coordinatorId)?.FirstName;
            var coordinatorLastName = _context.ProgrammeCoordinators.Find(coordinatorId)?.LastName;

            var claim = _context.Claims.Find(claimId);
            if (claim == null)
            {
                return NotFound();
            }

            claim.ClaimStatus = Models.Claim.Status.Rejected;
            claim.AdminComments = reason + " reviewed by: " + coordinatorName + " " + coordinatorLastName;

            _context.Claims.Update(claim);

            _context.SaveChanges();
            return RedirectToAction("ReviewClaims");
        }

        [HttpPost]
        public IActionResult ApproveClaim(Guid claimId)
        {
            //Identify coordinator from session, isolate their name for logging
            var coordinatorId = HttpContext.Session.GetString("CoordinatorID");
            var coordinatorName = _context.ProgrammeCoordinators.Find(coordinatorId)?.FirstName;
            var coordinatorLastName = _context.ProgrammeCoordinators.Find(coordinatorId)?.LastName;
            var claim = _context.Claims.Find(claimId);
            if (claim == null)
            {
                return NotFound();
            }
            claim.ClaimStatus = Models.Claim.Status.Approved;
            claim.AdminComments = "Approved by: " + coordinatorName + " " + coordinatorLastName;
            _context.Claims.Update(claim);
            _context.SaveChanges();
            return RedirectToAction("ReviewClaims");
        }
    }
}
