using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Aurora.FlowStudio.Data.Context
{
    /// <summary>
    /// Design-time factory for creating DbContext instances during migrations
    /// </summary>
    public class FlowStudioDbContextFactory : IDesignTimeDbContextFactory<FlowStudioDbContext>
    {
        public FlowStudioDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // Create DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<FlowStudioDbContext>();
            
            var connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=localhost;Database=AuroraFlowStudio;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(FlowStudioDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(120);
            });

            // Enable sensitive data logging in development
            if (configuration.GetValue<string>("Environment") == "Development")
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
            }

            return new FlowStudioDbContext(optionsBuilder.Options);
        }
    }
}
