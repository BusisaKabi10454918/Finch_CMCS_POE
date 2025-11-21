using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG.Data;
using System.Linq;
using System.Text;

namespace PROG.Controllers
{
    public class HumanResourcesController : Controller
    {
        private readonly FinchSystemDbContext _context;

        public HumanResourcesController(FinchSystemDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult HumanResourcesDash()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReviewAccepted()
        {
            var acceptedClaims = _context.Claims
                .Where(c => c.ClaimStatus == Models.Claim.Status.Approved.ToString() && c.SubmissionDate >= DateTime.Now.AddMonths(-6))
                .Include(c => c.Lecturer)
                .ToList();
            return View(acceptedClaims);
        }

        [HttpGet]
        public IActionResult ReviewRejected()
        {
            var rejectedClaims = _context.Claims
                .Where(c => c.ClaimStatus == Models.Claim.Status.Rejected.ToString() && c.SubmissionDate >= DateTime.Now.AddMonths(-6))
                .Include(c => c.Lecturer)
                .ToList();
            return View(rejectedClaims);
        }

        [HttpGet]
        public IActionResult ManageContracts()
        {
            var contracts = _context.Contracts.ToList();
            return View(contracts);
        }

        [HttpPost]
        public IActionResult DownloadReport(String reviewContext)
        {

            var sixMonthsAgo = DateTime.Now.AddMonths(-6);

            if (reviewContext == "AcceptContext")
            {
                var query = _context.Claims
                    .Where(c => c.ClaimStatus == Models.Claim.Status.Approved.ToString() && c.SubmissionDate >= sixMonthsAgo)
                    .Select(c => new
                    {
                        c.ClaimReadID,
                        c.Lecturer.FirstName,
                        c.Lecturer.LastName,
                        c.ModuleCode,
                        c.ClaimAmount,
                        c.SubmissionDate,
                        c.ClaimedHours,
                    })
                    .ToList();

                var sb = new StringBuilder();
                sb.AppendLine("Claim ID, Claimant, Module Code, Claimed Hours, Claim Amount, Submission Date");

                foreach (var record in query)
                {

                    sb.AppendLine($"{EscapeCsv(record.ClaimReadID)}," +
                        $"{EscapeCsv(record.FirstName)}," +
                        $"{EscapeCsv(record.LastName)}," +
                        $"{EscapeCsv(record.ModuleCode)}," +
                        $"{EscapeCsv((record.ClaimedHours).ToString())}," +
                        $"{EscapeCsv((record.ClaimAmount).ToString())}," +
                        $"{EscapeCsv((record.SubmissionDate).ToString())}");
                }

                var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                var filename = $"AcceptedClaimsReport_{DateOnly.FromDateTime(DateTime.Now)}.csv";

                return File(bytes, "text/csv", filename);
            }
            else
            {
                var query = _context.Claims
                    .Where(c => c.ClaimStatus == Models.Claim.Status.Rejected.ToString() && c.SubmissionDate >= sixMonthsAgo)
                    .Select(c => new
                    {
                        c.ClaimReadID,
                        c.Lecturer.FirstName,
                        c.Lecturer.LastName,
                        c.ModuleCode,
                        c.ClaimAmount,
                        c.SubmissionDate,
                        c.AdminComments,
                        c.ClaimedHours,
                    })
                    .ToList();

                var sb = new StringBuilder();
                sb.AppendLine("Claim ID, Claimant, Module Code, Claimed Hours, Claim Amount, Submission Date");

                foreach (var record in query)
                {

                    sb.AppendLine($"{EscapeCsv(record.ClaimReadID)}," +
                        $"{EscapeCsv(record.FirstName)}," +
                        $"{EscapeCsv(record.LastName)}," +
                        $"{EscapeCsv(record.ModuleCode)}," +
                        $"{EscapeCsv((record.ClaimedHours).ToString())}," +
                        $"{EscapeCsv((record.ClaimAmount).ToString())}," +
                        $"{EscapeCsv((record.SubmissionDate).ToString())}");
                }

                var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                var filename = $"RejectedClaimsReport_{DateOnly.FromDateTime(DateTime.Now)}.csv";

                return File(bytes, "text/csv", filename);
            }
        }
        private string EscapeCsv(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }


    }
}
