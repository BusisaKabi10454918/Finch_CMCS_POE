using Microsoft.EntityFrameworkCore;


namespace PROG_PART2.Data
{
    public class FinchSystemDbContext : DbContext
    {
        public FinchSystemDbContext(DbContextOptions<FinchSystemDbContext> options) : base(options) { }

        public DbSet<PROG_PART2.Models.AcademicManager> AcademicManagers { get; set; }
        public DbSet<PROG_PART2.Models.ProgrammeCoordinator> ProgrammeCoordinators { get; set; }
        public DbSet<PROG_PART2.Models.IndependentLecturer> IndependentLecturers { get; set; }
        public DbSet<PROG_PART2.Models.Module> Modules { get; set; }
        public DbSet<PROG_PART2.Models.Claim> Claims { get; set; }
        public DbSet<PROG_PART2.Models.User> Users { get; set; }
    }
}
