using Microsoft.EntityFrameworkCore;

namespace PROG.Data
{
    public class FinchSystemDbContext : DbContext
    {
        public FinchSystemDbContext(DbContextOptions<FinchSystemDbContext> options) : base(options) { }

        public DbSet<PROG.Models.Manager> AcademicManagers { get; set; }
        public DbSet<PROG.Models.Coordinator> ProgrammeCoordinators { get; set; }
        public DbSet<PROG.Models.Lecturer> IndependentLecturers { get; set; }
        public DbSet<PROG.Models.HumanResources> HRAdministrators { get; set; }
        public DbSet<PROG.Models.Claim> Claims { get; set; }
        public DbSet<PROG.Models.Module> Modules { get; set; }
        public DbSet<PROG.Models.Contract> Contracts { get; set; }

    }
}
