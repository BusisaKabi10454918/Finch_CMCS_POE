using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PROG_PART2.Data
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