using Microsoft.AspNetCore.Mvc;
using PROG_PART2.Data;

namespace PROG_PART2.Controllers
{
    public class LecturerController : Controller
    {
        private readonly FinchSystemDbContext _context;

        public LecturerController(FinchSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lecturer/Dashboard_Lecturer")]
        public IActionResult Dashboard() => View();
        
        [HttpGet]
        [Route("Lecturer/View_Claims_Lecturer")]
        public IActionResult ViewClaims() => View();

        [HttpGet]
        [Route("Lecturer/Create_Claim_Lecturer")]
        public IActionResult CreateClaim() => View();
    }
}
