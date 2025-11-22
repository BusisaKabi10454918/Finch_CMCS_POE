using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG.Data;
using PROG.Models;

namespace PROG.Controllers
{
    public class LecturerController : Controller
    {
        private readonly FinchSystemDbContext _context;
        public LecturerController(FinchSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LecturerDash() => View();

        [HttpGet]
        public IActionResult CreateClaimView() => View();

        public IActionResult ViewClaims()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        [HttpGet]
        public IActionResult ManageClaimView()
        {
            var lecturerIdString = HttpContext.Session.GetString("LecturerId");
            if (string.IsNullOrEmpty(lecturerIdString) || !Guid.TryParse(lecturerIdString, out var lecturerId))
                return Unauthorized();

            var claims = _context.Claims
                .Include(c => c.Lecturer)
                .Where(c => c.LecturerID == lecturerId)
                .ToList();

            if (!claims.Any())
            {
                TempData["NoClaimsMessage"] = "No claims made.";
                return RedirectToAction("LecturerDash");
            }

            return View(claims);
        }

        [HttpPost]
        public IActionResult SubmitClaim(DateTime submissionDate, int sessions, string moduleCode, DateOnly claimStartDate, DateOnly claimEndDate, IFormFileCollection supportingDocuments)
        {
            Claim newClaim = new Claim
            {
                SubmissionDate = submissionDate,
                ClaimedHours = Convert.ToInt32(sessions),
                ModuleCode = moduleCode,
                ClaimAmount = Convert.ToDecimal(sessions * 220),
                ClaimPeriodStart = claimStartDate,
                ClaimPeriodEnd = claimEndDate,
                SupportingDocuments = "{",
            };

            foreach (var doc in supportingDocuments)
            {
                // Here you would typically save the document to a file system or database
                // For simplicity, we are just appending the file names
                    newClaim.SupportingDocuments += doc.FileName + "; ";
            }

            newClaim.SupportingDocuments += "}";

            //retrieve lecturer from session
            Guid lecturerID = Guid.Parse(HttpContext.Session.GetString("LecturerId")!);

            //Pull that lecturer from the database
            Lecturer lecturerInSession = _context.IndependentLecturers.Find(lecturerID)!;

            //Lecturer in the session is the lecturer submitting the claim
            newClaim.ClaimReadID = Claim.GenerateClaimReadID(moduleCode, DateOnly.FromDateTime(submissionDate), lecturerInSession.NumberOfClaims, lecturerInSession); 
            

            newClaim.LecturerID = lecturerInSession.LecturerID;
            lecturerInSession.NumberOfClaims += 1;
            
            _context.Claims.Add(newClaim);
            _context.SaveChanges();

            return RedirectToAction("LecturerDash");
        }
    }
}
