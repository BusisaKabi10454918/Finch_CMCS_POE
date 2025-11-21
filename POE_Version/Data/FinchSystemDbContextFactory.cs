using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PROG.Data
{
    public class FinchSystemDbContextFactory : IDesignTimeDbContextFactory<FinchSystemDbContext>
    {
        public FinchSystemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinchSystemDbContext>();

            // Load connection string from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);

            return new FinchSystemDbContext(optionsBuilder.Options);
        }
    }
}
