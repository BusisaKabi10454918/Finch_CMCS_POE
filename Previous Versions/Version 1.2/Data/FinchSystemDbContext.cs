using Microsoft.EntityFrameworkCore;

namespace PROG.Data
{
    public class FinchSystemDbContext : DbContext
    {
        public FinchSystemDbContext(DbContextOptions<FinchSystemDbContext> options) : base(options) { }

        public DbSet<PROG.Models.Manager> AcademicManagers { get; set; }
        public DbSet<PROG.Models.Coordinator> ProgrammeCoordinators { get; set; }
        public DbSet<PROG.Models.Lecturer> IndependentLecturers { get; set; }
        public DbSet<PROG.Models.Claim> Claims { get; set; }
        
    }
}
